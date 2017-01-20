using _90_Days_Challenge.Models;
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
using SQLite.Net;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238




namespace _90_Days_Challenge.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    
    public sealed partial class Progress : Page
    {
        private SQLiteConnection conn;
        ObservableCollection<ChartModels> daysProgress { get; set; }
        ObservableCollection<DayProductivity> daysProductivity { get; set; }
        ObservableCollection<BmiHistory> bmiHistory { get; set; }
        List<Set> allsets;
        List<Day> alldays;
        List<BmiHistory> history;
        public Progress()
        {
            this.InitializeComponent();
            daysProgress = new ObservableCollection<ChartModels>();
            daysProductivity = new ObservableCollection<DayProductivity>();
            bmiHistory = new ObservableCollection<BmiHistory>();
            // get all sets 
            allsets = App.db.Table<Set>().ToList();
            // get all days off the program
            alldays = App.db.Table<Day>().ToList();
            // get all BMI History the program
            history = App.db.Table<BmiHistory>().ToList();
        }

        private void populateProgresses()
        {
           

            // populate Complete Progress
            ProgressBar_Complete.Value = getPlanPreogress(1, allsets, alldays);

            // populate Complete Progress
            ProgressBar_Abs.Value = getPlanPreogress(2, allsets, alldays);

            // populate Complete Progress
            ProgressBar_Arms.Value = getPlanPreogress(3, allsets, alldays);

            // populate Complete Progress
            ProgressBar_Back.Value = getPlanPreogress(4, allsets, alldays);

            // populate Complete Progress
            ProgressBar_Chest.Value = getPlanPreogress(5, allsets, alldays);

            // populate Complete Progress
            ProgressBar_Legs.Value = getPlanPreogress(6, allsets, alldays);


            foreach (var item in history)
            {
                bmiHistory.Add(item);
            }
        }





        private int getPlanPreogress(int programeId,List<Set> allsets, List<Day> alldays)
        {
            var finalSets = new List<Set>();
            var days = alldays.Where(d => d.Program_Id_Ref == programeId).Select(s => s.Day_Id).ToList();
            allsets.ForEach(s => AddSetToList(s, finalSets, days));
            float fprogress = ((float)finalSets.Sum(s => s.Set_Progress) / (float)(100 * finalSets.Count)) * 100;
            return (int)(fprogress);

        }




        private void AddSetToList(Set s, List<Set> finalSets, List<int> days)
        {
            if (days.Contains(s.Day_Id_Ref))
            {
                finalSets.Add(s);
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            conn = e.Parameter as SQLiteConnection;
        }

        private void PrepareProgressBars() {
            var query = conn.Table<Plan>();
            foreach (var plan in query)
            {
                switch (plan.Plan_Name.Trim())
                {
                    case "Legs":
                        ProgressBar_Legs.Value = plan.Plan_Progress;
                        break;
                    case "Abs":
                        ProgressBar_Abs.Value = plan.Plan_Progress;
                        break;
                    case "Arms":
                        ProgressBar_Arms.Value = plan.Plan_Progress;
                        break;
                    case "Back":
                        ProgressBar_Back.Value = plan.Plan_Progress;
                        break;
                    case "Chest":
                        ProgressBar_Chest.Value = plan.Plan_Progress;
                        break;
                    case "Complete":
                        ProgressBar_Complete.Value = plan.Plan_Progress;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            populateProgresses();
            prepareDaysProgressChart();
        }

        private void prepareDaysProgressChart()
        {
            // dayId/progress List
            var dayprogress = (from s in allsets group s by new { s.Day_Id_Ref } into ns select new { ns.Key.Day_Id_Ref, Progress = ns.Sum(s => s.Set_Progress)/4 }).ToList();

            // joined days sets
            var joineddayssets = (from dp in dayprogress
                                 join d in alldays on dp.Day_Id_Ref equals d.Day_Id
                                 select new { ProgramId = d.Program_Id_Ref, DayProgress = dp.Progress,DayNumber = d.Day_Number }).OrderBy(d=> d.DayNumber).ToList();

            var dayProductivity = (from d in joineddayssets group d by new { d.DayNumber } into dd select new { DayNumber = dd.Key.DayNumber , DayProductivity = dd.Sum(d=>d.DayProgress)/4 }).ToList();
           

            joineddayssets.ForEach(jds=> DaysProgressChartList(jds.ProgramId, jds.DayProgress,jds.DayNumber));
            dayProductivity.ForEach(dp => addProducvityDay(dp.DayNumber,dp.DayProductivity));
        }

        private void addProducvityDay(int dayNumber, int dayProductivity)
        {
            DayProductivity dp = new DayProductivity();
            dp.Day = dayNumber.ToString();
            dp.Productivity = dayProductivity;
            daysProductivity.Add(dp);
        }

        private void DaysProgressChartList(int programid,int dayprogress,int daynumber)
        {
            
            if (daynumber > daysProgress.Count)
            {
                ChartModels pg = new ChartModels();
                pg.DayNumber = daynumber.ToString();
                switch (programid)
                {
                    case 1:
                        pg.CompleteProgress = dayprogress;
                        break;
                    case 2:
                        { 
                        pg.AbsProgress = dayprogress;
                        }
                        break;
                    case 3:
                        pg.ArmsProgress = dayprogress;
                        break;
                    case 4:
                        pg.BackProgress = dayprogress;
                        break;
                    case 5:
                        pg.ChestProgress = dayprogress;
                        break;
                    case 6:
                        pg.LegsProgress = dayprogress;
                        break;
                    default:
                        break;
                }
                daysProgress.Add(pg);
                
            }
            else
            {
                ChartModels npg = new ChartModels();
                switch (programid)
                {
                    case 1:
                        daysProgress[daynumber-1].CompleteProgress = dayprogress;
                        break;
                    case 2:
                        {
                             npg = daysProgress[daynumber - 1];
                             npg.AbsProgress = dayprogress;
                            daysProgress.RemoveAt(daynumber - 1);
                            daysProgress.Insert(daynumber-1,npg);
                        }
                        break;
                    case 3:
                        {
                             npg = daysProgress[daynumber - 1];
                             npg.ArmsProgress = dayprogress;
                            daysProgress.RemoveAt(daynumber - 1);
                            daysProgress.Insert(daynumber - 1, npg);
                        }
                        break;
                    case 4:
                        {
                            npg = daysProgress[daynumber - 1];
                            npg.BackProgress = dayprogress;
                            daysProgress.RemoveAt(daynumber - 1);
                            daysProgress.Insert(daynumber - 1, npg);
                        }
                        break;
                    case 5:
                        {
                            npg = daysProgress[daynumber - 1];
                            npg.ChestProgress = dayprogress;
                            daysProgress.RemoveAt(daynumber - 1);
                            daysProgress.Insert(daynumber - 1, npg);
                        }
                        break;
                    case 6:
                        {
                            npg = daysProgress[daynumber - 1];
                            npg.LegsProgress = dayprogress;
                            daysProgress.RemoveAt(daynumber - 1);
                            daysProgress.Insert(daynumber - 1, npg);
                        }
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
