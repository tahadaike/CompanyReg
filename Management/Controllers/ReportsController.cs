//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.draw;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting.Internal;
//using Services.PDF;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using Vue.Models;
//using Web.Services;
//using static Web.Services.Helper;

//namespace Management.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/admin/Reports")]
//    public class ReportsController : Controller
//    {
//        private Helper help;
//        private readonly IWebHostEnvironment _hostingEnvironment;

//        private readonly EstbianUpgradeContext db;

//        public ReportsController(EstbianUpgradeContext context, IWebHostEnvironment hostingEnvironment)
//        {
//            this.db = context;
//            _hostingEnvironment = hostingEnvironment;
//            help = new Helper();
//        }




//        public class ResultChartObj
//        {
//            public List<string> labels { get; set; }
//            public ChartObj datasets { get; set; }
//        }
        
        
//        public class ResultChartOb
//        {
//            public List<string> labels { get; set; }
//            public List<ChartObj> datasets { get; set; }
//        }
        
//        public class ResultChartObj1
//        {
//            public List<string> labels { get; set; }
//            public ChartObj1 datasets { get; set; }
//            public IList<MostUsedCompanyCharts> mostUsedCompanyDataset { get; set; }
//        }
        
//        public class ChartObj
//        {
//            public string label { get; set; }
//            public List<int> data { get; set; }
//        }

//        public class MostUsedCompanyCharts
//        {
//            public string label { get; set; }
//            public string count { get; set; }
//        }
//        public class ChartObj1
//        {
//            public List<string> label { get; set; }
//            public List<int?> data { get; set; }
//        }

//        [HttpGet("GetDashboardInfo")]
//        public IActionResult GetDashboardInfo()
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();

//                if(user.UserType==1 || user.UserType == 3)
//                {
//                    int Municipalities = db.Municipalities.Where(x => x.Status != 9).Count();
//                    int KidneyCenters = db.KidneyCenters.Where(x => x.Status != 9).Count();
//                    int Companies = db.Companies.Where(x => x.Status != 9).Count();
//                    int? DevicesCount = db.Devices.Where(x => x.Status != 9).Count();
//                    int Users = db.Users.Where(x => x.Status != 9).Count();
//                    int AllPatients = db.Patients.Where(x => x.Status == 1).Count();
//                    int LibyanPatients = db.Patients.Where(x => x.Status == 1 && x.NationalyId == 2).Count();
//                    int ForginPatients = db.Patients.Where(x => x.Status == 1 && x.NationalyId != 2).Count();
//                    int ActiveAcount = db.Users.Where(x => x.LastLoginOn >= DateTime.Now.AddMinutes(-30) && x.LastLoginOn <= DateTime.Now.AddMinutes(30)).Count();
//                    int TodayUsed = db.PatientAttendance.AsEnumerable().Where(x => x.Status != 9 && x.CreatedOn >= DateTime.Today ).Count();

//                    List<ChartObj> ChartObjList = new List<ChartObj>();
//                    List<int> ChartUsedByMonth = new List<int>();

//                    for (int i = 1; i < 13; i++)
//                    {
//                        var info = db.PatientAttendance.AsEnumerable()
//                            .Where(x => x.CreatedOn.GetValueOrDefault().Year == DateTime.Now.Year &&
//                            x.CreatedOn.GetValueOrDefault().Month == i).Count();
//                        ChartUsedByMonth.Add(info);
//                    }
//                    ChartObjList.Add(new ChartObj
//                    {
//                        label = "عدد مرات الغسيل",
//                        data = ChartUsedByMonth,
//                    });

//                    ChartObj berMount = new ChartObj();
//                    berMount.label = "عدد مرات الغسيل";
//                    berMount.data = ChartUsedByMonth;

//                    var mounts = Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i)).ToList();

//                    ResultChartOb result = new ResultChartOb();
//                    result.labels = mounts;
//                    result.datasets = ChartObjList;

//                    ChartObjList = new List<ChartObj>();
//                    ChartUsedByMonth = new List<int>();

//                    for (int i = DateTime.Now.Day - 7; i < DateTime.Now.Day; i++)
//                    {
//                        var info = db.PatientAttendance.AsEnumerable()
//                            .Where(x =>
//                            x.CreatedOn.GetValueOrDefault().Year == DateTime.Now.Year &&
//                            x.CreatedOn.GetValueOrDefault().Month == DateTime.Now.Month &&
//                            x.CreatedOn.GetValueOrDefault().Day == i).Count();
//                        ChartUsedByMonth.Add(info);
//                    }
//                    //ChartObjList.Add(new ChartObj
//                    //{
//                    //    label = "عدد مرات الغسيل",
//                    //    data = ChartUsedByMonth,
//                    //});

//                    ChartObj chartObj = new ChartObj();
//                    chartObj.label = "عدد مرات الغسيل";
//                    chartObj.data = ChartUsedByMonth;

//                    mounts = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i).ToString("ddd")).ToList();

//                    ResultChartObj PerDay = new ResultChartObj();
//                    PerDay.labels = mounts;
//                    PerDay.datasets = chartObj;



//                    ///per company
//                    List<ChartObj1> ChartObjList1 = new List<ChartObj1>();
//                    List<int?> value = new List<int?>();
//                    List<string> names = new List<string>();

//                    List<long> ids = db.Companies.Where(x => x.Status != 9).Select(x => x.Id).ToList();

//                    foreach (var item in db.Companies.Where(x => x.Status != 9).Select(x => x.Id).ToList())
//                    {
//                        var count = db.PatientAttendance
//                            .Include(x => x.Filter)
//                            .Include(x => x.Filter.Device)
//                            .Include(x => x.Filter.Device.Company)
//                            .AsEnumerable().Where(x => x.Filter.Device.CompanyId == item).Count();
//                        string CompanyName = db.Companies.Where(x => x.Id == item).SingleOrDefault().Name;

//                        value.Add(count);
//                        names.Add(CompanyName);
//                    }

//                    //var topCompanies = db.KidneyCentersDevices.Where(x => x.Status != 9).Include(x => x.Device).ThenInclude(x => x.Company).GroupBy(x => new { x.Device.CompanyId, x.Device.Company.Name }).Select(x => new
//                    //{
//                    //    label = x.Key.Name,
//                    //    count = x.Sum(s => s.Count),
//                    //}).OrderByDescending(o => o.count).Take(5).ToList();

