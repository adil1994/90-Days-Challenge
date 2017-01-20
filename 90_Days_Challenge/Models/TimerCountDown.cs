using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace _90_Days_Challenge.Models
{
    public class TimerCountDown
    {
        public delegate void TimerFinishedHandler(object timer, EventArgs args);
        public TimerFinishedHandler TimerFinished;
        public TimerFinishedHandler TimerSecondTick;

        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        public int timesTicked = 1;
        public int timesToTick ;
        TextBlock resultTextBlock;

        public TimerCountDown(TextBlock a,int timeofticks)
        {
            timesToTick = timeofticks;
            resultTextBlock = a;
            
        }
        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            TimerSecondTick(this, new EventArgs());
            lastTime = time;
            domath();
            timesTicked++;
            if (timesTicked > timesToTick)
            {
                TimerFinished(this,new EventArgs());
                stopTime = time;
                dispatcherTimer.Stop();
                span = stopTime - startTime;
            }
        }

        private void domath()
        {
            int time = timesToTick - timesTicked;
            string left = "0";
            string right = "";

            left  += time / 60;
            right += time % 60;
            resultTextBlock.Text = left + ":" + right;
        }


        public int StopTimer()
        {
            dispatcherTimer.Stop();
            return (timesToTick / timesTicked)*100;
        }
    }
}
