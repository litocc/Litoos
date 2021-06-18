using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litools.Model
{
    public class M2gLog : ObservableObject
    {
        private double _totaltime = 10;

        public double TotalTime
        {
            get => _totaltime;
            set => Set(ref _totaltime, value);
        }

        private double _pcetime = 0;

        public double Pcetime
        {
            get => _pcetime;
            set => Set(ref _pcetime, value);
        }

        private bool _showDia = false;
        public bool ShowDia
        {
            get => _showDia;
            set => Set(ref _showDia, value);
        }
    }
}
