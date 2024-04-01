﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using Vue.Models;
using ZXing;
using System.Collections.Generic;
using ZXing.QrCode;
using System.Drawing;
using ZXing.Common;
using System.Drawing.Imaging;

namespace Web.Services
{
    public class Helper
    {
        IConfiguration configuration;
        static readonly HttpClient client = new HttpClient();
        private readonly CompanyRegistryContext db;
        public Helper(IConfiguration iConfig, CompanyRegistryContext context)
        {
            configuration = iConfig;
            this.db = context;

        }

        public Helper()
        {
        }



        //public async System.Threading.Tasks.Task<bool> SentSmsAsync(string To, string TextMessage)
        //{
        //    try
        //    {
        //        string url = "";
        //        // almdar 
        //        if (To.Substring(0, 2) == "91")
        //        {
        //            var SMSCURL = configuration.GetSection("Links").GetSection("SMSCClient").Value;
        //            url = SMSCURL + string.Format("?smsc=smsc_smpp_almv1&username=MOEM&password=MOEM&from=MOELEARNING&to=218{0}&text={1}&charset=utf-8&coding=2", To, TextMessage);
        //        }

        //        if (To.Substring(0, 2) == "92" || To.Substring(0, 2) == "94")
        //        {
        //            var SMSCURL = configuration.GetSection("Links").GetSection("SMSCClient").Value;
        //            url = SMSCURL + string.Format("?username=MOE&password=MOE&from=218899055&to=218{0}&text={1}&charset=utf-8&coding=2", To, TextMessage);
        //        }


        //        HttpRequestMessage request = new HttpRequestMessage();
        //        request.RequestUri = new System.Uri(url);
        //        request.Method = HttpMethod.Get;
        //        HttpResponseMessage response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        string responseBody = await response.Content.ReadAsStringAsync();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public long GetCurrentUser(HttpContext HttpUser)
        {
            try
            {
                var user = HttpUser.User;
                if (user == null || user.Claims == null)
                {
                    return 0;
                }

                var claims = user.Claims.ToList();

                if (claims.Count == 0)
                {
                    //return 0;
                    return 1;
                }
                string userIdClaim = "";
                if (claims.Count > 1)
                {
                    userIdClaim = claims.Where(p => p.Type == "http://schemas.xmlsoap.org/ws/2022/10/identity/claims/Id").ToList()[0].Value;
                }
                else
                {
                    userIdClaim = claims.Where(p => p.Type == "http://schemas.xmlsoap.org/ws/2022/10/identity/claims/Id").SingleOrDefault().Value;
                }


                long userId = Convert.ToInt64(userIdClaim);


                return Convert.ToInt64(userId);
            }
            catch (Exception)
            {
                return -999;
            }
        }


        public string RandomNumber(int size)
        {
            string PlainText = "";
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                int rInt = r.Next(0, 9);
                PlainText = PlainText + rInt;
            }
            return PlainText;
        }

        public string GenreateEmail(string FirstName = "Ahmed", string LastName = "Tareck", string host = "sub-DNS")
        {
            try
            {
                if (FirstName == null || LastName == null)
                    //return NotFound("خــطأ : الرجاء التحقق من البيانات");
                    return null;

                if (FirstName.Length < 3)
                    FirstName += GenerateRandomNo().ToString();

                if (LastName.Length < 3)
                    LastName += GenerateRandomNo().ToString();

                FirstName = FirstName.Replace(" ", "");
                LastName = LastName.Replace(" ", "");


                string UserName = FirstName.Substring(0, 1);
                UserName += LastName.Substring(0, 3);
                UserName += GenerateRandomNo().ToString();
                UserName += "@" + host;
                UserName += ".dl.edu.ly";

                return UserName;
                //return Ok(new { UserName});

            }
            catch (Exception ex)
            {
                return null;
                //return StatusCode(500, ex.Message);
            }
        }

