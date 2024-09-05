using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace TranAnhDung.API.Common.Helper
{
    public class ApplicationSettings
    {
        private readonly IConfigurationRoot _configurationRoot;

        public ApplicationSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, optional: false, reloadOnChange: true);
            _configurationRoot = configurationBuilder.Build();
        }

        public object GetConfiguration(string section, string key, object obj)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(key);
            var configValue = _configurationRoot.GetSection(section).GetSection(key).Value;

            if (propertyInfo != null && configValue != null)
            {
                var convertedValue = Convert.ChangeType(configValue, propertyInfo.PropertyType);
                propertyInfo.SetValue(obj, convertedValue, null);
            }

            return obj;
        }

        public string GetConfigurationValue(string section, string key)
        {
            return _configurationRoot.GetSection(section)?.GetSection(key)?.Value ?? string.Empty;
        }
    }
}