//                    //var topCompanies = db.Patients.Where(x => x.Status != 9).Include(x => x.Filter).ThenInclude(x => x.Device).ThenInclude(x => x.Company).GroupBy(x => new { x.Filter.Device, x.Filter.Device.Company.Name }).Select(x => new
//                    //{
//                    //    label = x.Key.Name,
//                    //    count = x.Count(),
//                    //}).OrderByDescending(o => o.count).Take(5).ToList();


//                    //foreach (var item in topCompanies)
//                    //{
//                    //    value.Add(item.count);
//                    //    names.Add(item.label);
//                    //}


//                    ChartObj1 BerCompanyObje = new ChartObj1();
//                    BerCompanyObje.label = names;
//                    BerCompanyObje.data = value;
//                    ResultChartObj1 PerCompany = new ResultChartObj1();
//                    PerCompany.labels = names;
//                    PerCompany.datasets = BerCompanyObje;


//                    var KidneyCentersInfo = db.KidneyCenters
//                        .Include(x => x.Patients)
//                        .Include(x => x.PatientAttendance)
//                        .Include(x => x.KidneyCentersDevices)
//                        .ThenInclude(x => x.Device).ThenInclude(x => x.Company)
//                        .Where(x => x.Status != 9).Select(x => new {
//                            PatintCount = x.Patients.Where(x => x.Status == 1).Count(),
//                            PatintZeraCount = x.Patients.Where(x => x.Status == 2).Count(),
//                            PatintDeathCount = x.Patients.Where(x => x.Status == 3).Count(),
//                            KidneyCenters = x.ArabicName,
//                            UserdFilter = x.PatientAttendance
//                            .Where(k => k.Status != 9 && k.Patient.Status == 1)
//                            .Select(k => new {
//                                k.Filter.Name,
//                                k.UsedCount,
//                            }).ToList(),
//                            DeviceInfo = x.KidneyCentersDevices.Where(x => x.Status != 9)
//                            .Select(x => new {
//                                x.Device.Name,
//                                x.Count
//                            }).ToList(),
//                            DeviceCount = x.KidneyCentersDevices.Sum(g => g.Count),
//                        }).ToList();

//                    var Info = new
//                    {
//                        Municipalities = Municipalities,
//                        KidneyCenters = KidneyCenters,
//                        Companies = Companies,
//                        Devices = DevicesCount,
//                        Users = Users,
//                        LibyanPatients = LibyanPatients,
//                        ForginPatients = ForginPatients,
//                        KidneyCentersInfo = KidneyCentersInfo,
//                        ActiveAcount = ActiveAcount,
//                        AllPatients = AllPatients,
//                        ChartObjList = result,
//                        PerCompany = PerCompany,
//                        PerDay = PerDay,
//                        TodayUsed = TodayUsed,
//                    };

//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    int Municipalities = db.Municipalities.Where(x => x.Status != 9 ).Count();
//                    int KidneyCenters = db.KidneyCenters.Where(x => x.Status != 9).Count();
//                    int Companies = db.Companies.Where(x => x.Status != 9).Count();
//                    int? DevicesCount = db.KidneyCentersDevices.Where(x => x.Status != 9 && x.KidneyCentersId==user.KidneyCentersId).Sum(x => x.Count);
//                    int Users = db.Users.Where(x => x.KidneyCentersId == user.KidneyCentersId && x.Status != 9).Count();
//                    int AllPatients = db.Patients.Where(x => x.Status == 1 && x.KidneyCenterId==user.KidneyCentersId).Count();
//                    int LibyanPatients = db.Patients.Where(x => x.Status == 1 && x.NationalyId == 2 && x.KidneyCenterId == user.KidneyCentersId).Count();
//                    int ForginPatients = db.Patients.Where(x => x.Status == 1 && x.NationalyId != 2 && x.KidneyCenterId == user.KidneyCentersId).Count();
//                    int ActiveAcount = db.Users.Where(x => x.LastLoginOn >= DateTime.Now.AddMinutes(-30) && x.LastLoginOn <= DateTime.Now.AddMinutes(30) && x.KidneyCentersId == user.KidneyCentersId).Count();
//                    int TodayUsed = db.PatientAttendance.AsEnumerable().Where(x => x.Status != 9 && x.CreatedOn >= DateTime.Today && x.KidneyCentersId == user.KidneyCentersId).Count();

//                    List<ChartObj> ChartObjList = new List<ChartObj>();
//                    List<int> ChartUsedByMonth = new List<int>();

//                    for (int i = 1; i < 13; i++)
//                    {
//                        var info = db.PatientAttendance.AsEnumerable()
//                            .Where(x => x.CreatedOn.GetValueOrDefault().Year == DateTime.Now.Year &&
//                            x.CreatedOn.GetValueOrDefault().Month == i && x.KidneyCentersId==user.KidneyCentersId).Count();
//                        ChartUsedByMonth.Add(info);
//                    }
//                    ChartObjList.Add(new ChartObj
//                    {
//                        label = "عدد مرات الغسيل",
//                        data = ChartUsedByMonth,
//                    });

//                    ChartObj berMount = new ChartObj();
//                    berMount.label = "عدد مرات الغسيل";
//                    berMount.data = ChartUsedByMonth;

//                    var mounts = Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i)).ToList();

//                    ResultChartOb result = new ResultChartOb();
//                    result.labels = mounts;
//                    result.datasets = ChartObjList;




//                    ChartObjList = new List<ChartObj>();
//                    ChartUsedByMonth = new List<int>();

//                    for (int i = DateTime.Now.Day - 7; i < DateTime.Now.Day; i++)
//                    {
//                        var info = db.PatientAttendance.AsEnumerable()
//                            .Where(x =>
//                            x.CreatedOn.GetValueOrDefault().Year == DateTime.Now.Year &&
//                            x.CreatedOn.GetValueOrDefault().Month == DateTime.Now.Month &&
//                            x.CreatedOn.GetValueOrDefault().Day == i &&
//                            x.KidneyCentersId==user.KidneyCentersId

//                            ) 
                            
//                            .Count();
//                        ChartUsedByMonth.Add(info);
//                    }
//                    //ChartObjList.Add(new ChartObj
//                    //{
//                    //    label = "عدد مرات الغسيل",
//                    //    data = ChartUsedByMonth,
//                    //});

