using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timeliste
{
    class Start
    {
        public Start()
        {
            StartMainWindow();
        }
        /*static void Main()
        {
            
            Thread thread = new Thread(() => StartMainWindow());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }*/ 

        private static void StartMainWindow()
        {
            List<Model.Date> dateList = new List<Model.Date>();
            List<Model.Shift> shifts = new List<Model.Shift>();
            List<Model.Timeperiods> shiftList = new List<Model.Timeperiods>();
            List<Model.Period> monthList = new List<Model.Period>();
            List<Model.Users> userList = new List<Model.Users>();

            // adding dates
            for (int i = 1; i < 32; i++)
                dateList.Add(new Model.Date() { DateString = i + " " });


            // adding shift periods
            shiftList.Add(new Model.Timeperiods() { PeriodString = "07:00 - 15:00", Starttime = "07:00", Endtime = "15:00" });
            shiftList.Add(new Model.Timeperiods() { PeriodString = "08:00 - 16:00", Starttime = "08:00", Endtime = "16:00" });
            shiftList.Add(new Model.Timeperiods() { PeriodString = "14:00 - 23:00", Starttime = "14:00", Endtime = "23:00" });
            shiftList.Add(new Model.Timeperiods() { PeriodString = "15:00 - 23:00", Starttime = "15:00", Endtime = "23:00" });
            shiftList.Add(new Model.Timeperiods() { PeriodString = "10:00 - 18:00", Starttime = "10:00", Endtime = "18:00" });


            // adding months
            monthList.Add(new Model.Period() { Month = 1, MonthString = "Januar" });
            monthList.Add(new Model.Period() { Month = 2, MonthString = "Februar" });
            monthList.Add(new Model.Period() { Month = 3, MonthString = "Mars" });
            monthList.Add(new Model.Period() { Month = 4, MonthString = "April" });
            monthList.Add(new Model.Period() { Month = 5, MonthString = "Mai" });
            monthList.Add(new Model.Period() { Month = 6, MonthString = "Juni" });
            monthList.Add(new Model.Period() { Month = 7, MonthString = "Juli" });
            monthList.Add(new Model.Period() { Month = 8, MonthString = "August" });
            monthList.Add(new Model.Period() { Month = 9, MonthString = "September" });
            monthList.Add(new Model.Period() { Month = 10, MonthString = "Oktober" });
            monthList.Add(new Model.Period() { Month = 11, MonthString = "November" });
            monthList.Add(new Model.Period() { Month = 12, MonthString = "Desember" });


            // adding users
            userList.Add(new Model.Users() { Name = "Andreas Hove" });
            userList.Add(new Model.Users() { Name = "Christian Sørseth" });
            userList.Add(new Model.Users() { Name = "Hans Ludvig Kleivdal" });
            userList.Add(new Model.Users() { Name = "Øyvind Blauuw" });

            // creating ViewModel and setting datacontext
            ViewModel.MainWindowVm viewmodel = new ViewModel.MainWindowVm(dateList, shiftList, shifts, userList, monthList);
            MainWindow view = new MainWindow();
            view.DataContext = viewmodel;
            // Venter til vinduet er lukket
            view.ShowDialog();
        }
    }
}
