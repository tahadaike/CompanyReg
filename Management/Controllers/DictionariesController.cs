using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using Vue.Models;
using Web.Services;
using static Web.Services.Helper;

namespace Management.Controllers
{
    [Produces("application/json")]
    [Route("api/admin/Dictionaries")]
    public class CitiesController : Controller
    {
        private Helper help;

        private readonly CompanyRegistryContext db;

        public CitiesController(CompanyRegistryContext context, IConfiguration iConfig)
        {
            this.db = context;
            help = new Helper(iConfig, context);
        }



        public partial class OfficesBodyObject
        {
            public long? Id { get; set; }
            public long MunicipalitiesId { get; set; }
            public string Name { get; set; }
        }
        //Offices
        //[HttpGet("Offices/Get")]
        //public IActionResult GetOffices(int pageNo, int pageSize, int CityId, int MunicipalitiesId)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        int Count = db.Offices.Include(x => x.Municipality)
        //            .Where(x => x.Status != 9
        //             && (CityId > 0 ? x.Municipality.CityId == CityId : true)
        //             && (MunicipalitiesId > 0 ? x.MunicipalityId == MunicipalitiesId : true)
        //            ).Count();
        //        var Info = db.Offices
        //            .Where(x => x.Status != 9
        //            && (CityId > 0 ? x.Municipality.CityId == CityId : true)
        //            && (MunicipalitiesId > 0 ? x.MunicipalityId == MunicipalitiesId : true)
        //        ).Select(x => new
        //        {
        //            x.Id,
        //            x.Name,
        //            x.Municipality.CityId,
        //            CityName = x.Municipality.City.Name,
        //            x.MunicipalityId,
        //            MunicipalityName = x.Municipality.Name,
        //            UserCount = db.Users.Where(k => k.OfficeId == x.Id && x.Status != 9).Count(),
        //            PrisonersCount = db.Prisoners.Where(k => k.OfficeId == x.Id && x.Status == 1).Count(),
        //            x.Status,
        //            x.CreatedOn,
        //            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
        //        }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


        //        return Ok(new { info = Info, count = Count });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        ////Offices
        //[HttpGet("Offices/GetById")]
        //public IActionResult GetOfficesById(long Id)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var Info = db.Offices
        //            .Where(x => x.Status != 9 && x.MunicipalityId == Id
        //        ).Select(x => new
        //        {
        //            x.Id,
        //            x.Name,
        //            x.Municipality.CityId,
        //            CityName = x.Municipality.City.Name,
        //            x.MunicipalityId,
        //            MunicipalityName = x.Municipality.Name,
        //            UserCount = db.Users.Where(k => k.OfficeId == x.Id && x.Status != 9).Count(),
        //            PrisonersCount = db.Prisoners.Where(k => k.OfficeId == x.Id && x.Status == 1).Count(),
        //            x.Status,
        //            x.CreatedOn,
        //            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
        //        }).ToList();


        //        return Ok(new { info = Info });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("Offices/Add")]
        //public IActionResult AddOffices([FromBody] OfficesBodyObject bodyObject)
        //{
        //    try
        //    {
        //        if (bodyObject == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


        //        if (string.IsNullOrWhiteSpace(bodyObject.Name))
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);


        //        var isExist = db.Offices.Where(x => x.Name == bodyObject.Name && x.MunicipalityId == bodyObject.MunicipalitiesId && x.Status != 9).SingleOrDefault();
        //        if (isExist != null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

        //        Offices row = new Offices();
        //        row.Name = bodyObject.Name;
        //        row.MunicipalityId = bodyObject.MunicipalitiesId;
        //        row.CreatedBy = userId;
        //        row.CreatedOn = DateTime.Now;
        //        row.Status = 1;
        //        db.Offices.Add(row);

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        rowTrans.Operations = TransactionsType.Add;
        //        rowTrans.Descriptions = "إضافة بيانات فرع جديد ";
        //        rowTrans.Controller = "Offices/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();
        //        return Ok(BackMessages.SucessAddOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("Offices/Edit")]
        //public IActionResult EditOffices([FromBody] OfficesBodyObject bodyObject)
        //{
        //    try
        //    {
        //        if (bodyObject == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        var row = db.Offices.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
        //        if (row == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
        //        rowTrans.OldObject = JsonConvert.SerializeObject(row);