        public string GenreatePass()
        {
            try
            {
                return "Moh" + GenerateRandomNo().ToString();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public int GenerateRandomNoBarCode()
        {
            int _min = 10000000;
            int _max = 99999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }


        public bool SendEmail(string email, string massege)
        {
            try
            {

                //var SMS = SentSmsAsync("927294572", massege);
                //SMS.Wait();
                //if (!SMS.Result)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}

                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("No-replay@moe.gov.ly");

                //mail.From = new MailAddress("moe.gov.ly");

                //mail.To.Add(email);
                //mail.Subject = "Test Email";
                //mail.Body = massege;

                ////SmtpServer.Host = "No-replay@moe.gov.ly";
                ////SmtpServer.UseDefaultCredentials = true;
                //SmtpServer.UseDefaultCredentials = true;
                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("No-replay@moe.gov.ly", "asd123@123456789dl");
                //SmtpServer.EnableSsl = false;
                //SmtpServer.Send(mail);



                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("No-replay@moe.gov.ly");

                mail.To.Add(email);

                mail.Subject = "وزارة التعليم - تنويه";

                mail.Body = massege;

                mail.IsBodyHtml = true;

                var smtp = new SmtpClient("moe.gov.ly")
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("No-replay@moe.gov.ly", "asd123@123456789dl"),
                    Port = 587,
                    EnableSsl = false
                };
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string GetDefaultImage()
        {
            return "iVBORw0KGgoAAAANSUhEUgAAASwAAAEsCAYAAAB5fY51AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAK6wAACusBgosNWgAAABZ0RVh0Q3JlYXRpb24gVGltZQAwMS8wOS8xNazTZpAAAAAcdEVYdFNvZnR3YXJlAEFkb2JlIEZpcmV3b3JrcyBDUzbovLKMAAAgAElEQVR4nOx9eZRV1ZX + d + 4bah4pLGbRYhKQoSagUBE1GElQUKOCEhNNJ3FI7E7an7rS6ZjVnaUmadOm05oBO9FoOu2QaByIU5YaFYUqVFCDoEJkhqImanqv3rvn98erc + u8++67d + 9b7xWF1l4rK1K177mnzj1nnz18e28hpcQIjZCi2tpamKZpAAgACAIICSHyABQKIQqklIVSyiLDMAoB5AIolFLmAcgTQggppSGECMjExooJIfqklDEhRK9pmhHDMHoARKWUR4UQ7UKIo1LKLgBdpmlGAoFATErZByAOQDY2Nh6bhRihYUliRGB9 + mjevPlCCBEIBIwQgDwpZYmUskwIUSqEKJdSFgIoAJAPIFcIERiCaUkl2AD0AugE0C6lbBVCtAJoB9CR + J3ZZxgyvnHjmyOb91NGIwLrE041NTWGlDIMoBjAKMMwKgE5AUAZIAoB5ACAlBJCiGM5VQpJABFAdgqBFinFx1LKQ1LKFsMwjkop + 5qamsxjPckRyh6NCKxPENXW1goAIQBFACqllFUARgshiqWUpQl5JKDkEvXT++HPBq / TM / 371xRCtEkpj0opDwuBDwzDOASIo6Yp + 5qaRszKTwqNCKzjmObPn41gsCDXMGRZPG5OBjAFQDkS5lzYzq9rUG7fPVXTkv3PpJ9L8jPSUwgN8HN4aeP3C + YIgKNSylYAOwD83TTNtu7u7si2bdvcXzhCw5ZGBNZxRvPnzw8EAoEyAJOEEDMAjAFkvpQIuj1nP / RUgUXdH + oZCj9nfM68PebSB6AbwF4p5fsAdhuG0bZp06YRE / I4ohGBdRxQbW1tDoDxAGYAmCiEKIfme3KjDB54z / GHsbCykwTQI4RoEQL7hMC2WMzc09TUFPV8wQgdUxoRWMOQampqhJQybBjGBCllLYBxQohCJKAGQyJQuGNTnsmmcOPy2 + Yel1IeBbBPSrnJNM29gUAgOgKpGH40IrCGCS1cuADRaF8OgAlCiBoAJwkhcgEknayh0FC4ZmA258LlzQC / lFJ2CSE + NE28JWV8b0FBXvSVV17zHHOEsk8jAusYU0PDgmAsFh8Tj8tqAKcIIfKcIAafIG1m2AgrL14pASHQLYR4X0qzMRaL73 / rrbdGfF7HkEYE1jGguXPnIhwOlwgh5kopZ0ppjrEpUin0aTTtjvXcFQwEAEzThJRytxDiHSnluy0tLZ27du3ynN8IZZZGBNYQ0ty5c41AIDAxEAgsBjBBSpnv9cynUfOhPHOMI5kdAPbG4 / HX4nFz75Ytb49oXUNEIwJrCKg / yncKgAVCiBOklJ6pLoPRHLj8w0lL4gifY23qSiljhiEOAdgohPG3jRs3RjxfMEKDohGBlSWqra0FEojzOgCzAZSJ / tPwadKUjqfAAJdf45UAWqSUbwNoAtA1EmHMDo0IrAxTbW2tkFIWCSHORAI3lQ / 4P8TDSUOhjD9cNTfKMxmay1EA20zT / KthGEcbGxtHDlgGaURgZYj68 / jKAJwNYBoSOX0jBz5Dc + GOf6zXUUrZJ4TYJoR4wTCM9jfeeGPkoGWARgTWIGncuHEYN27cKABLkTD9kmg4 + X6Gyszkjp9N4XkMHPJ2XlNK + Q6AlxobG4 + QJjNCaWlEYA2CamtrSwAsATAX / Sh0IPuHncv / SdHyhsvY1Gds / FHTNLdIKV / evHlzh + fLRsiRRgSWD5o7d25eKBRaAmAO + n1UOh3jkHtaXu74TrxCCIRCIeTk5CAUCkEIYf0sGAwiEAjAMAwACexSPB5HPB6HaZqIxWKIx + Po6 + tDX18fotEoGbqQibm7jT + E69glpdwihHipsbGx13OQEUqiEYHFoOrq + SFA1AghGoQQxen4jlcNJS8vD / n5 + cjNzcWoUaNQWVmJsrIy5Ofno7CwEEVFRSgqKkJpaSmKi4sRDochBBAIBBEMBiGEgGEYSfPrB1wCAKLRKKLRKCKRCLq6utDZ2Yn29nYcPXoUnZ2d6O7uRmtrKw4cOICWlhb09vaiu7sbPT09nn8n92 / NttapnnHhbZdSvhyPx99 + 6623YqQBR2hEYFGovr5emKY5BcC5UsoK + 2bXaTg6tXV + wzAQCARQVFSEyZMnY8yYMdb / Ro0ahVGjRqGkpASlpaUIh8OWxqS0pmyREmyxWAzRaBTt7e1obW1Fa2srWlpasG / fPuzbtw + HDh3Crl27cPToUUtzO45NXgnggBDiOSC + c9OmzSOH0YNGBJYLNTQ0IB43S6U0l8fj8anCTVJheAqr3NxcFBYWYty4cZgyZQqmTJmCsWPHYtSoURg / fjyKi9MqimlJmXNCCOTm5roKMyklIpEIYrEYQqFQv1bmnobkNlZHRwf27t2L1tZW7N +/ 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";
        }

        public string GetAttachmentType(string attach)
        {
            return attach.Substring(attach.LastIndexOf('.'));
        }


        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (Phone.Length < 9)
                    return false;

                Phone = Phone.Substring(Phone.Length - 9);
                if (Phone.Substring(0, 2) != "91" && Phone.Substring(0, 2) != "92" && Phone.Substring(0, 2) != "93" && Phone.Substring(0, 2) != "94")
                    return false;

                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public bool IsValidNID(string NID)
        {
            try
            {
                if (NID.Length != 12)
                    return false;

                //string Check = NID.Substring(0, 3);

                //if(Check!="119" && Check!="219" && Check!="120" && Check!="220")
                //    return false;

                var firstNumbner = NID.ToString().FirstOrDefault();
                if (firstNumbner != '1' && firstNumbner != '2')
                    return false;

                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public bool IsCorrectPassword(string Password, string ConfirmPassword)
        {
            try
            {
                return Password.Equals(ConfirmPassword);
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public string UploadFile(string AttachmentName, string Type, string base64)
        {

            //String path = @"~\\Clintapp\\public\\Uploads";

            string path = Path.Combine(Directory.GetCurrentDirectory(), "clientapp", "dist", "Uploads");


            bool exists = System.IO.Directory.Exists(path);
            if (!exists)
                System.IO.Directory.CreateDirectory(path);

            Guid guid = Guid.NewGuid();
            AttachmentName = AttachmentName + "--" + guid;

            string FileName = AttachmentName + Type;

            String WebPath = @"/Uploads/";

            byte[] filebyte = Convert.FromBase64String(base64.Substring(base64.IndexOf(",") + 1));

            MemoryStream streams = new MemoryStream(filebyte);

            IFormFile file = new FormFile(streams, 0, streams.Length, "Attachments", FileName);

            string fullpath = Path.Combine(path, file.FileName);
            using (var fileStream = new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return WebPath + FileName;
        }

        public string UploadFile(string AttachmentName, string base64)
        {

            //String path = @"~\\Clintapp\\public\\Uploads";

            string path = Path.Combine(Directory.GetCurrentDirectory(), "clientapp", "dist", "Uploads");


            bool exists = System.IO.Directory.Exists(path);
            if (!exists)
                System.IO.Directory.CreateDirectory(path);

            Guid guid = Guid.NewGuid();
            AttachmentName = guid + "--" + AttachmentName;

            string FileName = AttachmentName;

            String WebPath = @"/Uploads/";

            byte[] filebyte = Convert.FromBase64String(base64.Substring(base64.IndexOf(",") + 1));

            MemoryStream streams = new MemoryStream(filebyte);

            IFormFile file = new FormFile(streams, 0, streams.Length, "Attachments", FileName);

            string fullpath = Path.Combine(path, file.FileName);
            using (var fileStream = new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return WebPath + FileName;
        }


        public string GenerateQRCode(string content)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "clientapp", "dist", "Uploads");

            bool exists = System.IO.Directory.Exists(path);
            if (!exists)
                System.IO.Directory.CreateDirectory(path);
            // Create a blank image
            int width = 400;
            int height = 200;
            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);

            // Generate the barcode
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions
            {
                Width = 600,
                Height = 200
            };
            barcodeWriter.Format = BarcodeFormat.CODE_128;
            barcodeWriter.Options = encodingOptions;
            Bitmap barcodeBitmap = barcodeWriter.Write(content);

            // Overlay the barcode onto the image
            int x = (width - barcodeBitmap.Width) / 2;
            int y = (height - barcodeBitmap.Height) / 2;
            graphics.DrawImage(barcodeBitmap, x, y);

            // Create the directory if it doesn't exist
            Directory.CreateDirectory(path);

            // Save the image to a file with the barcode content as the name
            string fileName = $"{content}.png";
            string filePath = Path.Combine(path, fileName);
            image.Save(filePath, ImageFormat.Png);

            // Clean up resources
            graphics.Dispose();
            image.Dispose();
            barcodeBitmap.Dispose();


            return @"/Uploads/" + content + ".png";

        }




        public enum TransactionsType
        {
            Add,
            Edit,
            Delete,
            CahngeStatus,
            Accept,
            Reject,
            Other,
        }


        public class TransactionsObject
        {
            public TransactionsType Operations { get; set; }
            public long? ItemId { get; set; }
            public string Descriptions { get; set; }
            public string Controller { get; set; }
            public string OldObject { get; set; }
            public string NewObject { get; set; }
            public long CreatedBy { get; set; }
        }
        public void WriteTransactions(TransactionsObject bodyObject)
        {

            Transactions row = new Transactions();
            row.Operations = bodyObject.Operations.ToString();
            row.ItemId = bodyObject.ItemId;
            row.Descriptions = bodyObject.Descriptions;
            row.Controller = bodyObject.Controller;
            row.OldObject = bodyObject.OldObject;
            row.NewObject = bodyObject.NewObject;
            row.CreatedOn = DateTime.Now;
            row.CreatedBy = bodyObject.CreatedBy;
            db.Transactions.Add(row);
        }

        public DateTime ParesStringToDateOnly(string datetime)
        {
            return DateTime.ParseExact(datetime, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }



        //DateTime From = DateTime.ParseExact(bodyObject.From, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime.Compare(x.CreatedOn.Value.Date, DateTime.Now.Date) == 0
        //DateTime To = DateTime.ParseExact(bodyObject.To, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //JsonConvert.SerializeObject(row);
        //JsonConvert.SerializeObject(row, Formatting.None,new JsonSerializerSettings(){ReferenceLoopHandling = ReferenceLoopHandling.Ignore });


        //Get By Date
        //Html
        //<el-date-picker v-model="byDate"
        //                type="date"
        //                @change="GetInfo()"
        //                placeholder="التاريخ">
        //</el-date-picker>
        //Java script
        //byDate: [],
        //if (this.byDate)
        //        this.byDate = this.ChangeDate(this.byDate);

        //ChangeDate(date) {
        //        if (date === null) {
        //            return "فارغ";
        //        }
        //        return moment(date).format("YYYY-MM-DD");
        //},




        public static class BackMessages
        {
            public static int StatusCode = 401;
            public static string EmptyBodyObject = "خطاء في اسال البيانات الرجاء اعادة المحاولة";
            public static string NotAuthorized = "الرجاء الـتأكد من أنك قمت بتسجيل الدخول";
            public static string NameExist = "الاسم موجود مسبقاً الرجاء اعادة الادخال";
            public static string FileNameExist = "اسم الملف موجود مسبقاً الرجاء اعادة الادخال";
            public static string DayExist = "اليوم موجود مسبقاً الرجاء اعادة الادخال";
            public static string DeviceExist = "الجهاز موجود مسبقاً الرجاء اعادة الادخال";
            public static string PhoneExist = "رقم الهاتف موجود مسبقاً الرجاء اعادة الادخال";
            public static string PhoneExist1 = "رقم الهاتف موجود مسبقاً ";
            public static string EmailExist = "البريد الإلكتروني موجود مسبقاً الرجاء اعادة الادخال";
            public static string NIDExist = "الرقم الوطني موجود مسبقا";
            public static string FileNumberExist = "رقم الملف موجود مسبقا";
            public static string FileNumberEmpty = "الرجاء ادخال الملف ";
            public static string PassportExist = "رقم جواز السفر موجود مسبقا";
            public static string NameEmpty = "الرجاء كتابة الاسم بطريقة صحيحة";
            public static string PassportEmpty = "الرجاء كتابة رقم جواز السفر بطريقة صحيحة";
            public static string NationalityEmpty = "الرجاء كتابة  الجنسية  ";
            public static string AddressEmpty = "الرجاء إدخال  العنوان  ";
            public static string WorkplaceEmpty = "الرجاء إدخال  مكان العمل  ";
            public static string DayEmpty = "الرجاء تحديد الايام  ";
            public static string PhoneEmpty = "الرجاء كتابة رقم الهاتف  ";
            public static string CheckPhone = "الرجاء كتابة رقم الهاتف بطريقة صحيحة  ";
            public static string CheckPhoneStartWith = "يجب ان يكون الهاتف يبدأ ب (91,92,94) ليبيانا او المدار !!";
            public static string PermissioneEmpty = "الرجاء تحديد الصلاحيات للمستخدم";
            public static string AttachmentEmpty = "الرجاء ارفاق المرفقات المطلوبة  ";
            public static string MuncitpitlyEmpty = "الرجاء ادخال  المركز  ";
            public static string UserNotExist = "المستخدم غير موجود";
            public static string KidneyCenterEmpty = "الرجاء إختيار المركز الصحي";
            public static string BloodTypeEmpty = "الرجاء إختيار فصيلة الدم";
            public static string ViralAssaysEmpty = "الرجاء إختيار التحليل الفيروسي";
            //public static string KidneyFailureResoneEmpty = "الرجاء كتابة سبب الاصابة";
            public static string FilterNotFound = "لم يتم العتور على الفلتر ";
            public static string TimeNotValid = "الرجاء التأكد من التوقيت ";
            public static string PatientStatusNotValid = "يجب ان تكون حالة المريض مستمر حتي تتمكن من التعديل على جدول المواعيد ";
            public static string ChangeResoneEmpty = "يجب كتابة سبب الانتقال ";
            public static string PatientCometodyBefore = "المريض قام بعملية الغسيل مسبقا في هذا اليوم   ";
            public static string PatientStatusNotAllowd = "حالة المريض لا تسمح بالغسيل الرجاء تعديل حالة المريض   ";
            public static string DeviceStatusPercentage = "الرجاء ادخال حالة الجهاز";


            public static string NotFound = "لم يتم العتور علي السجل الرجاء التأكد من البيانات";
            public static string HasChild = "لا يمكن مسح العنصر حتي يتم مسح العناصر المرتبطة به اولا ";


            public static string SucessAddOperations = "تمت عملية الاضافة بنجاح";
            public static string SucessChangeStatusOperations = "تم تغير حالة الحساب بنجاح";
            public static string SucessEditOperations = "تمت عملية التعديل بنجاح";
            public static string SucessDeleteOperations = "تمت عملية الحذف بنجاح";
            public static string SucessResetOperations = "تمت عملية التهيئة بنجاح";


            public static int NotAuthroizedStatusCode = 401;
            public static string EnterEmailandUserName = "الرجاء ادخال البريد الالكتروني او اسم الدخول";
            public static string NotAllowed = "انت لست مخول لاتمام هه العملية  ";
            public static string DontHavePermisine = "ليس لديك صلاحية للوصول الي النظام   ";
            public static string DontHavePermisineToProsseger = "ليس لديك صلاحية للقيام بهذه العملية   ";
            public static string YourAcountisBlocked = "لايمكنك الدخول للنظام: تم ايقافك   ";

            public static string userPermissone = "الرجاء  تحديد نوع المستخدم";
            public static string RongNID = "  الرجاء إدخال الرقم الوطني بطريقة الصحيحه";
            public static string RongWitnessNID = "  الرجاء إدخال الرقم الوطني للشاهد الأول بطريقة الصحيحه";
            public static string RongWitnessNID1 = "  الرجاء إدخال الرقم الوطني للشاهد التاني بطريقة الصحيحه";
            public static string WitnessNIDExist = " الرقم الوطني للشاهد الاول والتاني متساوين وهذا يعتبر خطاء في الإدخال";
            public static string WitnessNIDExistNID = " الرقم الوطني للشاهد هوا نفس الرقم الخاص بالاسير وهذا يعتبر خطاء في الإدخال";
            public static string MustStartWith1Or2 = "يجب ان يبدأ الرقم الوطني برقم 1 أو 2";
            public static string InvoiceIdEmpty = "رقم الفاتورة موجود مسبقا";
            public static string InvoiceIdExist = "رقم الفاتورة موجود مسبقا";
            public static string RecordExist = "الحقل موجود مسبقا الرجاء اعادة الادخال";
            public static string LoginNameExist = "اسم الدخول موجود مسبقاً الرجاء اعادة الادخال";
            public static string WhatsPhoneExist = "رقم الواتس اب موجود مسبقا";
            public static string LotExist = "المركبة موجودة مسبقا";
            public static string LotSolidExist = "تم شراء المركبة مسبقا";
            public static string WhatsExist = "رقم الواتس اب موجود مسبقا";
            public static string IdEmpty = "الرجاءإدخال المعرف";
            public static string IdExist = "المعرف موجود مسبقا";
            public static string RequestExist = "الطلب موجود مسبقا";
            public static string UserOfficeIdEmpty = "لم يتم تنسيبك لفرع معين يرجي مراجعة الادارة ";
            public static string WhereHouseEmpty = "الرجاء اختيار الموقع ";
            public static string NIDEmpty = "الرجاء ادخال الرقم الوطني";
            public static string WitnessNIDEmpty = "الرجاء ادخال الرقم الوطني للشاهد الأول";
            public static string WitnessNIDEmpty1 = "الرجاء ادخال الرقم الوطني للشاهد التاني";
            public static string KidneyFailureResoneEmpty = "الرجاء ادخال الرقم الوطني";
            public static string RelationsIsEmpty = "الرجاء ادخال صلة القرابة";
            public static string AcountNumberEmpty = "الرجاء ادخال رقم الحساب المصرفي ";
            public static string DateNotCorrect = "الرجاء التأكد من التواريخ  ";
            public static string BirthYearMustBeEqualToOrLessThenCurrentYear = $"يجب ان تكون سنة الميلاد تساوي او اقل من {DateTime.Now.Year}";
            public static string KidneyFailureDateMustBeEqualToOrLessThenCurrentYear = $"يجب ان تكون سنة تاريخ بداية   تساوي او اقل من {DateTime.Now.Year}";

            public static string OrderExist = "الطلبية موجودة مسبقا";
            public static string CdOrderIdExist = "الطلبية موجودة مسبقا";
            public static string OrderURLExist = "لقد قمت بالطلب من نفس الرابط مسبقا";
            public static string ShippingInPlanExist = "لقد قمت باضافة الشحنة الي الخطة مسبقا";




            public static string EmailNotValid = "البريد الالكتروني غير صحيح";
            public static string PhoneNotValid = "الرجاء إدخال الهـاتف بطريقة الصحيحة";
            public static string ConfirmPassword = "الرجاء تاكد من تطابق كلة المرور";








            public static string MessageEmpty = "الرجاء ادخال نص الرسالة";
            public static string CannotDeleteWithWalletValue = "لا يمكن مسح الزبون وهوا يمتلك قيمة في المحفظة";
            public static string URLEmpty = "الرجاء كتابة الرابط";
            public static string ContryEmpty = "الرجاء اختيار المدينة";
            public static string WebSiteEmpty = "الرجاء اختيار الموقع";
            public static string CatogaryEmpty = "الرجاء اختيار التصنيف";
            public static string EmailEmpty = "الرجاء كتابة البريد الالكتروني بطريقة صحيحة";
            public static string GenderEmpty = "الرجاء ادخال الجنس (ذكر - انثي)";
            public static string PermissionsEmpty = "الرجاء تحديد الصلاحيات للمستخدم";
            public static string DateEmpty = "الرجاء ادخال التاريخ";
            public static string FreightTypeEmpty = "الرجاء ادخال نوع الشحن بري او بحري";
            public static string OfficeIdEmpty = "الرجاء اختيار المكتب";
            public static string BarCodeEmpty = "الرجاء ادخال الباركود ";
            public static string OrderListEmpty = "الرجاء ادخال الفواتير";
            public static string ShippingListEmpty = "الرجاء اختيار الشحنات";

            public static string FeesEmpty = "الرجاء ادخال العمولة";
            public static string NoteEmpty = "الرجاء ادخال ملاحظة";
            public static string LocationNameEmpty = "الرجاء ادخال المكان";
            public static string LocationNameNotFound = "هذه المنطقة غير متوفرة حاليا في نظام سيتم اضافتها قريبا";
            public static string FastFeesEmpty = "الرجاء ادخال قيمة الحوالة السريعة";
            public static string MadeBy = "الرجاء ادخال منفد الحوالة";
            public static string ValueEmpty = "الرجاء ادخال القيمة";
            public static string WalletEmpty = "يجب تعبئة المحفظة اولا";
            public static string WalletNotEnife = "قيمة الحافظة لاتغطي القيمة المطلوبة";
            public static string BidPriceEmpty = "الرجاء ادخال سعر المزايدة";
            public static string PriceEmpty = "الرجاء ادخال السعر";
            public static string QuantityEmpty = "الرجاء ادخال الكمية";
            public static string VehicleTypeEmpty = "الرجاء ادخال نوع المركبة";
            public static string VehicleSizeEmpty = "الرجاء ادخال حجم المركبة";
            public static string FrefhtPriceEmpty = "الرجاء التاكد من البيانات لم نتمكن من الوصول لاسعار الشحن البري والبحري";
            public static string Model = "الرجاء ادخال الموديل";
            public static string BiddnotSame = "يجب ان تكون جميع السيارات تملك نفس رقم الحساب  ";
            public static string OfficenotSame = "يجب ان تكون جميع السيارات من نفس المكتب  ";
            public static string EmptyTargetSystem = "الرجاء اختيار النظام المستهدف  ";
            public static string BidderNotCorrect = "خطاء في رقم الحساب   ";
            public static string BidSoled = "تم بيع المركبة لا يمكن طلب المزايدة  ";
            public static string RemittanceEmpty = "يجب ادخال الحوالة  ";
            public static string CarInfoIsEmpty = "يجب ادخال بيانات المركبة  ";
            public static string ClintEmpty = "الرجاء اختيار العميل   ";
            public static string WebInfoIsEmpty = "يجب ادخال بيانات الطلب  ";
            public static string BidRejected = "تم رفض السيارة مسبقا الرجاء التاكد من البيانات  ";
            public static string CdSuggestedPriceEmpty = "الرجاء ادخال السعر المتوقع ";
            public static string InlandPriceEmpty = "الرجاء ادخال سعر الشحن البري ";
            public static string BookingNumberandContinarNumberEmpty = "الرجاء ادخال رقم الحاوية او الرقم التسلسلي ";
            public static string ProviderIdEmpty = "الرجاء اختيار المزود ";
            public static string WalletTrasaryEmpty = "لا توجد قيمة في الخزينة ";
            public static string UsedCountNotCorrect = "الرجاء التأكد من الكمية  ";


            public static string EnterPassword = "الرجاء ادخال كلمة المرور";
            public static string NotActiveAcount = "حسابك غير مفعل";
            public static string BlockedAcount = "حسابك غير مفعل";
            public static string RongPasswordandEmail = "الرجاء التاكد من البريد الالكتروني وكلمة المرور";
            public static string PasswordRong = "كلمة المرور غير صحيحة";
            public static string PasswordLenght = "كلمة المرور يجب أن تتكون من 8 أحرف على الأقل";
            public static string EnterCurrentPass = "الرجاء ادخال كلمة المرور الحالية";
            public static string PassNotMatched = "كلمة المرور الحالية الذي أدخلتها غير صحيحة";
            public static string NoRecivedValue = "لم يتم استلام اي قيمة من قبل الخزينة";
            public static string BackBigestthenRecivedValue = "القيمة المعادة اكبر من القيمة المستلمة";
            public static string YouDontHaveCenter = "لم يتم تنسيبك الى اي مركز الرجاء الاتصال بمدير النظام لإتمام عملية تفعيل الحساب";
            public static string SelectCenter = "الرجاء تحديد مركز الكلى";
            public static string CountRoung = "الرجاء التأكد من الكمية";
            public static string PatientExistToday = "قام المريض بالغسيل مسبقا";

            public static string NotCorrectLevel = "الرجاء التأكد من حالة الحوالة";
            public static string CanotDeleteBeforeLevel = "لا يمكن حذف الحوالة حتي تكون حالتها مبدئية ";
            public static string Errorwhilelogout = "error while logout ";



            public static string ErorFile = "الرجاء التأكد من المرفقات";




            public static string SucessSaveOperations = "تمت عملية الحفظ بنجاح";
            public static string SucessBlockOperations = "تم ايقاف المستخدم بنجاح";
            public static string SucessActiveOperations = "تم تفعيل المستخدم بنجاح";
            public static string SucessAcceptedRequest = "تمت الموافقة علي الطلب";
            public static string SucessRejectRequest = "تم رفض الطلب";
            public static string SuccessChangeStatus = "تم تغير الحالة بنجاح";
            public static string SucessTransfer = "تمت عملية التحويل بجاح";
            public static string SucessRequestOperations = "تمت عملية الطلب بجاح";
            public static string SucessSentMessage = "تم ارسال الرسالة بنجاح";
            public static string CahngeLevels = "تمت العملية بنجاح";
            public static string Active = "تمت عملية تفعيل الشركة بنجاح";


        }




    }
}
