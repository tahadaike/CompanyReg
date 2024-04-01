//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Vue.Models;
//using Web.Services;
//using static Vue.Controllers.SecurityController;
//using static Web.Services.Helper;

//namespace Management.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/admin/Prisoners")]
//    public class PrisonersController : Controller
//    {
//        private Helper help;

//        private readonly PrisonersContext db;

//        public PrisonersController(PrisonersContext context, IConfiguration iConfig)
//        {
//            this.db = context;
//            help = new Helper(iConfig, context);
//        }


                
//        public partial class BodyObject
//        {
//            public long? Id { get; set; }

//            public string Name { get; set; }
//            public string MotharName { get; set; }
//            public short IsLive { get; set; }
//            public short IsFinsh { get; set; }

//            public string NID { get; set; }
//            public string Passport { get; set; }
//            public short MaritalStatus { get; set; }
//            public short ChildrenCount { get; set; }

//            public short Gender { get; set; }
//            public DateTime BirthDate { get; set; }
//            public string Address { get; set; }
//            public string Workplace { get; set; }


//            public bool? IsLiveMothar { get; set; }
//            public bool? IsLiveFather { get; set; }
//            public short Qualification { get; set; }

//            public string WakelName { get; set; }
//            public string WakelNID { get; set; }
//            public string WakelPhone { get; set; }

//            public string FatherNID { get; set; }
//            public string MotherNID { get; set; }
//            public string WifeNID { get; set; }
//            public string WifeNID1 { get; set; }
//            public string WifeNID2 { get; set; }
//            public string WifeNID3 { get; set; }
//            public string Child1 { get; set; }
//            public string Child2 { get; set; }
//            public string Child3 { get; set; }
//            public string Child4 { get; set; }
//            public string Child5 { get; set; }
//            public string Child6 { get; set; }
//            public string Child7 { get; set; }
//            public string Child8 { get; set; }
//            public string Child9 { get; set; }
//            public string Child10 { get; set; }

//            public short HealthIssues { get; set; }
//            public short? Disease { get; set; }
            
//            public string Phone { get; set; }
//            public long BankBranchesId { get; set; }
//            public string AcountNumber { get; set; }
            
//            public long BankBranchesId1 { get; set; }
//            public string AcountNumber1 { get; set; }

//            public long? MunicipalitiesId { get; set; }
//            public long? OfficeId { get; set; }

//            public string WitnessName { get; set; }
//            public string WitnessNID { get; set; }
//            public string WitnessName1 { get; set; }
//            public string WitnessNID1 { get; set; }

            
//            public DateTime? StartDate { get; set; }
//            public DateTime? EndDate { get; set; }
//            public DateTime? StartDate1 { get; set; }
//            public DateTime? EndDate1 { get; set; }
//            public DateTime? StartDate2 { get; set; }
//            public DateTime? EndDate2 { get; set; }
            
//            //public string BarCode { get; set; }
//            public string Notes { get; set; }
            
//            public string ImageName { get; set; }
//            public string ImageType { get; set; }
//            public string fileBase64 { get; set; }
            
//            public List<AttachmentsObj> Attachments { get; set; } = new List<AttachmentsObj>();
//        }

//        public class AttachmentsObj
//        {
//            public string ImageName { get; set; }
//            public string ImageType { get; set; }
//            public string fileBase64 { get; set; }
//        }


//        [HttpPost("Add")]
//        public IActionResult Add([FromBody] BodyObject bodyObject)
//        {
//            try
//            {
//                if (bodyObject == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var User = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
//                if (User == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//                //valideations
//                if (string.IsNullOrWhiteSpace(bodyObject.Name))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);
                
//                if (string.IsNullOrWhiteSpace(bodyObject.MotharName))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);
                
//                if (string.IsNullOrWhiteSpace(bodyObject.NID))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NIDEmpty);

//                if (!this.help.IsValidNID(bodyObject.NID))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.RongNID);

//                if (!string.IsNullOrWhiteSpace(bodyObject.WakelNID))
//                {
//                    if (!this.help.IsValidNID(bodyObject.WakelNID))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (  الوكيل)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.FatherNID))
//                {
//                    if (!this.help.IsValidNID(bodyObject.FatherNID))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (  الأب)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.MotherNID))
//                {
//                    if (!this.help.IsValidNID(bodyObject.MotherNID))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأم)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child1))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child1))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن الأول)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child2))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child2))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التاني)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child3))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child3))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التالت)");
//                }
                    
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child4))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child4))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن الرابع)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child5))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child5))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن الخامس)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child6))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child6))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن السادس)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child7))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child7))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن السابع)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child8))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child8))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التامن)");
//                }
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child9))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child9))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التاسع)");
//                }
                    
                
//                if (!string.IsNullOrWhiteSpace(bodyObject.Child10))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child10))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن العاشر)");
//                }


//                //if (string.IsNullOrWhiteSpace(bodyObject.WitnessNID))
//                //    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDEmpty);
                
//                //if (string.IsNullOrWhiteSpace(bodyObject.WitnessNID1))
//                //    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDEmpty1);

//                //if(bodyObject.WitnessNID==bodyObject.WitnessNID1)
//                //    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDExist);
                
//                //if(bodyObject.WitnessNID==bodyObject.NID || bodyObject.WitnessNID1==bodyObject.NID)
//                //    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDExistNID);

                

//                //if (string.IsNullOrWhiteSpace(bodyObject.Passport))
//                //    return StatusCode(BackMessages.StatusCode, BackMessages.PassportEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.Address))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.AddressEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.Workplace))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.WorkplaceEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.Phone))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PhoneEmpty);

//                if (!this.help.IsValidPhone(bodyObject.Phone))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PhoneNotValid);

//                if (string.IsNullOrWhiteSpace(bodyObject.AcountNumber))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.AcountNumberEmpty);

//                if (!string.IsNullOrWhiteSpace(bodyObject.WakelPhone))
//                {
//                    if (!this.help.IsValidPhone(bodyObject.WakelPhone))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.PhoneNotValid + "الوكيل");
//                }
                    




//                if (User.UserType == 1)
//                {
//                    if (bodyObject.OfficeId <= 0)
//                        return StatusCode(BackMessages.StatusCode, BackMessages.OfficeIdEmpty);
//                }

//                if (bodyObject.BirthDate.Year > DateTime.Now.Year)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.BirthYearMustBeEqualToOrLessThenCurrentYear);

//                if (bodyObject.StartDate == DateTime.MinValue)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.DateEmpty);
                
//                if (bodyObject.EndDate == DateTime.MinValue)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.DateEmpty);
                
//                if (bodyObject.StartDate.GetValueOrDefault().Year > DateTime.Now.Year)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.KidneyFailureDateMustBeEqualToOrLessThenCurrentYear);
                
//                if (bodyObject.StartDate.GetValueOrDefault().Date > bodyObject.EndDate.GetValueOrDefault().Date)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.DateNotCorrect);

//                //if(bodyObject.WitnessNID==bodyObject.WitnessNID1)


//                //Is Exist Patint
//                var isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Nid == bodyObject.NID && x.Status != 9).SingleOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NIDExist + " في " + isExist.Office.Name);
                
//                isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Name == bodyObject.Name && x.Status != 9).SingleOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NameExist + " في " + isExist.Office.Name);
                
//                isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Phone == bodyObject.Phone && x.Status != 9).SingleOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PhoneExist + " في " + isExist.Office.Name);



//                int CountOfDay= int.Parse((bodyObject.EndDate.GetValueOrDefault() - bodyObject.StartDate.GetValueOrDefault()).TotalDays.ToString());

//                List<PrisonersSchedule> prisonersSchedules = new List<PrisonersSchedule>();
//                PrisonersSchedule prisonersSchedule=new PrisonersSchedule();
//                prisonersSchedule.From = bodyObject.StartDate;
//                prisonersSchedule.To = bodyObject.EndDate;
//                prisonersSchedule.CountOfDay = CountOfDay;
//                prisonersSchedule.CreatedOn = DateTime.Now;
//                prisonersSchedule.CreatedBy = userId;
//                prisonersSchedule.Status=1;

//                prisonersSchedules.Add(prisonersSchedule);

//                if(bodyObject.StartDate1!=null && bodyObject.EndDate1!=null && bodyObject.StartDate1>DateTime.MinValue && bodyObject.EndDate1>DateTime.MinValue)
//                {
//                    int CountOfDay1=int.Parse((bodyObject.EndDate1.GetValueOrDefault() - bodyObject.StartDate1.GetValueOrDefault()).TotalDays.ToString());

//                    if(CountOfDay1>0)
//                    {
//                        prisonersSchedule = new PrisonersSchedule();
//                        prisonersSchedule.From = bodyObject.StartDate1;
//                        prisonersSchedule.To = bodyObject.EndDate1;
//                        prisonersSchedule.CountOfDay = CountOfDay;
//                        prisonersSchedule.CreatedOn = DateTime.Now;
//                        prisonersSchedule.CreatedBy = userId;
//                        prisonersSchedule.Status = 1;
//                        prisonersSchedules.Add(prisonersSchedule);
//                        CountOfDay += CountOfDay1;
//                    }
//                }
                
//                if(bodyObject.StartDate2 != null && bodyObject.EndDate2 != null && bodyObject.StartDate2>DateTime.MinValue && bodyObject.EndDate2>DateTime.MinValue)
//                {
//                    int CountOfDay2=int.Parse((bodyObject.EndDate2.GetValueOrDefault() - bodyObject.StartDate2.GetValueOrDefault()).TotalDays.ToString());

//                    if(CountOfDay2>0)
//                    {
//                        prisonersSchedule = new PrisonersSchedule();
//                        prisonersSchedule.From = bodyObject.StartDate2;
//                        prisonersSchedule.To = bodyObject.EndDate2;
//                        prisonersSchedule.CountOfDay = CountOfDay;
//                        prisonersSchedule.CreatedOn = DateTime.Now;
//                        prisonersSchedule.CreatedBy = userId;
//                        prisonersSchedule.Status = 1;
//                        prisonersSchedules.Add(prisonersSchedule);
//                        CountOfDay += CountOfDay2;
//                    }
//                }




//                List<PrisonersAttachments> PrisonersAttachmentsList = new List<PrisonersAttachments>();
//                if (bodyObject.Attachments.Count > 0)
//                {
//                    foreach (var item1 in bodyObject.Attachments)
//                    {
//                        PrisonersAttachments PrisonersAttachmentsRow = new PrisonersAttachments();
//                        PrisonersAttachmentsRow.Name = item1.ImageName;
//                        PrisonersAttachmentsRow.Path = this.help.UploadFile(item1.ImageName, this.help.GetAttachmentType(item1.ImageName), item1.fileBase64);
//                        PrisonersAttachmentsRow.CreatedBy = userId;
//                        PrisonersAttachmentsRow.CreatedOn = DateTime.Now;
//                        PrisonersAttachmentsRow.Status = 1;
//                        PrisonersAttachmentsList.Add(PrisonersAttachmentsRow);
//                    }
//                }




//                Prisoners row = new Prisoners();
//                if (User.UserType != 1)
//                {
//                    row.OfficeId = User.OfficeId;
//                }
//                else
//                {
//                    row.OfficeId = bodyObject.OfficeId;
//                }

//                row.Name = bodyObject.Name;
//                row.MotharName = bodyObject.MotharName;
//                row.IsLive = bodyObject.IsLive;
//                row.IsFinsh = bodyObject.IsFinsh;

//                row.MaritalStatus = bodyObject.MaritalStatus;
//                row.ChildrenCount = bodyObject.ChildrenCount;
//                row.Nid = bodyObject.NID;
//                row.Passport = bodyObject.Passport;

//                row.Gender = bodyObject.Gender;
//                row.BirthDate = bodyObject.BirthDate;
//                row.WorkPlace = bodyObject.Workplace;
//                row.Address = bodyObject.Address;

//                row.IsLiveFather = bodyObject.IsLiveFather.GetValueOrDefault() ? true : false;
//                row.IsLiveMothar = bodyObject.IsLiveMothar.GetValueOrDefault() ? true : false;
//                row.Qualification = bodyObject.Qualification;

//                row.WakelName = bodyObject.WakelName;
//                row.WakelNid = bodyObject.WakelNID;
//                row.WakelPhone = bodyObject.WakelPhone;

//                row.FatherNid = bodyObject.FatherNID;
//                row.MotherNid = bodyObject.MotherNID;
//                row.WifeNid = bodyObject.WifeNID;
//                row.WifeNid1 = bodyObject.WifeNID1;
//                row.WifeNid2 = bodyObject.WifeNID2;
//                row.WifeNid3 = bodyObject.WifeNID3;
//                row.Child1 = bodyObject.Child1;
//                row.Child2= bodyObject.Child2;
//                row.Child3 = bodyObject.Child3;
//                row.Child4 = bodyObject.Child4;
//                row.Child5 = bodyObject.Child5;
//                row.Child6 = bodyObject.Child6;
//                row.Child7 = bodyObject.Child7;
//                row.Child8 = bodyObject.Child8;
//                row.Child9 = bodyObject.Child9;
//                row.Child10 = bodyObject.Child10;

//                row.HealthIssues = bodyObject.HealthIssues;
//                row.Disease = bodyObject.Disease;

//                row.Phone = bodyObject.Phone;
//                row.AcountNumber = bodyObject.AcountNumber;
//                row.BankBrancheId = bodyObject.BankBranchesId;

//                if(bodyObject.BankBranchesId1>0)
//                {
//                    row.SecondBankBrancheId=bodyObject.BankBranchesId1;
//                    row.AcountNumber1 = bodyObject.AcountNumber1;
//                }

//                row.MunicipalityId = bodyObject.MunicipalitiesId;

//                row.WitnessName = bodyObject.WitnessName;
//                row.WitnessNid = bodyObject.WitnessNID;
//                row.WitnessName1 = bodyObject.WitnessName1;
//                row.WitnessNid1 = bodyObject.WitnessNID1;

//                //row.StartDate = bodyObject.StartDate;
//                //row.EndDate = bodyObject.EndDate;
//                row.CountOfDay = CountOfDay ;

//                //row.BarCode = this.help.GenerateRandomNoBarCode().ToString();
//                row.Notes = bodyObject.Notes;
                
//                if (!string.IsNullOrEmpty(bodyObject.fileBase64) && !string.IsNullOrEmpty(bodyObject.ImageName))
//                {
//                    row.Image = this.help.UploadFile(bodyObject.ImageName, this.help.GetAttachmentType(bodyObject.ImageName), bodyObject.fileBase64);
//                }
//                else
//                {
//                    row.Image = this.help.UploadFile("Default Image", ".jpg", this.help.GetDefaultImage());
//                }

//                long? MaxQrNumber=db.Prisoners.Where(x=>x.Status!=9).Max(x=>x.BarCode);
//                if (MaxQrNumber == null || MaxQrNumber <= 0)
//                {
//                    MaxQrNumber = 800001;
//                }else
//                {
//                    MaxQrNumber+= 1;
//                }



//                row.BarCode = MaxQrNumber.GetValueOrDefault();
//                row.BarCodeImage=this.help.GenerateQRCode(MaxQrNumber.ToString());
//                row.CountOfDayPay = 0;
//                row.CountOfDayRemind = 0;
//                row.PrisonersAttachments = PrisonersAttachmentsList;
//                row.PrisonersSchedule = prisonersSchedules;
//                row.CreatedBy = userId;
//                row.CreatedOn = DateTime.Now;
//                row.Status = 1;
//                db.Prisoners.Add(row);


//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Add;
//                rowTrans.Descriptions = "إضافة بيانات اسير جديد ";
//                rowTrans.Controller = "Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);


//                db.SaveChanges();
//                return Ok(BackMessages.SucessAddOperations + " رقم الملف الجديد : " + MaxQrNumber);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpPost("Edit")]
//        public IActionResult Edit([FromBody] BodyObject bodyObject)
//        {
//            try
//            {
//                if (bodyObject == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var User = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
//                if (User == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var row = db.Prisoners.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
//                if (row == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);


//                //valideations
//                if (string.IsNullOrWhiteSpace(bodyObject.Name))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.MotharName))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.NID))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NIDEmpty);

//                if (!this.help.IsValidNID(bodyObject.NID))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.RongNID);

//                if (!string.IsNullOrWhiteSpace(bodyObject.WakelNID))
//                {
//                    if (!this.help.IsValidNID(bodyObject.WakelNID))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (  الوكيل)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.FatherNID))
//                {
//                    if (!this.help.IsValidNID(bodyObject.FatherNID))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (  الأب)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.MotherNID))
//                {
//                    if (!this.help.IsValidNID(bodyObject.MotherNID))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأم)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child1))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child1))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن الأول)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child2))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child2))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التاني)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child3))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child3))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التالت)");
//                }


//                if (!string.IsNullOrWhiteSpace(bodyObject.Child4))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child4))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن الرابع)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child5))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child5))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن الخامس)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child6))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child6))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن السادس)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child7))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child7))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن السابع)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child8))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child8))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التامن)");
//                }

//                if (!string.IsNullOrWhiteSpace(bodyObject.Child9))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child9))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن التاسع)");
//                }


