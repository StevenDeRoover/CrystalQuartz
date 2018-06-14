namespace CrystalQuartz.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CrystalQuartz.Application;
    using CrystalQuartz.Core.SchedulerProviders;
    using global::Owin;
    using Quartz;

    public static class AppBuilderExtensions
    {
        public static void UseCrystalQuartz(
            this IAppBuilder app,
            IScheduler scheduler)
        {
            UseCrystalQuartz(app, scheduler, null);
        }

        public static void UseCrystalQuartz(
            this IAppBuilder app,
            IScheduler scheduler,
            CrystalQuartzOptions options)
        {
            app.UseCrystalQuartz(() => scheduler, options);
        }

        public static void UseCrystalQuartz(
            this IAppBuilder app,
            Func<IScheduler> schedulerProvider)
        {
            UseCrystalQuartz(app, schedulerProvider, null);
        }

        public static void UseCrystalQuartz(
            this IAppBuilder app,
            Func<IScheduler> schedulerProvider,
            CrystalQuartzOptions options)
        {
            app.UseCrystalQuartz(new FuncSchedulerProvider(schedulerProvider), options);
        }

        public static void UseCrystalQuartz(
            this IAppBuilder app,
            ISchedulerProvider scheduleProvider)
        {
            UseCrystalQuartz(app, scheduleProvider, null);
        }

        public static void UseCrystalQuartz(
            this IAppBuilder app,
            ISchedulerProvider scheduleProvider,
            CrystalQuartzOptions options)
        {
            CrystalQuartzOptions actualOptions = options ?? new CrystalQuartzOptions();
            string url = actualOptions.Path ?? "/quartz";

            app.Map(url, privateApp =>
            {
                if (actualOptions.UseAuthentication)
                {
                    privateApp.UseBasicAuthentication("SecureApi", async (u, p) => await Authenticate(actualOptions, u, p));
                }
                privateApp.Use(typeof(CrystalQuartzPanelMiddleware), scheduleProvider, actualOptions);
            });
        }

        private static async Task<IEnumerable<Claim>> Authenticate(CrystalQuartzOptions options, string username, string password)
        {
            return await Task.Run<IEnumerable<Claim>>(() =>
            {
                // authenticate user
                if (options.ValidateUser(username, password))
                {
                    return new List<Claim> { new Claim("name", username) };
                }

                return null;
            });
        }

        private static IAppBuilder UseBasicAuthentication(this IAppBuilder app, string realm, BasicAuthenticationMiddleware.CredentialValidationFunction validationFunction)
        {
            var options = new BasicAuthenticationOptions(realm, validationFunction);
            return app.UseBasicAuthentication(options);
        }

        private static IAppBuilder UseBasicAuthentication(this IAppBuilder app, BasicAuthenticationOptions options)
        {
            return app.Use<BasicAuthenticationMiddleware>(options);
        }
    }
}
