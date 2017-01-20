using _90_Days_Challenge.Models;
using _90_Days_Challenge.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _90_Days_Challenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public SQLite.Net.SQLiteConnection conn { get; set; }
         
        public MainPage()
        {
            this.InitializeComponent();
            string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db_90dayschallenge.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            App.db = conn;
            MainFrame.Navigate(typeof(MainHome));
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(MainHome),conn);
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Progress), conn);
        }

        private void ProgramButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Home), conn);
        }
    }
}
