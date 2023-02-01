using System;
using System.Windows;
using System.Media;

using System.Windows.Threading;

namespace Timer
{

    public partial class MainWindow : Window
    {
        private int min;
        public int Minutes_time {
            get {
                return min;
            }
            set {
                min = value;
                m.Text = min.ToString();
            } 
        }

        private int sec;
        public int Seconds_time {
            get {
                return sec;
            }
            set {
                sec = value;
                s.Text = sec.ToString();
            } 
        }

        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            min = 0;
            sec = 0;
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            
            if (min != 0 || sec != 0)
            {
                Seconds_Down_Button_Click(sender, new RoutedEventArgs());
            }
            else
            {
                _timer.Stop();
                _timer_Elapsed();
            }
        }

        private void Minute_Up_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Minutes_time < 99)
                Minutes_time++;
        }

        private void Minute_Down_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Minutes_time > 0)
                Minutes_time--;
        }

        private void Seconds_Up_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Seconds_time < 59)
                Seconds_time++;
            else
            {
                Seconds_time = 0;
                Minutes_time++;
            }
        }

        private void Seconds_Down_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Seconds_time > 0)
                Seconds_time--;
            else if (Minutes_time > 0)
            {
                Seconds_time = 59;
                Minutes_time--;
            }
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((min * 60 + sec) > 0)
            {
                _timer.Interval = new TimeSpan(0, 0, 1);
                _timer.Start();
            }
        }

        private void _timer_Elapsed()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream soundStream = assembly.GetManifestResourceStream(@"Timer.shhebetanie_ptic_na_budilnik.wav");
            SoundPlayer player = new SoundPlayer(soundStream);

            player.PlaySync();
        }

        private void Pause_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PauseButton.Content.ToString() == "Pause")
            {
                _timer.Stop();
                PauseButton.Content = "Resume";
            }
            else {
                PauseButton.Content = "Pause";
                _timer.Start();
            }
        }
    }
}
