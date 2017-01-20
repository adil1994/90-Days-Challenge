using _90_Days_Challenge.Models;
using SQLite.Net;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _90_Days_Challenge.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        private SQLiteConnection conn;
        public Home()
        {
            this.InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            conn = e.Parameter as SQLiteConnection;
        }

        private void GeneralPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Program),"Complete");
        }

        private void AbsPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Program), "Abs");
        }

        private void LegsPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Program), "Legs");
        }

        private void BackPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Program), "Back");
        }

        private void ChestPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Program), "Chest");
        }

        private void ArmsPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Program), "Arms");
        }
    }


   
}
