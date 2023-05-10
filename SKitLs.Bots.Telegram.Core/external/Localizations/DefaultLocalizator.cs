﻿using Newtonsoft.Json;

namespace SKitLs.Bots.Telegram.Core.external.Localizations
{
    public class DefaultLocalizator : ILocalizator
    {
        public static string NotDefinedKey => "local.KeyNotDefined";
        public string LocalsPath { get; set; }
        Dictionary<LangKey, Dictionary<string, string>> Localizations { get; set; }

        public DefaultLocalizator(string localsPath)
        {
            LocalsPath = localsPath;
            Localizations = new();
            if (!Directory.Exists(LocalsPath)) Directory.CreateDirectory(LocalsPath);
            var files = Directory.GetFiles(LocalsPath).Select(x => new FileInfo(x));
            foreach (LangKey lang in Enum.GetValues(typeof(LangKey)))
            {
                string? langName = Enum.GetName(typeof(LangKey), lang);
                if (langName is null) throw new ArgumentNullException(nameof(langName));
                foreach (var lFile in files.Where(x => x.Name.StartsWith(langName, true, null)))
                {
                    using var reader = new StreamReader(lFile.FullName);
                    var json = reader.ReadToEnd();
                    var langCollection = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                        ?? throw new Exception("Deserialize");
                    if (Localizations.ContainsKey(lang))
                    {
                        foreach (var pair in langCollection)
                        {
                            Localizations[lang].Add(pair.Key, pair.Value);
                        }
                    }
                    else Localizations.Add(lang, langCollection);
                }
            }
        }

        public string ResolveString(LangKey lang, string key, params string?[] format)
        {
            if (!Localizations.ContainsKey(lang))
            {
                if (lang == LangKey.EN) return "String Data is not defined";
                else return ResolveString(LangKey.EN, key, format);
            }
            
            if (!Localizations[lang].ContainsKey(key)) return ResolveString(lang, NotDefinedKey, key);
            
            return string.Format(Localizations[lang][key], format);
        }
    }
}