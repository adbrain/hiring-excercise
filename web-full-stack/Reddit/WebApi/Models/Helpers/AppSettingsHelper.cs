using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.Wrappers
{
    public class AppSettingsHelper : IAppSettingsHelper
    {
        private readonly NameValueCollection _appSettings;

        public AppSettingsHelper(NameValueCollection appSettings)
        {
            _appSettings = appSettings;
        }

        public string GetString(string key)
        {
            string value = _appSettings[key];

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return null;
        }
    }
}