//                if (!string.IsNullOrWhiteSpace(bodyObject.Child10))
//                {
//                    if (!this.help.IsValidNID(bodyObject.Child10))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.RongNID + " (الأبن العاشر)");
//                }


//                if (string.IsNullOrWhiteSpace(bodyObject.WitnessNID))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.WitnessNID1))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDEmpty1);

//                if (bodyObject.WitnessNID == bodyObject.WitnessNID1)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDExist);

//                if (bodyObject.WitnessNID == bodyObject.NID || bodyObject.WitnessNID1 == bodyObject.NID)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.WitnessNIDExistNID);



//                //if (string.IsNullOrWhiteSpace(bodyObject.Passport))
//                //    return StatusCode(BackMessages.StatusCode, BackMessages.PassportEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.Address))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.AddressEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.Workplace))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.WorkplaceEmpty);

//                if (string.IsNullOrWhiteSpace(bodyObject.Phone))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PhoneEmpty);

//                if (!this.help.IsValidPhone(bodyObject.Phone))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PhoneNotValid);

//                if (string.IsNullOrWhiteSpace(bodyObject.AcountNumber))
//                    return StatusCode(BackMessages.StatusCode, BackMessages.AcountNumberEmpty);

//                if (!string.IsNullOrWhiteSpace(bodyObject.WakelPhone))
//                {
//                    if (!this.help.IsValidPhone(bodyObject.WakelPhone))
//                        return StatusCode(BackMessages.StatusCode, BackMessages.PhoneNotValid + "الوكيل");
//                }





//                if (User.UserType == 1)
//                {
//                    if (bodyObject.OfficeId <= 0)
//                        return StatusCode(BackMessages.StatusCode, BackMessages.OfficeIdEmpty);
//                }

//                if (bodyObject.BirthDate.Year > DateTime.Now.Year)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.BirthYearMustBeEqualToOrLessThenCurrentYear);

                


//                //Is Exist Patint
//                var isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Nid == bodyObject.NID && x.Status != 9 && x.Id!=bodyObject.Id).SingleOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NIDExist + " في " + isExist.Office.Name);

//                isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Name == bodyObject.Name && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NameExist + " في " + isExist.Office.Name);

//                isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Phone == bodyObject.Phone && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PhoneExist + " في " + isExist.Office.Name);




//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.OldObject = JsonConvert.SerializeObject(row, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });

//                if (User.UserType != 1)
//                {
//                    row.OfficeId = User.OfficeId;
//                }
//                else
//                {
//                    row.OfficeId = bodyObject.OfficeId;
//                }

//                row.Name = bodyObject.Name;
//                row.MotharName = bodyObject.MotharName;
//                row.IsLive = bodyObject.IsLive;
//                row.IsFinsh = bodyObject.IsFinsh;

//                row.MaritalStatus = bodyObject.MaritalStatus;
//                row.ChildrenCount = bodyObject.ChildrenCount;
//                row.Nid = bodyObject.NID;
//                row.Passport = bodyObject.Passport;

//                row.Gender = bodyObject.Gender;
//                row.BirthDate = bodyObject.BirthDate;
//                row.WorkPlace = bodyObject.Workplace;
//                row.Address = bodyObject.Address;

//                row.IsLiveFather = bodyObject.IsLiveFather.GetValueOrDefault() ? true : false;
//                row.IsLiveMothar = bodyObject.IsLiveMothar.GetValueOrDefault() ? true : false;
//                row.Qualification = bodyObject.Qualification;

//                row.WakelName = bodyObject.WakelName;
//                row.WakelNid = bodyObject.WakelNID;
//                row.WakelPhone = bodyObject.WakelPhone;

//                row.FatherNid = bodyObject.FatherNID;
//                row.MotherNid = bodyObject.MotherNID;
//                row.WifeNid = bodyObject.WifeNID;
//                row.WifeNid1 = bodyObject.WifeNID1;
//                row.WifeNid2 = bodyObject.WifeNID2;
//                row.WifeNid3 = bodyObject.WifeNID3;
//                row.Child1 = bodyObject.Child1;
//                row.Child2 = bodyObject.Child2;
//                row.Child3 = bodyObject.Child3;
//                row.Child4 = bodyObject.Child4;
//                row.Child5 = bodyObject.Child5;
//                row.Child6 = bodyObject.Child6;
//                row.Child7 = bodyObject.Child7;
//                row.Child8 = bodyObject.Child8;
//                row.Child9 = bodyObject.Child9;
//                row.Child10 = bodyObject.Child10;

//                row.HealthIssues = bodyObject.HealthIssues;
//                row.Disease = bodyObject.Disease;

//                row.Phone = bodyObject.Phone;
//                row.AcountNumber = bodyObject.AcountNumber;
//                row.BankBrancheId = bodyObject.BankBranchesId;

//                if(bodyObject.BankBranchesId1>0)
//                {
//                    row.SecondBankBrancheId = bodyObject.BankBranchesId1;
//                    row.AcountNumber1 = bodyObject.AcountNumber1;
//                }    

//                row.MunicipalityId = bodyObject.MunicipalitiesId;

//                row.WitnessName = bodyObject.WitnessName;
//                row.WitnessNid = bodyObject.WitnessNID;
//                row.WitnessName1 = bodyObject.WitnessName1;
//                row.WitnessNid1 = bodyObject.WitnessNID1;

//                row.Notes = bodyObject.Notes;

//                if (!string.IsNullOrEmpty(bodyObject.fileBase64) && !string.IsNullOrEmpty(bodyObject.ImageName))
//                {
//                    row.Image = this.help.UploadFile(bodyObject.ImageName, this.help.GetAttachmentType(bodyObject.ImageName), bodyObject.fileBase64);
//                }
//                else
//                {
//                    row.Image = this.help.UploadFile("Default Image", ".jpg", this.help.GetDefaultImage());
//                }

//                row.ModifiedBy = userId;
//                row.ModifiedOn = DateTime.Now;


                
//                rowTrans.Operations = TransactionsType.Edit;
//                rowTrans.ItemId = row.Id;
//                rowTrans.Descriptions = "تعديل بيانات اسير  ";
//                rowTrans.Controller = "Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);

//                db.SaveChanges();
//                return Ok(BackMessages.SucessEditOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }


//        [HttpGet("Get")]
//        public IActionResult Get(int pageNo, int pageSize, long? CityId, long? MunicipalityId, long? OfficeId, short? PrisonerStatus, short? PrisonerDocumentStatus, long? PrisonersId)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                int Count = db.Prisoners
//                        .Include(x => x.Office)
//                        .Include(x => x.Office.Municipality)
//                        .Include(x => x.Office.Municipality.City)
//                     .Where(x => x.Status != 9
//                       && (user.UserType != 1 ? x.OfficeId == user.OfficeId : true)
//                       && (CityId > 0 && user.UserType == 1 ? x.Office.Municipality.CityId == CityId : true)
//                       && (MunicipalityId > 0 && user.UserType == 1 ? x.Office.MunicipalityId == MunicipalityId : true)
//                       && (OfficeId > 0 && user.UserType == 1 ? x.OfficeId == OfficeId : true)
//                       && (PrisonersId > 0 ? (user.UserType != 1 ? x.OfficeId == user.OfficeId : true) && x.Id == PrisonersId : true)
//                       && (PrisonerStatus > 0 ? x.Status == PrisonerStatus : true)
//                       && (PrisonerDocumentStatus > 0 ? x.IsFinsh == PrisonerDocumentStatus : true)
//                     ).Count();

//                var Info = db.Prisoners
//                        .Include(x => x.Office)
//                        .Include(x => x.Office.Municipality)
//                        .Include(x => x.Office.Municipality.City)
//                        .Include(x=>x.BankBranche)
//                        .Include(x=>x.BankBranche.Bank)
//                        .Include(x=>x.PrisonersAttachments)
//                        .Include(x=>x.SecondBankBranche)
//                     .Where(x => x.Status != 9
//                       && (user.UserType != 1 ? x.OfficeId == user.OfficeId : true)
//                       && (CityId > 0 && user.UserType == 1 ? x.Office.Municipality.CityId == CityId : true)
//                       && (MunicipalityId > 0 && user.UserType == 1 ? x.Office.MunicipalityId == MunicipalityId : true)
//                       && (OfficeId > 0 && user.UserType == 1 ? x.OfficeId == OfficeId : true)
//                       && (PrisonersId > 0 ? (user.UserType != 1 ? x.OfficeId == user.OfficeId : true) && x.Id == PrisonersId : true)
//                       && (PrisonerStatus > 0 ? x.Status == PrisonerStatus : true)
//                       && (PrisonerDocumentStatus > 0 ? x.IsFinsh == PrisonerDocumentStatus : true)
//                     ).Select(x => new
//                     {
//                         x.Id,
//                         x.Name,
//                         x.MotharName,
//                         x.Nid,
//                         x.Passport,
//                         x.Phone,
//                         x.MaritalStatus,
//                         x.Gender,
//                         x.BirthDate,
//                         x.Address,
//                         x.WorkPlace,
//                         x.Image,
//                         x.ChildrenCount,
//                         x.AcountNumber,
//                         x.AcountNumber1,
//                         BankBrancheName = x.BankBranche.Name,
//                         x.BankBrancheId,
//                         x.BankBranche.BankId,
//                         BankName=x.BankBranche.Bank.Name,
//                         x.SecondBankBrancheId,
//                         BankId1=x.SecondBankBrancheId>0 ? x.SecondBankBranche.BankId : 0, 
//                         x.Qualification,
//                         x.CountOfDay,
//                         x.CountOfDayPay,
//                         x.CountOfDayRemind,
//                         x.BarCode,
//                         x.BarCodeImage,
//                         x.IsLiveFather,
//                         x.IsLiveMothar,
//                         CityCenterId = x.Office.Municipality.CityId,
//                         CityCenterName = x.Office.Municipality.City.Name,
//                         MunicipalitiesId = x.Office.MunicipalityId,
//                         MunicipalitiesName = x.Office.Municipality.Name,
//                         x.OfficeId,
//                         OfficeName = x.Office.Name,
//                         x.Status,
//                         x.Notes,
//                         x.IsLive,
//                         x.IsFinsh,
//                         x.WakelName,
//                         x.WakelNid,
//                         x.WakelPhone,
//                         x.FatherNid,
//                         x.MotherNid,
//                         x.WifeNid,
//                         x.WifeNid1,
//                         x.WifeNid2,
//                         x.WifeNid3,
//                         x.Child1,
//                         x.Child2,
//                         x.Child3,
//                         x.Child4,
//                         x.Child5,
//                         x.Child6,
//                         x.Child7,
//                         x.Child8,
//                         x.Child9,
//                         x.Child10,
//                         x.WakelNid2,
//                         x.HealthIssues,
//                         x.Disease,
//                         x.WitnessName,
//                         x.WitnessName1,
//                         x.WitnessNid,
//                         x.WitnessNid1,
//                         Attachments = x.PrisonersAttachments.Where(k => k.Status != 9).Select(k => new
//                         {
//                             k.Id,
//                             url = k.Path
//                         }).ToList(),
//                         CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                         x.CreatedOn
//                     }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//                return Ok(new { info = Info, count = Count });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("GetAllPrisonersToPrint")]
//        public IActionResult GetAllPatientToPrint()
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
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.Prisoners
//                    .Include(x => x.Office)
//                    .Include(x => x.Office.Municipality)
//                    .Include(x => x.Office.Municipality.City)
//                    .Where(x => x.Status != 9).Select(x => new
//                    {
//                        x.Id,
//                        x.Name,
//                        x.MotharName,
//                        x.Phone,
//                        x.Nid,
//                        x.Passport,
//                        x.MaritalStatus,
//                        x.Gender,
//                        x.BirthDate,
//                        x.Address,
//                        x.WorkPlace,
//                        x.Image,
//                        x.ChildrenCount,
//                        x.AcountNumber,
//                        BankBrancheName = x.BankBranche.Name,
//                        x.BankBrancheId,
//                        x.BankBranche.BankId,
//                        BankName = x.BankBranche.Bank.Name,
//                        x.Qualification,
//                        x.CountOfDay,
//                        x.CountOfDayPay,
//                        x.CountOfDayRemind,
//                        x.BarCode,
//                        x.IsLiveFather,
//                        x.IsLiveMothar,
//                        CityCenterId = x.Office.Municipality.CityId,
//                        CityCenterName = x.Office.Municipality.City.Name,
//                        MunicipalitiesId = x.Office.MunicipalityId,
//                        MunicipalitiesName = x.Office.Municipality.Name,
//                        x.OfficeId,
//                        OfficeName = x.Office.Name,
//                        x.Status,
//                        x.Notes,
//                        CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                        x.CreatedOn
//                    }).OrderByDescending(x => x.OfficeName).ToList();
//                return Ok(new { info = Info });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("GetByName")]
//        public IActionResult GetByName(string code)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.Prisoners
//                    .Where(x => x.Status != 9
//                     && (user.UserType != 1 ? x.OfficeId == user.OfficeId : true)
//                    && (x.Passport.Contains(code) || x.Nid.Contains(code) || x.Name.Contains(code) || x.BarCode.ToString().Contains(code)))
//                    .Select(x => new
//                    {
//                        x.Id,
//                        x.Name,
//                        x.Nid,
//                        x.Passport,
//                        x.BarCode,
//                    }).ToList();

//                return Ok(new { info = Info });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        //[HttpPost("Edit")]
//        //public IActionResult Edit([FromBody] BodyObject bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var User = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
//        //        if (User == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var row = db.Prisoners.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
//        //        if (row == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.OldObject = JsonConvert.SerializeObject(row, Formatting.None,
//        //                new JsonSerializerSettings()
//        //                {
//        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//        //                });

//        //        //valideations
//        //        if (string.IsNullOrWhiteSpace(bodyObject.Name))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.MotharName))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.NID))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NIDEmpty);

//        //        if (!this.help.IsValidNID(bodyObject.NID))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.RongNID);

//        //        //if (string.IsNullOrWhiteSpace(bodyObject.Passport))
//        //        //    return StatusCode(BackMessages.StatusCode, BackMessages.PassportEmpty);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.Address))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.AddressEmpty);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.Workplace))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.WorkplaceEmpty);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.Phone))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PhoneEmpty);

//        //        if (!this.help.IsValidPhone(bodyObject.Phone))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PhoneNotValid);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.AcountNumber))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.AcountNumberEmpty);


//        //        if (User.UserType == 1)
//        //        {
//        //            if (bodyObject.OfficeId <= 0)
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.OfficeIdEmpty);
//        //        }

//        //        //if (string.IsNullOrWhiteSpace(bodyObject.BarCode))
//        //        //    return StatusCode(BackMessages.StatusCode, BackMessages.BarCodeEmpty);

//        //        if (bodyObject.StartDate == DateTime.MinValue)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DateEmpty);

//        //        if (bodyObject.EndDate == DateTime.MinValue)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DateEmpty);

//        //        if (bodyObject.StartDate.Year > DateTime.Now.Year)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.KidneyFailureDateMustBeEqualToOrLessThenCurrentYear);

//        //        if (bodyObject.StartDate.Date > bodyObject.EndDate.Date)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DateNotCorrect);


//        //        //Is Exist Patint
//        //        var isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Nid == bodyObject.NID && x.Status != 9 && x.Id!=bodyObject.Id).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NIDExist + " في " + isExist.Office.Name);

//        //        isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Name == bodyObject.Name && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist + " في " + isExist.Office.Name);

//        //        isExist = db.Prisoners.Include(x => x.Office).Where(x => x.Phone == bodyObject.Phone && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PhoneExist + " في " + isExist.Office.Name);


//        //        if (User.UserType != 1)
//        //        {
//        //            row.OfficeId = User.OfficeId;
//        //        }
//        //        else
//        //        {
//        //            row.OfficeId = bodyObject.OfficeId;
//        //        }


//        //        row.Name = bodyObject.Name;
//        //        row.MotharName = bodyObject.MotharName;
//        //        row.Nid = bodyObject.NID;
//        //        row.Passport = bodyObject.Passport;
//        //        row.Phone = bodyObject.Phone;
//        //        row.MaritalStatus = bodyObject.MaritalStatus;
//        //        row.Gender = bodyObject.Gender;
//        //        row.BirthDate = bodyObject.BirthDate;
//        //        row.WorkPlace = bodyObject.Workplace;
//        //        row.MaritalStatus = bodyObject.MaritalStatus;
//        //        row.Address = bodyObject.Address;
//        //        row.Notes = bodyObject.Notes;
//        //        if (!string.IsNullOrEmpty(bodyObject.fileBase64) && !string.IsNullOrEmpty(bodyObject.ImageName))
//        //        {
//        //            row.Image = this.help.UploadFile(bodyObject.ImageName, this.help.GetAttachmentType(bodyObject.ImageName), bodyObject.fileBase64);
//        //        }
//        //        row.AcountNumber = bodyObject.AcountNumber;
//        //        row.BankBrancheId = bodyObject.BankBranchesId;
//        //        row.ChildrenCount = bodyObject.ChildrenCount;
//        //        row.Qualification = bodyObject.Qualification;
//        //        row.StartDate = bodyObject.StartDate;
//        //        row.EndDate = bodyObject.EndDate;
//        //        row.EndDate = bodyObject.EndDate;
//        //        row.CountOfDay = int.Parse((bodyObject.EndDate - bodyObject.StartDate).TotalDays.ToString());
//        //        row.CountOfDayPay = 0;
//        //        row.CountOfDayRemind = 0;
//        //        row.IsLiveFather = bodyObject.IsLiveFather.GetValueOrDefault() ? true : false;
//        //        row.IsLiveMothar = bodyObject.IsLiveMothar.GetValueOrDefault() ? true : false;
//        //        //row.BarCode = bodyObject.BarCode;
//        //        row.MunicipalityId = bodyObject.MunicipalitiesId;
//        //        row.ModifiedBy = userId;
//        //        row.ModifiedOn = DateTime.Now;
//        //        db.Prisoners.Add(row);
                
