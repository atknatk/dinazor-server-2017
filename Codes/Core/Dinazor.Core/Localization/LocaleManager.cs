
using System.Globalization;
using System.Resources;
using Dinazor.Core.Resources;

namespace Dinazor.Core.Localization
{
    public class LocaleManager
    {

        private static readonly CultureInfo CultureInfo;
        private static readonly ResourceManager ErrorResourceManager;

        static LocaleManager()
        {
            //TODO: Get Culture From Config
            CultureInfo = CultureInfo.CreateSpecificCulture("tr");
            ErrorResourceManager = new ResourceManager("Dinazor.Core.Resources.ErrorMessages", typeof(Resource).Assembly); 
        }

        public static string GetMessage(string key, string[] replaceValue = null)
        {
            string value = ErrorResourceManager.GetString(key, CultureInfo);
            if (value == null)
            {
                return "No Such Key";
            }
            if (replaceValue != null && replaceValue.Length == value.Split('{').Length - 1)
            {
                for (int i = 0; i < value.Split('{').Length - 1; i++)
                {
                    string a = "{" + i + "}";
                    value = value.Replace(a, replaceValue[i]);
                }
            }
            return value;
        }
    }
}
