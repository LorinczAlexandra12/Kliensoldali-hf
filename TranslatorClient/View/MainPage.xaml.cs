using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TranslatorClient.Service;
using TranslatorClient.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TranslatorClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainPageViewModel();
            DataContext = ViewModel;
            this.Loaded += new RoutedEventHandler(PageLoadedHandler);
        }

        private void PageLoadedHandler(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }

        private void SearchClickHandler(object sender, RoutedEventArgs e)
        {
            
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Search(tbSearch.Text);
        }
    }
}