//        //        //transacions
//        //        rowTrans.Operations = TransactionsType.Edit;
//        //        rowTrans.ItemId = row.Id;
//        //        rowTrans.Descriptions = "تعديل بيانات الاسير ";
//        //        rowTrans.Controller = "Prisoners";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//        //                new JsonSerializerSettings()
//        //                {
//        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//        //                });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessEditOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        [HttpPost("{Id}/Delete")]
//        public IActionResult Delete(long Id)
//        {
//            try
//            {
//                if (Id <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.Prisoners.Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//                if (Info == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//                Info.Status = 9;

//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Delete;
//                rowTrans.Descriptions = "حذف اسير ";
//                rowTrans.ItemId = Id;
//                rowTrans.Controller = "Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(Info);
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);

//                db.SaveChanges();
//                return Ok(BackMessages.SucessDeleteOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }










//        // *******************************************| Attachment |*****************************************

//        public class AttachemntBodyObject
//        {
//            public long Id { get; set; }
//            public string ImageName { get; set; }
//            public string ImageType { get; set; }
//            public string fileBase64 { get; set; }
//        }
//        [HttpPost("Attachments/Add")]
//        public IActionResult AddAttahcment([FromBody] AttachemntBodyObject bodyObject)
//        {
//            try
//            {
//                if (bodyObject == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                if (bodyObject.Id <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var isExist = db.PrisonersAttachments.Where(x => x.PrisonerId == bodyObject.Id && x.Name == bodyObject.ImageName && x.Status != 9).FirstOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.FileNameExist);

//                PrisonersAttachments row = new PrisonersAttachments();
//                row.PrisonerId = bodyObject.Id;
//                row.Name = bodyObject.ImageName;
//                row.Path = this.help.UploadFile(bodyObject.ImageName, this.help.GetAttachmentType(bodyObject.ImageName), bodyObject.fileBase64);
//                row.CreatedOn = DateTime.Now;
//                row.CreatedBy = userId;
//                row.Status = 1;
//                db.PrisonersAttachments.Add(row);

//                var returnObje = new
//                {
//                    row.Id,
//                    url = row.Path,
//                };

//                //transactions
//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Add;
//                rowTrans.Descriptions = "إضافة مرفقات للاسير  ";
//                rowTrans.ItemId = bodyObject.Id;
//                rowTrans.Controller = "Attachment/Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(row);
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);
//                db.SaveChanges();
//                return Ok(new { Info= returnObje, Message= BackMessages.SucessAddOperations } );
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("Attachments/Get")]
//        public IActionResult GetAttahcment(long Id)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.PrisonersAttachments
//                        .Where(x => x.Status == 1 && x.PrisonerId == Id).Select(x => new
//                        {
//                            x.Id,
//                            x.Name,
//                            x.Path,
//                            x.CreatedOn,
//                            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                        }).OrderByDescending(x => x.CreatedOn).ToList();

//                return Ok(new { info = Info });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpPost("{Id}/Attachments/Delete")]
//        public IActionResult DeleteAttahcment(long Id)
//        {
//            try
//            {
//                if (Id <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.PrisonersAttachments.Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//                if (Info == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//                Info.Status = 9;

                

//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Delete;
//                rowTrans.Descriptions = "حذف مرفق   ";
//                rowTrans.ItemId = Info.PrisonerId;
//                rowTrans.Controller = "Attachment/Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(Info);
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);

//                db.SaveChanges();
//                return Ok(BackMessages.SucessDeleteOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }





//        // *******************************************| Schedule |*****************************************
//        public class ScheduleBodyObject
//        {
//            public long Id { get; set; }
//            //public long FilterId { get; set; }
//            public DateTime From { get; set; }
//            public DateTime To { get; set; }
//        }

//        [HttpPost("Schedule/Add")]
//        public IActionResult AddSchedule([FromBody] ScheduleBodyObject bodyObject)
//        {
//            try
//            {
//                if (bodyObject == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                if (bodyObject.From==DateTime.MinValue || bodyObject.To==DateTime.MinValue)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.TimeNotValid);

//                //DateTime From = DateTime.ParseExact(bodyObject.From, "MM DD YYYY", System.Globalization.CultureInfo.InvariantCulture);
//                //DateTime To = DateTime.ParseExact(bodyObject.To, "MM DD YYYY", System.Globalization.CultureInfo.InvariantCulture);

//                if (bodyObject.From >= bodyObject.To)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.DateNotCorrect);

//                var Prisoners = db.Prisoners.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
//                if (Prisoners == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//                if (Prisoners.Status != 1)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.PatientStatusNotValid);

//                var isExist = db.PrisonersSchedule.Where(x => x.PrisonerId == bodyObject.Id && x.From == bodyObject.From
//                     && x.To == bodyObject.To && x.Status!=9).FirstOrDefault();
//                if (isExist != null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.DayExist);

//                int CountOfDay= int.Parse((bodyObject.To - bodyObject.From).TotalDays.ToString());
//                Prisoners.CountOfDay += CountOfDay;
//                Prisoners.CountOfDayRemind += CountOfDay;


//                PrisonersSchedule row = new PrisonersSchedule();
//                row.PrisonerId = bodyObject.Id;
//                row.CountOfDay = CountOfDay;
//                row.From = bodyObject.From;
//                row.To = bodyObject.To;
//                row.CreatedOn = DateTime.Now;
//                row.CreatedBy = userId;
//                row.Status = 1;
//                db.PrisonersSchedule.Add(row);


//                //transactions
//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Add;
//                rowTrans.Descriptions = "إضافة تواريخ جديدة   ";
//                rowTrans.ItemId = bodyObject.Id;
//                rowTrans.Controller = "Schedule/Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });
//                //rowTrans.NewObject = JsonConvert.SerializeObject(row);
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);
//                db.SaveChanges();
//                return Ok(BackMessages.SucessAddOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("Schedule/Get")]
//        public IActionResult GetSchedule(long Id)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.PrisonersSchedule
//                        .Where(x => x.Status == 1 && x.PrisonerId == Id).Select(x => new
//                        {
//                            x.Id,
//                            x.CountOfDay,
//                            x.From,
//                            x.To,
//                            x.CreatedOn,
//                            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                        }).OrderByDescending(x => x.CreatedOn).ToList();

//                return Ok(new { info = Info });
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpPost("{Id}/Schedule/Delete")]
//        public IActionResult DeleteSchedule(long Id)
//        {
//            try
//            {
//                if (Id <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var Info = db.PrisonersSchedule.Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//                if (Info == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//                Info.Status = 9;

//                var row = db.Prisoners.Where(x => x.Id == Info.PrisonerId && x.Status != 9).SingleOrDefault();
//                if (row == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//                row.CountOfDay -= Info.CountOfDay;


//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Delete;
//                rowTrans.Descriptions = "حذف تواريخ    ";
//                rowTrans.ItemId = Info.PrisonerId;
//                rowTrans.Controller = "Schedule/Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(Info, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);

//                db.SaveChanges();
//                return Ok(BackMessages.SucessDeleteOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }





//        // *******************************************| Calculat |*****************************************

//        [HttpGet("Calculat/Get")]
//        public IActionResult GetCalculatStatic()
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//                int PrisonersCount = db.Prisoners.Where(x => x.Status == 1).Count();
//                int PrisonersDayRemindCount = db.Prisoners.Where(x => x.Status == 1).Sum(x => x.CountOfDayRemind).GetValueOrDefault();
//                int PrisonersDayRemindCountMore30 = db.Prisoners.Where(x => x.Status == 1 && x.CountOfDayRemind>30).Count();
//                int PrisonersDayRemindCountMore60 = db.Prisoners.Where(x => x.Status == 1 && x.CountOfDayRemind>60).Count();
//                int PrisonersDayRemindCountMore120 = db.Prisoners.Where(x => x.Status == 1 && x.CountOfDayRemind>120).Count();

//                var Info = new
//                {
//                    PrisonersCount,
//                    PrisonersDayRemindCount,
//                    PrisonersDayRemindCountMore30,
//                    PrisonersDayRemindCountMore60,
//                    PrisonersDayRemindCountMore120,
//                };

//                return Ok(new { info = Info });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        public class PayBodyObject
//        {
//            public string Descriptions { get; set; }
//            public int MaxValue { get; set; }
//            public int PriceOfDay { get; set; }
//        }
//        [HttpGet("Calculat/CalculatToPay")]
//        public IActionResult GetCalculatToPay(long MaxValue, double PriceOfDay)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//                var PrisonersCount=db.Prisoners.Where(x => x.Status == 1).Count();

//                double valueOfPay = MaxValue / PrisonersCount;
//                double valueOfDay = valueOfPay / PriceOfDay;
//                double valueOfHour = valueOfDay / 24;

//                var Info = new
//                {
//                    PrisonersCount,
//                    valueOfPay,
//                    valueOfDay,
//                    valueOfHour,
//                };




//                return Ok(new { info = Info });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpPost("Calculat/Pay")]
//        public IActionResult AddPay([FromBody] PayBodyObject bodyObject)
//        {
//            try
//            {
//                if (bodyObject == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//                var PrisonersInfo = db.Prisoners.Where(x => x.Status == 1).ToList();
//                var PrisonersCount = db.Prisoners.Where(x => x.Status == 1).Count();

//                int Value = bodyObject.MaxValue / PrisonersCount;
//                int CountDay = Value / bodyObject.PriceOfDay;
//                int CountHour = CountDay / 24;


//                decimal ActualValue = 0;
//                List<PayDetails> PayDetailsList = new List<PayDetails>();

//                foreach (var item in PrisonersInfo)
//                {
//                    if(item.CountOfDayRemind>=CountDay)
//                    {
//                        item.CountOfDayRemind -= CountDay;
//                        item.CountOfDayPay += CountDay;
//                        PayDetails payDetails = new PayDetails();
//                        payDetails.PrisonersId = item.Id;
//                        payDetails.PriceForDay = bodyObject.PriceOfDay;
//                        payDetails.PriceForOur = CountDay/24;
//                        payDetails.CountOfDay = CountDay;
//                        payDetails.Value = bodyObject.PriceOfDay * CountDay;
//                        ActualValue += bodyObject.PriceOfDay * CountDay;
//                        payDetails.CreatedBy = userId;
//                        payDetails.CreatedOn = DateTime.Now;
//                        payDetails.Status = 1;
//                        PayDetailsList.Add(payDetails);

//                    }
//                    else
//                    {
//                        int AcualCountDay = item.CountOfDayRemind.GetValueOrDefault();
//                        item.CountOfDayPay += AcualCountDay;
//                        item.CountOfDayRemind -= AcualCountDay;
//                        item.Status = 2;
//                        PayDetails payDetails = new PayDetails();
//                        payDetails.PrisonersId = item.Id;
//                        payDetails.PriceForDay = bodyObject.PriceOfDay;
//                        payDetails.PriceForOur = item.CountOfDayRemind / 24;
//                        payDetails.CountOfDay = AcualCountDay;
//                        payDetails.Value = bodyObject.PriceOfDay * AcualCountDay; ;
//                        ActualValue += bodyObject.PriceOfDay * AcualCountDay;
//                        payDetails.CreatedBy = userId;
//                        payDetails.CreatedOn = DateTime.Now;
//                        payDetails.Status = 1;
//                        PayDetailsList.Add(payDetails);
//                    }
//                }



//                Pay row = new Pay();
//                row.MaxValue = bodyObject.MaxValue;
//                row.PayDetails = PayDetailsList;
//                row.Value = ActualValue;
//                row.ValueForPersone = decimal.Parse(Value.ToString());
//                row.CountOfPersone = PrisonersCount;
//                row.CountOfDayPerPersone = int.Parse(CountDay.ToString());
//                row.PriceOfDay = bodyObject.PriceOfDay;
//                row.Descriptions = bodyObject.Descriptions;
//                row.CreatedOn = DateTime.Now;
//                row.CreatedBy = userId;
//                row.Status = 1;
//                db.Pay.Add(row);


//                //transactions
//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Add;
//                rowTrans.Descriptions = "إضافة عملية صرف   ";
//                //rowTrans.ItemId = bodyObject.;
//                rowTrans.Controller = "Pay/Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });
//                //rowTrans.NewObject = JsonConvert.SerializeObject(row);
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);
//                db.SaveChanges();
//                return Ok(BackMessages.SucessAddOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }

//        [HttpGet("Calculat/PayInfoTransactions/Get")]
//        public IActionResult GetPayInfoTransactions(int pageNo, int pageSize, long? CityId, long? MunicipalitiesId, long? OfficeId, short? PrisonerStatus, long? PrisonersId)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                int Count = db.PayDetails
//                    .Include(x=>x.Prisoners)
//                        .Include(x => x.Prisoners.Office)
//                        .Include(x => x.Prisoners.Office.Municipality)
//                        .Include(x => x.Prisoners.Office.Municipality.City)
//                     .Where(x => x.Status != 9
//                       && (user.UserType != 1 ? x.Prisoners.OfficeId == user.OfficeId : true)
//                       && (CityId > 0 && user.UserType == 1 ? x.Prisoners.Office.Municipality.CityId == CityId : true)
//                       && (MunicipalitiesId > 0 && user.UserType == 1 ? x.Prisoners.Office.MunicipalityId == MunicipalitiesId : true)
//                       && (OfficeId > 0 && user.UserType == 1 ? x.Prisoners.OfficeId == OfficeId : true)
//                       && (PrisonersId > 0 ? x.PrisonersId == PrisonersId : true)
//                       && (PrisonerStatus > 0 ? x.Status == PrisonerStatus : true)
//                     ).Count();

//                var Info = db.PayDetails
//                        .Include(x => x.Pay)
//                        .Include(x => x.Prisoners.Office)
//                        .Include(x => x.Prisoners.Office.Municipality)
//                        .Include(x => x.Prisoners.Office.Municipality.City)
//                        .Include(x => x.Prisoners.BankBranche)
//                        .Include(x => x.Prisoners.BankBranche.Bank)
//                     .Where(x => x.Status != 9
//                       && (user.UserType != 1 ? x.Prisoners.OfficeId == user.OfficeId : true)
//                       && (CityId > 0 && user.UserType == 1 ? x.Prisoners.Office.Municipality.CityId == CityId : true)
//                       && (MunicipalitiesId > 0 && user.UserType == 1 ? x.Prisoners.Office.MunicipalityId == MunicipalitiesId : true)
//                       && (OfficeId > 0 && user.UserType == 1 ? x.Prisoners.OfficeId == OfficeId : true)
//                       && (PrisonersId > 0 ? x.PrisonersId == PrisonersId : true)
//                       && (PrisonerStatus > 0 ? x.Status == PrisonerStatus : true)
//                     ).Select(x => new
//                     {
//                         x.Id,
//                         PayOn=x.Pay.CreatedOn,
//                         x.PriceForDay,
//                         x.PriceForOur,
//                         x.CountOfDay,
//                         x.Pay.Descriptions,
//                         x.Prisoners.Name,
//                         x.Prisoners.MotharName,
//                         x.Prisoners.Nid,
//                         x.Prisoners.Passport,
//                         x.Prisoners.Phone,
//                         x.Prisoners.MaritalStatus,
//                         x.Prisoners.Gender,
//                         x.Prisoners.BirthDate,
//                         x.Prisoners.Address,
//                         x.Prisoners.WorkPlace,
//                         x.Prisoners.Image,
//                         x.Prisoners.ChildrenCount,
//                         x.Prisoners.AcountNumber,
//                         BankBrancheName = x.Prisoners.BankBranche.Name,
//                         x.Prisoners.BankBrancheId,
//                         x.Prisoners.BankBranche.BankId,
//                         BankName = x.Prisoners.BankBranche.Bank.Name,
//                         x.Prisoners.Qualification,
//                         PCountOfDay=x.Prisoners.CountOfDay,
//                         x.Prisoners.CountOfDayPay,
//                         x.Prisoners.CountOfDayRemind,
//                         x.Prisoners.BarCode,
//                         x.Prisoners.IsLiveFather,
//                         x.Prisoners.IsLiveMothar,
//                         CityCenterId = x.Prisoners.Office.Municipality.CityId,
//                         CityCenterName = x.Prisoners.Office.Municipality.City.Name,
//                         MunicipalitiesId = x.Prisoners.Office.MunicipalityId,
//                         MunicipalitiesName = x.Prisoners.Office.Municipality.Name,
//                         x.Prisoners.OfficeId,
//                         OfficeName = x.Prisoners.Office.Name,
//                         x.Status,
//                         x.Prisoners.Notes,
//                         CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                         x.Prisoners.CreatedOn
//                     }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//                return Ok(new { info = Info, count = Count });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }


//        [HttpGet("Calculat/GetPay")]
//        public IActionResult GetPay(int pageNo, int pageSize)
//        {
//            try
//            {
//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                int Count = db.Pay
//                    .Where(x => x.Status != 9
//                    ).Count();
//                var Info = db.Pay
//                    .Include(x=>x.PayDetails)
//                    .Where(x => x.Status != 9
//                ).Select(x => new
//                {
//                    x.Id,
//                    PayDetails=x.PayDetails.Where(k=>k.Status!=9).Select(k=> new { 
//                        k.Prisoners.Name,
//                        k.Prisoners.Nid,
//                        k.Prisoners.Phone,
//                        k.Value,
//                        k.CountOfDay,
//                        k.PriceForDay,
//                    }).ToList(),
//                    PayDetails1=x.PayDetails.Where(k=>k.Status!=9 && k.Value!=x.ValueForPersone).Select(k=> new { 
//                        k.Prisoners.Name,
//                        k.Prisoners.Nid,
//                        k.Prisoners.Phone,
//                        k.Value,
//                        k.CountOfDay,
//                        k.PriceForDay,
//                    }).ToList(),
//                    x.MaxValue,
//                    x.Value,
//                    x.ValueForPersone,
//                    x.CountOfPersone,
//                    x.CountOfDayPerPersone,
//                    x.PriceOfDay,
//                    x.Descriptions,
//                    x.Status,
//                    x.CreatedOn,
//                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//                return Ok(new { info = Info, count = Count });
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }


//        [HttpPost("{Id}/Calculat/Delete")]
//        public IActionResult DeletePay(long Id)
//        {
//            try
//            {
//                if (Id <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//                var userId = this.help.GetCurrentUser(HttpContext);
//                if (userId <= 0)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//                var row = db.Pay.Include(x=>x.PayDetails).Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//                if (row == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
                
//                row.Status = 9;

//                if(row.PayDetails.Count()>0)
//                {
//                    foreach (var item in row.PayDetails)
//                    {
//                        if(item.Status!=9)
//                        {
//                            var Prisoners = db.Prisoners.Where(x => x.Id == item.Id).SingleOrDefault();
//                            if(Prisoners==null)
//                                return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//                            Prisoners.CountOfDayPay -= item.CountOfDay;
//                            Prisoners.CountOfDayRemind+=item.CountOfDay;
//                        }
                        
//                    }
//                }


//                TransactionsObject rowTrans = new TransactionsObject();
//                rowTrans.Operations = TransactionsType.Delete;
//                rowTrans.Descriptions = "حذف عملية صرف    ";
//                rowTrans.ItemId = row.Id;
//                rowTrans.Controller = "Calculat/Prisoners";
//                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                        });
//                rowTrans.CreatedBy = userId;
//                this.help.WriteTransactions(rowTrans);

//                db.SaveChanges();
//                return Ok(BackMessages.SucessDeleteOperations);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
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
//                if (user == null)
//                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//                int UserCount = db.Users.Where(x => x.Status != 9 && (user.UserType!=1 ? x.Id==userId : true)).Count();
//                int PrisonersCount = db.Prisoners.Where(x => x.Status != 9 && (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Count();
//                int ActivePrisonersCount = db.Prisoners.Where(x => x.Status == 1 && (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Count();
//                int FinshPrisonersCount = db.Prisoners.Where(x => x.Status == 2 && (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Count();
//                int LivePrisonersCount = db.Prisoners.Where(x => x.IsLive == 1 && x.Status!=9 && (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Count();
//                int DeathPrisonersCount = db.Prisoners.Where(x => x.IsLive == 2 && x.Status!=9 && (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Count();
//                int CountOfDay = db.Prisoners
//                    .Where(x => x.Status != 9 &&  (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Sum(x=>x.CountOfDay).GetValueOrDefault();
                
//                int CountOfDayPay = db.Prisoners
//                    .Where(x => x.Status != 9 &&  (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Sum(x=>x.CountOfDayPay).GetValueOrDefault();
//                int CountOfDayRemind = db.Prisoners
//                    .Where(x => x.Status != 9 &&  (user.UserType!=1 ? x.OfficeId==user.OfficeId : true)).Sum(x=>x.CountOfDayRemind).GetValueOrDefault();
//                int PayCount = db.Pay.Where(x => x.Status != 9 && (user.UserType != 1 ? x.Status == 20 : true)).Count();
//                decimal PayValue = db.Pay.Where(x => x.Status != 9 && (user.UserType != 1 ? x.Status == 20 : true)).Sum(x=>x.Value).GetValueOrDefault();

//                int Bank = db.Bank.Where(x => x.Status != 9).Count();
//                int Cities = db.Cities.Where(x => x.Status != 9).Count();
//                int Municipalities = db.Municipalities.Where(x => x.Status != 9).Count();
//                int Offices = db.Offices.Where(x => x.Status != 9).Count();

//                var Info = new
//                {
//                    Bank,
//                    Cities,
//                    Municipalities,
//                    Offices,
//                    ActivePrisonersCount,
//                    FinshPrisonersCount,
//                    LivePrisonersCount,
//                    DeathPrisonersCount,
//                    UserCount,
//                    PrisonersCount,
//                    CountOfDay,
//                    CountOfDayPay,
//                    CountOfDayRemind,
//                    PayCount,
//                    PayValue,
//                };




//                return Ok(new { info = Info });


//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }






//        //// *******************************************| Transactions |*****************************************
//        //[HttpGet("Transactions/Get")]
//        //public IActionResult GetTransactions(int pageNo, int pageSize, long PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (PatientId <= 0)
//        //        {
//        //            int Count = db.Transactions
//        //                .Where(x => x.Controller == "Patient"
//        //                || x.Controller == "Attachment/Patient"
//        //                || x.Controller == "Phone/Patient"
//        //                || x.Controller == "Schedule/Patient").Count();
//        //            var Info = db.Transactions
//        //                .Where(x => x.Controller == "Patient"
//        //                || x.Controller == "Attachment/Patient"
//        //                || x.Controller == "Phone/Patient"
//        //                || x.Controller == "Schedule/Patient"
//        //                ).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Item = db.Patients.Where(k => k.Id == x.ItemId).SingleOrDefault().Name,
//        //                    FileNumber = db.Patients.Where(k => k.Id == x.ItemId).SingleOrDefault().FileNumber,
//        //                    x.Operations,
//        //                    x.Controller,
//        //                    x.Descriptions,
//        //                    x.OldObject,
//        //                    x.NewObject,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.Transactions
//        //            .Where(x => (x.Controller == "Patient"
//        //            || x.Controller == "Attachment/Patient"
//        //            || x.Controller == "Phone/Patient"
//        //            || x.Controller == "Schedule/Patient") && x.ItemId == PatientId).Count();
//        //            var Info = db.Transactions
//        //                .Where(x => (x.Controller == "Patient"
//        //                || x.Controller == "Attachment/Patient"
//        //                || x.Controller == "Phone/Patient"
//        //                || x.Controller == "Schedule/Patient") && x.ItemId == PatientId
//        //                ).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Item = db.Patients.Where(k => k.Id == x.ItemId).SingleOrDefault().Name,
//        //                    FileNumber = db.Patients.Where(k => k.Id == x.ItemId).SingleOrDefault().FileNumber,
//        //                    x.Operations,
//        //                    x.Descriptions,
//        //                    x.Controller,
//        //                    x.OldObject,
//        //                    x.NewObject,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}











//        //// *******************************************| ChangeRequest |*****************************************
//        //public class ChangeCenterObj
//        //{
//        //    public long PatientId { get; set; }
//        //    public long KidneyCenterId { get; set; }
//        //    public string Note { get; set; }
//        //}
//        //[HttpPost("ChangeRequest/Add")]
//        //public IActionResult AddChangeRequest([FromBody] ChangeCenterObj bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Patient = db.Patients.Where(x => x.Id == bodyObject.PatientId && x.Status != 9).SingleOrDefault();
//        //        if (Patient == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        if (string.IsNullOrEmpty(bodyObject.Note))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.ChangeResoneEmpty);

//        //        if (user.UserType != 1)
//        //        {
//        //            if (Patient.KidneyCenterId != user.KidneyCentersId)
//        //            {
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.DontHavePermisineToProsseger);
//        //            }
//        //        }

//        //        string FromName = db.KidneyCenters.Where(x => x.Id == Patient.KidneyCenterId).SingleOrDefault().ArabicName;
//        //        if (string.IsNullOrEmpty(FromName))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        string ToName = db.KidneyCenters.Where(x => x.Id == bodyObject.KidneyCenterId).SingleOrDefault().ArabicName;
//        //        if (string.IsNullOrEmpty(ToName))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);


//        //        var isExist = db.PatientsChangeCenter.Where(x => x.PatientId == Patient.Id
//        //        && x.Status != 9
//        //        && x.Level == 1
//        //        && x.FromCenter == Patient.KidneyCenterId
//        //        && x.ToCenter == bodyObject.KidneyCenterId).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.RequestExist);


//        //        PatientsChangeCenter row = new PatientsChangeCenter();
//        //        row.PatientId = Patient.Id;
//        //        row.FromCenter = Patient.KidneyCenterId;
//        //        row.FromName = FromName;
//        //        row.ToCenter = bodyObject.KidneyCenterId;
//        //        row.ToName = ToName;
//        //        row.Level = 1;
//        //        row.CreatedBy = userId;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.Status = 1;
//        //        db.PatientsChangeCenter.Add(row);

//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Add;
//        //        rowTrans.Descriptions = "تقديم طلب انتقال";
//        //        rowTrans.ItemId = bodyObject.PatientId;
//        //        rowTrans.Controller = "ChangeRequest/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
//        //                new JsonSerializerSettings()
//        //                {
//        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//        //                });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessRequestOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("ChangeRequest/Get")]
//        //public IActionResult GetChangeRequest(int pageNo, int pageSize, long CityId, long MunicipalitiesId, long KidneyCenterId, short? Level, long? PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Count = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Include(x => x.Patient.KidneyCenter.Municipality)
//        //                .Where(x => x.Status != 9).ToList();
//        //        var Info = db.PatientsChangeCenter
//        //            .Include(x => x.Patient)
//        //            .Include(x => x.Patient.KidneyCenter)
//        //            .Include(x => x.Patient.KidneyCenter.Municipality)
//        //            .Where(x => x.Status != 9).Select(x => new
//        //            {
//        //                x.Id,
//        //                x.Patient.KidneyCenter.Municipality.CityId,
//        //                x.Patient.KidneyCenter.MunicipalityId,
//        //                x.Patient.KidneyCenterId,
//        //                x.PatientId,
//        //                x.Patient.Name,
//        //                x.Patient.FileNumber,
//        //                x.Patient.Nid,
//        //                AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                x.FromCenter,
//        //                x.FromName,
//        //                x.ToCenter,
//        //                x.ToName,
//        //                AcceptedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                x.AcceptedOn,
//        //                RejectBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                x.RejectOn,
//        //                x.RejectResone,
//        //                x.Note,
//        //                x.Level,
//        //                x.Status,
//        //                x.CreatedOn,
//        //                CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //            }).ToList();

//        //        if (Level > 0)
//        //        {
//        //            Count = Count.Where(x => x.Level == Level).ToList();
//        //            Info = Info.Where(x => x.Level == Level).ToList();
//        //        }

//        //        if (PatientId > 0)
//        //        {
//        //            Count = Count.Where(x => x.PatientId == PatientId).ToList();
//        //            Info = Info.Where(x => x.PatientId == PatientId).ToList();
//        //        }


//        //        if (CityId > 0)
//        //        {
//        //            Count = Count.Where(x => x.Patient.KidneyCenter.Municipality.CityId == CityId).ToList();
//        //            Info = Info.Where(x => x.CityId == CityId).ToList();
//        //        }

//        //        if (MunicipalitiesId > 0)
//        //        {
//        //            Count = Count.Where(x => x.Patient.KidneyCenter.MunicipalityId == MunicipalitiesId).ToList();
//        //            Info = Info.Where(x => x.MunicipalityId == MunicipalitiesId).ToList();
//        //        }

//        //        if (KidneyCenterId > 0)
//        //        {
//        //            Count = Count.Where(x => x.Patient.KidneyCenter.MunicipalityId == MunicipalitiesId).ToList();
//        //            Info = Info.Where(x => x.KidneyCenterId == KidneyCenterId).ToList();
//        //        }

//        //        Info = Info.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //        return Ok(new { info = Info, count = Count.Count() });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("ChangeRequest/GetRequest")]
//        //public IActionResult GetChangeRequest(int pageNo, int pageSize, short? Level, long? PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Count = db.PatientsChangeCenter
//        //                .Where(x => x.Status != 9
//        //            && x.ToCenter == user.KidneyCentersId).ToList();

//        //        var Info = db.PatientsChangeCenter
//        //            .Include(x => x.Patient)
//        //            .Where(x => x.Status != 9 &&
//        //            x.ToCenter == user.KidneyCentersId).Select(x => new
//        //            {
//        //                x.Id,
//        //                x.PatientId,
//        //                x.Patient.Name,
//        //                x.Patient.FileNumber,
//        //                x.Patient.Nid,
//        //                AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                x.FromCenter,
//        //                x.FromName,
//        //                x.ToCenter,
//        //                x.ToName,
//        //                AcceptedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                x.AcceptedOn,
//        //                RejectBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                x.RejectOn,
//        //                x.RejectResone,
//        //                x.Note,
//        //                x.Level,
//        //                x.Status,
//        //                x.CreatedOn,
//        //                CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //            }).ToList();

//        //        if (Level > 0)
//        //        {
//        //            Count = Count.Where(x => x.Level == Level).ToList();
//        //            Info = Info.Where(x => x.Level == Level).ToList();
//        //        }

//        //        if (PatientId > 0)
//        //        {
//        //            Count = Count.Where(x => x.PatientId == PatientId).ToList();
//        //            Info = Info.Where(x => x.PatientId == PatientId).ToList();
//        //        }


//        //        Info = Info.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //        return Ok(new { info = Info, count = Count.Count() });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("ChangeRequest/GetMyRequest")]
//        //public IActionResult GetMyRequest(int pageNo, int pageSize, short? Level, long? PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Count = db.PatientsChangeCenter
//        //                .Where(x => x.Status != 9
//        //            && x.FromCenter == user.KidneyCentersId).ToList();

//        //        var Info = db.PatientsChangeCenter
//        //            .Include(x => x.Patient)
//        //            .Where(x => x.Status != 9 &&
//        //            x.FromCenter == user.KidneyCentersId).Select(x => new
//        //            {
//        //                x.Id,
//        //                x.PatientId,
//        //                x.Patient.Name,
//        //                x.Patient.FileNumber,
//        //                x.Patient.Nid,
//        //                AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                x.FromCenter,
//        //                x.FromName,
//        //                x.ToCenter,
//        //                x.ToName,
//        //                AcceptedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                x.AcceptedOn,
//        //                RejectBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                x.RejectOn,
//        //                x.RejectResone,
//        //                x.Note,
//        //                x.Level,
//        //                x.Status,
//        //                x.CreatedOn,
//        //                CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //            }).ToList();

//        //        if (Level > 0)
//        //        {
//        //            Count = Count.Where(x => x.Level == Level).ToList();
//        //            Info = Info.Where(x => x.Level == Level).ToList();
//        //        }

//        //        if (PatientId > 0)
//        //        {
//        //            Count = Count.Where(x => x.PatientId == PatientId).ToList();
//        //            Info = Info.Where(x => x.PatientId == PatientId).ToList();
//        //        }


//        //        Info = Info.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //        return Ok(new { info = Info, count = Count.Count() });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/ChangeRequest/Delete")]
//        //public IActionResult DeleteChangeRequest(long id)
//        //{
//        //    try
//        //    {
//        //        if (id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var Info = db.PatientsChangeCenter.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Status = 9;

//        //        var Patient = db.Patients.Where(x => x.Id == Info.PatientId).SingleOrDefault();
//        //        if (Patient == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);


//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Delete;
//        //        rowTrans.Descriptions = "حذف طلب انتقال";
//        //        rowTrans.ItemId = Patient.Id;
//        //        rowTrans.Controller = "ChangeRequest/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(Info, Formatting.None,
//        //                new JsonSerializerSettings()
//        //                {
//        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//        //                });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/ChangeRequest/Accept")]
//        //public IActionResult AccepteChangeRequest(long id)
//        //{
//        //    try
//        //    {
//        //        if (id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var Info = db.PatientsChangeCenter.Where(x => x.Id == id && x.Status != 9 && x.Level == 1).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Level = 2;
//        //        Info.AcceptedBy = userId;
//        //        Info.AcceptedOn = DateTime.Now;

//        //        var Patient = db.Patients.Where(x => x.Id == Info.PatientId).SingleOrDefault();
//        //        if (Patient == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//        //        Patient.KidneyCenterId = Info.ToCenter;


//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Accept;
//        //        rowTrans.Descriptions = "الموافقة علي طلب انتقال";
//        //        rowTrans.ItemId = Patient.Id;
//        //        rowTrans.Controller = "ChangeRequest/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(Info, Formatting.None,
//        //                new JsonSerializerSettings()
//        //                {
//        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//        //                });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAcceptedRequest);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //public class RejectChangeRequestObje
//        //{
//        //    public long Id { get; set; }
//        //    public string Resone { get; set; }
//        //}

