using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeliste.Model
{
    class Period : BindableBase
    {
        private int _year;
        private int _month;
        private string _monthString;

        public Period()
        {

        }

        public int Year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); }
        }

        public int Month
        {
            get { return _month; }
            set { SetProperty(ref _month, value); }
        }

        public string MonthString
        {
            get { return _monthString; }
            set { SetProperty(ref _monthString, value); }
        }
    }
}
