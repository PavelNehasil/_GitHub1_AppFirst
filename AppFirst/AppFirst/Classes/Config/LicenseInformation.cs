using Westwind.Utilities;

namespace AppFirst.Classes.Config
{
    public class LicenseInformation
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string LicenseKey { get; set; }

        public static LicenseInformation FromString(string data)
        {
            return StringSerializer.Deserialize<LicenseInformation>(data, ",");
        }

        public override string ToString()
        {
            return StringSerializer.SerializeObject(this, ",");
        }
    }
}