//                    ChartObj chartObj = new ChartObj();
//                    chartObj.label = "عدد مرات الغسيل";
//                    chartObj.data = ChartUsedByMonth;

//                    mounts  = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i).ToString("ddd")).ToList();

//                    ResultChartObj PerDay = new ResultChartObj();
//                    PerDay.labels = mounts;
//                    PerDay.datasets = chartObj;


//                    ///per company
//                    List<ChartObj1> ChartObjList1 = new List<ChartObj1>();
//                    List<int?> value = new List<int?>();
//                    List<string> names = new List<string>();

//                    List<long> ids = db.Companies.Where(x => x.Status != 9).Select(x => x.Id).ToList();

//                    foreach (var item in db.Companies.Where(x => x.Status != 9).Select(x => x.Id).ToList())
//                    {
//                        var count = db.PatientAttendance
//                            .Include(x => x.Filter)
//                            .Include(x => x.Filter.Device)
//                            .Include(x => x.Filter.Device.Company)
//                            .AsEnumerable().Where(x => x.Filter.Device.CompanyId == item && x.KidneyCentersId == user.KidneyCentersId).Count();
//                        string CompanyName = db.Companies.Where(x => x.Id == item).SingleOrDefault().Name;

//                        value.Add(count);
//                        names.Add(CompanyName);
//                    }

//                    //var topCompanies = db.KidneyCentersDevices.Where(x => x.Status != 9 && x.KidneyCentersId == user.KidneyCentersId).Include(x => x.Device).ThenInclude(x => x.Company).GroupBy(x => new { x.Device.CompanyId, x.Device.Company.Name }).Select(x => new
//                    //{
//                    //    label = x.Key.Name,
//                    //    count = x.Sum(s => s.Count),
//                    //}).OrderByDescending(o => o.count).Take(5).ToList();



//                    //foreach (var item in topCompanies)
//                    //{


//                    //    value.Add(item.count);
//                    //    names.Add(item.label);
//                    //}


//                    ChartObj1 BerCompanyObje = new ChartObj1();
//                    BerCompanyObje.label = names;
//                    BerCompanyObje.data = value;
//                    ResultChartObj1 PerCompany = new ResultChartObj1();
//                    PerCompany.labels = names;
//                    PerCompany.datasets = BerCompanyObje;


//                    var KidneyCentersInfo = db.KidneyCenters
//                        .Include(x => x.Patients)
//                        .Include(x => x.PatientAttendance)
//                        .Include(x => x.KidneyCentersDevices)
//                        .Where(x => x.Status != 9).Select(x => new {
//                            PatintCount = x.Patients.Where(x => x.Status == 1 && x.KidneyCenterId==user.KidneyCentersId).Count(),
//                            PatintZeraCount = x.Patients.Where(x => x.Status == 2 && x.KidneyCenterId==user.KidneyCentersId).Count(),
//                            PatintDeathCount = x.Patients.Where(x => x.Status == 3 && x.KidneyCenterId==user.KidneyCentersId).Count(),
//                            KidneyCenters = x.ArabicName,
//                            UserdFilter = x.PatientAttendance
//                            .Where(k => k.Status != 9 && k.KidneyCentersId==user.KidneyCentersId)
//                            .Select(k => new {
//                                k.Filter.Name,
//                                k.UsedCount,
//                            }).ToList(),
//                            DeviceInfo = x.KidneyCentersDevices.Where(x => x.Status != 9 && x.KidneyCentersId==user.KidneyCentersId)
//                            .Select(x => new {
//                                x.Device.Name,
//                                x.Count
//                            }).ToList(),
//                            DeviceCount  = x.Patients.Where(x => x.Status == 1).Count()
//                        }).ToList();

//                    var Info = new
//                    {
//                        Municipalities = Municipalities,
//                        KidneyCenters = KidneyCenters,
//                        Companies = Companies,
//                        Devices = DevicesCount,
//                        Users = Users,
//                        LibyanPatients = LibyanPatients,
//                        ForginPatients = ForginPatients,
//                        KidneyCentersInfo = KidneyCentersInfo,
//                        ActiveAcount = ActiveAcount,
//                        AllPatients = AllPatients,
//                        ChartObjList = result,
//                        PerCompany = PerCompany,
//                        PerDay = PerDay,
//                        TodayUsed = TodayUsed,
//                    };

//                    return Ok(new { info = Info });
//                }

            
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }






//        public class YearChart
//        {
//            public List<string> labels { get; set; }
//            public List<SubYearChart> datasets { get; set; }=new List<SubYearChart>();
//        }
        
//        public class SubYearChart
//        {
//            public string labels { get; set; }
//            public List<int> data { get; set; } = new List<int>();
//        }