        //        var isExist = db.Offices.Where(x => x.Name == bodyObject.Name && x.MunicipalityId == bodyObject.MunicipalitiesId && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
        //        if (isExist != null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

        //        row.Name = bodyObject.Name;
        //        row.MunicipalityId = bodyObject.MunicipalitiesId;
        //        row.ModifiedBy = userId;
        //        row.ModifiedOn = DateTime.Now;

        //        rowTrans.Operations = TransactionsType.Edit;
        //        rowTrans.ItemId = row.Id;
        //        rowTrans.Descriptions = "تعديل بيانات فرع  ";
        //        rowTrans.Controller = "Offices/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();

        //        return Ok(BackMessages.SucessEditOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("{Id}/Offices/Delete")]
        //public IActionResult DeleteOffices(long id)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


        //        var row = db.Offices.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

        //        if (row == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

        //        var HaveChiled = db.Prisoners.Where(x => x.OfficeId == id && x.Status != 9).ToList();
        //        if (HaveChiled.Count > 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

        //        var HaveUserChiled = db.Users.Where(x => x.OfficeId == id && x.Status != 9).ToList();
        //        if (HaveUserChiled.Count > 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

        //        row.Status = 9;

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        rowTrans.Operations = TransactionsType.Delete;
        //        rowTrans.ItemId = row.Id;
        //        rowTrans.Descriptions = "حذف بيانات فرع  ";
        //        rowTrans.Controller = "Offices/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();
        //        return Ok(BackMessages.SucessDeleteOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}





        public partial class CitiesBodyObject
        {
            public long? Id { get; set; }
            public string Name { get; set; }
        }
        //Cities
        [HttpGet("Cities/Get")]
        public IActionResult GetCities(int pageNo, int pageSize)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                int Count = db.Cities
                    .Where(x => x.Status != 9).Count();
                var Info = db.Cities
                    .Where(x => x.Status != 9).Select(x => new
                    {
                        x.Id,
                        x.Name,
                        MunicipalitiesCount = db.Municipalities.Where(k => k.CitiesId == x.Id && x.Status != 9).Count(),
                        //PrisonersCount = db.Prisoners.Include(x => x.Office).Where(k => k.Office.Municipality.CityId == x.Id && x.Status == 1).Count(),
                        x.Status,
                        x.CreatedOn,
                        CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
                    }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


                return Ok(new { info = Info, count = Count });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("Cities/GetAll")]
        public IActionResult GetAllCities()
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                int Count = db.Cities
                    .Where(x => x.Status != 9).Count();
                var Info = db.Cities
                    .Where(x => x.Status != 9).Select(x => new
                    {
                        x.Id,
                        x.Name,

                    }).OrderByDescending(x => x.Name).ToList();


