using Westwind.Utilities.Configuration;

namespace AppFirst.Classes.Config
{
    public class JsonFileConfiguration : AppConfiguration
    {
        public string ApplicationName { get; set; }
        public DebugModes DebugMode { get; set; }
        public int MaxDisplayListItems { get; set; }
        public bool SendAdminEmailConfirmations { get; set; }
        public string Password { get; set; }
        public string AppConnectionString { get; set; }
        public string AppConnectionStringLocal { get; set; }
        public string AppConnectionStringLocalSqlite1 { get; set; }
        public LicenseInformation License { get; set; }
        public int themeIndex { get; set; }

        // Must implement public default constructor

        // Automatically initialize with default config and config file

        public JsonFileConfiguration()
        {
            ApplicationName = "AppFirst";
            DebugMode = DebugModes.Default;
            MaxDisplayListItems = 15;
            SendAdminEmailConfirmations = false;
            Password = "polo";
            AppConnectionString = @"server=CCCP\MSSQL;database=pokus2;uid=sa;pwd=polo;";
            AppConnectionStringLocal = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Database1.mdf;Integrated Security=True";
            AppConnectionStringLocalSqlite1 = @"Data Source=|DataDirectory|\Database\Database1.db";
            themeIndex = 0;

            License = new LicenseInformation
            {
                Name = "pavel",
                Company = "negasilp.eu",
                LicenseKey = "E0972787-3E3A-45BB-955B-84C02C6C530E"
            };
        }

        public void Initialize(string configFile)
        {
            if (String.IsNullOrEmpty(configFile))
            {
                base.Initialize(configData: ConfigurationHelpers.GetConfigFilenamePath());
            }
            else
                base.Initialize(configData: configFile);
        }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            string jsonFile =  ConfigurationHelpers.GetConfigFilenamePath();
            if (configData != null)
                jsonFile = configData as string;

            var provider = new JsonFileConfigurationProvider<JsonFileConfiguration>()
            {
                JsonConfigurationFile = jsonFile,
                EncryptionKey = "ultra-seekrit",  // use a generated value here
                PropertiesToEncrypt = "Password,AppConnectionString,License.LicenseKey"
            };

            return provider;
        }
    }
}
