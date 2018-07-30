using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace FormsMVVM.ViewModel
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        public TimerViewModel()
        {
            StartTimer();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private const string Format = "dd/MM/yyyy HH:mm:ss";
        private DateTime _CurrentTime = DateTime.Now;
        public string CurrentTime {
            get
            {
                return _CurrentTime.ToString(Format);
            }
            set
            {
                _CurrentTime = DateTime.Parse(value);
                OnPropertyChanged("CurrentTime");
            }
        }

        private int _FontSize = 20;
        public int FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value;
                OnPropertyChanged("FontSize");
            }
        }

        private void StartTimer()
        {
            Task.Factory.StartNew(async () => 
            {
                while (true)
                {
                    CurrentTime = _CurrentTime.AddSeconds(1.0).ToString(Format);
                    await Task.Delay(1000);
                }
            }
            , TaskCreationOptions.LongRunning);
        }
    }
}
