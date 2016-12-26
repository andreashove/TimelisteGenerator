using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using OfficeOpenXml;
using System.IO;
using System.Windows;

namespace Timeliste.ViewModel
{
	public class CookieAwareWebClient : WebClient
	{
	    private CookieContainer cookie = new CookieContainer();
	
	    protected override WebRequest GetWebRequest(Uri address)
	    {
	        WebRequest request = base.GetWebRequest(address);
	        if (request is HttpWebRequest)
	        {
	            (request as HttpWebRequest).CookieContainer = cookie;
	        }
	        return request;
	    }
	}
	
    class MainWindowVm
    {
        private List<Model.Date> _dateList;
        private List<Model.Timeperiods> _timeOfShifts;
        private List<Model.Period> _monthList;
        private List<Model.Users> _userList;
        private List<Model.Shift> _shifts;

        public ICollectionView ObservableDates { get; private set; }
        public ICollectionView ObservableTimeperiods { get; private set; }
        public ICollectionView ObservableMonths { get; private set; }
        public ICollectionView ObservableUsers { get; private set; }
        public ICollectionView ObservableShifts { get; private set; }


        private readonly DelegateCommand<string> _addShiftCommand;
        private readonly DelegateCommand<string> _removeShiftCommand;
        private readonly DelegateCommand<string> _createTimelisteCommand;

        public MainWindowVm(List<Model.Date> dateList, List<Model.Timeperiods> timeOfShifts, List<Model.Shift> shifts, List<Model.Users> userList, List<Model.Period> monthList)
        {
            _dateList = dateList;
            _timeOfShifts = timeOfShifts;
            _shifts = shifts;
            _monthList = monthList;
            _userList = userList;


            // Et eksempel på en liste som kan vises i WPF, kan selvfølgelig gjøres på mange andre måter
            ObservableDates = new ListCollectionView(_dateList);
            ObservableTimeperiods = new ListCollectionView(_timeOfShifts);
            ObservableMonths = new ListCollectionView(_monthList);
            ObservableUsers = new ListCollectionView(_userList);
            ObservableShifts = new ListCollectionView(_shifts);

            RetrieveCookie();

            // Et eksempel på Commandoer til knapper i WPF, bare bind det public commandend til en knapp i WPF
            _addShiftCommand = new DelegateCommand<string>(
            (s) => { AddShift(); }, //Forteller om hva knappen skal gjøre
            (s) => { return true; } //Forteller om knappen kan trykkes.
            );

            _removeShiftCommand = new DelegateCommand<string>(
            (s) => { RemoveShift(); },
            (s) => { return true; }
            );

            _createTimelisteCommand = new DelegateCommand<string>(
            (s) => { CreateTimeliste(); },
            (s) => { return true; }
            );
        }

        public DelegateCommand<string> AddShiftCommand
        {
            get { return _addShiftCommand; }
        }
        public DelegateCommand<string> RemoveShiftCommand
        {
            get { return _removeShiftCommand; }
        }
        public DelegateCommand<string> CreateTimelisteCommand
        {
            get { return _createTimelisteCommand; }
        }

