using Westwind.Utilities;

namespace AppFirst.Classes.Config
{
    public class ConfigLicenseInformation
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string LicenseKey { get; set; }

        public static ConfigLicenseInformation FromString(string data)
        {
            return StringSerializer.Deserialize<ConfigLicenseInformation>(data, ",");
        }

        public override string ToString()
        {
            return StringSerializer.SerializeObject(this, ",");
        }
    }
}