//        [HttpGet("GetDashboardInfo1")]
//        public IActionResult GetDashboardInfo1()
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if(user==null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Companies = db.Companies.Where(x => x.Status != 9).ToList();


//                int KidneyCenters = db.KidneyCenters.Where(x => x.Status != 9).Count();
//                int CompaniesCount = Companies.Count();
//                int DevicesType = db.Devices.Where(x => x.Status != 9).Count();
//                int KidneyCenterDevices = db.KidneyCentersDevices.Where(x => x.Status != 9).Sum(x=>x.Count).GetValueOrDefault();
//                int FilterType = db.Devices.Where(x => x.Status!=9).Count();

//                int Users = db.Users.Where(x => x.Status != 9).Count();
//                int ActiveUser = db.Users.Where(x => x.Status==1).Count();
//                int BlocedUser = db.Users.Where(x => x.Status==2).Count();
//                int AdminUser = db.Users.Where(x => x.UserType==1).Count();
//                int EmployUser = db.Users.Where(x => x.UserType!=1).Count();
//                int ActiveAcount = db.Users.Where(x => x.LastLoginOn >= DateTime.Now.AddMinutes(-30) && x.LastLoginOn <= DateTime.Now.AddMinutes(30) && x.KidneyCentersId == user.KidneyCentersId).Count();

//                int ActivePatients=db.Patients.Where(x => x.Status == 1 && (user.UserType ==2 ? x.KidneyCenterId == user.KidneyCentersId : true)).Count();
//                int DeathPatients=db.Patients.Where(x => x.Status == 3 && (user.UserType == 2 ? x.KidneyCenterId == user.KidneyCentersId : true)).Count();
//                int ElsePatients=db.Patients.Where(x => x.Status == 2 && (user.UserType == 2 ? x.KidneyCenterId == user.KidneyCentersId : true)).Count();
//                int LibyanPatients=db.Patients.Where(x => x.Status == 1 && x.NationalyId==2 && (user.UserType == 2 ? x.KidneyCenterId == user.KidneyCentersId : true)).Count();
//                int ForginPatients = db.Patients.Where(x => x.Status == 1 && x.NationalyId!=2 && (user.UserType == 2 ? x.KidneyCenterId == user.KidneyCentersId : true)).Count();


//                int AllUsedCount = db.PatientAttendance.Where(x => x.Status != 9
//                 && (user.UserType == 2 ? x.KidneyCentersId == user.KidneyCentersId : true)
//                ).Count();

//                var CompanyPercentage = from e in db.PatientAttendance
//                    .Include(x => x.Filter)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company)
//                    .Where(x => x.Status != 9
//                     && (user.UserType == 2 ? x.KidneyCentersId == user.KidneyCentersId : true)
//                    )
//                                        group e by new
//                                        {
//                                            e.Filter.Device.CompanyId,
//                                            CompanyName = e.Filter.Device.Company.Name,
//                                        }
//                           into eg
//                                        select new
//                                        {
//                                            eg.Key.CompanyName,
//                                            Count = eg.Sum(rl => rl.UsedCount).GetValueOrDefault(),
//                                            Percentage = (eg.Sum(rl => rl.UsedCount).GetValueOrDefault() / AllUsedCount) * 100,
//                                        };


//                int TodayUsed = db.PatientAttendance.Where(x => x.Status != 9
//                     && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Today) == 0
//                     && (user.UserType == 2 ? x.KidneyCentersId == user.KidneyCentersId : true)
//                ).Sum(x=>x.UsedCount).GetValueOrDefault();


//                var TodayUsedInfo = from e in db.PatientAttendance
//                    .Include(x => x.Filter)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company)
//                    .Where(x => x.Status != 9
//                        && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Today) == 0
//                    )
//                           group e by new
//                           {
//                               e.FilterId,
//                               FilterName = e.Filter.Name,
//                               DeviceName = e.Filter.Device.Name,
//                               CompanyName = e.Filter.Device.Company.Name,
//                           }
//                           into eg
//                           select new
//                           {
//                               eg.Key.CompanyName,
//                               eg.Key.DeviceName,
//                               eg.Key.FilterName,
//                               UsedCount = eg.Sum(rl => rl.UsedCount)
//                           };



//                var UsedInfo = from e in db.PatientAttendance
//                    .Include(x => x.Filter)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company)
//                    .Where(x => x.Status != 9
//                        && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Today) == 0
//                    )
//                                    group e by new
//                                    {
//                                        e.FilterId,
//                                        FilterName = e.Filter.Name,
//                                        DeviceName = e.Filter.Device.Name,
//                                        CompanyName = e.Filter.Device.Company.Name,
//                                    }
//                           into eg
//                                    select new
//                                    {
//                                        eg.Key.CompanyName,
//                                        eg.Key.DeviceName,
//                                        eg.Key.FilterName,
//                                        UsedCount = eg.Sum(rl => rl.UsedCount)
//                                    };




//                YearChart yearChart = new YearChart();
//                List<SubYearChart> subYearCharts = new List<SubYearChart>();
//                yearChart.labels = Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i)).ToList();
                
//                foreach (var item in Companies)
//                {
//                    SubYearChart subYearChart = new SubYearChart();
//                    for (int i = 1; i < 13; i++)
//                    {
//                        subYearChart.labels = item.Name;
//                        subYearChart.data.Add(db.PatientAttendance
//                        .Include(x => x.Filter)
//                        .Include(x => x.Filter.Device)
//                        .Include(x => x.Filter.Device.Company)
//                        .Where(x => x.Status != 9
//                         && (user.UserType == 2 ? x.KidneyCentersId == user.KidneyCentersId : true)
//                         && x.Filter.Device.CompanyId==item.Id
//                         && x.CreatedOn.Value.Month == i
//                        ).Sum(x=>x.UsedCount).GetValueOrDefault());

//                    }
//                    subYearCharts.Add(subYearChart);
//                }
//                yearChart.datasets = subYearCharts;












//                //List<ChartObj> ChartObjList = new List<ChartObj>();
//                //List<int> ChartUsedByMonth = new List<int>();

//                //for (int i = 1; i < 13; i++)
//                //{
//                //    var info = db.PatientAttendance.AsEnumerable()
//                //        .Where(x => x.CreatedOn.GetValueOrDefault().Year == DateTime.Now.Year &&
//                //        x.CreatedOn.GetValueOrDefault().Month == i && x.KidneyCentersId == user.KidneyCentersId).Count();
//                //    ChartUsedByMonth.Add(info);
//                //}
//                //ChartObjList.Add(new ChartObj
//                //{
//                //    label = "عدد مرات الغسيل",
//                //    data = ChartUsedByMonth,
//                //});

//                //ChartObj berMount = new ChartObj();
//                //berMount.label = "عدد مرات الغسيل";
//                //berMount.data = ChartUsedByMonth;

//                //var mounts = Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i)).ToList();

//                //ResultChartOb result = new ResultChartOb();
//                //result.labels = mounts;
//                //result.datasets = ChartObjList;




//                //ChartObjList = new List<ChartObj>();
//                //ChartUsedByMonth = new List<int>();

//                //for (int i = DateTime.Now.Day - 7; i < DateTime.Now.Day; i++)
//                //{
//                //    var info = db.PatientAttendance.AsEnumerable()
//                //        .Where(x =>
//                //        x.CreatedOn.GetValueOrDefault().Year == DateTime.Now.Year &&
//                //        x.CreatedOn.GetValueOrDefault().Month == DateTime.Now.Month &&
//                //        x.CreatedOn.GetValueOrDefault().Day == i &&
//                //        x.KidneyCentersId == user.KidneyCentersId

//                //        )

//                //        .Count();
//                //    ChartUsedByMonth.Add(info);
//                //}
//                ////ChartObjList.Add(new ChartObj
//                ////{
//                ////    label = "عدد مرات الغسيل",
//                ////    data = ChartUsedByMonth,
//                ////});

//                //ChartObj chartObj = new ChartObj();
//                //chartObj.label = "عدد مرات الغسيل";
//                //chartObj.data = ChartUsedByMonth;

//                //mounts = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i).ToString("ddd")).ToList();

//                //ResultChartObj PerDay = new ResultChartObj();
//                //PerDay.labels = mounts;
//                //PerDay.datasets = chartObj;


//                /////per company
//                //List<ChartObj1> ChartObjList1 = new List<ChartObj1>();
//                //List<int?> value = new List<int?>();
//                //List<string> names = new List<string>();

//                //List<long> ids = db.Companies.Where(x => x.Status != 9).Select(x => x.Id).ToList();

//                //foreach (var item in db.Companies.Where(x => x.Status != 9).Select(x => x.Id).ToList())
//                //{
//                //    var count = db.PatientAttendance
//                //        .Include(x => x.Filter)
//                //        .Include(x => x.Filter.Device)
//                //        .Include(x => x.Filter.Device.Company)
//                //        .AsEnumerable().Where(x => x.Filter.Device.CompanyId == item && x.KidneyCentersId == user.KidneyCentersId).Count();
//                //    string CompanyName = db.Companies.Where(x => x.Id == item).SingleOrDefault().Name;

//                //    value.Add(count);
//                //    names.Add(CompanyName);
//                //}

//                ////var topCompanies = db.KidneyCentersDevices.Where(x => x.Status != 9 && x.KidneyCentersId == user.KidneyCentersId).Include(x => x.Device).ThenInclude(x => x.Company).GroupBy(x => new { x.Device.CompanyId, x.Device.Company.Name }).Select(x => new
//                ////{
//                ////    label = x.Key.Name,
//                ////    count = x.Sum(s => s.Count),
//                ////}).OrderByDescending(o => o.count).Take(5).ToList();



//                ////foreach (var item in topCompanies)
//                ////{


//                ////    value.Add(item.count);
//                ////    names.Add(item.label);
//                ////}


//                //ChartObj1 BerCompanyObje = new ChartObj1();
//                //BerCompanyObje.label = names;
//                //BerCompanyObje.data = value;
//                //ResultChartObj1 PerCompany = new ResultChartObj1();
//                //PerCompany.labels = names;
//                //PerCompany.datasets = BerCompanyObje;


//                //var KidneyCentersInfo = db.KidneyCenters
//                //    .Include(x => x.Patients)
//                //    .Include(x => x.PatientAttendance)
//                //    .Include(x => x.KidneyCentersDevices)
//                //    .Where(x => x.Status != 9).Select(x => new {
//                //        PatintCount = x.Patients.Where(x => x.Status == 1 && x.KidneyCenterId == user.KidneyCentersId).Count(),
//                //        PatintZeraCount = x.Patients.Where(x => x.Status == 2 && x.KidneyCenterId == user.KidneyCentersId).Count(),
//                //        PatintDeathCount = x.Patients.Where(x => x.Status == 3 && x.KidneyCenterId == user.KidneyCentersId).Count(),
//                //        KidneyCenters = x.ArabicName,
//                //        UserdFilter = x.PatientAttendance
//                //        .Where(k => k.Status != 9 && k.KidneyCentersId == user.KidneyCentersId)
//                //        .Select(k => new {
//                //            k.Filter.Name,
//                //            k.UsedCount,
//                //        }).ToList(),
//                //        DeviceInfo = x.KidneyCentersDevices.Where(x => x.Status != 9 && x.KidneyCentersId == user.KidneyCentersId)
//                //        .Select(x => new {
//                //            x.Device.Name,
//                //            x.Count
//                //        }).ToList(),
//                //        DeviceCount = x.Patients.Where(x => x.Status == 1).Count()
//                //    }).ToList();

//                var Info = new
//                {
//                    //Municipalities = Municipalities,
//                    //KidneyCenters = KidneyCenters,
//                    //Companies = Companies,
//                    //Devices = DevicesCount,
//                    //Users = Users,
//                    //LibyanPatients = LibyanPatients,
//                    //ForginPatients = ForginPatients,
//                    //KidneyCentersInfo = KidneyCentersInfo,
//                    //ActiveAcount = ActiveAcount,
//                    //AllPatients = AllPatients,
//                    //ChartObjList = result,
//                    //PerCompany = PerCompany,
//                    //PerDay = PerDay,
//                    //TodayUsed = TodayUsed,
//                };

//                return Ok(new { info = yearChart });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

















//        [HttpGet("GetUsed")]
//        public IActionResult GetUsed()
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if(user.UserType!=1)
//                {
//                    var Info = from e in db.PatientAttendance
//                           .Include(x => x.Filter)
//                           .Include(x => x.Filter.Device)
//                           .Include(x => x.Filter.Device.Company)
//                           .Where(x => x.Status != 9 && x.KidneyCentersId==user.KidneyCentersId && x.Patient.Status == 1)
//                               group e by new
//                               {
//                                   e.FilterId,
//                                   FilterName = e.Filter.Name,
//                                   DeviceName = e.Filter.Device.Name,
//                                   CompanyName = e.Filter.Device.Company.Name,
//                               }
//                           into eg
//                               select new
//                               {
//                                   eg.Key.CompanyName,
//                                   eg.Key.DeviceName,
//                                   eg.Key.FilterName,
//                                   UsedCount = eg.Sum(rl => rl.UsedCount)
//                               };


//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = from e in db.PatientAttendance
//                           .Include(x => x.Filter)
//                           .Include(x => x.Filter.Device)
//                           .Include(x => x.Filter.Device.Company)
//                           .Where(x => x.Status != 9 && x.Patient.Status == 1)
//                               group e by new
//                               {
//                                   e.FilterId,
//                                   FilterName = e.Filter.Name,
//                                   DeviceName = e.Filter.Device.Name,
//                                   CompanyName = e.Filter.Device.Company.Name,
//                               }
//                           into eg
//                               select new
//                               {
//                                   eg.Key.CompanyName,
//                                   eg.Key.DeviceName,
//                                   eg.Key.FilterName,
//                                   UsedCount = eg.Sum(rl => rl.UsedCount)
//                               };


//                    return Ok(new { info = Info });
//                }

               
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }
        
        
//        [HttpGet("GetUsedRequierd")]
//        public IActionResult GetUsedRequierd()
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if(user.UserType!=1)
//                {


