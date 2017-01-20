using _90_Days_Challenge.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _90_Days_Challenge.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    


    public sealed partial class Program : Page
    {
        TimerCountDown t;
        int startTime;
        string selectedPlan;
        int selectedDayId;
        ObservableCollection<Day> Days { set; get; }
        ObservableCollection<Set> Sets { set; get; }
        private int clickedSetId;

        public Program()
        {
            this.InitializeComponent();
            Days = new ObservableCollection<Day>();
            Sets = new ObservableCollection<Set>();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedPlan = e.Parameter as string;
            InitDaysGrid(Days);
        }

        private void InitDaysGrid(ObservableCollection<Day> days)
        {
            // TODO get the list of plan's days 
            //var plandays = App.db.
            // get the plan id + get the program ID

            var plans = App.db.Table<Plan>();
            var planId = plans.Where(p => p.Plan_Name.Equals(selectedPlan)).FirstOrDefault().Plan_Id;


            var programs = App.db.Table<Models.Program>();
            var programId = programs.Where(p => p.Plan_Id_Ref == planId).FirstOrDefault().Program_Id;


            var Alldays = App.db.Table<Day>();
            var planDays = Alldays.Where(d => d.Program_Id_Ref == programId).ToList();

            planDays.ForEach(d => Days.Add(d));

            // Filter Day Table by ProgramID
        }

       

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sets.Clear();
            // get the sets for a given day id
            var day = (Day)e.ClickedItem;
            var allsets = App.db.Table<Set>();
            selectedDayId = day.Day_Id;
            var daysets = allsets.Where(s => s.Day_Id_Ref == day.Day_Id).ToList();
            daysets.ForEach(s => Sets.Add(s));

            if (Sets.Count <= 0)
            {
                temporaryTimerMessage.Visibility = Visibility.Collapsed;
                finishSetButton.Visibility = Visibility.Collapsed;
                stopTimerButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                temporaryTimerMessage.Visibility = Visibility.Visible;
                finishSetButton.Visibility = Visibility.Collapsed;
                stopTimerButton.Visibility = Visibility.Collapsed;
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            temporaryTimerMessage.Visibility = Visibility.Collapsed;
            finishSetButton.Visibility = Visibility.Visible;
            stopTimerButton.Visibility = Visibility.Visible;
            Set s = e.ClickedItem as Set;
            clickedSetId = s.Set_Id;
            startTime= (e.ClickedItem as Set).Set_Duration * 60;

            if(t != null)
            t.StopTimer();

            t = new TimerCountDown(timerText, s.Set_Duration * 60);
            t.DispatcherTimerSetup();

            t.TimerFinished += completeProgress;
            t.TimerSecondTick += ineveryThik;
        }

        private void ineveryThik(object timer, EventArgs args)
        {
           
        }

        private void completeProgress(object timer, EventArgs args)
        {
            var setToUpdate = Sets.Where(s => s.Set_Id == clickedSetId).FirstOrDefault();
            setToUpdate.Set_Progress = 100;
            App.db.Update(setToUpdate).ToString();
            App.db.Commit();
            Sets.Clear();
            var allsets = App.db.Table<Set>();
            var daysets = allsets.Where(s => s.Day_Id_Ref == selectedDayId).ToList();
            daysets.ForEach(s => Sets.Add(s));
            updateSetProgress(clickedSetId);
        }

        private void updateSetProgress(int v)
        {
            
        }

        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            t.StopTimer();
            var setToUpdate = Sets.Where(s => s.Set_Id == clickedSetId).FirstOrDefault();
            float rest = (float)t.timesTicked / (float)t.timesToTick;
            setToUpdate.Set_Progress = (int)(rest * 100);
            timerText.Text = "00:00" ;
            App.db.Update(setToUpdate).ToString();
            App.db.Commit();
            Sets.Clear();
            var allsets = App.db.Table<Set>();
            var daysets = allsets.Where(s => s.Day_Id_Ref == selectedDayId).ToList();
            daysets.ForEach(s => Sets.Add(s));
            updateSetProgress(clickedSetId);
        }

        private void finishSetButton_Click(object sender, RoutedEventArgs e)
        {
            timerText.Text = "00:00";
            t.StopTimer();
            var setToUpdate = Sets.Where(s => s.Set_Id == clickedSetId).FirstOrDefault();
            setToUpdate.Set_Progress = 100;
            App.db.Update(setToUpdate).ToString();
            App.db.Commit();
            Sets.Clear();
            var allsets = App.db.Table<Set>();
            var daysets = allsets.Where(s => s.Day_Id_Ref == selectedDayId).ToList();
            daysets.ForEach(s => Sets.Add(s));
            updateSetProgress(clickedSetId);
        }
    }
}
