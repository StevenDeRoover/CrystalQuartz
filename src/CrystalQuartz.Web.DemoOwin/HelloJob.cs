namespace CrystalQuartz.Web.DemoOwin
{
    using System;
    using System.Threading.Tasks;
    using Quartz;

    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => throw new Exception("Test exception"));
        }
    }
}