//                    var Info = from e in db.Patients
//                           .Include(x => x.PatientSchedule)
//                           .Include(x => x.Filter)
//                           .Include(x => x.Filter.Device)
//                           .Include(x => x.Filter.Device.Company).AsEnumerable()
//                           .Where(x => x.Status == 1 && x.KidneyCenterId==user.KidneyCentersId)
//                               group e by new
//                               {
//                                   RequierdCount=e.PatientSchedule.Count,
//                                   e.FilterId,
//                                   FilterName = e.Filter.Name,
//                                   DeviceName = e.Filter.Device.Name,
//                                   CompanyName = e.Filter.Device.Company.Name,
//                               }
//                           into eg
//                               select new
//                               {
//                                   eg.Key.CompanyName,
//                                   eg.Key.DeviceName,
//                                   eg.Key.FilterName,
//                                   UsedCount = eg.Count()*eg.Key.RequierdCount,
//                                   WecklyCount = eg.Count()*eg.Key.RequierdCount,
//                                   MonthlyCount = eg.Count()*eg.Key.RequierdCount*4,
//                                   YearCount = eg.Count()*eg.Key.RequierdCount*52,
//                               };


//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = from e in db.Patients
//                           .Include(x => x.PatientSchedule)
//                           .Include(x => x.Filter)
//                           .Include(x => x.Filter.Device)
//                           .Include(x => x.Filter.Device.Company)
//                           .AsEnumerable()
//                           .Where(x => x.Status == 1)
//                               group e by new
//                               {
//                                   RequierdCount = e.PatientSchedule.Count,
//                                   e.FilterId,
//                                   FilterName = e.Filter.Name,
//                                   DeviceName = e.Filter.Device.Name,
//                                   CompanyName = e.Filter.Device.Company.Name,
//                               }
//                           into eg
//                               select new
//                               {
//                                   eg.Key.CompanyName,
//                                   eg.Key.DeviceName,
//                                   eg.Key.FilterName,
//                                   UsedCount = eg.Count() * eg.Key.RequierdCount,
//                                   WecklyCount = eg.Count() * eg.Key.RequierdCount,
//                                   MonthlyCount = eg.Count() * eg.Key.RequierdCount * 4,
//                                   YearCount = eg.Count() * eg.Key.RequierdCount * 52,
//                               };


