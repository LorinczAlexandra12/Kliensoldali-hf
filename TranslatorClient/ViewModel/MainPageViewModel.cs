using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TranslatorClient.Model;
using TranslatorClient.Service;

namespace TranslatorClient.ViewModel
{
    public class MainPageViewModel
    {
        private string fromLang = "";
        private string toLang = "";
        private bool withSynonym = false;
        private WordsInfo wordsinfo;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Model.WordsInfo WordsInfo
        {
            get{ return wordsinfo; }
            set{ 
                if(value != wordsinfo)
                {
                    wordsinfo = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool combValid
        {
            get
            {
                if (!IsCombinationValid())
                    return true;
                else
                    return false;
            }
        }
         public string FromLang
        {
            get { return fromLang; }
            set
            {
                if(fromLang != value)
                {
                    fromLang = value;
                    IsCombinationValid();
                    OnPropertyChanged();
                }
            }
        }

        public string ToLang
        {
            get { return toLang; }
            set
            {
                if(toLang != value)
                {
                    toLang = value;
                    IsCombinationValid();
                    OnPropertyChanged();
                }
            }
        }

        public bool WithSynonym
        {
            get { return withSynonym; }
            set
            {
                if(withSynonym != value)
                {
                    withSynonym = value;
                    WordsInfo = new WordsInfo(); 
                    OnPropertyChanged();
                }
            }
        }

        public async void LoadData()
        {
            ValidLangComb = (List<string>)await App.Service.GetLangAsync();
            if (ValidLangComb.Count != 0)
            {
                Languages.Clear();
                foreach (string lang in ValidLangComb)
                {
                    if(!Languages.Contains(lang.Split('-')[0]))
                        Languages.Add(lang.Split('-')[0]);
                }
                Languages = Languages.Distinct().ToList();
                Languages.Sort();
                OnPropertyChanged();
            }
            else
                Languages = new List<string>(new[] { "Could not load languages :(" });
        }

        public async void Search(string word)
        {
            if(word != null && word != "")
            {
                if (withSynonym)
                    WordsInfo = await App.Service.GetTranslationAsync(word, FromLang, ToLang, true);
                else
                    WordsInfo = await App.Service.GetTranslationAsync(word, FromLang, ToLang, false);
            }
        }

        public List<string> ValidLangComb { get; private set; } = new List<string>();
        public List<string> Languages { get; set; } = new List<string>();

        private bool IsCombinationValid()
        {
            string comb = $"{FromLang}-{toLang}";
            return ValidLangComb.Contains(comb);
        }
    }
}