//        //[HttpPost("ChangeRequest/Reject")]
//        //public IActionResult RejectChangeRequest([FromBody] RejectChangeRequestObje bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);


//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.PatientsChangeCenter.Where(x => x.Id == bodyObject.Id && x.Status != 9 && x.Level == 1).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);


//        //        Info.Level = 3;
//        //        Info.RejectBy = userId;
//        //        Info.RejectOn = DateTime.Now;
//        //        Info.RejectResone = bodyObject.Resone;
//        //        Info.ModifiedBy = userId;
//        //        Info.ModifiedOn = DateTime.Now;

//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Reject;
//        //        rowTrans.Descriptions = "رفض علي طلب انتقال";
//        //        rowTrans.ItemId = Info.PatientId;
//        //        rowTrans.Controller = "ChangeRequest/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(Info, Formatting.None,
//        //                new JsonSerializerSettings()
//        //                {
//        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//        //                });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessRejectRequest);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}












//        //// *******************************************| ChangeRequest Attachment |*****************************************
//        //[HttpPost("ChangeRequest/Attachments/Add")]
//        //public IActionResult AddChangeRequestAttahcment([FromBody] AttachemntBodyObject bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        if (bodyObject.Id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var isExist = db.PatientChangeCenterAttachments.Where(x => x.PatientsChangeCenterId == bodyObject.Id && x.Name == bodyObject.ImageName && x.Status != 9).FirstOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.FileNameExist);

//        //        var Info = db.PatientsChangeCenter.Where(x => x.Id == bodyObject.Id).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        PatientChangeCenterAttachments row = new PatientChangeCenterAttachments();
//        //        row.PatientsChangeCenterId = bodyObject.Id;
//        //        row.Name = bodyObject.ImageName;
//        //        row.Path = this.help.UploadFile(bodyObject.ImageName, this.help.GetAttachmentType(bodyObject.ImageName), bodyObject.fileBase64);
//        //        row.CreatedOn = DateTime.Now;
//        //        row.CreatedBy = userId;
//        //        row.Status = 1;
//        //        db.PatientChangeCenterAttachments.Add(row);


//        //        //transactions
//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Add;
//        //        rowTrans.Descriptions = "إضافة مرفقات لعملية نقل   ";
//        //        rowTrans.ItemId = Info.PatientId;
//        //        rowTrans.Controller = "ChangeRequest/Attachment/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("ChangeRequest/Attachments/Get")]
//        //public IActionResult GetChangeRequestAttahcment(long Id)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.PatientChangeCenterAttachments
//        //                .Where(x => x.Status == 1 && x.PatientsChangeCenterId == Id).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    x.Name,
//        //                    x.Path,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();

//        //        return Ok(new { info = Info });


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/ChangeRequest/Attachments/Delete")]
//        //public IActionResult DeleteChangeRequestAttahcment(long Id)
//        //{
//        //    try
//        //    {
//        //        if (Id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.PatientChangeCenterAttachments.Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//        //        Info.Status = 9;

//        //        var Patient = db.PatientsChangeCenter.Where(x => x.Id == Info.PatientsChangeCenterId).SingleOrDefault();
//        //        if (Patient == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Delete;
//        //        rowTrans.Descriptions = "حذف مرفق من عملية نقل";
//        //        rowTrans.ItemId = Patient.PatientId;
//        //        rowTrans.Controller = "ChangeRequest/Attachment/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(Info, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}















//        //// *******************************************| Daily Used |*****************************************
//        //public class AttendanceManuelBodyObject
//        //{
//        //    public long PatientId { get; set; }
//        //    public long FilterId { get; set; }
//        //    public int UsedCount { get; set; }
//        //}
//        //[HttpGet("DailyUsed/GetTodayPatient")]
//        //public IActionResult GetTodayPatient(long PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 2)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        if (PatientId > 0)
//        //        {
//        //            int Day = (int)DateTime.Now.DayOfWeek;

//        //            var Attend = db.PatientAttendance.Where(x => x.KidneyCentersId == user.KidneyCentersId
//        //            && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Now.Date) == 0)
//        //                .Select(x => x.PatientId).ToList();

//        //            var Info = db.PatientSchedule
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.Filter)
//        //                .Include(x => x.Patient.Filter.Device)
//        //                .Include(x => x.Patient.Filter.Device.Company)
//        //                    .Where(x => x.Status == 1
//        //                    && x.PatientId == PatientId
//        //                    && x.Patient.KidneyCenterId == user.KidneyCentersId
//        //                    && x.Day == Day
//        //                    && !Attend.Contains(PatientId)
//        //                    )
//        //                    .Select(x => new
//        //                    {
//        //                        x.Id,
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        x.Patient.Passport,
//        //                        x.Patient.Filter.Device.CompanyId,
//        //                        CompanyName = x.Patient.Filter.Device.Company.Name,
//        //                        x.Patient.Filter.DeviceId,
//        //                        DeviceName = x.Patient.Filter.Device.Name,
//        //                        x.Patient.FilterId,
//        //                        FilterName = x.Patient.Filter.Name,
//        //                        x.From,
//        //                        x.To,
//        //                        x.CreatedOn,
//        //                    }).OrderByDescending(x => x.From).ToList();

//        //            return Ok(new { info = Info });
//        //        }
//        //        else
//        //        {
//        //            int Day = (int)DateTime.Now.DayOfWeek;

//        //            var Attend = db.PatientAttendance.Where(x => x.KidneyCentersId == user.KidneyCentersId
//        //            && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Now.Date) == 0
//        //            )
//        //                .Select(x => x.PatientId).ToList();

//        //            var Info = db.PatientSchedule
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.Filter)
//        //                .Include(x => x.Patient.Filter.Device)
//        //                .Include(x => x.Patient.Filter.Device.Company)
//        //                    .Where(x => x.Status == 1
//        //                    && x.Patient.KidneyCenterId == user.KidneyCentersId
//        //                    && x.Day == Day
//        //                    && !Attend.Contains(x.PatientId)
//        //                    )
//        //                    .Select(x => new
//        //                    {
//        //                        x.Id,
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        x.Patient.Passport,
//        //                        x.Patient.Filter.Device.CompanyId,
//        //                        CompanyName = x.Patient.Filter.Device.Company.Name,
//        //                        x.Patient.Filter.DeviceId,
//        //                        DeviceName = x.Patient.Filter.Device.Name,
//        //                        x.Patient.FilterId,
//        //                        FilterName = x.Patient.Filter.Name,
//        //                        x.From,
//        //                        x.To,
//        //                        x.CreatedOn,
//        //                    }).OrderByDescending(x => x.From).ToList();

//        //            return Ok(new { info = Info });

//        //        }



//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/DailyUsed/Add")]
//        //public IActionResult NewAttendance(long Id)
//        //{
//        //    try
//        //    {
//        //        if (Id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId && x.Status == 1).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.PatientSchedule
//        //            .Include(x => x.Patient)
//        //            .Include(x => x.Patient.KidneyCenter)
//        //            .Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        var Patients = db.Patients.Where(x => x.Status != 9 && x.Id == Info.PatientId).SingleOrDefault();
//        //        if (Patients == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        if (user.KidneyCentersId != Info.Patient.KidneyCenterId)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DontHavePermisineToProsseger);

//        //        //isExist
//        //        var isExist = db.PatientAttendance
//        //            .Where(x => x.KidneyCentersId == user.KidneyCentersId
//        //             && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Now.Date) == 0
//        //             && x.PatientId == Info.PatientId
//        //             )
//        //                .Select(x => x.PatientId).FirstOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PatientCometodyBefore);


//        //        PatientAttendance row = new PatientAttendance();
//        //        row.PatientId = Info.PatientId;
//        //        row.FilterId = Info.Patient.FilterId;
//        //        row.KidneyCentersId = Info.Patient.KidneyCenterId;
//        //        row.UsedCount = 1;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.CreatedBy = userId;
//        //        row.Status = 1;
//        //        db.PatientAttendance.Add(row);


//        //        //transactions
//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Add;
//        //        rowTrans.Descriptions = "تسجيل حضور يومي (ألي )";
//        //        rowTrans.ItemId = Info.PatientId;
//        //        rowTrans.Controller = "DailyUsed/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("DailyUsed/AddManuel")]
//        //public IActionResult NewAttendanceManuel([FromBody] AttendanceManuelBodyObject bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId && x.Status == 1).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (bodyObject.UsedCount <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.UsedCountNotCorrect);

//        //        if (bodyObject.UsedCount > 20)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.UsedCountNotCorrect);

//        //        var Info = db.Patients.Where(x => x.Status != 9 && x.Id == bodyObject.PatientId).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        if (Info.Status != 1)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PatientStatusNotAllowd);

//        //        if (user.KidneyCentersId != Info.KidneyCenterId)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DontHavePermisineToProsseger);

//        //        //isExist
//        //        var isExist = db.PatientAttendance
//        //            .Where(x => x.KidneyCentersId == user.KidneyCentersId
//        //             && DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Now.Date) == 0
//        //             && x.PatientId == Info.Id).Select(x => x.PatientId).FirstOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PatientCometodyBefore);


//        //        PatientAttendance row = new PatientAttendance();
//        //        row.PatientId = Info.Id;
//        //        row.FilterId = Info.FilterId;
//        //        row.KidneyCentersId = Info.KidneyCenterId;
//        //        row.UsedCount = bodyObject.UsedCount;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.CreatedBy = userId;
//        //        row.Status = 1;
//        //        db.PatientAttendance.Add(row);


//        //        //transactions
//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Add;
//        //        rowTrans.Descriptions = "تسجيل حضور يومي (يدوي )";
//        //        rowTrans.ItemId = Info.Id;
//        //        rowTrans.Controller = "DailyUsed/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("DailyUsed/Get")]
//        //public IActionResult GetDailyUsed(int pageNo, int pageSize, long CityId, long MunicipalitiesId, long KidneyCenterId, long? PatientId, DateTime ByDate, long CompanyId, long DeviceId, long FilterId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);



//        //        int Count = db.PatientAttendance
//        //              .Include(x => x.Patient)
//        //              .Include(x => x.Patient.KidneyCenter)
//        //              .Include(x => x.Patient.KidneyCenter.Municipality)
//        //              .Include(x => x.Filter)
//        //              .Include(x => x.Filter.Device)
//        //              .Where(x => x.Status != 9
//        //                && (user.UserType != 1 ? x.Patient.KidneyCenterId == user.KidneyCentersId : true)
//        //                && (CityId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.Municipality.CityId == CityId : true)
//        //                && (MunicipalitiesId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.MunicipalityId == MunicipalitiesId : true)
//        //                && (KidneyCenterId > 0 && user.UserType == 1 ? x.Patient.KidneyCenterId == KidneyCenterId : true)
//        //                && (PatientId > 0 ? (user.UserType != 1 ? x.Patient.KidneyCenterId == user.KidneyCentersId : true) && x.PatientId == PatientId : true)
//        //                && (ByDate > DateTime.MinValue ? DateTime.Compare(x.CreatedOn.Value.Date, ByDate.Date) == 0 : true)
//        //                && (CompanyId > 0 ? x.Filter.Device.CompanyId == CompanyId : true)
//        //                && (DeviceId > 0 ? x.Filter.DeviceId == DeviceId : true)
//        //                && (FilterId > 0 ? x.FilterId == FilterId : true)
//        //              ).Count();
//        //        var Info = db.PatientAttendance
//        //            .Include(x => x.Patient)
//        //            .Include(x => x.Filter)
//        //            .Include(x => x.Filter.Device)
//        //            .Include(x => x.Patient.KidneyCenter)
//        //            .Include(x => x.Patient.KidneyCenter.Municipality)
//        //            .Where(x => x.Status != 9
//        //            && (user.UserType != 1 ? x.Patient.KidneyCenterId == user.KidneyCentersId : true)
//        //                && (CityId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.Municipality.CityId == CityId : true)
//        //                && (MunicipalitiesId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.MunicipalityId == MunicipalitiesId : true)
//        //                && (KidneyCenterId > 0 && user.UserType == 1 ? x.Patient.KidneyCenterId == KidneyCenterId : true)
//        //                && (PatientId > 0 ? (user.UserType != 1 ? x.Patient.KidneyCenterId == user.KidneyCentersId : true) && x.PatientId == PatientId : true)
//        //                && (ByDate > DateTime.MinValue ? DateTime.Compare(x.CreatedOn.Value.Date, ByDate.Date) == 0 : true)
//        //                && (CompanyId > 0 ? x.Filter.Device.CompanyId == CompanyId : true)
//        //                && (DeviceId > 0 ? x.Filter.DeviceId == DeviceId : true)
//        //                && (FilterId > 0 ? x.FilterId == FilterId : true)
//        //            ).Select(x => new
//        //            {
//        //                x.PatientId,
//        //                x.Patient.Name,
//        //                x.Patient.FileNumber,
//        //                x.Patient.Nid,
//        //                x.Patient.Passport,
//        //                x.Patient.Filter.Device.CompanyId,
//        //                CompanyName = x.Filter.Device.Company.Name,
//        //                x.Patient.Filter.DeviceId,
//        //                DeviceName = x.Filter.Device.Name,
//        //                x.Patient.FilterId,
//        //                FilterName = x.Filter.Name,
//        //                x.UsedCount,
//        //                x.CreatedOn,
//        //                CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //            }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //        return Ok(new { info = Info, count = Count });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("DailyUsed/GetReport")]
//        //public IActionResult GetDailyUsedReport(int pageNo, int pageSize, long CityId, long MunicipalitiesId, long KidneyCenterId, DateTime From, DateTime To, long CompanyId, long DeviceId, long FilterId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);



//        //        var Info = from e in db.PatientAttendance
//        //            .Include(x => x.Patient)
//        //            .Include(x => x.Filter)
//        //            .Include(x => x.Filter.Device)
//        //            .Include(x => x.Filter.Device.Company)
//        //            .Include(x => x.Patient.KidneyCenter)
//        //            .Include(x => x.Patient.KidneyCenter.Municipality)
//        //            .Where(x => x.Status != 9
//        //            && (user.UserType != 1 ? x.Patient.KidneyCenterId == user.KidneyCentersId : true)
//        //                && (CityId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.Municipality.CityId == CityId : true)
//        //                && (MunicipalitiesId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.MunicipalityId == MunicipalitiesId : true)
//        //                && (KidneyCenterId > 0 && user.UserType == 1 ? x.Patient.KidneyCenterId == KidneyCenterId : true)
//        //                && (From > DateTime.MinValue ? x.CreatedOn.Value.Date >= From.Date && x.CreatedOn.Value.Date <= To.Date : true)
//        //                //&& (From > DateTime.MinValue ? DateTime.Compare(x.CreatedOn.Value.Date, From.Date) == 0 : true)
//        //                && (CompanyId > 0 ? x.Filter.Device.CompanyId == CompanyId : true)
//        //                && (DeviceId > 0 ? x.Filter.DeviceId == DeviceId : true)
//        //                && (FilterId > 0 ? x.FilterId == FilterId : true)
//        //            )
//        //                   group e by new
//        //                   {
//        //                       e.FilterId,
//        //                       FilterName = e.Filter.Name,
//        //                       DeviceName = e.Filter.Device.Name,
//        //                       CompanyName = e.Filter.Device.Company.Name,
//        //                   }
//        //                   into eg
//        //                   select new
//        //                   {
//        //                       eg.Key.CompanyName,
//        //                       eg.Key.DeviceName,
//        //                       eg.Key.FilterName,
//        //                       UsedCount = eg.Sum(rl => rl.UsedCount)
//        //                   };

//        //        //.GroupBy(x => x.FilterId)  
//        //        //.Select(x => new
//        //        //{
//        //        //    x.Key,
//        //        //    CompanyName = x.Key
//        //        //    DeviceName = x.Select(s=>s.Filter.Device.Name),
//        //        //    FilterName = x.Select(s=>s.Filter.Name),
//        //        //    UsedCount = x.Sum(s => s.UsedCount),
//        //        //}).OrderByDescending(x => x.CompanyName).ToList();

//        //        return Ok(new { info = Info });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("DailyUsed/GetRequired")]
//        //public IActionResult GetDailyUsedGetRequired(int pageNo, int pageSize, long CityId, long MunicipalitiesId, long KidneyCenterId, long CompanyId, long DeviceId, long FilterId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var Info = from e in db.PatientSchedule
//        //             .Include(x => x.Patient)
//        //             .Include(x => x.Patient.Filter)
//        //             .Include(x => x.Patient.Filter.Device)
//        //             .Include(x => x.Patient.Filter.Device.Company)
//        //             .Include(x => x.Patient.KidneyCenter)
//        //             .Include(x => x.Patient.KidneyCenter.Municipality)
//        //             .Where(x => x.Status == 1 && x.Patient.Status == 1
//        //             && (user.UserType != 1 ? x.Patient.KidneyCenterId == user.KidneyCentersId : true)
//        //                 && (CityId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.Municipality.CityId == CityId : true)
//        //                 && (MunicipalitiesId > 0 && user.UserType == 1 ? x.Patient.KidneyCenter.MunicipalityId == MunicipalitiesId : true)
//        //                 && (KidneyCenterId > 0 && user.UserType == 1 ? x.Patient.KidneyCenterId == KidneyCenterId : true)
//        //                 && (CompanyId > 0 ? x.Patient.Filter.Device.CompanyId == CompanyId : true)
//        //                 && (DeviceId > 0 ? x.Patient.Filter.DeviceId == DeviceId : true)
//        //                 && (FilterId > 0 ? x.Patient.FilterId == FilterId : true)
//        //             )

