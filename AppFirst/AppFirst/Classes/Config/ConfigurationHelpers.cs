namespace AppFirst.Classes.Config
{
    internal class ConfigurationHelpers
    {
        public static string GetDataConfigFilePath()
        {
            return (typeof(ConfigurationHelpers).Assembly.CodeBase).Replace("file:///", "");
        }

        public static string GetConfigFilename()
        {
            return "AppConfigMain.json";
        }

        public static string GetConfigFilenamePath()
        {
            return Path.GetDirectoryName(ConfigurationHelpers.GetDataConfigFilePath()) + "\\" + GetConfigFilename();
        }
    }
}
