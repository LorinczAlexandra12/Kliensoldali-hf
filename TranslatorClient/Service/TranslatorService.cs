using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TranslatorClient.Model;
using Windows.Web.Http.Filters;

namespace TranslatorClient.Service
{
    class TranslatorService : ITranslatorService
    {
        private static readonly Uri dictUri = new Uri("https://dictionary.yandex.net/api/v1/dicservice.json/");
        private readonly string apiKeyDict = "dict.1.1.20220430T084913Z.80a8ba45fcd47c78.424e079df8324bc66a2318e70872f468bdd83d5b";
        private static readonly HttpClient httpClient;


        static TranslatorService()
        {
            httpClient = new HttpClient();
        }
        public async Task<IEnumerable<string>> GetLangAsync()
        {
            var response = await httpClient.GetAsync(new Uri(dictUri, $"getLangs?key={apiKeyDict}"));
            string json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);
            var obj = JsonConvert.DeserializeObject<IEnumerable<string>>(json);
            return obj;
        }

        public async Task<Model.WordsInfo> GetTranslationAsync(string word, string fromLanguage, string toLanguage, bool withSynonym)
        {
            var response = await httpClient.GetAsync(new Uri(dictUri, $"lookup?key={apiKeyDict}&lang={fromLanguage}-{toLanguage}&text={word}"));
            string json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<PrettyTranslation>(json);
            var synonyms = new List<string>();
            if (obj.def != null && obj.def.Length > 0)
            {
                if (withSynonym)
                {
                    
                    foreach (Syn syn in obj.def[0].tr[0].syn)
                    {
                        if (syn != null)
                            synonyms.Add(syn.text);
                    }
                    if (synonyms.Count == 0)
                        synonyms = new List<string>(new[] { "No synonyms found" });
                }
                return new Model.WordsInfo
                {
                    FromWord = obj.def[0].text,
                    ToWord = obj.def[0].tr[0].text,
                    SynonymsList = synonyms
                };

            }
            else
            {
                return new Model.WordsInfo
                {
                    FromWord = word,
                    ToWord = "No translation found"
                };
            }
        }

    }
}
