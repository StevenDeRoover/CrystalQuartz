namespace CrystalQuartz.Web.Demo
{
    using System;
    using System.Threading.Tasks;
    using Quartz;

    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => Console.WriteLine("Hello world!"));
        }
    }
}