//        //                   group e by new
//        //                   {
//        //                       e.Patient.FilterId,
//        //                       FilterName = e.Patient.Filter.Name,
//        //                       DeviceName = e.Patient.Filter.Device.Name,
//        //                       CompanyName = e.Patient.Filter.Device.Company.Name,
//        //                   }
//        //                   into eg
//        //                   select new
//        //                   {
//        //                       eg.Key.FilterId,
//        //                       eg.Key.CompanyName,
//        //                       eg.Key.DeviceName,
//        //                       eg.Key.FilterName,
//        //                       WecklyCount = eg.Count(),
//        //                   };
//        //        return Ok(new { info = Info });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/DailyUsed/Delete")]
//        //public IActionResult DeleteAttendance(long Id)
//        //{
//        //    try
//        //    {
//        //        if (Id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.PatientAttendance.Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Status = 9;



//        //        TransactionsObject rowTrans = new TransactionsObject();
//        //        rowTrans.Operations = TransactionsType.Delete;
//        //        rowTrans.Descriptions = "حذف عملية تسجيل حضور";
//        //        rowTrans.ItemId = Info.PatientId;
//        //        rowTrans.Controller = "DailyUsed/Patient";
//        //        rowTrans.NewObject = JsonConvert.SerializeObject(Info, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

//        //        rowTrans.CreatedBy = userId;
//        //        this.help.WriteTransactions(rowTrans);

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}


















//        //[HttpGet("GetOld")]
//        //public IActionResult GetOld([FromQuery] int pageNo, [FromQuery] int pageSize, [FromQuery] long? CityId, [FromQuery] long? MunicipalityId, [FromQuery] long? KidneyCenterId, [FromQuery] short? PatientStatus)

//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var patients = db.Patients
//        //            .Include(x => x.City)
//        //            .Include(x => x.KidneyCenter)
//        //            .Include(x => x.KidneyCenter.Municipality)
//        //            .Include(x => x.KidneyCenter.Municipality.City)
//        //            .Where(x => x.Status != 9);
//        //        if (user.UserType == 2)
//        //        {
//        //            patients = patients.Where(x => x.KidneyCenterId == user.KidneyCentersId);

//        //            if (PatientStatus != null && PatientStatus > 0)
//        //                patients = patients.Where(x => x.Status == PatientStatus);

//        //            int Count = patients.Count();
//        //            var Info = patients
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //               .Select(x => new
//        //               {
//        //                   x.Id,
//        //                   City = new
//        //                   {
//        //                       x.CityId,
//        //                       x.City.Name,
//        //                   },
//        //                   KidneyCenter = new
//        //                   {
//        //                       x.KidneyCenterId,
//        //                       x.KidneyCenter.ArabicName,
//        //                       x.KidneyCenter.EnglishName,
//        //                       Municipality = new
//        //                       {
//        //                           x.KidneyCenter.MunicipalityId,
//        //                           x.KidneyCenter.Municipality.Name,
//        //                           City = new
//        //                           {
//        //                               x.KidneyCenter.Municipality.CityId,
//        //                               x.KidneyCenter.Municipality.City.Name,
//        //                               x.KidneyCenter.Municipality.City.ArabicName,
//        //                               x.KidneyCenter.Municipality.City.EnglishName,
//        //                           }
//        //                       },
//        //                   },
//        //                   Filter = new
//        //                   {
//        //                       x.FilterId,
//        //                       x.Filter.Name,
//        //                       Device = new
//        //                       {
//        //                           x.Filter.DeviceId,
//        //                           x.Filter.Device.Name,
//        //                           Company = new
//        //                           {
//        //                               x.Filter.Device.CompanyId,
//        //                               x.Filter.Device.Company.Name,
//        //                           },
//        //                       },
//        //                   },
//        //                   Nationality = new
//        //                   {
//        //                       x.NationalyId,
//        //                       x.Nationaly.Name
//        //                   },
//        //                   x.Image,
//        //                   x.Nid,
//        //                   x.Passport,
//        //                   x.FileNumber,
//        //                   x.FirstName,
//        //                   x.MiddleName,
//        //                   x.GrandfatherName,
//        //                   x.LastName,
//        //                   x.Name,
//        //                   x.Gender,
//        //                   x.DateOfBirth,
//        //                   x.Workplace,
//        //                   x.MaritalStatus,
//        //                   x.Address,
//        //                   x.BloodType,
//        //                   x.ViralAssays,
//        //                   x.KidneyFailureDate,
//        //                   x.KidneyFailureCause,
//        //                   x.Notes,
//        //                   x.CreatedOn,
//        //                   x.Status,
//        //                   CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //               }).Skip((pageNo - 1) * pageSize).Take(pageSize).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else if (user.UserType == 1)
//        //        {
//        //            if (KidneyCenterId != null && KidneyCenterId > 0)
//        //                patients = patients.Where(x => x.KidneyCenterId == KidneyCenterId);

//        //            else if (MunicipalityId != null && MunicipalityId > 0)
//        //                patients = patients.Where(x => x.KidneyCenter.MunicipalityId == MunicipalityId);

//        //            if (CityId != null && CityId > 0)
//        //                patients = patients.Where(x => x.CityId == CityId);

//        //            if (PatientStatus != null && PatientStatus > 0)
//        //                patients = patients.Where(x => x.Status == PatientStatus);



//        //            //Include(p => p.Filter).ThenInclude(p => p.Device)
//        //            int Count = patients.Count();
//        //            var Info = patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).Skip((pageNo - 1) * pageSize).Take(pageSize).OrderByDescending(x => x.CreatedOn).OrderByDescending(x => x.CreatedOn).ToList();


//        //            //if (CityId > 0)
//        //            //    Info = Info.Where(x => x.City.CityId == CityId).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            //if (MunicipalityId > 0)
//        //            //    Info = Info.Where(x => x.KidneyCenter.Municipality.MunicipalityId == MunicipalityId)
//        //            //        .Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            //if (KidneyCenterId > 0)
//        //            //    Info = Info.Where(x => x.KidneyCenter.KidneyCenterId == KidneyCenterId)
//        //            //        .Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            //if(CityId<=0 && MunicipalityId<=0 && KidneyCenterId<=0)
//        //            //    Info = Info.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            return Ok(new { info = Info, count = Count });
//        //        }

//        //        return Ok(new { info = Enumerable.Empty<object>(), count = 0 });
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}


//        //[HttpGet("GetAll")]
//        //public IActionResult GetAll()
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);
//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            var Info = db.Patients
//        //                .Where(x => x.Status == 1 && x.KidneyCenterId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    x.Name,
//        //                    x.FileNumber,
//        //                }).OrderByDescending(x => x.Name).ToList();


//        //            return Ok(new { info = Info });
//        //        }
//        //        else
//        //        {
//        //            var Info = db.Patients
//        //                .Where(x => x.Status == 1).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    x.Name,
//        //                    x.FileNumber,
//        //                }).OrderByDescending(x => x.Name).ToList();


//        //            return Ok(new { info = Info });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetById")]
//        //public IActionResult GetById(long KidneyCenterId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.Patients.Where(x => x.Status != 9 && x.KidneyCenterId == KidneyCenterId && x.KidneyCenterId == user.KidneyCentersId).Count();
//        //            var Info = db.Patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Where(x => x.Status != 9 && x.KidneyCenterId == KidneyCenterId && x.KidneyCenterId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.Patients.Where(x => x.Status != 9 && x.KidneyCenterId == KidneyCenterId).Count();
//        //            var Info = db.Patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Where(x => x.Status != 9 && x.KidneyCenterId == KidneyCenterId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}


//        //[AllowAnonymous]
//        //[HttpGet("GetByMunicipalitId")]
//        //public IActionResult GetByMunicipalitId(long MunicipalitId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.Patients.Where(x => x.Status != 9 && x.KidneyCenter.MunicipalityId == MunicipalitId && x.KidneyCenterId == user.KidneyCentersId).Count();
//        //            var Info = db.Patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Where(x => x.Status != 9 && x.KidneyCenter.MunicipalityId == MunicipalitId && x.KidneyCenterId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.Patients.Where(x => x.Status != 9 && x.KidneyCenter.MunicipalityId == MunicipalitId).Count();
//        //            var Info = db.Patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Where(x => x.Status != 9 && x.KidneyCenter.MunicipalityId == MunicipalitId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}



//        //[AllowAnonymous]
//        //[HttpGet("GetByHospitalId")]
//        //public IActionResult GetByHospitalId(long HospitalId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.Patients.Where(x => x.Status != 9 && x.KidneyCenterId == HospitalId && x.KidneyCenterId == user.KidneyCentersId).Count();
//        //            var Info = db.Patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Where(x => x.Status != 9 && x.KidneyCenterId == HospitalId && x.KidneyCenterId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.Patients.Where(x => x.Status != 9 && x.KidneyCenterId == HospitalId).Count();
//        //            var Info = db.Patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Where(x => x.Status != 9 && x.KidneyCenterId == HospitalId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetByPatientInfo")]
//        //public IActionResult GetByPatientInfo(long? NationalId, string FileNumber)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var patients = db.Patients.Where(x => x.Status != 9 &&
//        //                                              (NationalId == null ? true : x.Nid == NationalId) &&
//        //                                              (string.IsNullOrEmpty(FileNumber) ? true : x.FileNumber == FileNumber));

//        //        if (user.UserType == 2)
//        //        {
//        //            patients = patients.Where(x => x.KidneyCenterId == user.KidneyCentersId);
//        //            int Count = patients.Count();
//        //            var Info = patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly)
//        //                .Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else if (user.UserType == 1)
//        //        {

//        //            int Count = patients.Count();
//        //            var Info = patients
//        //                .Include(x => x.City)
//        //                .Include(x => x.KidneyCenter)
//        //                .Include(x => x.KidneyCenter.Municipality)
//        //                .Include(x => x.KidneyCenter.Municipality.City)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.PatientSchedule)
//        //                .Include(x => x.Nationaly).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    City = new
//        //                    {
//        //                        x.CityId,
//        //                        x.City.Name,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCenterId,
//        //                        x.KidneyCenter.ArabicName,
//        //                        x.KidneyCenter.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenter.MunicipalityId,
//        //                            x.KidneyCenter.Municipality.Name,
//        //                            City = new
//        //                            {
//        //                                x.KidneyCenter.Municipality.CityId,
//        //                                x.KidneyCenter.Municipality.City.Name,
//        //                                x.KidneyCenter.Municipality.City.ArabicName,
//        //                                x.KidneyCenter.Municipality.City.EnglishName,
//        //                            }
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    Nationality = new
//        //                    {
//        //                        x.NationalyId,
//        //                        x.Nationaly.Name
//        //                    },
//        //                    x.Image,
//        //                    x.Nid,
//        //                    x.Passport,
//        //                    x.FileNumber,
//        //                    x.FirstName,
//        //                    x.MiddleName,
//        //                    x.GrandfatherName,
//        //                    x.LastName,
//        //                    x.Name,
//        //                    x.Gender,
//        //                    x.DateOfBirth,
//        //                    x.Workplace,
//        //                    x.MaritalStatus,
//        //                    x.Address,
//        //                    x.BloodType,
//        //                    x.ViralAssays,
//        //                    x.KidneyFailureDate,
//        //                    x.KidneyFailureCause,
//        //                    x.Notes,
//        //                    x.CreatedOn,
//        //                    x.Status,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }

//        //        return Ok(new { info = Enumerable.Empty<object>(), count = 0 });

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}





//        ////[HttpPost("Add")]
//        ////public IActionResult Add([FromBody] BodyObject bodyObject)
//        ////{
//        ////    try
//        ////    {
//        ////        if (bodyObject == null)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        ////        var userId = this.help.GetCurrentUser(HttpContext);
//        ////        if (userId <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        ////        //isValid Info
//        ////        if (bodyObject.NID <= 0 && string.IsNullOrEmpty(bodyObject.Passport))
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.ProfileEmpty);

//        ////        if (bodyObject.NationalyId == 2)
//        ////        {
//        ////            if (!this.help.IsValidNID(bodyObject.NID.ToString()))
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.RongNID);

//        ////            var firstNumbner = bodyObject.NID.ToString().FirstOrDefault();

//        ////            if (firstNumbner != '1' && firstNumbner != '2')
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.MustStartWith1Or2);
//        ////        }
//        ////        else
//        ////        {
//        ////            if (string.IsNullOrWhiteSpace(bodyObject.Passport))
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.PassportEmpty);
//        ////        }


//        ////        if (string.IsNullOrWhiteSpace(bodyObject.FirstName) ||
//        ////            string.IsNullOrWhiteSpace(bodyObject.FatherName) ||
//        ////            string.IsNullOrWhiteSpace(bodyObject.GrandFatherName) ||
//        ////            string.IsNullOrWhiteSpace(bodyObject.LastName))
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);

//        ////        if (bodyObject.Day.Length <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.DayEmpty);



//        ////        var isExist = db.Patients.Include(x => x.KidneyCenter).Where(x => x.FileNumber == bodyObject.FileNumber && x.Status != 9).SingleOrDefault();
//        ////        if (isExist != null)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.FileNumberExist + " في " + isExist.KidneyCenter.ArabicName);


//        ////        if (bodyObject.NationalyId == 2)
//        ////        {
//        ////            isExist = db.Patients.Include(x => x.KidneyCenter).Where(x => x.Nid == bodyObject.NID && x.Status != 9).SingleOrDefault();
//        ////            if (isExist != null)
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.NIDExist + " في " + isExist.KidneyCenter.ArabicName);

//        ////        }
//        ////        else
//        ////        {
//        ////            isExist = db.Patients.Include(x => x.KidneyCenter).Where(x => x.Passport == bodyObject.Passport && x.Status != 9).SingleOrDefault();
//        ////            if (isExist != null)
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.PassportExist + " في " + isExist.KidneyCenter.ArabicName);
//        ////        }

//        ////        if (bodyObject.DateOfBirth.Year > DateTime.Now.Year)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.BirthYearMustBeEqualToOrLessThenCurrentYear);

//        ////        if (bodyObject.KidneyFailureDate.Year > DateTime.Now.Year)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.KidneyFailureDateMustBeEqualToOrLessThenCurrentYear);

//        ////        List<PatientSchedule> patientScheduleList = new List<PatientSchedule>();
//        ////        foreach (var item in bodyObject.Day)
//        ////        {
//        ////            PatientSchedule patientSchedule = new PatientSchedule();
//        ////            patientSchedule.Day = item;
//        ////            patientSchedule.CreatedBy = userId;
//        ////            patientSchedule.CreatedOn = DateTime.Now;
//        ////            patientSchedule.Status = 1;
//        ////            patientScheduleList.Add(patientSchedule);
//        ////        }

//        ////        List<PatientAttachments> patientAttachmentsList = new List<PatientAttachments>();
//        ////        foreach (var item in bodyObject.PatientAttachmentsList)
//        ////        {

//        ////            if (string.IsNullOrEmpty(item.fileBase64))
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.ErorFile);

//        ////            PatientAttachments patientAttachments = new PatientAttachments();
//        ////            patientAttachments.Name = item.Name;
//        ////            patientAttachments.Path = this.help.UploadFile(item.Name, item.Type, item.fileBase64);
//        ////            patientAttachments.CreatedOn = DateTime.Now;
//        ////            patientAttachments.CreatedBy = userId;
//        ////            patientAttachments.Status = 1;
//        ////            patientAttachmentsList.Add(patientAttachments);
//        ////        }

//        ////        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        ////        if (user == null)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        ////        Patients row = new Patients();
//        ////        row.CityId = bodyObject.CityId;
//        ////        if (user.UserType != 1)
//        ////        {
//        ////            row.KidneyCenterId = user.KidneyCentersId;
//        ////        }
//        ////        else
//        ////        {
//        ////            row.KidneyCenterId = bodyObject.KidneyCenterId;
//        ////        }

//        ////        if (user.Status == 1)
//        ////        {
//        ////            row.FilterId = bodyObject.FilterId;
//        ////        }
//        ////        row.Nid = bodyObject.NID;
//        ////        row.Passport = bodyObject.Passport;
//        ////        row.FileNumber = bodyObject.FileNumber;
//        ////        row.FirstName = bodyObject.FirstName;
//        ////        row.MiddleName = bodyObject.FatherName;
//        ////        row.GrandfatherName = bodyObject.GrandFatherName;
//        ////        row.LastName = bodyObject.LastName;
//        ////        row.Name = bodyObject.FirstName + " " + bodyObject.FatherName + " " + bodyObject.GrandFatherName + " " + bodyObject.LastName;
//        ////        row.Gender = bodyObject.Gender;
//        ////        row.DateOfBirth = bodyObject.DateOfBirth;
//        ////        row.NationalyId = bodyObject.NationalyId;
//        ////        row.Workplace = bodyObject.Workplace;
//        ////        row.MaritalStatus = bodyObject.MaritalStatus;
//        ////        row.Address = bodyObject.Address;
//        ////        row.BloodType = bodyObject.BloodType;
//        ////        row.ViralAssays = bodyObject.ViralAssays;
//        ////        row.KidneyFailureDate = bodyObject.KidneyFailureDate;
//        ////        row.KidneyFailureCause = bodyObject.KidneyFailureCause;
//        ////        row.Notes = bodyObject.Notes;

