using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeliste.Model
{
    class Timeperiods : BindableBase
    {
        private string _periodstring;
        private string _starttime;
        private string _endtime;

        public Timeperiods()
        {

        }
        public string PeriodString
        {
            get { return _starttime + " - " + _endtime; }
            set { SetProperty(ref _periodstring, value); }
        }

        public string Starttime
        {
            get { return _starttime; }
            set { SetProperty(ref _starttime, value); }
        }

        public string Endtime
        {
            get { return _endtime; }
            set { SetProperty(ref _endtime, value); }
        }

    }
}
