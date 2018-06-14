using System;

namespace CrystalQuartz.Application
{
    public class CrystalQuartzOptions
    {
        public string Path { get; set; }

        public string CustomCssUrl { get; set; }

        public bool UseAuthentication { get; set; }

        public Func<string, string, bool> ValidateUser { get;set;}
    }
}