//        ////        if (patientScheduleList.Count > 0)
//        ////            row.PatientSchedule = patientScheduleList;

//        ////        if (patientAttachmentsList.Count > 0)
//        ////            row.PatientAttachments = patientAttachmentsList;

//        ////        row.CreatedBy = userId;
//        ////        row.CreatedOn = DateTime.Now;
//        ////        row.Status = bodyObject.Status;
//        ////        db.Patients.Add(row);
//        ////        db.SaveChanges();
//        ////        return Ok(BackMessages.SucessAddOperations);
//        ////    }
//        ////    catch (Exception e)
//        ////    {
//        ////        return StatusCode(500, e.Message);
//        ////    }
//        ////}


//        //[HttpPost("EditOld")]
//        //public IActionResult EditOld([FromBody] BodyObject bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);


//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.Patients.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        if (bodyObject.NationalyId == 2)
//        //        {
//        //            if (bodyObject.NID.ToString().Length != 12)
//        //            {
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.RongNID);
//        //            }

//        //            var firstNumbner = bodyObject.NID.ToString().FirstOrDefault();

//        //            if (firstNumbner != '1' && firstNumbner != '2')
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.MustStartWith1Or2);
//        //        }
//        //        else
//        //        {
//        //            if (string.IsNullOrWhiteSpace(bodyObject.Passport))
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.PassportEmpty);
//        //        }

//        //        if (bodyObject.BirthDate.Year > DateTime.Now.Year)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.BirthYearMustBeEqualToOrLessThenCurrentYear);

//        //        if (bodyObject.KidneyFailureDate.Year > DateTime.Now.Year)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.KidneyFailureDateMustBeEqualToOrLessThenCurrentYear);

//        //        if (string.IsNullOrWhiteSpace(bodyObject.Name) ||
//        //            string.IsNullOrWhiteSpace(bodyObject.MotharName) ||
//        //            string.IsNullOrWhiteSpace(bodyObject.GrandFatherName) ||
//        //            string.IsNullOrWhiteSpace(bodyObject.LastName))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);



//        //        var isExist = db.Patients.Where(x => x.FileNumber == bodyObject.FileNumber && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.FileNumberExist);

//        //        if (bodyObject.NationalyId == 2)
//        //        {
//        //            isExist = db.Patients.Where(x => x.Nid == bodyObject.NID && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//        //            if (isExist != null)
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.NIDExist);
//        //        }
//        //        else
//        //        {
//        //            isExist = db.Patients.Where(x => x.Passport == bodyObject.Passport && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
//        //            if (isExist != null)
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.PassportExist);
//        //        }

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);



//        //        if (user.UserType != 1)
//        //        {
//        //            Info.KidneyCenterId = user.KidneyCentersId;
//        //        }
//        //        else
//        //        {
//        //            Info.KidneyCenterId = bodyObject.KidneyCenterId;
//        //        }
//        //        Info.FilterId = bodyObject.FilterId;
//        //        Info.CityId = bodyObject.CityId;
//        //        Info.Nid = bodyObject.NID;
//        //        Info.Passport = bodyObject.Passport;
//        //        Info.FileNumber = bodyObject.FileNumber;
//        //        Info.FirstName = bodyObject.Name;
//        //        Info.MiddleName = bodyObject.MotharName;
//        //        Info.GrandfatherName = bodyObject.GrandFatherName;
//        //        Info.LastName = bodyObject.LastName;
//        //        Info.Name = bodyObject.Name + " " + bodyObject.MotharName + " " + bodyObject.GrandFatherName + " " + bodyObject.LastName;
//        //        Info.Gender = bodyObject.Gender;

//        //        if (bodyObject.BirthDate != null)
//        //            Info.DateOfBirth = bodyObject.BirthDate;

//        //        Info.NationalyId = bodyObject.NationalyId;
//        //        Info.Workplace = bodyObject.Workplace;
//        //        Info.MaritalStatus = bodyObject.MaritalStatus;
//        //        Info.Address = bodyObject.Address;
//        //        Info.BloodType = bodyObject.BloodType;
//        //        Info.ViralAssays = bodyObject.ViralAssays;
//        //        Info.KidneyFailureDate = bodyObject.KidneyFailureDate;
//        //        Info.KidneyFailureCause = bodyObject.KidneyFailureCause;
//        //        Info.Notes = bodyObject.Notes;
//        //        Info.Status = bodyObject.Status;
//        //        Info.ModifiedBy = userId;
//        //        Info.ModifiedOn = DateTime.Now;
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessEditOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}


//        //public class PatientImageObj
//        //{
//        //    public long? PatientId { get; set; }
//        //    public string Name { get; set; }
//        //    public string fileBase64 { get; set; }
//        //    public string Type { get; set; }
//        //}
//        //[DisableRequestSizeLimit]
//        //[HttpPost("ChangePatintImage")]
//        //public IActionResult ChangePatintImage([FromBody] PatientImageObj bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject.PatientId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (string.IsNullOrEmpty(bodyObject.fileBase64))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.ErorFile);

//        //        var Info = db.Patients.Where(x => x.Id == bodyObject.PatientId).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Image = this.help.UploadFile(bodyObject.Name, bodyObject.Type, bodyObject.fileBase64);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}






//        ////PatientAttachments Info
//        //[DisableRequestSizeLimit]
//        //[HttpPost("AddAttachments")]
//        //public IActionResult AddAttachments([FromBody] PatientAttachmentsObj bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject.Id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (string.IsNullOrEmpty(bodyObject.fileBase64))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.ErorFile);

//        //        PatientAttachments row = new PatientAttachments();
//        //        row.PatientId = bodyObject.PatientId;
//        //        row.Name = bodyObject.Name;
//        //        row.Path = this.help.UploadFile(bodyObject.Name, bodyObject.Type, bodyObject.fileBase64);
//        //        row.CreatedBy = userId;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.Status = 1;
//        //        db.PatientAttachments.Add(row);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[DisableRequestSizeLimit]
//        //[HttpPost("AddAttachmentsList")]
//        //public IActionResult AddAttachmentsList([FromBody] PatientAttachmentsObj[] bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject.Count() <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        foreach (var item in bodyObject)
//        //        {
//        //            if (string.IsNullOrEmpty(item.fileBase64))
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.ErorFile);

//        //            PatientAttachments row = new PatientAttachments();
//        //            row.PatientId = item.PatientId;
//        //            row.Name = item.Name;
//        //            row.Path = this.help.UploadFile(item.Name, item.Type, item.fileBase64);
//        //            row.CreatedOn = DateTime.Now;
//        //            row.Status = 1;
//        //            db.PatientAttachments.Add(row);
//        //        }

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetAttachments")]
//        //public IActionResult GetAttachments(long PatientId)
//        //{
//        //    try
//        //    {
//        //        //check IsAuthorized
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var count = db.PatientAttachments.Where(x => x.Status != 9 && x.PatientId == PatientId).Count();
//        //        var Info = db.PatientAttachments.Where(x => x.Status != 9 && x.PatientId == PatientId).Select(x => new
//        //        {
//        //            x.Id,
//        //            x.Path,
//        //            x.Name,
//        //            x.CreatedOn,
//        //            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //        }).OrderByDescending(x => x.CreatedOn).ToList();

//        //        return Ok(new { info = Info, count = count });
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/RemoveAttachments")]
//        //public IActionResult RemoveAttachments(long id)
//        //{
//        //    try
//        //    {
//        //        if (id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var Info = db.PatientAttachments.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();
//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Status = 9;
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}



//        //public class returnObject
//        //{
//        //    public long? FilterId { get; set; }
//        //    public string Filter { get; set; }
//        //    public string Device { get; set; }
//        //    public long? DeviceId { get; set; }
//        //    public string Company { get; set; }
//        //    public long? CompanyId { get; set; }
//        //}

//        ////PatientSchedule Info
//        //[HttpGet("GetPatientSchedule")]
//        //public IActionResult GetPatientSchedule(long PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        returnObject returnObject = new returnObject();

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientSchedule
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.PatientId == PatientId && x.Patient.KidneyCenterId == user.KidneyCentersId).Count();
//        //            var Info = db.PatientSchedule
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.PatientId == PatientId && x.Patient.KidneyCenterId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    x.Day,
//        //                    Filter = x.FilterId == null ? returnObject : db.Filters
//        //                                                .Include(k => k.Device)
//        //                                                .Include(k => k.Device.Company)
//        //                                                .Where(k => k.Id == x.FilterId)
//        //                                                .Select(k => new returnObject
//        //                                                {
//        //                                                    FilterId = x.FilterId,
//        //                                                    Filter = k.Name,
//        //                                                    Device = k.Device.Name,
//        //                                                    DeviceId = k.DeviceId,
//        //                                                    Company = k.Device.Company.Name,
//        //                                                    CompanyId = k.Device.CompanyId,
//        //                                                }).SingleOrDefault(),
//        //                    x.From,
//        //                    x.To,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.Day).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientSchedule.Where(x => x.Status != 9 && x.PatientId == PatientId).Count();
//        //            var Info = db.PatientSchedule.Where(x => x.Status != 9 && x.PatientId == PatientId).Select(x => new
//        //            {
//        //                x.Id,
//        //                x.Day,
//        //                Filter = x.FilterId == null ? returnObject : db.Filters
//        //                                                .Include(k => k.Device)
//        //                                                .Include(k => k.Device.Company)
//        //                                                .Where(k => k.Id == x.FilterId)
//        //                                                .Select(k => new returnObject
//        //                                                {
//        //                                                    FilterId = x.FilterId,
//        //                                                    Filter = k.Name,
//        //                                                    Device = k.Device.Name,
//        //                                                    DeviceId = k.DeviceId,
//        //                                                    Company = k.Device.Company.Name,
//        //                                                    CompanyId = k.Device.CompanyId,
//        //                                                }).SingleOrDefault(),
//        //                x.From,
//        //                x.To,
//        //                x.Status,
//        //                x.CreatedOn,
//        //                CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //            }).OrderByDescending(x => x.Day).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //public class FilterFullInfoObj
//        //{
//        //    public string Filter { get; set; }
//        //    public long? FilterId { get; set; }
//        //    public string Company { get; set; }
//        //    public long? CompanyId { get; set; }
//        //    public string Device { get; set; }
//        //    public long? DeviceId { get; set; }
//        //}

//        //public FilterFullInfoObj GetFilterInfo(long? FilterId)
//        //{
//        //    if (FilterId <= 0 || FilterId == null)
//        //    {
//        //        return null;
//        //    }
//        //    else
//        //    {
//        //        return db.Filters
//        //            .Include(x => x.Device)
//        //            .Include(x => x.Device.Company)
//        //            .Where(x => x.Id == FilterId)
//        //            .Select(x => new FilterFullInfoObj
//        //            {
//        //                FilterId = FilterId,
//        //                Filter = x.Name,
//        //                Device = x.Device.Name,
//        //                DeviceId = x.DeviceId,
//        //                Company = x.Device.Company.Name,
//        //                CompanyId = x.Device.CompanyId,
//        //            }).SingleOrDefault();
//        //    }
//        //}

//        //public class PatientScheduleObj
//        //{
//        //    public long PatintId { get; set; }
//        //    public long FilterId { get; set; }
//        //    public short Day { get; set; }
//        //    public DateTime From { get; set; }
//        //    public DateTime To { get; set; }
//        //}

//        //[HttpPost("AddPatientSchedule")]
//        //public IActionResult AddPatientSchedule([FromBody] PatientScheduleObj bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);


//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (bodyObject.FilterId > 0)
//        //        {
//        //            var Fitler = db.Filters.Where(x => x.Id == bodyObject.FilterId && x.Status != 9).SingleOrDefault();
//        //            if (Fitler == null)
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//        //        }

//        //        var isExist = db.PatientSchedule.Where(x => x.Day == bodyObject.Day && x.Status != 9 && x.PatientId == bodyObject.PatintId).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DayExist);

//        //        PatientSchedule row = new PatientSchedule();
//        //        row.PatientId = bodyObject.PatintId;
//        //        row.FilterId = bodyObject.FilterId;
//        //        row.From = bodyObject.From;
//        //        row.To = bodyObject.To;
//        //        row.Day = bodyObject.Day;
//        //        row.CreatedBy = userId;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.Status = 1;
//        //        db.PatientSchedule.Add(row);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //public class EditPatientScheduleObj
//        //{
//        //    public long PatientScheduleId { get; set; }
//        //    public long FilterId { get; set; }
//        //    public DateTime From { get; set; }
//        //    public DateTime To { get; set; }
//        //}
//        //[HttpPost("EditPatientSchedule")]
//        //public IActionResult EditPatientSchedule([FromBody] EditPatientScheduleObj[] bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);


//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        foreach (var item in bodyObject)
//        //        {
//        //            if (item.FilterId > 0)
//        //            {
//        //                var Fitler = db.Filters.Where(x => x.Id == item.FilterId && x.Status != 9).SingleOrDefault();
//        //                if (Fitler == null)
//        //                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
//        //            }

//        //            var row = db.PatientSchedule.Where(x => x.Id == item.PatientScheduleId).SingleOrDefault();
//        //            if (row == null)
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //            row.FilterId = item.FilterId;
//        //            row.From = item.From;
//        //            row.To = item.To;
//        //            row.ModifiedBy = userId;
//        //            row.ModifiedOn = DateTime.Now;
//        //        }

//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessEditOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/DeletePatientSchedule")]
//        //public IActionResult DeletePatientSchedule(long id)
//        //{
//        //    try
//        //    {
//        //        if (id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var Info = db.PatientSchedule.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        //var HaveChiled = db.Devices.Where(x => x.CompanyId == id && x.Status != 9).ToList();
//        //        //if (HaveChiled.Count > 0)
//        //        //    return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

//        //        Info.Status = 9;
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}





//        ////Chagne Center Info


//        //[HttpGet("GetCahngeCenterByHospital")]
//        //public IActionResult GetCahngeCenterByHospital(int pageNo, int pageSize, int Hospitalid)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientsChangeCenter.Where(x => x.Status != 9 && x.ToCenter == Hospitalid && (x.ToCenter == user.KidneyCentersId || x.FromCenter == user.KidneyCentersId)).Count();
//        //            var Info = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && (x.ToCenter == Hospitalid && x.ToCenter == user.KidneyCentersId || x.FromCenter == user.KidneyCentersId)).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patients = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        CurrentKidneyCenter = new
//        //                        {
//        //                            x.Patient.KidneyCenterId,
//        //                            x.Patient.KidneyCenter.ArabicName,
//        //                            x.Patient.KidneyCenter.EnglishName,
//        //                        }
//        //                    },
//        //                    AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                    x.FromCenter,
//        //                    x.FromName,
//        //                    x.ToCenter,
//        //                    x.ToName,
//        //                    x.AcceptedBy,
//        //                    x.AcceptedOn,
//        //                    x.RejectBy,
//        //                    x.RejectOn,
//        //                    x.RejectResone,
//        //                    x.Note,
//        //                    x.Level,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientsChangeCenter.Where(x => x.Status != 9 && x.ToCenter == Hospitalid).Count();
//        //            var Info = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.ToCenter == Hospitalid).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patients = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        CurrentKidneyCenter = new
//        //                        {
//        //                            x.Patient.KidneyCenterId,
//        //                            x.Patient.KidneyCenter.ArabicName,
//        //                            x.Patient.KidneyCenter.EnglishName,
//        //                        }
//        //                    },
//        //                    AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                    x.FromCenter,
//        //                    x.FromName,
//        //                    x.ToCenter,
//        //                    x.ToName,
//        //                    x.AcceptedBy,
//        //                    x.AcceptedOn,
//        //                    x.RejectBy,
//        //                    x.RejectOn,
//        //                    x.RejectResone,
//        //                    x.Note,
//        //                    x.Level,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}


