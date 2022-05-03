using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorClient.Service
{
    public interface ITranslatorService
    {
        Task<IEnumerable<string>> GetLangAsync();
        Task<Model.WordsInfo> GetTranslationAsync(string word, string fromLanguage, string toLanguage, bool withSyn);
    }
}