//                    return Ok(new { info = Info });
//                }

               
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("GetUsedByDate")]
//        public IActionResult GetUsedByDate(DateTime From,DateTime To)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if (user.UserType != 1)
//                {
//                    var Info = from e in db.PatientAttendance
//                           .Include(x => x.Filter)
//                           .Include(x => x.Filter.Device)
//                           .Include(x => x.Filter.Device.Company)
//                           .Where(x => x.Status != 9 && x.CreatedOn >= From && x.CreatedOn <= To && x.KidneyCentersId==user.KidneyCentersId)
//                               group e by new
//                               {
//                                   e.FilterId,
//                                   FilterName = e.Filter.Name,
//                                   DeviceName = e.Filter.Device.Name,
//                                   CompanyName = e.Filter.Device.Company.Name,
//                               }
//                           into eg
//                               select new
//                               {
//                                   eg.Key.CompanyName,
//                                   eg.Key.DeviceName,
//                                   eg.Key.FilterName,
//                                   UsedCount = eg.Sum(rl => rl.UsedCount)
//                               };
//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = from e in db.PatientAttendance
//                           .Include(x => x.Filter)
//                           .Include(x => x.Filter.Device)
//                           .Include(x => x.Filter.Device.Company)
//                           .Where(x => x.Status != 9 && x.CreatedOn >= From && x.CreatedOn <= To)
//                               group e by new
//                               {
//                                   e.FilterId,
//                                   FilterName = e.Filter.Name,
//                                   DeviceName = e.Filter.Device.Name,
//                                   CompanyName = e.Filter.Device.Company.Name,
//                               }
//                           into eg
//                               select new
//                               {
//                                   eg.Key.CompanyName,
//                                   eg.Key.DeviceName,
//                                   eg.Key.FilterName,
//                                   UsedCount = eg.Sum(rl => rl.UsedCount)
//                               };
//                    return Ok(new { info = Info });
//                }
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("GetUsedDaily")]
//        public IActionResult GetUsedDaily()
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if (user.UserType != 1)
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x=>x.Status!=9 && x.KidneyCentersId==user.KidneyCentersId && x.CreatedOn.GetValueOrDefault().Date==DateTime.Now.Date && x.Patient.Status == 1)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.NationalyId == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date && x.Patient.Status == 1)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.NationalyId == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }

//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }



//        [AllowAnonymous]
//        [HttpGet("GetUsedDailyByCity")]
//        public IActionResult GetUsedDailyByCity(int cityid)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if (user.UserType != 1)
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.KidneyCenters.Municipality.City)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.KidneyCenters.Municipality.CityId== cityid && x.KidneyCentersId == user.KidneyCentersId && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.Nid == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.KidneyCenters.Municipality.City)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.KidneyCenters.Municipality.CityId == cityid && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.Nid == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }

//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }


//        [AllowAnonymous]
//        [HttpGet("GetUsedDailyByMunicipalitId")]
//        public IActionResult GetUsedDailyByMunicipalitId(int MunicipalitId)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if (user.UserType != 1)
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.KidneyCenters.Municipality.City)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.KidneyCenters.MunicipalityId == MunicipalitId && x.KidneyCentersId == user.KidneyCentersId && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.Nid == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.KidneyCenters.Municipality.City)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.KidneyCenters.MunicipalityId == MunicipalitId && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.Nid == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }

//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }


