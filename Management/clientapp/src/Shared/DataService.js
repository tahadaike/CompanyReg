import axios from "axios";

axios.defaults.headers.common["X-CSRF-TOKEN"] = document.cookie.split("=")[1];

//const baseUrl = 'http://localhost:4810/Api';

export default {
    // ********************************************************************| Authintecations |***********************************************************

    login(bodyObjeect) {
        return axios.post(`/Security/loginUser`, bodyObjeect);
    },

    AddMuntisbl(bodyObjeect) {
        return axios.post(`/api/admin/Municipalities/Add`, bodyObjeect);
    },

    IsLoggedin() {
        return axios.get(`/Security/IsLoggedin`);
    },

    Logout() {
        return axios.post(`/Security/Logout`);
    },







    // ********************************************************************| Dictionaries |***********************************************************

    // Office
    GetOffices(pageNo, pageSize, CityId, MunicipalitiesId) {
        return axios.get(`api/admin/Dictionaries/Offices/Get?pageno=${pageNo}&pagesize=${pageSize}&CityId=${CityId}&MunicipalitiesId=${MunicipalitiesId}`);
    },

    GetOfficesById(MunicipalitiesId) {
        return axios.get(`api/admin/Dictionaries/Offices/GetById?Id=${MunicipalitiesId}`);
    },

    AddOffices(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Offices/Add`, bodyObject);
    },

    EditOffices(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Offices/Edit`, bodyObject);
    },

    DeleteOffices(Id) {
        return axios.post(`api/admin/Dictionaries/${Id}/Offices/Delete`);
    },



    // Cities
    GetCitie(pageNo, pageSize) {
        return axios.get(`api/admin/Dictionaries/Cities/Get?pageno=${pageNo}&pagesize=${pageSize}`);
    },

    GetCities() {
        return axios.get(`api/admin/Dictionaries/Cities/GetAll`);
    },

    AddCities(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Cities/Add`, bodyObject);
    },

    EditCities(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Cities/Edit`, bodyObject);
    },

    DeleteCities(Id) {
        return axios.post(`api/admin/Dictionaries/${Id}/Cities/Delete`);
    },





    // Municipalities
    GetMunicipalities(pageNo, pageSize, CityId) {
        return axios.get(`api/admin/Dictionaries/Municipalities/Get?pageno=${pageNo}&pagesize=${pageSize}&CityId=${CityId}`);
    },

    GetMunicipalitiesByCiteisID(Id) {
        return axios.get(`api/admin/Dictionaries/Municipalities/GetById?CityId=${Id}`);
    },

    AddMunicipalities(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Municipalities/Add`, bodyObject);
    },

    EditMunicipalities(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Municipalities/Edit`, bodyObject);
    },

    DeleteMunicipalities(Id) {
        return axios.post(`api/admin/Dictionaries/${Id}/Municipalities/Delete`);
    },



    //BankBranches
    GetBankBranches(pageNo, pageSize, BankId) {
        return axios.get(`api/admin/Dictionaries/BankBranches/Get?pageno=${pageNo}&pagesize=${pageSize}&BrancheId=${BankId}`);
    },

    GetBankBranchesByBankId(BankId) {
        return axios.get(`api/admin/Dictionaries/BankBranches/GetById?BankId=${BankId}`);
    },

    AddBankBranches(bodyObject) {
        return axios.post(`api/admin/Dictionaries/BankBranches/Add`, bodyObject);
    },

    EditBankBranches(bodyObject) {
        return axios.post(`api/admin/Dictionaries/BankBranches/Edit`, bodyObject);
    },

    DeleteBankBranches(Id) {
        return axios.post(`api/admin/Dictionaries/${Id}/BankBranches/Delete`);
    },



    //Bank
    GetBank(pageNo, pageSize) {
        return axios.get(`api/admin/Dictionaries/Bank/Get?pageno=${pageNo}&pagesize=${pageSize}`);
    },

    GetAllBank() {
        return axios.get(`api/admin/Dictionaries/Bank/GetAll`);
    },

    AddBank(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Bank/Add`, bodyObject);
    },

    EditBank(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Bank/Edit`, bodyObject);
    },

    DeleteBank(Id) {
        return axios.post(`api/admin/Dictionaries/${Id}/Bank/Delete`);
    },







    // ********************************************************************| Prisoners |***********************************************************


    GetPrisoners(pageNo, pageSize, CityId, MunicipalitId, OfficeId, PrisonerStatus, PrisonerDocumentStatus, PrisonersId) {
        return axios.get(`api/admin/Prisoners/Get?pageNo=${pageNo}&pageSize=${pageSize}&CityId=${CityId}
        &MunicipalityId=${MunicipalitId}&OfficeId=${OfficeId}&PrisonerStatus=${PrisonerStatus}
        &PrisonerDocumentStatus=${PrisonerDocumentStatus}&PrisonersId=${PrisonersId}`);
    },

    GetAllPrisonersToPrint() {
        return axios.get(`api/admin/Prisoners/GetAllPrisonersToPrint`);
    },

    GetPrisonersById(code) {
        return axios.get(`api/admin/Prisoners/GetByName?code=${code}`);
    },

    AddPrisoners(bodyObject) {
        return axios.post(`api/admin/Prisoners/Add`, bodyObject);
    },

    EditPrisoners(bodyObject) {
        return axios.post(`api/admin/Prisoners/Edit`, bodyObject);
    },

    DeletePrisoners(Id) {
        return axios.post(`api/admin/Prisoners/${Id}/Delete`);
    },



    // Attachemtn
    AddPrisonersAttahcment(bodyObject) {
        return axios.post(`api/admin/Prisoners/Attachments/Add`, bodyObject);
    },

    GetPrisonersAttachment(Id) {
        return axios.get(`api/admin/Prisoners/Attachments/Get?Id=${Id}`);
    },

    DeletePrisonersAttachment(Id) {
        return axios.post(`api/admin/Prisoners/${Id}/Attachments/Delete`);
    },


    // Schedule
    AddPrisonersSchedule(bodyObject) {
        return axios.post(`api/admin/Prisoners/Schedule/Add`, bodyObject);
    },

    GetPrisonersSchedule(Id) {
        return axios.get(`api/admin/Prisoners/Schedule/Get?Id=${Id}`);
    },

    DeletePrisonersSchedule(Id) {
        return axios.post(`api/admin/Prisoners/${Id}/Schedule/Delete`);
    },




    //Calculat
    GetCalculatStatic() {
        return axios.get(`api/admin/Prisoners/Calculat/Get`);
    },

    CalculatToPay(MaxValue, PriceOfDay) {
        return axios.get(`api/admin/Prisoners/Calculat/CalculatToPay?MaxValue=${MaxValue}&PriceOfDay=${PriceOfDay}`);
    },

    Pay(bodyObject) {
        return axios.post(`api/admin/Prisoners/Calculat/Pay`, bodyObject);
    },

    PayInfoTransactions(pageNo, pageSize, CityId, MunicipalitiesId, OfficeId, PrisonerStatus, PrisonersId) {
        return axios.get(`api/admin/Prisoners/Calculat/PayInfoTransactions/Get?pageNo=${pageNo}&pageSize=${pageSize}&CityId=${CityId}
        &MunicipalitiesId=${MunicipalitiesId}&OfficeId=${OfficeId}&PrisonerStatus=${PrisonerStatus}&PrisonersId=${PrisonersId}`);
    },

    GetPay(pageNo, pageSize) {
        return axios.get(`api/admin/Prisoners/Calculat/GetPay?pageno=${pageNo}&pagesize=${pageSize}`);
    },

    DeletePayInfo(Id) {
        return axios.post(`api/admin/Prisoners/${Id}/Calculat/Delete`);
    },







    GetDashboardInfo() {
        return axios.get(`api/admin/Prisoners/GetDashboardInfo`);
    },























    //Transactins
    GetPatientTransactions(pageNo, pageSize, PatientId) {
        return axios.get(`api/admin/Patients/Transactions/Get?pageNo=${pageNo}&pageSize=${pageSize}&PatientId=${PatientId}`);
    },



    //Cahnge Request
    AddChangeRequest(bodyObject) {
        return axios.post(`api/admin/Patients/ChangeRequest/Add`, bodyObject);
    },

    GetChangeRequest(pageNo, pageSize, CityId, MunicipalitiesId, KidneyCenterId, Level, PatientId) {
        return axios.get(`api/admin/Patients/ChangeRequest/Get?pageno=${pageNo}&pagesize=${pageSize}&CityId=${CityId}&MunicipalitiesId=${MunicipalitiesId}&KidneyCenterId=${KidneyCenterId}&Level=${Level}&PatientId=${PatientId}`);
    },

    GetCenterChangeRequest(pageNo, pageSize, Level, PatientId) {
        return axios.get(`api/admin/Patients/ChangeRequest/GetRequest?pageno=${pageNo}&pagesize=${pageSize}&Level=${Level}&PatientId=${PatientId}`);
    },

    GetMyCenterChangeRequest(pageNo, pageSize, Level, PatientId) {
        return axios.get(`api/admin/Patients/ChangeRequest/GetMyRequest?pageno=${pageNo}&pagesize=${pageSize}&Level=${Level}&PatientId=${PatientId}`);
    },

    DeleteChangeRequest(Id) {
        return axios.post(`api/admin/Patients/${Id}/ChangeRequest/Delete`);
    },

    AcceptChangeRequestAttachment(Id) {
        return axios.post(`api/admin/Patients/${Id}/ChangeRequest/Accept`);
    },

    RejectChangeRequest(bodyObject) {
        return axios.post(`api/admin/Patients/ChangeRequest/Reject`, bodyObject);
    },



    //Change Request Attahcment
    AddChangeRequestAttahcment(bodyObject) {
        return axios.post(`api/admin/Patients/ChangeRequest/Attachments/Add`, bodyObject);
    },

    GetChangeRequestAttachment(Id) {
        return axios.get(`api/admin/Patients/ChangeRequest/Attachments/Get?Id=${Id}`);
    },

    DeleteChangeRequestAttachment(Id) {
        return axios.post(`api/admin/Patients/${Id}/ChangeRequest/Attachments/Delete`);
    },




    //Daily Used 
    GetTodayPatients(PatientId) {
        return axios.get(`api/admin/Patients/DailyUsed/GetTodayPatient?PatientId=${PatientId}`);
    },

    NewAttendance(Id) {
        return axios.post(`api/admin/Patients/${Id}/DailyUsed/Add`);
    },

    NewAttendanceManuel(bodyObject) {
        return axios.post(`api/admin/Patients/DailyUsed/AddManuel`, bodyObject);
    },

    GetUsedDaily(pageNo, pageSize, CityId, MunicipalitiesId, KidneyCenterId, PatientId, ByDate, CompanyId, DeviceId, FilterId) {
        return axios.get(`api/admin/Patients/DailyUsed/Get?pageNo=${pageNo}&pagesize=${pageSize}&CityId=${CityId}
            &MunicipalitiesId=${MunicipalitiesId}&KidneyCenterId=${KidneyCenterId}&PatientId=${PatientId}
            &ByDate=${ByDate}&CompanyId=${CompanyId}&DeviceId=${DeviceId}&FilterId=${FilterId}`);
    },

    DeleteAttendance(Id) {
        return axios.post(`api/admin/Patients/${Id}/DailyUsed/Delete`);
    },

    GetUsedDailyReportInfo(pageNo, pageSize, CityId, MunicipalitiesId, KidneyCenterId, From,To, CompanyId, DeviceId, FilterId) {
        return axios.get(`api/admin/Patients/DailyUsed/GetReport?pageNo=${pageNo}&pagesize=${pageSize}&CityId=${CityId}
            &MunicipalitiesId=${MunicipalitiesId}&KidneyCenterId=${KidneyCenterId}
            &From=${From}&To=${To}&CompanyId=${CompanyId}&DeviceId=${DeviceId}&FilterId=${FilterId}`);
    },

    GetUsedDailyRequired(pageNo, pageSize, CityId, MunicipalitiesId, KidneyCenterId, CompanyId, DeviceId, FilterId) {
        return axios.get(`api/admin/Patients/DailyUsed/GetRequired?pageNo=${pageNo}&pagesize=${pageSize}&CityId=${CityId}
            &MunicipalitiesId=${MunicipalitiesId}&KidneyCenterId=${KidneyCenterId}
            &CompanyId=${CompanyId}&DeviceId=${DeviceId}&FilterId=${FilterId}`);
    },

























    // ********************************************************************| Users |***********************************************************
    GetUsers(pageNo, pageSize, UserType, CityId, MunicipalitiesId, OfficeId) {
        return axios.get(`api/admin/User/Get?pageNo=${pageNo}&pagesize=${pageSize}&UserType=${UserType}
            &CityId=${CityId}&MunicipalitiesId=${MunicipalitiesId}&OfficeId=${OfficeId}`);
    },

    AddUser(bodyObject) {
        return axios.post("api/admin/User/Add", bodyObject);
    },

    EditUser(bodyObject) {
        return axios.post("api/admin/User/Edit", bodyObject);
    },

    ChangeStatusUser(Id) {
        return axios.post(`api/admin/User/${Id}/ChangeStatus`);
    },

    RestePassword(Id) {
        return axios.post(`api/admin/User/${Id}/RestePassword`);
    },

    DeleteUser(Id) {
        return axios.post(`api/admin/User/${Id}/Delete`);
    },

    UploadImage(bodyObject) {
        return axios.post("/Api/Admin/User/UploadImage", bodyObject);
    },

    EditUsersProfile(bodyObject) {
        return axios.post("/Api/Admin/User/EditUsersProfile", bodyObject);
    },

    ChangePassword(userPassword) {
        return axios.post(`/Api/Admin/User/ChangePassword`, userPassword);
    },











    // ********************************************************************| Dashboard Info |***********************************************************
    

















    


























    // ********************************************************************| Helper |***********************************************************
    

    //GetMunicipalitiesName(id) {
    //    return axios.get(`/api/admin/Municipalities/GetAll?id=${id}`);
    //  },

    GetAllMunicipalitiesName() {
        return axios.get(`/api/admin/Municipalities/GetAll`);
    },

    GetMunicipalitiesName(id) {
        return axios.get(`/Api/Admin/Helper/GetMunicipalitiesName?id=${id}`);
    },


    

    GetByIdKidneyCenterId(id) {
        return axios.get(`api/admin/Patients/GetById?KidneyCenterId=${id}`);
    },

    GetByPatientInfo(NationalId, FileNumber) {
        return axios.get(`api/admin/Patients/GetByPatientInfo?NationalId=${NationalId}&FileNumber=${FileNumber}`);
    },


    UpDateMuntisbl(bodyObject) {
        return axios.post(`api/admin/Municipalities/Edit`, bodyObject);
    },

    KidneyCenters(pageNo, pageSize) {
        return axios.get(
            `api/admin/Offices/Get?pageNo=${pageNo}&pageSize=${pageSize}`
        );
    },

    deleteMunticbil(id) {
        return axios.post(`api/admin/Municipalities/${id}/Delete`);
    },

    //deleteKidneyCenters(id) {
    //    return axios.post(`api/admin/Offices/${id}/Delete`);
    //},

    //AddKidneyCenters(bodyObject) {
    //    return axios.post(`api/admin/Offices/Add`, bodyObject);
    //},

    UpdateKidneyCenters(bodyObject) {
        return axios.post(`api/admin/Offices/Edit`, bodyObject);
    },

    GetAllMunicipalities(cityid) {
        return axios.get(`api/admin/Municipalities/GetAll?cityid=${cityid}`);
    },

    GetLocationsName(id) {
        return axios.get(`/Api/Admin/Helper/GetLocationsName?id=${id}`);
    },

    ChangePasswords(object) {
        return axios.post(`/Api/Admin/User/ChangePassword`, object);
    },

    

    GetUsersHospital(pageNo, pageSize, KidneyCentersId) {
        return axios.get(
            `/Api/Admin/User/GetUsersHospital?pageNo=${pageNo}&pagesize=${pageSize}&KidneyCentersId=${KidneyCentersId}`
        );
    },

    GetUsersPerType(pageNo, pageSize, UserType) {
        return axios.get(
            `/Api/Admin/User/GetUsersPerType?pageNo=${pageNo}&pagesize=${pageSize}&userType=${UserType}`
        );
    },

    

    // **********************************    Applications ***********************

    AddApplications(bodyObject) {
        return axios.post(`/Api/Admin/Applications/Add`, bodyObject);
    },

    EditApplications(bodyObject) {
        return axios.post(`/Api/Admin/Applications/Edit`, bodyObject);
    },

    GetApplications(pageNo, pageSize, MunicipalitId, SelectedHospitalsId) {
        return axios.get(
            `/Api/Admin/Applications/Get?pageno=${pageNo}&pagesize=${pageSize}&MunicipalitId=${MunicipalitId}&selectedHospitalsId=${SelectedHospitalsId}`
        );
    },

    GetAttachments(id) {
        return axios.get(`api/admin/Patients/GetAttachments?PatientId=${id}`);
    },

    AddAttachment(bodyObject) {
        return axios.post(`api/admin/Patients/AddAttachments`, bodyObject);
    },

    DeleteAttachment(id) {
        return axios.post(`api/admin/Patients/${id}/RemoveAttachments`);
    },

    AddImage(bodyObject) {
        return axios.post(`api/admin/Patients/ChangePatintImage`, bodyObject);
    },

    GetPhones(id) {
        return axios.get(
            `api/admin/Patients/GetPatientPhoneNumbers?PatientId=${id}`
        );
    },

    AddPhones(bodyObject) {
        return axios.post(`api/admin/Patients/AddPatientPhoneNumbers`, bodyObject);
    },

    DeletePhones(id) {
        return axios.post(`api/admin/Patients/${id}/DeletePatientPhoneNumbers`);
    },

    deleteApp(id) {
        return axios.post(`/Api/Admin/Applications/${id}/delete`);
    },

    GetFilterById(id) {
        return axios.get(`api/admin/Filters/GetById?DeviceId=${id}`);
    },

    GetBranchesName(id) {
        return axios.get(`/Api/Admin/Applications/GetBranchesNames?cityId=${id}`);
    },
    //GetPatientSchedule(id) {
    //    return axios.get(`api/admin/Patients/GetPatientSchedule?PatientId=${id}`);
    //},
    GetBranchesAllName() {
        return axios.get(`/Api/Admin/Applications/GetAllBranchesNames`);
    },

    // **********************************    Hospitals ***********************
    //AddPatientSchedule(b) {
    //    return axios.post(`api/admin/Patients/AddPatientSchedule`, b);
    //},
    GetHospitalsAllName(MunicipalitId) {
        return axios.get(
            `/Api/Admin/Hospitals/GetAllHospitalsNames?MunicipalitId=${MunicipalitId}`
        );
    },

    //DeletePatientSchedule(id) {
    //    return axios.post(`api/admin/Patients/${id}/DeletePatientSchedule`);
    //},

    AddHospitals(bodyObject) {
        return axios.post(`/Api/Admin/Hospitals/Add`, bodyObject);
    },

    GetHospitals(pageNo, pageSize, MunicipalitId) {
        return axios.get(
            `/Api/Admin/Hospitals/Get?pageno=${pageNo}&pagesize=${pageSize}&MunicipalitId=${MunicipalitId}`
        );
    },

    EditHospitals(bodyObject) {
        return axios.post(`/Api/Admin/Hospitals/Edit`, bodyObject);
    },

    deleteHospitals(id) {
        return axios.post(`/Api/Admin/Hospitals/${id}/delete`);
    },

    

    GetRequiment() {
        return axios.get(`/Api/Admin/Municipalities/GetRequiment`);
    },


    getManticiplintKednyCenter(id) {
        return axios.get(`api/admin/Patients/GetByMunicipalitId?MunicipalitId=${id}`);
    },
    /////   Devices ///////

    //GetDevices(pageNo, pageSize, id) {
    //    return axios.get(
    //        `api/admin/Devices/Get?pageNo=${pageNo}&pageSize=${pageSize}&companyid=${id}`
    //    );
    //},

    GetAllDevices() {
        return axios.get(
            `api/admin/Devices/GetAll`
        );
    },

    //AddDevices(bodyObject) {
    //    return axios.post(`api/admin/Devices/Add`, bodyObject);
    //},

    UpdateDevices(bodyObject) {
        return axios.post(`api/admin/Devices/Edit`, bodyObject);
    },

    //deleteDevices(id) {
    //    return axios.post(`api/admin/Devices/${id}/Delete`);
    //},

    GetAllCompanies() {
        return axios.get(`api/admin/Dictionaries/GetAll`);
    },
    GetDviceById(id) {
        return axios.get(
            `api/admin/Devices/GetById?pageNo=1&pageSize=1000&CompanyId=${id}`
        );
    },
    /////   Devices ///////

    AddDeviceToKindycenter(bodyObject) {
        return axios.post(`api/admin/KidneyCentersDevices/Add`, bodyObject);
    },
    EditDeviceToKindycenter(bodyObject) {
        return axios.post(`api/admin/KidneyCentersDevices/Edit`, bodyObject);
    },
    /////// Company   //////////////
    GetByReceiptNumber() {
        return axios.get(`api/admin/Companies/GetByReceiptNumber`);
    },
    deleteDeviceKidny(id) {
        return axios.post(`api/admin/KidneyCentersDevices/${id}/Delete`);
    },
    GetCompanies(pageNo, pageSize, Id) {
        return axios.get(
            `api/admin/Companies/Get?pageNo=${pageNo}&pageSize=${pageSize}&Id=${Id}`
        );
    },
    GetTransactions(pageNo, pageSize) {
        return axios.get(
            `api/admin/Companies/Get/Transactions?pageNo=${pageNo}&pageSize=${pageSize}`
        );
    },
    AddCompany(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Add`, bodyObject);
    },

    GetFelter(pageNo, pageSize, id) {
        return axios.get(
            `api/admin/Filters/Get?pageNo=${pageNo}&pageSize=${pageSize}&filterid=${id}`
        );
    },
    UpdateCompany(bodyObject) {
        return axios.post(`api/admin/Dictionaries/Edit`, bodyObject);
    },

    deleteCompany(id) {
        return axios.post(`api/admin/Dictionaries/${id}/Delete`);
    },
    CahngeLevels(id) {
        return axios.post(`api/admin/Companies/${id}/CahngeLevels`);
    },
    Active(id) {
        return axios.post(`api/admin/Companies/${id}/Active`);
    },
    deleteFelter(id) {
        return axios.post(`api/admin/Filters/${id}/Delete`);
    },

    DeletePatientAttendance(id) {
        return axios.post(`api/admin/Patients/${id}/DeletePatientAttendance`);
    },

    UpdateFilters(bodyObject) {
        return axios.post(`api/admin/Filters/Edit`, bodyObject);
    },
    AddIssuse(bodyObject) {
        return axios.post(`api/admin/Companies/Issuse/Add`, bodyObject);
    },
    //AddFilters(bodyObject) {
    //    return axios.post(`api/admin/Filters/Add`, bodyObject);
    //},



    PrintPatients(pageNo, pageSize, MunicipalitId, HospitalId, companiesId) {
        return axios.get(`api/admin/Reports/Print/Patients?pageNo=${pageNo}&pageSize=${pageSize}&MunicipalityId=${MunicipalitId}&KidneyCenterId=${HospitalId}&CompanyId=${companiesId}`, { responseType: 'arraybuffer' });
    },

    deletePatient(id) {
        return axios.post(`api/admin/Patients/${id}/Delete`);
    },

    ShowDevices(id) {
        return axios.get(
            `api/admin/KidneyCentersDevices/Get?KidneyCentersId=${id}`
        );
    },

    GetNationalites() {
        return axios.get(`api/admin/Nationalites/GetAll`);
    },

    /////// Company //////////

    // **********************************    Municipalities ***********************

    //   GetAllMunicipalities() {
    //     return axios.get(`/Api/Admin/Municipalities/GetAll`);
    //   },

    //AddMunicipalities(bodyObject) {
    //    return axios.post(`/Api/Admin/Municipalities/AddWithoutCity`, bodyObject);
    //},

    //GetMunicipalities(pageNo, pageSize) {
    //    return axios.get(
    //        `/Api/Admin/Municipalities/Get?pageno=${pageNo}&pagesize=${pageSize}`
    //    );
    //},

    //EditMunicipalities(bodyObject) {
    //    return axios.post(`/Api/Admin/Municipalities/EditWithoutCity`, bodyObject);
    //},

    //DeleteMunicipalities(id) {
    //    return axios.post(`/Api/Admin/Municipalities/${id}/Delete`);
    //},

    // **********************************    GetDevicesName ***********************

    GetDevicesNames() {
        return axios.get(`/Api/Admin/Devices/GetDevicesName`);
    },

    GetDevicesbyCompany(id) {
        return axios.get(`/Api/Admin/Devices/GetById?CompanyId=${id}`);
    },

    GetFilterNames(id) {
        return axios.get(`/Api/Admin/Filters/GetById?DeviceId=${id}`);
    },

    GetAllFilterNames() {
        return axios.get(`/Api/Admin/Devices/GetAllNames`);
    },

    // **********************************    Get Change Center ***********************

    

    GetChangeAttachments(id) {
        return axios.get(
            `/Api/Admin/Patients/GetChangeAttachments?PatientsChangeCenterId=${id}`
        );
    },



    GetKidneyCentersNames() {
        return axios.get(`/Api/Admin/Offices/GetAll`);
    },

    

    //****************************************************************************************
    

    GetPatientNames() {
        return axios.get(`/Api/Admin/Patients/GetAll`);
    },

    GetCompanyNames() {
        return axios.get(`/Api/Admin/Dictionaries/GetAll`);
    },

    GetUsed() {
        return axios.get(`/Api/Admin/Reports/GetUsed`);
    },

    GetUsedRequierd() {
        return axios.get(`/Api/Admin/Reports/GetUsedRequierd`);
    },

    GetByDate(day1, day2) {
        return axios.get(
            `/Api/Admin/Reports/GetUsedByDate?From=${day1}&To=${day2}`
        );
    },

    AddPatientAttendance(bodyObject) {
        return axios.post(`api/admin/Patients/AddPatientAttendance`, bodyObject);
    },

    GetKednyCenterByCitie(id) {
        return axios.get(`api/admin/Offices/GetById?MunicipalityId=${id}`);
    },
    GetKednyCenterByMunicipality(id) {
        return axios.get(`api/admin/Offices/GetById?MunicipalityId=${id}`);
    },










    UpdatePationtScagualList(schema) {
        return axios.post(`api/admin/Patients/EditPatientSchedule`, schema);
    },


    //AddHospitals(bodyObject) {
    //    return axios.post(`/Api/Admin/Devices/Add`, bodyObject);
    //},

    //GetHospitals(pageNo, pageSize, SelectedCityId) {
    //    return axios.get(`/Api/Admin/Devices/GetBranches?pageno=${pageNo}&pagesize=${pageSize}&selectedCityId=${SelectedCityId}`);
    //},

    //EditHospitals(bodyObject) {
    //    return axios.post(`/Api/Admin/Devices/EditBranches`, bodyObject);
    //},

    //deleteHospitals(id) {
    //    return axios.post(`/Api/Admin/Devices/${id}/deleteBranches`);
    //},
};
