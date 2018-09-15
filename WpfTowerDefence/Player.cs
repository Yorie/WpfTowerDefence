using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace WpfTowerDefence
{
    public class Player
    {
        // TODO: Why dispatcher timer? Why not Threading.Timer?
        DispatcherTimer timer;
        // TOOD: Money is modified from UI thread, as seen in AddTower method, and from timer. Seems it should be marked as `volatile` at first, and Player object should be locked when performing money changes?
        private double _money = 10;
        public delegate void MethodContainer(double money); // TODO: Good, but could avoid registering delegate type by using Action<double> type.
        public event MethodContainer onCount;

        public double Money
        {
            get { return _money; }
            set { _money = value; onCount?.Invoke(Money); }
        }

        public void timerPlayerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(AddMoney);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
        }

        void AddMoney(object sender, EventArgs e)
        {
            // TODO: When object modifies self private fields it is better to use field instead of propety.
            // Moreover, we've got AddMoney method and additionally Money property setter. Why both? It is enough to have one method that changes money field value.
            Money += 0.5;
            Debug.WriteLine("Money{0}", Money);
        }

        internal void timerPlayerStop()
        {
            timer.Stop();
        }

    }
}