//        [AllowAnonymous]
//        [HttpGet("GetUsedDailyByHospitalId")]
//        public IActionResult GetUsedDailyByHospitalId(int HospitalId)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if (user.UserType != 1)
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.KidneyCenters)
//                    .Include(x => x.KidneyCenters.Municipality.City)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.KidneyCentersId == HospitalId && x.KidneyCentersId == user.KidneyCentersId && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.Nid == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }
//                else
//                {
//                    var Info = db.PatientAttendance
//                    .Include(x => x.Patient)
//                    .Include(x => x.Patient.Nationaly)
//                    .Include(x => x.Filter)
//                    .Include(x => x.KidneyCenters.Municipality.City)
//                    .Include(x => x.Filter.Device)
//                    .Include(x => x.Filter.Device.Company).AsEnumerable()
//                    .Where(x => x.Status != 9 && x.KidneyCentersId == HospitalId && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date)
//                    .Select(x => new
//                    {
//                        x.Id,
//                        PatinetName = x.Patient.Name,
//                        FileNumber = x.Patient.FileNumber,
//                        CardId = x.Patient.Nid == 2 ? x.Patient.Nid.ToString() : x.Patient.Passport,
//                        Filter = x.Filter.Name,
//                        Device = x.Filter.Device.Name,
//                        Company = x.Filter.Device.Company.Name,
//                        Count = x.UsedCount,
//                        x.CreatedOn,
//                    }).OrderByDescending(x => x.CreatedOn).ToList();
//                    return Ok(new { info = Info });
//                }

//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("Print/Patients")]
//        public IActionResult PrintPatients(int pageNo, int pageSize, long MunicipalityId, long KidneyCenterId, long CompanyId)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var patients = db.Patients
//                    .Include(x => x.City)
//                    .Include(x => x.KidneyCenter)
//                    .Include(x => x.KidneyCenter.Municipality)
//                    .Include(x => x.KidneyCenter.Municipality.City)
//                    .Where(x => x.Status == 1);
//                if (CompanyId > 0)
//                    patients = patients.Where(x => x.Filter.Device.CompanyId == CompanyId);
//                if (user.UserType == 2)
//                {

//                    patients = patients.Where(x => x.KidneyCenterId == user.KidneyCentersId);
//                }
//                else
//                {
                   

//                    if (KidneyCenterId > 0)
//                        patients = patients.Where(x => x.KidneyCenterId == KidneyCenterId);

//                    else if (MunicipalityId > 0)
//                        patients = patients.Where(x => x.KidneyCenter.MunicipalityId == MunicipalityId);

                    
//                }
                  




                   
//                    var Info = patients
//                        .Include(x => x.Filter)
//                        .Include(x => x.Filter.Device)
//                        .Include(x => x.Filter.Device.Company)
//                        .Include(x => x.PatientSchedule)
//                        .Include(x => x.Nationaly)
//                       .Select(x => new
//                       {
//                           x.Id,
//                           City = new
//                           {
//                               x.CityId,
//                               x.City.Name,
//                           },
//                           KidneyCenter = new
//                           {
//                               x.KidneyCenterId,
//                               x.KidneyCenter.ArabicName,
//                               x.KidneyCenter.EnglishName,
//                               Municipality = new
//                               {
//                                   x.KidneyCenter.MunicipalityId,
//                                   x.KidneyCenter.Municipality.Name,
//                                   City = new
//                                   {
//                                       x.KidneyCenter.Municipality.CityId,
//                                       x.KidneyCenter.Municipality.City.Name,
//                                       x.KidneyCenter.Municipality.City.ArabicName,
//                                       x.KidneyCenter.Municipality.City.EnglishName,
//                                   }
//                               },
//                           },
//                           Filter = new
//                           {
//                               x.FilterId,
//                               x.Filter.Name,
//                               Device = new
//                               {
//                                   x.Filter.DeviceId,
//                                   x.Filter.Device.Name,
//                                   Company = new
//                                   {
//                                       x.Filter.Device.CompanyId,
//                                       x.Filter.Device.Company.Name,
//                                   },
//                               },
//                           },
//                           Nationality = new
//                           {
//                               x.NationalyId,
//                               x.Nationaly.Name
//                           },
//                           x.Image,
//                           x.Nid,
//                           x.Passport,
//                           x.FileNumber,
//                           x.FirstName,
//                           x.MiddleName,
//                           x.GrandfatherName,
//                           x.LastName,
//                           x.Name,
//                           x.Gender,
//                           x.DateOfBirth,
//                           x.Workplace,
//                           x.MaritalStatus,
//                           x.Address,
//                           x.BloodType,
//                           x.ViralAssays,
//                           x.KidneyFailureDate,
//                           x.KidneyFailureCause,
//                           x.Notes,
//                           x.CreatedOn,
//                           x.Status,
//                           CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                       }).Skip((pageNo - 1) * pageSize).Take(pageSize).OrderByDescending(x => x.CreatedOn).ToList();



//                using (var stream = new MemoryStream())
//                {
//                    //    var pageDimension = PageSize.A4;
//                    //var pdfDocument = new Document(pageDimension);
//                    //byte[] content = null;

//                    //pdfDocument.Open();
//                    ////PdfWriter.GetInstance(pdfDocument, new FileStream("patiens", FileMode.Create));
//                    //pdfDocument.SetMargins(25f, 25f, 10, 100f);


//                    //PdfPTable table = new PdfPTable(3);

//                    //PdfPCell cell = new PdfPCell(new Phrase("Row 1 , Col 1, Col 2 and col 3"));
//                    //cell.Colspan = 3;
//                    //cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                    //table.AddCell(cell);

//                    //table.AddCell("Row 2, Col 1");
//                    //table.AddCell("Row 2, Col 1");
//                    //table.AddCell("Row 2, Col 1");

//                    //table.AddCell("Row 3, Col 1");
//                    //cell = new PdfPCell(new Phrase("Row 3, Col 2 and Col3"));
//                    //cell.Colspan = 2;
//                    //table.AddCell(cell);

//                    //cell = new PdfPCell(new Phrase("Row 4, Col 1 and Col2"));
//                    //cell.Colspan = 2;
//                    //table.AddCell(cell);
//                    //table.AddCell("Row 4, Col 3");
//                    //var mainOuterTable = GetTable(new[] { 100 });

//                    //mainOuterTable.AddCell(table);
//                    //pdfDocument.Add(mainOuterTable);
//                    ////Close the document

//                    //pdfDocument.Close();



//                    //PdfWriter.GetInstance(pdfDocument, stream);
//                    //content = stream.ToArray();


//                    ////if (content != null)
//                    ////{
//                    ////    Response.ContentType = "application/pdf";
//                    ////    Response.Headers.Add("content-length", content.Length.ToString());
//                    ////    Response.Body.Write(content);
//                    ////}
//                    var pageDim = PageSize.A4;
//                    Document doc = new Document(pageDim);
//                    var lineTable = AddLine(pageDim);
//                    PdfWriter.GetInstance(doc,stream);
//                    doc.Open();
//                    doc.SetMargins(25f, 25f, 10, 100f);
                    

