namespace CrystalQuartz.Owin
{
    using System.Threading.Tasks;
    using CrystalQuartz.Application;
    using CrystalQuartz.Core;
    using CrystalQuartz.Core.SchedulerProviders;
    using CrystalQuartz.WebFramework;
    using CrystalQuartz.WebFramework.HttpAbstractions;
    using CrystalQuartz.WebFramework.Owin;
    using Microsoft.Owin;

    using OwinRequest = WebFramework.Owin.OwinRequest;

    public class CrystalQuartzPanelMiddleware : OwinMiddleware
    {
        private readonly RunningApplication _runningApplication;
        private OwinMiddleware _next;
        private CrystalQuartzOptions _options;

        public CrystalQuartzPanelMiddleware(
            OwinMiddleware next,
            ISchedulerProvider schedulerProvider,
            CrystalQuartzOptions options) : base(next)
        {
            _next = next;
            Application application = new CrystalQuartzPanelApplication(
                schedulerProvider,
                new DefaultSchedulerDataProvider(schedulerProvider),
                options);
            _options = options;
            _runningApplication = application.Run();
        }

        public override async Task Invoke(IOwinContext context)
        {
            IRequest owinRequest = new OwinRequest(context.Request.Query, await context.Request.ReadFormAsync());
            IResponseRenderer responseRenderer = new OwinResponseRenderer(context);

            if (_options.UseAuthentication && (context.Authentication.User == null || context.Authentication.User.Identities == null || !context.Authentication.User.Identity.IsAuthenticated))
            {
                responseRenderer.Render(new NotAuthorizedResponse());
            }
            else
            {
                _runningApplication.Handle(owinRequest, responseRenderer);
            }
        }
    }
}