        private void AddShift()
        {
            var date = (Model.Date)ObservableDates.CurrentItem;
            var timeperiod = (Model.Timeperiods)ObservableTimeperiods.CurrentItem;

            bool _shouldAdd = true;

            if (date != null)
            {
                foreach (Model.Shift sh in _shifts)
                {
                    if ((sh.Date == date))
                        _shouldAdd = false;
                }
            }

            if ((date != null) && (timeperiod != null))
            {
                if (_shouldAdd)
                {

                    _shifts.Add(new Model.Shift()
                    {
                        Date = date,
                        ShiftString = timeperiod.PeriodString,
                        Timeperiod = timeperiod
                    });

                    ObservableShifts.Refresh();
                }
                else
                {
                    System.Media.SystemSounds.Hand.Play();
                }
            }
        }
        private void RemoveShift()
        {
            var shift = (Model.Shift)ObservableShifts.CurrentItem;
            if (shift != null)
            {
                _shifts.Remove(shift);
                ObservableShifts.Refresh();
            }
        }
        private void CreateTimeliste()
        {
            var SpreadsheetPath = new FileInfo(@"Timeliste.xlsx");
            var package = new ExcelPackage(SpreadsheetPath);
            var month = (Model.Period)ObservableMonths.CurrentItem;
            var user = (Model.Users)ObservableUsers.CurrentItem;

            ExcelWorkbook workBook = package.Workbook;
            ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

            if (workBook != null)
            {
                if (workBook.Worksheets.Count > 0)
                {
                    if (month != null)
                    {
                        if (month.Month == 2) currentWorksheet = workBook.Worksheets[2];
                        else if (month.Month == 3) currentWorksheet = workBook.Worksheets[3];
                        else if (month.Month == 4) currentWorksheet = workBook.Worksheets[4];
                        else if (month.Month == 5) currentWorksheet = workBook.Worksheets[5];
                        else if (month.Month == 6) currentWorksheet = workBook.Worksheets[6];
                        else if (month.Month == 7) currentWorksheet = workBook.Worksheets[7];
                        else if (month.Month == 8) currentWorksheet = workBook.Worksheets[8];
                        else if (month.Month == 9) currentWorksheet = workBook.Worksheets[9];
                        else if (month.Month == 10) currentWorksheet = workBook.Worksheets[10];
                        else if (month.Month == 11) currentWorksheet = workBook.Worksheets[11];
                        else if (month.Month == 12) currentWorksheet = workBook.Worksheets[12];
                    }


                    int firstdate = 8;
                    currentWorksheet.SetValue(4, 3, user.Name);
                    foreach (Model.Shift sh in _shifts)
                    {
                        var currentDate = firstdate + Int32.Parse(sh.Date.DateString);
                        currentWorksheet.SetValue(currentDate, 2, "Vakt");
                        currentWorksheet.SetValue(currentDate, 4, sh.Timeperiod.Starttime);
                        currentWorksheet.SetValue(currentDate, 5, sh.Timeperiod.Endtime);
                    }
                }
            }
            package.Save();
            System.Diagnostics.Process.Start(@"Timeliste.xlsx");
        }
        private void RetrieveCookie() {
		        /*	
			var client = new CookieAwareWebClient();
			client.BaseAddress = @"https://app.timecloud.no/login.php";
			var loginData = new NameValueCollection();
			loginData.Add("Email", "ah@tampnet.com");
			loginData.Add("Password", "y7vcxIY\"");
			client.UploadValues("login.php", "POST", loginData);
			
			//Now you are logged in and can request pages    
			string htmlSource = client.DownloadString("index.php");
        	*/
        	string formUrl = "https://app.timecloud.no/login.php"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
			string formParams = string.Format("Email={0}&Password={1}", "ah@tampnet.com	", "y7vcxIY\"");
			string cookieHeader;
			WebRequest req = WebRequest.Create(formUrl);
			req.ContentType = "application/x-www-form-urlencoded";
			req.Method = "POST";
			byte[] bytes = Encoding.ASCII.GetBytes(formParams);
			req.ContentLength = bytes.Length;
			using (Stream os = req.GetRequestStream())
			{
			    os.Write(bytes, 0, bytes.Length);
			}
			WebResponse resp = req.GetResponse();
			string pageSource;
			cookieHeader = resp.Headers["Set-cookie"];
			MessageBox.Show(cookieHeader);
			using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
			{
			    pageSource = sr.ReadToEnd();
			}
			MessageBox.Show(pageSource);
			//return cookieHeader;
			string getUrl = "https://app.timecloud.no/home.php";
			//string getUrl = "https://app.timecloud.no/shifts.php?p=overview&y=2016&m=04&d=01";
			WebRequest getRequest = WebRequest.Create(getUrl);
			getRequest.Headers.Add("Cookie", cookieHeader);
			WebResponse getResponse = getRequest.GetResponse();
			using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
			{
			    pageSource = sr.ReadToEnd();
			}
			MessageBox.Show(pageSource);
			/*
        	string data = retrieveHTML(targetSite);
        	if (data == "") {
        		Console.WriteLine("Empty string");
        	}else{
        		Console.WriteLine(data);
        		MessageBox.Show(data);
        	}
        	*/
        }
        
        private string retrieveHTML(string cookie, string urlAddress){
        	//string urlAddress = "http://google.com";
			// being forwarded to login page. Login first then request.
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			
			if (response.StatusCode == HttpStatusCode.OK)
			{
			  Stream receiveStream = response.GetResponseStream();
			  StreamReader readStream = null;
			
			  if (response.CharacterSet == null)
			  {
			     readStream = new StreamReader(receiveStream);
			  }
			  else
			  {
			     readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
			  }
			
			  string data = readStream.ReadToEnd();
			
			  response.Close();
			  readStream.Close();
			  return data;
			}
			return "";
        }
    }
}
