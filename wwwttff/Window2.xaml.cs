using System;
using System.Windows;
using System.Windows.Threading;

namespace wwwttff
{
    public partial class Window2 : Window
    {
        private DispatcherTimer timer;
        private int secondsLeft = 120;

        public Window2()
        {
            InitializeComponent();
            InitializeTimer();
        }
        Window1 window1 = new Window1(App.DataBase);
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;

            if (secondsLeft == 0)
            {
                timer.Stop();
                StartTimerInWindow1();
                MessageBox.Show("Время истекло!");
                window1.Show();
                Close();
            }

            UpdateDisplay();

            if (secondsLeft == 30)
            {
                MessageBox.Show("Осталось 30 секунд!");
            }
        }

        private void UpdateDisplay()
        {
            int minutes = secondsLeft / 60;
            int seconds = secondsLeft % 60;
            timerLabel.Content = $"{minutes:00}:{seconds:00}";
        }

        private void StartTimerInWindow1()
        {
            DispatcherTimer blockTimer = new DispatcherTimer();
            blockTimer.Interval = TimeSpan.FromSeconds(10);
            blockTimer.Tick += (sender, e) =>
            {
                blockTimer.Stop();
                window1.IsEnabled = true;
            };
            window1.IsEnabled = false;
            blockTimer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 auth = new Window1(App.DataBase);
            auth.Show();
            Close();
        }
    }
}
