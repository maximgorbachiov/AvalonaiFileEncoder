using System.Collections.Generic;

namespace EncoderService.Configuration
{
    public class ConfigurationReader
    {
        private static ConfigurationReader configuration;
        private Dictionary<string, string> values = new Dictionary<string, string>();

        public string Password
        {
            get
            {
                return this.values["password"];
            }
        }

        private ConfigurationReader() { }

        public static ConfigurationReader Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = new ConfigurationReader();
                }

                return configuration;
            }
        }
    }
}
