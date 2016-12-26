using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeliste.Model
{
    class Shift : BindableBase
    {
        private string _shiftstring;
        private Date _date;
        private Timeperiods _timeperiod;

        public Shift()
        {

        }
        public string ShiftString
        {
            get { return _date.DateString + ". " + _timeperiod.Starttime + " - " + _timeperiod.Endtime; }
            set { SetProperty(ref _shiftstring, value); }
        }
        public Date Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
        public Timeperiods Timeperiod
        {
            get { return _timeperiod; }
            set { SetProperty(ref _timeperiod, value); }
        }

    }

}
