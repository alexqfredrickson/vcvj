using System.Configuration;

namespace vcvj.Tests
{
    public static class Config
    {
        public static string WorkingDirectory = ConfigurationManager.AppSettings["WorkingDirectory"];
        public static string SourceImagePath = WorkingDirectory + @"\" + ConfigurationManager.AppSettings["SourceImageFileName"];
        public static string DestinationImagePath = WorkingDirectory + @"\" + ConfigurationManager.AppSettings["DestinationImageFileName"];
    }
}
