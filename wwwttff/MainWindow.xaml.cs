using System;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Linq;
using System.Diagnostics;

namespace wwwttff
{
    public partial class MainWindow : Window
    {
        private static System.Timers.Timer staticTimer;

        static MainWindow()
        {
            // Создаем и запускаем статический таймер
            staticTimer = new System.Timers.Timer();
            staticTimer.Interval = 1000; // Интервал таймера в миллисекундах
            staticTimer.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            StartTimer();
        }

        private async void StartTimer()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            NewWindow();
            Close();
        }

        private void NewWindow()
        {
            var newUser = new Entities.userData();
            App.DataBase.userDatas.Add(newUser);
            registration reg = new registration(App.DataBase, newUser);
            reg.Show();
        }
    }
}
