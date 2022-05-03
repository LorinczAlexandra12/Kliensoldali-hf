using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorClient.Model
{
    public class WordsInfo
    {
        public string FromWord { get; set; }
        public string ToWord { get; set; }
        public List<string> SynonymsList { get; set; }
    }
}