//                    var mainTable = GetTable(new[] {100} );
//                    var headerTable = GetTable(new[] { 20, 60,20 });
//                    headerTable.DefaultCell.Border = Rectangle.NO_BORDER;
//                    headerTable.AddCell(GetTable(new[] { 100 }));
//                    var titleCell = GetTable(new[] { 100 }, horizontalAlignment: Element.ALIGN_RIGHT);
//                    titleCell.AddCell(GetCellWithNoBorder("دولة ليبيا", fontSize: 18, baseColor: BaseColor.BLACK));
//                    titleCell.AddCell(GetCellWithNoBorder("وزارة الصحة", fontSize: 18, baseColor: BaseColor.BLACK));
//                    headerTable.AddCell(titleCell);
//                    headerTable.AddCell(GetTable(new[] { 100 }));
//                    mainTable.DefaultCell.Border = Rectangle.NO_BORDER;
//                    mainTable.AddCell(headerTable);
//                    mainTable.AddCell(lineTable);

//                    var contentTable = GetTable(new[] { 25,25,25,25 });
//                    contentTable.AddCell(GetCell("إسم المريض", fontSize: 15, cellHeiht: 25));
//                    contentTable.AddCell(GetCell("رقم الملف", fontSize: 15, cellHeiht: 25));
//                    contentTable.AddCell(GetCell("المنطقة", fontSize: 15, cellHeiht: 25));
//                    contentTable.AddCell(GetCell("المركز", fontSize: 15, cellHeiht: 25));
//                    contentTable.SpacingBefore = 0;
//                    contentTable.SpacingAfter = 0;
//                    mainTable.AddCell(contentTable);
//                    foreach(var patient in patients)
//                    {
//                        var patientContent = GetTable(new[] { 25, 25, 25, 25 });
//                        patientContent.AddCell(GetCell(patient.Name, fontSize: 15, cellHeiht: 25));
//                        patientContent.AddCell(GetCell(patient.FileNumber, fontSize: 15, cellHeiht: 25));
//                        patientContent.AddCell(GetCell(patient.KidneyCenter.Municipality.Name, fontSize: 15, cellHeiht: 25));
//                        patientContent.AddCell(GetCell(patient.KidneyCenter.ArabicName, fontSize: 15, cellHeiht: 25));
//                        patientContent.SpacingBefore = 0;
//                        patientContent.SpacingAfter = 0;
//                        mainTable.AddCell(patientContent);

//                    }
                  

//                    doc.Add(mainTable);
//                    doc.Close();
//                    var content = stream.ToArray();
//                    return File(content, "application/pdf");


//            }
//            }
//            catch(Exception ex)
//            {
//                return StatusCode(500, ex.Message);
//            }
//        }

//        public PdfPTable GetTable(int[] columnsWidth, int horizontalAlignment = Element.ALIGN_CENTER, float widthPercentage = 100f)
//        {
//            var pdfPTable = new PdfPTable(columnsWidth.Length)
//            {
//                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
//                HorizontalAlignment = horizontalAlignment,
//                WidthPercentage = widthPercentage,
                
//            };

//            pdfPTable.SetWidths(columnsWidth);

//            return pdfPTable;
//        }
//        public PdfPCell GetCellWithNoBorder(string paragraph, int? fontSize = null, float cellHeiht = 27f, int horizontalAlignment = Element.ALIGN_CENTER, BaseColor? baseColor = default, int fontType = Font.NORMAL, bool roundedBorder = false, bool roundedFillWithNoBorder = false)
//        {
//            var font = GetFont(fontType: fontType);
//            font.Size = fontSize ?? font.Size;
//            font.Color = BaseColor.BLACK;

//            var cell = new PdfPCell(new Paragraph(paragraph, font))
//            {
//                FixedHeight = cellHeiht,
//                Padding = 3f,
//                Border = 0,
//                HorizontalAlignment = horizontalAlignment,
//                VerticalAlignment = Element.ALIGN_CENTER
//            };

//            if (roundedBorder)
//            {
//                cell.CellEvent = new RoundedBorder(FillType.Stroke, null);
//            }

//            if (roundedFillWithNoBorder)
//            {
//                cell.CellEvent = new RoundedBorder(FillType.Fill, BaseColor.LIGHT_GRAY);
//            }

//            return cell;
//        }

//        public Font GetFont(int fontSize = 13, int fontType = Font.NORMAL)
//        {
//            //we need to register the CodePages so that the .net core can recognise the code page 1252 used by font.
//            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

//            string fontPath = Path.Combine(_hostingEnvironment.ContentRootPath,"Services","PDF","fonts","majalla.ttf");
//            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

//            var font = new Font(baseFont, fontSize, fontType);
//            font.Color = BaseColor.BLACK;

//            return font;
//        }
//        public PdfPCell GetLineCell()
//        {
//            var lineCell = new PdfPCell
//            {
//                Border = Rectangle.NO_BORDER,
//                HorizontalAlignment = Element.ALIGN_CENTER,
//                BorderColor = BaseColor.BLACK,
//                BackgroundColor = BaseColor.BLACK,

//            };
//            LineSeparator line = new LineSeparator();
//            lineCell.AddElement(line);
//            return lineCell;
//        }
//        private PdfPTable AddLine(Rectangle pageSize)
//        {
//            var lineTable = GetTable(new[] { 100 }, horizontalAlignment: Element.ALIGN_RIGHT);
//            lineTable.TotalWidth = pageSize.Width - 80;
//            lineTable.DefaultCell.Border = Rectangle.NO_BORDER;
//            lineTable.AddCell(GetLineCell());

//            return lineTable;
//        }

//        public PdfPCell GetCell(string paragraph, int? fontSize = null, float cellHeiht = 27f, int horizontalAlignment = Element.ALIGN_CENTER, bool isWrap = false, BaseColor? baseColor = default)
//        {
//            var font = GetFont();
//            font.Size = fontSize ?? font.Size;

//            var cell = new PdfPCell(new Paragraph(paragraph, font))
//            {
//                Padding = 3f,
//                HorizontalAlignment = horizontalAlignment
//            };

//            if (!isWrap)
//            {
//                cell.FixedHeight = cellHeiht;
//            }

//            return cell;
//        }
//    }
//}