//        //[HttpGet("GetCahngeCenterByLevel")]
//        //public IActionResult GetCahngeCenterByLevel(short Level)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientsChangeCenter.Where(x => x.Status != 9 && x.Level == Level
//        //            && (x.ToCenter == user.KidneyCentersId || x.FromCenter == user.KidneyCentersId)).Count();
//        //            var Info = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.Level == Level && (x.ToCenter == user.KidneyCentersId || x.FromCenter == user.KidneyCentersId)).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patients = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        CurrentKidneyCenter = new
//        //                        {
//        //                            x.Patient.KidneyCenterId,
//        //                            x.Patient.KidneyCenter.ArabicName,
//        //                            x.Patient.KidneyCenter.EnglishName,
//        //                        }
//        //                    },
//        //                    AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                    x.FromCenter,
//        //                    x.FromName,
//        //                    x.ToCenter,
//        //                    x.ToName,
//        //                    x.AcceptedBy,
//        //                    x.AcceptedOn,
//        //                    x.RejectBy,
//        //                    x.RejectOn,
//        //                    x.RejectResone,
//        //                    x.Note,
//        //                    x.Level,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientsChangeCenter.Where(x => x.Status != 9 && x.Level == Level).Count();
//        //            var Info = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.Level == Level).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patients = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        CurrentKidneyCenter = new
//        //                        {
//        //                            x.Patient.KidneyCenterId,
//        //                            x.Patient.KidneyCenter.ArabicName,
//        //                            x.Patient.KidneyCenter.EnglishName,
//        //                        }
//        //                    },
//        //                    AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                    x.FromCenter,
//        //                    x.FromName,
//        //                    x.ToCenter,
//        //                    x.ToName,
//        //                    x.AcceptedBy,
//        //                    x.AcceptedOn,
//        //                    x.RejectBy,
//        //                    x.RejectOn,
//        //                    x.RejectResone,
//        //                    x.Note,
//        //                    x.Level,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetCahngeCenterByFileNumber")]
//        //public IActionResult GetCahngeCenterByFileNumber(string FileNumber)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);



//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientsChangeCenter.Include(x => x.Patient).Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber
//        //            && (x.ToCenter == user.KidneyCentersId || x.FromCenter == user.KidneyCentersId)).Count();
//        //            var Info = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber && (x.ToCenter == user.KidneyCentersId || x.FromCenter == user.KidneyCentersId)).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patients = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        CurrentKidneyCenter = new
//        //                        {
//        //                            x.Patient.KidneyCenterId,
//        //                            x.Patient.KidneyCenter.ArabicName,
//        //                            x.Patient.KidneyCenter.EnglishName,
//        //                        }
//        //                    },
//        //                    AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                    x.FromCenter,
//        //                    x.FromName,
//        //                    x.ToCenter,
//        //                    x.ToName,
//        //                    x.AcceptedBy,
//        //                    x.AcceptedOn,
//        //                    x.RejectBy,
//        //                    x.RejectOn,
//        //                    x.RejectResone,
//        //                    x.Note,
//        //                    x.Level,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientsChangeCenter.Include(x => x.Patient).Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber).Count();
//        //            var Info = db.PatientsChangeCenter
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Patient.KidneyCenter)
//        //                .Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patients = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                        CurrentKidneyCenter = new
//        //                        {
//        //                            x.Patient.KidneyCenterId,
//        //                            x.Patient.KidneyCenter.ArabicName,
//        //                            x.Patient.KidneyCenter.EnglishName,
//        //                        }
//        //                    },
//        //                    AttachmentCount = x.PatientChangeCenterAttachments.Count(),
//        //                    x.FromCenter,
//        //                    x.FromName,
//        //                    x.ToCenter,
//        //                    x.ToName,
//        //                    x.AcceptedBy,
//        //                    x.AcceptedOn,
//        //                    x.RejectBy,
//        //                    x.RejectOn,
//        //                    x.RejectResone,
//        //                    x.Note,
//        //                    x.Level,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        //                }).OrderByDescending(x => x.CreatedOn).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}



//        ////public class ChangeAttachmentsObj
//        ////{
//        ////    public long? Id { get; set; }
//        ////    public long? PatientsChangeCenterId { get; set; }
//        ////    public string Name { get; set; }
//        ////    public string fileBase64 { get; set; }
//        ////    public string Type { get; set; }
//        ////}












//        ////public class FileObje
//        ////{
//        ////    public string fileName { get; set; }
//        ////    public string fileBase64 { get; set; }
//        ////}

//        ////public class BodyObjectFile
//        ////{
//        ////    public long PatientsChangeCenterId { get; set; }
//        ////    public FileObje[] fileList { get; set; }
//        ////}


//        //////Cahnge Attachmnet Info
//        ////[DisableRequestSizeLimit]
//        ////[HttpPost("AddChangeAttachments")]
//        ////public IActionResult AddChangeAttachments([FromBody] BodyObjectFile bodyObject)
//        ////{
//        ////    try
//        ////    {
//        ////        if (bodyObject==null)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        ////        var userId = this.help.GetCurrentUser(HttpContext);
//        ////        if (userId <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        ////        foreach (var item in bodyObject.fileList)
//        ////        {

//        ////            if (string.IsNullOrEmpty(item.fileName))
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);



//        ////            int index = item.fileName.LastIndexOf('.');
//        ////            if (index <= 0)
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        ////            string Type = item.fileName.Substring(index);
//        ////            string FileName = item.fileName.Substring(0, index);

//        ////            PatientChangeCenterAttachments row = new PatientChangeCenterAttachments();
//        ////            row.PatientsChangeCenterId = bodyObject.PatientsChangeCenterId;
//        ////            row.Name = FileName;
//        ////            row.Path = this.help.UploadFile(FileName, Type, item.fileBase64);
//        ////            row.CreatedBy = userId;
//        ////            row.CreatedOn = DateTime.Now;
//        ////            row.Status = 1;
//        ////            db.PatientChangeCenterAttachments.Add(row);


//        ////        }
//        ////        db.SaveChanges();
//        ////        return Ok(BackMessages.SucessAddOperations);
//        ////    }
//        ////    catch (Exception e)
//        ////    {
//        ////        return StatusCode(500, e.Message);
//        ////    }
//        ////}

//        ////[DisableRequestSizeLimit]
//        ////[HttpPost("AddChangeAttachmentsList")]
//        ////public IActionResult AddChangeAttachmentsList([FromBody] ChangeAttachmentsObj[] bodyObject)
//        ////{
//        ////    try
//        ////    {
//        ////        if (bodyObject.Count() <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        ////        foreach (var item in bodyObject)
//        ////        {
//        ////            if (string.IsNullOrEmpty(item.fileBase64))
//        ////                return StatusCode(BackMessages.StatusCode, BackMessages.ErorFile);

//        ////            PatientChangeCenterAttachments row = new PatientChangeCenterAttachments();
//        ////            row.PatientsChangeCenterId = item.PatientsChangeCenterId;
//        ////            row.Name = item.Name;
//        ////            row.Path = this.help.UploadFile(item.Name, item.Type, item.fileBase64);
//        ////            row.CreatedOn = DateTime.Now;
//        ////            row.Status = 1;
//        ////            db.PatientChangeCenterAttachments.Add(row);
//        ////        }

//        ////        db.SaveChanges();
//        ////        return Ok(BackMessages.SucessAddOperations);
//        ////    }
//        ////    catch (Exception e)
//        ////    {
//        ////        return StatusCode(500, e.Message);
//        ////    }
//        ////}

//        ////[HttpGet("GetChangeAttachments")]
//        ////public IActionResult GetChangeAttachments(long PatientsChangeCenterId)
//        ////{
//        ////    try
//        ////    {
//        ////        //check IsAuthorized
//        ////        var userId = this.help.GetCurrentUser(HttpContext);
//        ////        if (userId <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        ////        var count = db.PatientChangeCenterAttachments.Where(x => x.Status != 9 && x.PatientsChangeCenterId == PatientsChangeCenterId).Count();
//        ////        var Info = db.PatientChangeCenterAttachments.Where(x => x.Status != 9 && x.PatientsChangeCenterId == PatientsChangeCenterId).Select(x => new
//        ////        {
//        ////            x.Id,
//        ////            x.Path,
//        ////            x.Name,
//        ////            x.CreatedOn,
//        ////            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
//        ////        }).OrderByDescending(x => x.CreatedOn).ToList();

//        ////        return Ok(new { info = Info, count = count });
//        ////    }
//        ////    catch (Exception e)
//        ////    {
//        ////        return StatusCode(500, e.Message);
//        ////    }
//        ////}

//        ////[HttpPost("{Id}/RemoveCahngeAttachments")]
//        ////public IActionResult RemoveCahngeAttachments(long id)
//        ////{
//        ////    try
//        ////    {
//        ////        if (id <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        ////        var userId = this.help.GetCurrentUser(HttpContext);
//        ////        if (userId <= 0)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        ////        var Info = db.PatientChangeCenterAttachments.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();
//        ////        if (Info == null)
//        ////            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        ////        Info.Status = 9;
//        ////        db.SaveChanges();
//        ////        return Ok(BackMessages.SucessDeleteOperations);
//        ////    }
//        ////    catch (Exception e)
//        ////    {
//        ////        return StatusCode(500, e.Message);
//        ////    }
//        ////}




//        ////PatientAttendance Info
//        //[HttpGet("GetPatientAttendance")]
//        //public IActionResult GetPatientAttendance(int pageNo, int pageSize)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientAttendance.Where(x => x.Status != 9 && x.KidneyCentersId == user.KidneyCentersId).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.KidneyCentersId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientAttendance.Where(x => x.Status != 9).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetPatientAttendanceByFileNumber")]
//        //public IActionResult GetPatientAttendanceByFileNumber(int pageNo, int pageSize, string FileNumber)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {

//        //            int Count = db.PatientAttendance.Include(x => x.Patient).Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber && x.KidneyCentersId == user.KidneyCentersId).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber && x.KidneyCentersId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {

//        //            int Count = db.PatientAttendance.Include(x => x.Patient).Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.Patient.FileNumber == FileNumber).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
//        //            return Ok(new { info = Info, count = Count });
//        //        }




//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetPatientAttendanceByPatient")]
//        //public IActionResult GetPatientAttendanceByPatient(int pageNo, int pageSize, long Patentid)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientAttendance.Include(x => x.Patient).Where(x => x.Status != 9 && x.PatientId == Patentid && x.KidneyCentersId == user.KidneyCentersId).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.PatientId == Patentid && x.KidneyCentersId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientAttendance.Include(x => x.Patient).Where(x => x.Status != 9 && x.PatientId == Patentid).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.PatientId == Patentid).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpGet("GetPatientAttendanceByKidneyCenter")]
//        //public IActionResult GetPatientAttendanceByKidneyCenter(int pageNo, int pageSize, long KidneyCenterId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (user.UserType != 1)
//        //        {
//        //            int Count = db.PatientAttendance.Include(x => x.KidneyCenters).Where(x => x.Status != 9 && x.KidneyCentersId == KidneyCenterId && x.KidneyCentersId == user.KidneyCentersId).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.KidneyCentersId == KidneyCenterId && x.KidneyCentersId == user.KidneyCentersId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }
//        //        else
//        //        {
//        //            int Count = db.PatientAttendance.Include(x => x.KidneyCenters).Where(x => x.Status != 9 && x.KidneyCentersId == KidneyCenterId).Count();
//        //            var Info = db.PatientAttendance
//        //                .Include(x => x.Patient)
//        //                .Include(x => x.Filter)
//        //                .Include(x => x.Filter.Device)
//        //                .Include(x => x.Filter.Device.Company)
//        //                .Include(x => x.KidneyCenters)
//        //                .Include(x => x.KidneyCenters.Municipality)
//        //                .Where(x => x.Status != 9 && x.KidneyCentersId == KidneyCenterId).Select(x => new
//        //                {
//        //                    x.Id,
//        //                    Patient = new
//        //                    {
//        //                        x.PatientId,
//        //                        x.Patient.Name,
//        //                        x.Patient.FileNumber,
//        //                        x.Patient.Nid,
//        //                    },
//        //                    KidneyCenter = new
//        //                    {
//        //                        x.KidneyCentersId,
//        //                        x.KidneyCenters.ArabicName,
//        //                        x.KidneyCenters.EnglishName,
//        //                        Municipality = new
//        //                        {
//        //                            x.KidneyCenters.MunicipalityId,
//        //                            x.KidneyCenters.Municipality.Name,
//        //                        },
//        //                    },
//        //                    Filter = new
//        //                    {
//        //                        x.FilterId,
//        //                        x.Filter.Name,
//        //                        Device = new
//        //                        {
//        //                            x.Filter.DeviceId,
//        //                            x.Filter.Device.Name,
//        //                            Company = new
//        //                            {
//        //                                x.Filter.Device.CompanyId,
//        //                                x.Filter.Device.Company.Name,
//        //                            },
//        //                        },
//        //                    },
//        //                    x.UsedCount,
//        //                    x.Status,
//        //                    x.CreatedOn,
//        //                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


//        //            return Ok(new { info = Info, count = Count });
//        //        }


//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //public class AttendanceObj
//        //{
//        //    public long PatientId { get; set; }
//        //    public long FilterId { get; set; }
//        //    public long? KidneyCentersId { get; set; }
//        //    public int UsedCount { get; set; }
//        //}

//        //[HttpPost("AddPatientAttendance")]
//        //public IActionResult AddPatientAttendance([FromBody] AttendanceObj bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);


//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
//        //        if (user == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        var patint = db.Patients.Where(x => x.Id == bodyObject.PatientId).SingleOrDefault();
//        //        if (patint == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        if (user.UserType == 2 && user.KidneyCentersId != patint.KidneyCenterId)
//        //        {
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.DontHavePermisineToProsseger);
//        //        }
//        //        else if (user.UserType == 1)
//        //        {
//        //            if (bodyObject.KidneyCentersId <= 0 || bodyObject.KidneyCentersId == null)
//        //                return StatusCode(BackMessages.StatusCode, BackMessages.SelectCenter);
//        //        }

//        //        if (bodyObject.UsedCount > 100)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.CountRoung);


//        //        var IsExistPatinet = db.PatientAttendance.AsEnumerable().Where(x => x.PatientId == bodyObject.PatientId
//        //            && x.CreatedOn.GetValueOrDefault().Date == DateTime.Now.Date).FirstOrDefault();
//        //        if (IsExistPatinet != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PatientExistToday);


//        //        PatientAttendance row = new PatientAttendance();
//        //        row.PatientId = bodyObject.PatientId;
//        //        if (user.UserType == 1)
//        //        {
//        //            row.KidneyCentersId = bodyObject.KidneyCentersId;
//        //        }
//        //        else
//        //        {
//        //            row.KidneyCentersId = user.KidneyCentersId;
//        //        }

//        //        row.FilterId = bodyObject.FilterId;
//        //        row.UsedCount = bodyObject.UsedCount;
//        //        row.CreatedBy = userId;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.Status = 1;
//        //        db.PatientAttendance.Add(row);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/DeletePatientAttendance")]
//        //public IActionResult DeletePatientAttendance(long Id)
//        //{
//        //    try
//        //    {
//        //        if (Id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var Info = db.PatientAttendance.Where(x => x.Id == Id && x.Status != 9).SingleOrDefault();

//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Status = 9;
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}




//        ////Patient Phone Number
//        ////PatientSchedule Info
//        //[HttpGet("GetPatientPhoneNumbers")]
//        //public IActionResult GetPatientPhoneNumbers(long PatientId)
//        //{
//        //    try
//        //    {
//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        int Count = db.PatientPhoneNumbers.Where(x => x.Status != 9 && x.PatientId == PatientId).Count();
//        //        var Info = db.PatientPhoneNumbers.Where(x => x.Status != 9 && x.PatientId == PatientId).Select(x => new
//        //        {
//        //            x.Id,
//        //            x.Phone,
//        //            x.Relations,
//        //            x.Status,
//        //            x.CreatedOn,
//        //            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,

//        //        }).OrderByDescending(x => x.CreatedOn).ToList();


//        //        return Ok(new { info = Info, count = Count });



//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //public class PatientPhoneNumbersObj
//        //{
//        //    public long PatintId { get; set; }
//        //    public string Phone { get; set; }
//        //    public string Relations { get; set; }
//        //}

//        //[HttpPost("AddPatientPhoneNumbers")]
//        //public IActionResult AddPatientPhoneNumbers([FromBody] PatientPhoneNumbersObj bodyObject)
//        //{
//        //    try
//        //    {
//        //        if (bodyObject == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);


//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

//        //        if (!this.help.IsValidPhone(bodyObject.Phone))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PhoneNotValid);

//        //        if (string.IsNullOrEmpty(bodyObject.Relations))
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.RelationsIsEmpty);

//        //        var isExist = db.PatientPhoneNumbers.Where(x => x.PatientId == bodyObject.PatintId && x.Status != 9 && x.Phone == bodyObject.Phone).SingleOrDefault();
//        //        if (isExist != null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.PhoneExist);

//        //        PatientPhoneNumbers row = new PatientPhoneNumbers();
//        //        row.PatientId = bodyObject.PatintId;
//        //        row.Phone = bodyObject.Phone;
//        //        row.Relations = bodyObject.Relations;
//        //        row.CreatedBy = userId;
//        //        row.CreatedOn = DateTime.Now;
//        //        row.Status = 1;
//        //        db.PatientPhoneNumbers.Add(row);
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessAddOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}

//        //[HttpPost("{Id}/DeletePatientPhoneNumbers")]
//        //public IActionResult DeletePatientPhoneNumbers(long id)
//        //{
//        //    try
//        //    {
//        //        if (id <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

//        //        var userId = this.help.GetCurrentUser(HttpContext);
//        //        if (userId <= 0)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


//        //        var Info = db.PatientPhoneNumbers.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

//        //        if (Info == null)
//        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

//        //        Info.Status = 9;
//        //        db.SaveChanges();
//        //        return Ok(BackMessages.SucessDeleteOperations);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return StatusCode(500, e.Message);
//        //    }
//        //}




//    }
//}