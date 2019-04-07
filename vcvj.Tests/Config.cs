using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vcvj.Tests
{
    public static class Config
    {
        public static string WorkingDirectory = ConfigurationManager.AppSettings["WorkingDirectory"];
        public static string SourceImagePath = WorkingDirectory + @"\" + ConfigurationManager.AppSettings["SourceImageFileName"];
        public static string DestinationImagePath = WorkingDirectory + @"\" + ConfigurationManager.AppSettings["DestinationImageFileName"];
    }
}
