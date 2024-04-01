<script>
    import blockUI from './BlockUIService.js';
    import DataService from './DataService.js';
    import CryptoJS from 'crypto-js';
    export default {
        data() {
            return {
                loginDetails: null,

                Cities: [],
                Municipalities: [],
                Offices: [],

                Bank: [],
                BankBranches: [],
                BankBranches1: [],


                


                
                //Locations: [],
                //AcademicLevels: [],
                //AcademicSpecialization: [],
                //Subjects: [],
                //Instructors: [],
                //InstructorsFullInfo: [],
                //PaymentMethods: [],
                //Courses: [],

                //ChartByYear: [],


                /*Publeish*/
                //ServerUrl:'https://kidney.moh.com.ly',
                
                /*Local*/
                ServerUrl: 'http://localhost:5000',
                Facebock: 'https://www.facebook.com/p/Traneem-100064940462078/?locale=ar_AR',
                Instagram: 'https://www.instagram.com/traneem5__/',
                TraneemPhone: '+218 94 457 81 48',
                TraneemEmail: 'info@traneem.ly',
                PlatFormPass: 'Traneem!@Platformv1',



            }
        },
        methods: {

            encrypt: function encrypt(data, SECRET_KEY) {
                var dataSet = CryptoJS.AES.encrypt(JSON.stringify(data), SECRET_KEY);
                dataSet = dataSet.toString();
                return dataSet;
            },
            decrypt: function decrypt(data, SECRET_KEY) {
                data = CryptoJS.AES.decrypt(data, SECRET_KEY);
                data = JSON.parse(data.toString(CryptoJS.enc.Utf8));
                return data;
            },


            //////////////////////////////////////////////// Auth //////////////////////////////

            //async CheckLoginStatus() {
            //    try {
            //        this.loginDetails = JSON.parse(this.decrypt(localStorage.getItem('currentUser-client'), this.PlatFormPass));
            //        if (this.loginDetails != null) {
            //            //window.location.href = '/Login';
            //        }
            //    } catch (error) {
            //        //window.location.href = '/Login';
            //    }
            //},

            async CheckLoginStatus() {
                try {
                    this.loginDetails = JSON.parse(this.decrypt(localStorage.getItem('currentUser-client'), this.PlatFormPass));
                    if (this.loginDetails != null) {
                        const res = await DataService.IsLoggedin();
                        if (!res.data)
                            this.logout();
                    } else {
                        this.logout();
                    }
                } catch (error) {
                    this.logout();
                }
            },

            async logout() {
                localStorage.removeItem('currentUser-client');
                localStorage.clear();
                document.cookie.split(";").forEach(function (c) { document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/"); });
                this.$http.Logout()
                    .then(() => {
                        window.location.href = "/Login";
                    })
            },

            //async CheckLoginStatus() {
            //    try {
            //        this.loginDetails = await JSON.parse(localStorage.getItem('currentUser-client'));
            //        if (this.loginDetails == null) {
            //            //window.location.href = '/Login';
            //        }
            //    } catch (error) {
            //        //window.location.href = '/Login';
            //    }
            //},






            async GetCities() {
                this.Cities = [],
                blockUI.Start();
                try {
                    const res = await DataService.GetCities();
                    this.Cities = res.data.info;
                    blockUI.Stop();
                } catch (err) {
                    blockUI.Stop();
                }
            },

            async GetMunicipalities(id) {
                this.Municipalities = [],
                blockUI.Start();
                try {
                    const res = await DataService.GetMunicipalitiesByCiteisID(id);
                    this.Municipalities = res.data.info;
                    blockUI.Stop();
                } catch (err) {
                    blockUI.Stop();
                }
            },

            async GetOfficesById(id) {
                this.Offices = [];
                blockUI.Start();
                try {
                    const res = await DataService.GetOfficesById(id);
                    this.Offices = res.data.info;
                    blockUI.Stop();
                } catch (err) {
                    blockUI.Stop();
                }
            },






            async GetBank() {
                this.Bank = [],
                    blockUI.Start();
                try {
                    const res = await DataService.GetAllBank();
                    this.Bank = res.data.info;
                    blockUI.Stop();
                } catch (err) {
                    blockUI.Stop();
                }
            },

            async GetBankBranchesByBankId(id) {
                this.BankBranches = [],
                    blockUI.Start();
                try {
                    const res = await DataService.GetBankBranchesByBankId(id);
                    this.BankBranches = res.data.info;
                    blockUI.Stop();
                } catch (err) {
                    blockUI.Stop();
                }
            },

            async GetBankBranchesByBankId1(id) {
                this.BankBranches1 = [],
                    blockUI.Start();
                try {
                    const res = await DataService.GetBankBranchesByBankId(id);
                    this.BankBranches1 = res.data.info;
                    blockUI.Stop();
                } catch (err) {
                    blockUI.Stop();
                }
            },


            //async GetKidneyCentersName(id) {
            //    this.Hospitals = [],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetKednyCenterByCitie(id);
            //        this.KidneyCentersName = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},
            //async GetLocations(id) {
            //    this.Locations=[],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetLocationsName(id);
            //        this.Locations = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},
            //async GetAcademicLevels() {
            //    this.AcademicLevels = [],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetAcademicLevelsName();
            //        this.AcademicLevels = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},
            //async GetAcademicSpecialization(id) {
            //    this.AcademicSpecialization=[],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetAcademicSpecializationName(id);
            //        this.AcademicSpecialization = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},
            //async GetCourseName(academicSpecializationId,subjectId) {
            //    this.Courses=[],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetCourses(academicSpecializationId, subjectId);
            //        this.Courses = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},
            //async GetSupjects() {
            //    this.Subjects=[],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetSupjectName();
            //        this.Subjects = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},
            //async GetInstructors() {
            //    this.Instructors=[],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetInstructorsName();
            //        this.Instructors = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},

            //async GetPaymentMethods() {
            //    this.PaymentMethods=[],
            //    blockUI.Start();
            //    try {
            //        const res = await DataService.GetPaymentMethod();
            //        this.PaymentMethods = res.data.info;
            //        blockUI.Stop();
            //    } catch (err) {
            //        blockUI.Stop();
            //    }
            //},





        }
    }
</script>
