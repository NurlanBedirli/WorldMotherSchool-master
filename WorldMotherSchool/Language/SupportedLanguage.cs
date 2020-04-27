using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Language
{
    public class SupportedLanguage
    {
        public static string DefaultLanguage { get; private set; } = "az-Latn-AZ";
        private static readonly Dictionary<string, string> _languages;

       static SupportedLanguage()
        {
            _languages = new Dictionary<string, string>()
            {
                {"az","az-Latn-AZ"},
                {"ru","ru-RU"},
                {"en","en-US"}
            };
        }

        public static string GetLanguage(string lang)
        {
            if (IsLanguageSupported(lang))
                return _languages[lang];
            else
                throw new Exception("Given Language is not supported");
        }

        public static string GetUILanguage(string lang)
        {
            string key = _languages.FirstOrDefault(x => x.Value == lang).Key;
            return key ?? "az";
        }

        public static string KeyCulture(string lang)
        {
            string key = _languages.FirstOrDefault(x => x.Key == lang).Key;
            return key ?? "az";
        }

        public static bool IsLanguageSupported(string lang)
        {
            return _languages.ContainsKey(lang);
        }

        public static IEnumerable<string> GetSupportedLanguages()
        {
            return _languages.Select(x => x.Value).AsEnumerable();
        }


        public static IEnumerable<string> GetSupportedUILanguages()
        {
            return _languages.Select(x => x.Value).AsEnumerable();
        }

    }
}
