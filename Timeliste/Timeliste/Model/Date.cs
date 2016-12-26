using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeliste.Model
{
    class Date : BindableBase
    {
        private string _date;

        public Date()
        {

        }

        public string DateString
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
    }
}