                return Ok(new { info = Info, count = Count });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Cities/Add")]
        public IActionResult AddCities([FromBody] CitiesBodyObject bodyObject)
        {
            try
            {
                if (bodyObject == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
                if (user == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                if (user.UserType != 1)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


                if (string.IsNullOrWhiteSpace(bodyObject.Name))
                    return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);


                var isExist = db.Cities.Where(x => x.Name == bodyObject.Name && x.Status != 9).SingleOrDefault();
                if (isExist != null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

                Cities row = new Cities();
                row.Name = bodyObject.Name;
                row.CreatedBy = userId;
                row.CreatedOn = DateTime.Now;
                row.Status = 1;
                db.Cities.Add(row);

                TransactionsObject rowTrans = new TransactionsObject();
                rowTrans.Operations = TransactionsType.Add;
                rowTrans.Descriptions = "إضافة بيانات  مدينة جديدة ";
                rowTrans.Controller = "Cities/Dictionaries";
                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                rowTrans.CreatedBy = userId;
                this.help.WriteTransactions(rowTrans);

                db.SaveChanges();
                return Ok(BackMessages.SucessAddOperations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Cities/Edit")]
        public IActionResult EditCities([FromBody] CitiesBodyObject bodyObject)
        {
            try
            {
                if (bodyObject == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
                if (user == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                if (user.UserType != 1)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                TransactionsObject rowTrans = new TransactionsObject();
                var row = db.Cities.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
                if (row == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
                rowTrans.OldObject = JsonConvert.SerializeObject(row);

                var isExist = db.Cities.Where(x => x.Name == bodyObject.Name && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
                if (isExist != null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

                row.Name = bodyObject.Name;
                //row.ModifiedBy = userId;
                //row.ModifiedOn = DateTime.Now;

                rowTrans.Operations = TransactionsType.Edit;
                rowTrans.ItemId = row.Id;
                rowTrans.Descriptions = "تعديل بيانات   ";
                rowTrans.Controller = "Cities/Dictionaries";
                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                rowTrans.CreatedBy = userId;
                this.help.WriteTransactions(rowTrans);

                db.SaveChanges();

                return Ok(BackMessages.SucessEditOperations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{Id}/Cities/Delete")]
        public IActionResult DeleteCities(long id)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
                if (user == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                if (user.UserType != 1)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


                var row = db.Cities.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

                if (row == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

                var HaveChiled = db.Municipalities.Where(x => x.CitiesId == id && x.Status != 9).ToList();
                if (HaveChiled.Count > 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

                row.Status = 9;

                TransactionsObject rowTrans = new TransactionsObject();
                rowTrans.Operations = TransactionsType.Delete;
                rowTrans.ItemId = row.Id;
                rowTrans.Descriptions = "حذف بيانات   ";
                rowTrans.Controller = "Cities/Dictionaries";
                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                rowTrans.CreatedBy = userId;
                this.help.WriteTransactions(rowTrans);

                db.SaveChanges();
                return Ok(BackMessages.SucessDeleteOperations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }





        public partial class MunicipalitiesBodyObject
        {
            public long? Id { get; set; }
            public long CityId { get; set; }
            public string Name { get; set; }
        }
        //Municipalities
        [HttpGet("Municipalities/Get")]
        public IActionResult GetMunicipalities(int pageNo, int pageSize, int CityId)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                int Count = db.Municipalities
                    .Where(x => x.Status != 9
                     && (CityId > 0 ? x.CitiesId == CityId : true)
                    ).Count();
                var Info = db.Municipalities
                    .Where(x => x.Status != 9
                    && (CityId > 0 ? x.CitiesId == CityId : true)
                ).Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.CitiesId,
                    CityName = x.Cities.Name,
                    //OfficesCount = db.Offices.Where(k => k.MunicipalityId == x.Id && x.Status != 9).Count(),
                    //PrisonersCount = db.Prisoners.Include(x => x.Office).Where(k => k.Office.MunicipalityId == x.Id && x.Status == 1).Count(),
                    x.Status,
                    x.CreatedOn,
                    CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
                }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


                return Ok(new { info = Info, count = Count });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("Municipalities/GetById")]
        public IActionResult GetMunicipalitiesById(int CityId)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var Info = db.Municipalities
                    .Where(x => x.Status != 9 && x.CitiesId == CityId
                ).Select(x => new
                {
                    x.Id,
                    x.Name,
                }).OrderByDescending(x => x.Name).ToList();

                return Ok(new { info = Info });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Municipalities/Add")]
        public IActionResult AddMunicipalities([FromBody] MunicipalitiesBodyObject bodyObject)
        {
            try
            {
                if (bodyObject == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
                if (user == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                if (user.UserType != 1)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


                if (string.IsNullOrWhiteSpace(bodyObject.Name))
                    return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);


                var isExist = db.Municipalities.Where(x => x.Name == bodyObject.Name && x.CitiesId == bodyObject.CityId && x.Status != 9).SingleOrDefault();
                if (isExist != null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

                Municipalities row = new Municipalities();
                row.Name = bodyObject.Name;
                row.CitiesId = bodyObject.CityId;
                row.CreatedBy = userId;
                row.CreatedOn = DateTime.Now;
                row.Status = 1;
                db.Municipalities.Add(row);

                TransactionsObject rowTrans = new TransactionsObject();
                rowTrans.Operations = TransactionsType.Add;
                rowTrans.Descriptions = "إضافة بيانات بلدية جديد ";
                rowTrans.Controller = "Municipalities/Dictionaries";
                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                rowTrans.CreatedBy = userId;
                this.help.WriteTransactions(rowTrans);

                db.SaveChanges();
                return Ok(BackMessages.SucessAddOperations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Municipalities/Edit")]
        public IActionResult EditMunicipalities([FromBody] MunicipalitiesBodyObject bodyObject)
        {
            try
            {
                if (bodyObject == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
                if (user == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                if (user.UserType != 1)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                TransactionsObject rowTrans = new TransactionsObject();
                var row = db.Municipalities.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
                if (row == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
                rowTrans.OldObject = JsonConvert.SerializeObject(row);

                var isExist = db.Municipalities.Where(x => x.Name == bodyObject.Name && x.CitiesId == bodyObject.CityId && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
                if (isExist != null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

                row.Name = bodyObject.Name;
                row.CitiesId = bodyObject.CityId;
                //row.ModifiedBy = userId;
                //row.ModifiedOn = DateTime.Now;

                rowTrans.Operations = TransactionsType.Edit;
                rowTrans.ItemId = row.Id;
                rowTrans.Descriptions = "تعديل بيانات بلدية  ";
                rowTrans.Controller = "Municipalities/Dictionaries";
                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                rowTrans.CreatedBy = userId;
                this.help.WriteTransactions(rowTrans);

                db.SaveChanges();

                return Ok(BackMessages.SucessEditOperations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{Id}/Municipalities/Delete")]
        public IActionResult DeleteMunicipalities(long id)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

                var userId = this.help.GetCurrentUser(HttpContext);
                if (userId <= 0)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
                if (user == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

                if (user.UserType != 1)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


                var row = db.Municipalities.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

                if (row == null)
                    return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

                //var HaveChiled = db.Offices.Where(x => x.MunicipalityId == id && x.Status != 9).ToList();
                //if (HaveChiled.Count > 0)
                //    return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

                row.Status = 9;

                TransactionsObject rowTrans = new TransactionsObject();
                rowTrans.Operations = TransactionsType.Delete;
                rowTrans.ItemId = row.Id;
                rowTrans.Descriptions = "حذف بيانات بلدية  ";
                rowTrans.Controller = "Municipalities/Dictionaries";
                rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                rowTrans.CreatedBy = userId;
                this.help.WriteTransactions(rowTrans);

                db.SaveChanges();
                return Ok(BackMessages.SucessDeleteOperations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }








        public partial class BankBranchesBodyObject
        {
            public long? Id { get; set; }
            public long BankId { get; set; }
            public string Name { get; set; }
        }
        //Bank
        //[HttpGet("BankBranches/Get")]
        //public IActionResult GetBankBranches(int pageNo, int pageSize, int BrancheId)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        int Count = db.BankBranches
        //            .Where(x => x.Status != 9
        //             && (BrancheId > 0 ? x.BankId == BrancheId : true)
        //            ).Count();
        //        var Info = db.BankBranches
        //            .Include(x => x.Bank)
        //            .Where(x => x.Status != 9
        //            && (BrancheId > 0 ? x.BankId == BrancheId : true)
        //        ).Select(x => new
        //        {
        //            x.Id,
        //            x.Name,
        //            x.BankId,
        //            BrancheName = x.Bank.Name,
        //            PrisonersCount = db.Prisoners.Include(x => x.BankBranche).Where(k => k.BankBrancheId == x.Id && x.Status == 1).Count(),
        //            x.Status,
        //            x.CreatedOn,
        //            CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
        //        }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


        //        return Ok(new { info = Info, count = Count });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("BankBranches/GetById")]
        //public IActionResult GetBankBranchesByBankId(long BankId)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var Info = db.BankBranches
        //            .Include(x => x.Bank)
        //            .Where(x => x.Status != 9 && x.BankId == BankId
        //        ).Select(x => new
        //        {
        //            x.Id,
        //            x.Name,
        //        }).OrderBy(x => x.Name).ToList();


        //        return Ok(new { info = Info });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("BankBranches/Add")]
        //public IActionResult AddBankBranches([FromBody] BankBranchesBodyObject bodyObject)
        //{
        //    try
        //    {
        //        if (bodyObject == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


        //        if (string.IsNullOrWhiteSpace(bodyObject.Name))
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);


        //        var isExist = db.BankBranches.Where(x => x.Name == bodyObject.Name && x.BankId == bodyObject.BankId && x.Status != 9).SingleOrDefault();
        //        if (isExist != null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

        //        BankBranches row = new BankBranches();
        //        row.Name = bodyObject.Name;
        //        row.BankId = bodyObject.BankId;
        //        row.CreatedBy = userId;
        //        row.CreatedOn = DateTime.Now;
        //        row.Status = 1;
        //        db.BankBranches.Add(row);

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        rowTrans.Operations = TransactionsType.Add;
        //        rowTrans.Descriptions = "إضافة بيانات فرع مصرف جديد ";
        //        rowTrans.Controller = "BankBranches/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();
        //        return Ok(BackMessages.SucessAddOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("BankBranches/Edit")]
        //public IActionResult EditBankBranches([FromBody] BankBranchesBodyObject bodyObject)
        //{
        //    try
        //    {
        //        if (bodyObject == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        var row = db.BankBranches.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
        //        if (row == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
        //        rowTrans.OldObject = JsonConvert.SerializeObject(row);

        //        var isExist = db.BankBranches.Where(x => x.Name == bodyObject.Name && x.BankId == bodyObject.BankId && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
        //        if (isExist != null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

        //        row.Name = bodyObject.Name;
        //        row.BankId = bodyObject.BankId;
        //        row.ModifiedBy = userId;
        //        row.ModifiedOn = DateTime.Now;

        //        rowTrans.Operations = TransactionsType.Edit;
        //        rowTrans.ItemId = row.Id;
        //        rowTrans.Descriptions = "تعديل بيانات فرع مصرف  ";
        //        rowTrans.Controller = "BankBranches/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();

        //        return Ok(BackMessages.SucessEditOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("{Id}/BankBranches/Delete")]
        //public IActionResult DeleteBankBranches(long id)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


        //        var row = db.BankBranches.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

        //        if (row == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

        //        var HaveChiled = db.Prisoners.Where(x => x.BankBrancheId == id && x.Status != 9).ToList();
        //        if (HaveChiled.Count > 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

        //        row.Status = 9;

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        rowTrans.Operations = TransactionsType.Delete;
        //        rowTrans.ItemId = row.Id;
        //        rowTrans.Descriptions = "حذف بيانات فرع مصرف  ";
        //        rowTrans.Controller = "BankBranches/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();
        //        return Ok(BackMessages.SucessDeleteOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}










        //public partial class BankBodyObject
        //{
        //    public long? Id { get; set; }
        //    public string Name { get; set; }
        //}
        ////Bank
        //[HttpGet("Bank/Get")]
        //public IActionResult GetBank(int pageNo, int pageSize)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        int Count = db.Bank
        //            .Where(x => x.Status != 9).Count();
        //        var Info = db.Bank
        //            .Where(x => x.Status != 9).Select(x => new
        //            {
        //                x.Id,
        //                x.Name,
        //                BranchesCount = db.BankBranches.Where(k => k.BankId == x.Id && x.Status == 1).Count(),
        //                x.Status,
        //                x.CreatedOn,
        //                CreatedBy = db.Users.Where(k => k.Id == x.CreatedBy).SingleOrDefault().Name,
        //            }).OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();


        //        return Ok(new { info = Info, count = Count });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("Bank/GetAll")]
        //public IActionResult GetAllBank()
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var Info = db.Bank
        //            .Where(x => x.Status != 9).Select(x => new
        //            {
        //                x.Id,
        //                x.Name,
        //            }).OrderBy(x => x.Name).ToList();
        //        return Ok(new { info = Info });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("Bank/Add")]
        //public IActionResult AddBank([FromBody] BankBranchesBodyObject bodyObject)
        //{
        //    try
        //    {
        //        if (bodyObject == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


        //        if (string.IsNullOrWhiteSpace(bodyObject.Name))
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameEmpty);


        //        var isExist = db.Bank.Where(x => x.Name == bodyObject.Name && x.Status != 9).SingleOrDefault();
        //        if (isExist != null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

        //        Bank row = new Bank();
        //        row.Name = bodyObject.Name;
        //        row.CreatedBy = userId;
        //        row.CreatedOn = DateTime.Now;
        //        row.Status = 1;
        //        db.Bank.Add(row);

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        rowTrans.Operations = TransactionsType.Add;
        //        rowTrans.Descriptions = "إضافة بيانات  مصرف جديد ";
        //        rowTrans.Controller = "Bank/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();
        //        return Ok(BackMessages.SucessAddOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("Bank/Edit")]
        //public IActionResult EditBank([FromBody] BankBranchesBodyObject bodyObject)
        //{
        //    try
        //    {
        //        if (bodyObject == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        var row = db.Bank.Where(x => x.Id == bodyObject.Id && x.Status != 9).SingleOrDefault();
        //        if (row == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);
        //        rowTrans.OldObject = JsonConvert.SerializeObject(row);

        //        var isExist = db.Bank.Where(x => x.Name == bodyObject.Name && x.Status != 9 && x.Id != bodyObject.Id).SingleOrDefault();
        //        if (isExist != null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NameExist);

        //        row.Name = bodyObject.Name;
        //        row.ModifiedBy = userId;
        //        row.ModifiedOn = DateTime.Now;

        //        rowTrans.Operations = TransactionsType.Edit;
        //        rowTrans.ItemId = row.Id;
        //        rowTrans.Descriptions = "تعديل بيانات مصرف   ";
        //        rowTrans.Controller = "Bank/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();

        //        return Ok(BackMessages.SucessEditOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("{Id}/Bank/Delete")]
        //public IActionResult DeleteBank(long id)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.EmptyBodyObject);

        //        var userId = this.help.GetCurrentUser(HttpContext);
        //        if (userId <= 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        var user = db.Users.Where(x => x.Id == userId && x.Status != 9).SingleOrDefault();
        //        if (user == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);

        //        if (user.UserType != 1)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotAuthorized);


        //        var row = db.Bank.Where(x => x.Id == id && x.Status != 9).SingleOrDefault();

        //        if (row == null)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.NotFound);

        //        var HaveChiled = db.BankBranches.Where(x => x.BankId == id && x.Status != 9).ToList();
        //        if (HaveChiled.Count > 0)
        //            return StatusCode(BackMessages.StatusCode, BackMessages.HasChild);

        //        row.Status = 9;

        //        TransactionsObject rowTrans = new TransactionsObject();
        //        rowTrans.Operations = TransactionsType.Delete;
        //        rowTrans.ItemId = row.Id;
        //        rowTrans.Descriptions = "حذف بيانات مصؤف   ";
        //        rowTrans.Controller = "BankBranches/Dictionaries";
        //        rowTrans.NewObject = JsonConvert.SerializeObject(row, Formatting.None,
        //                new JsonSerializerSettings()
        //                {
        //                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                });
        //        rowTrans.CreatedBy = userId;
        //        this.help.WriteTransactions(rowTrans);

        //        db.SaveChanges();
        //        return Ok(BackMessages.SucessDeleteOperations);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}




    }
}