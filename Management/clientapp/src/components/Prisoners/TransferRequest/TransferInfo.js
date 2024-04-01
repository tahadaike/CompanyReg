import Swal from "sweetalert2";
import moment from "moment";
import HelperMixin from '../../../Shared/HelperMixin.vue';

export default {
    name: "Transfer",
    mixins: [HelperMixin],
    async created() {
        await this.CheckLoginStatus();
        this.GetCities();
        this.GetInfo();

    },
    components: {
    },
    filters: {
        moment: function (date) {
            if (date === null) {
                return "فارغ";
            }
            // return moment(date).format('MMMM Do YYYY, h:mm:ss a');
            return moment(date).format("MMMM Do YYYY");
        },
    },
    data() {
        return {
            pageNo: 1,
            pageSize: 10,
            pages: 0,
            state: 0,
            Info: [],
            Attachment: [],

            Cities: [],
            Municipalities: [],
            KidnyCenter: [],

            CityId: '',
            MunicipalitiesId: '',
            KidneyCenterId: '',


            Patient: [],
            PatientId: '',
            Level: '',

            SelectedItem: '',

            AddDialog: false,
            AttachmentDilog: false,
            RejectDiloag: false,
            InfoDialog: false,




            ruleForm: {
                PatientId: '',
                CityCenterId: '',
                MunicipalitiesId: '',
                KidneyCenterId: '',
                Note: '',
            },
            rules: {
                PatientId: this.$helper.Required(),
                CityCenterId: this.$helper.Required(),
                MunicipalitiesId: this.$helper.Required(),
                KidneyCenterId: this.$helper.Required(),
                Note: this.$helper.Required(),
            },

            ruleFormAttahcment: {
                Id: '',
                ImageName: '',
                ImageType: '',
                fileBase64: '',
            },

            ruleFormReject: {
                Id: '',
                Resone: '',
            },














            //MunicipalitiesId: "",
            //CityId2: "",
            //citis: [],
            //KidnyCenter: [],
            //KidnyCenterId: "",
            //FileNumber: "",
            
            InfoAttachments: [],
            //KidneyCenters: [],

            //loginDetails: null,

            //Municipalities: [],
            //MunicipalitId: "",

            //Hospitals: [],
            //SelectedHospitalsId: "",

            ViewDilog: false,
            
           

            ViewInfoDialog: false,
            

            AddViewDilogRequest: false,

            //ruleForm: {
            //    PatientsChangeCenterId: "",
            //    fileList: [],
            //    fileBase64: [],
            //},
            //rules: {
            //    fileList: [
            //        {
            //            required: true,
            //            message: "الرجاء اختيار  اضافة الملف ",
            //            trigger: "blur",
            //        },
            //    ],
            //},

            
        };
    },
    methods: {

        GetCities() {
            this.$blockUI.Start();
            this.$http
                .GetCities()
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Cities = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        GetMunicipalities() {
            this.MunicipalitiesId = '';
            this.Municipalities = [];
            this.KidnyCenter = [];
            this.KidnyCenterId = '';
            this.GetInfo();
            this.$blockUI.Start();
            this.$http
                .GetMunicipalitiesByCiteisID(this.CityId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Municipalities = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        GetKednyCenter() {
            this.KidnyCenter = [];
            this.KidnyCenterId = '';
            this.GetInfo();
            this.$blockUI.Start();
            this.$http
                .GetKednyCenterByCitie(this.MunicipalitiesId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.KidnyCenter = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        Add_GetMunicipalities() {
            this.ruleForm.MunicipalitiesId = '';
            this.Municipalities = [];
            this.KidnyCenter = [];
            this.ruleForm.KidnyCenterId = '';
            this.$blockUI.Start();
            this.$http
                .GetMunicipalitiesByCiteisID(this.ruleForm.CityCenterId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Municipalities = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        Add_GetKednyCenter() {
            this.KidnyCenter = [];
            this.ruleForm.KidnyCenterId = '';
            this.$blockUI.Start();
            this.$http
                .GetKednyCenterByCitie(this.ruleForm.MunicipalitiesId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.KidnyCenter = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },


        FilterByName() {
            this.Patient = [];
            let code = "";
            if (document.getElementById('selectInputName') != null) {

                if (document.getElementById('selectInputName').value == null || document.getElementById('selectInputName').value == '')
                    return;

                code = document.getElementById('selectInputName').value;
            }
            if (code.length <= 3)
                return;

            this.$blockUI.Start();
            this.$http.GetPatientById(code)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Patient = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    //console.error(err);
                });
        },

        FilterByNid() {
            this.Patient = [];
            let code = "";
            if (document.getElementById('selectInputNid') != null) {

                if (document.getElementById('selectInputNid').value == null || document.getElementById('selectInputNid').value == '')
                    return;

                code = document.getElementById('selectInputNid').value;
            }
            if (code.length <= 3)
                return;

            this.$blockUI.Start();
            this.$http.GetPatientById(code)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Patient = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    //console.error(err);
                });
        },

        FilterByPass() {
            this.Patient = [];
            let code = "";
            if (document.getElementById('selectInputPass') != null) {

                if (document.getElementById('selectInputPass').value == null || document.getElementById('selectInputPass').value == '')
                    return;

                code = document.getElementById('selectInputPass').value;
            }
            if (code.length <= 3)
                return;

            this.$blockUI.Start();
            this.$http.GetPatientById(code)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Patient = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    //console.error(err);
                });
        },

        FilterByFileNumber() {
            this.Patient = [];
            let code = "";
            if (document.getElementById('selectInputFileNumber') != null) {

                if (document.getElementById('selectInputFileNumber').value == null || document.getElementById('selectInputFileNumber').value == '')
                    return;

                code = document.getElementById('selectInputFileNumber').value;
            }
            if (code.length <= 3)
                return;

            this.$blockUI.Start();
            this.$http.GetPatientById(code)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Patient = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    //console.error(err);
                });
        },


        //Info
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.ruleForm.PatientId = Number(this.ruleForm.PatientId);
                    this.$blockUI.Start();
                    this.$http.AddChangeRequest(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            this.resetForm(formName);
                            this.GetInfo();
                            this.AddDialog = false;
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية التعديل', err.response.data);
                        });


                } else {
                    this.$helper.showSwal('warning');
                    return false;
                }
            });
        },

        resetForm(formName) {
            this.$refs[formName].resetFields();
        },
        
        GetInfo(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http
                .GetChangeRequest(this.pageNo, this.pageSize, this.CityId, this.MunicipalitiesId, this.KidneyCenterId, this.Level,this.PatientId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Info = response.data.info;
                    this.pages = response.data.count;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    this.pages = 0;
                });
        },

        Refresh() {
            this.Level = '';
            this.Patient = [];
            this.PatientId = '';
            this.GetInfo();
        },

        Accept(Id) {
            Swal.fire({
                title: "هـل انت متأكد من الموافقة على عملية الانتقال  ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.AcceptChangeRequestAttachment(Id)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                            this.GetInfo();
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية الحذف', err.response.data);
                        });
                    return;
                }
            });
        },

        OpenRejectDiloag(item) {
            this.SelectedItem = item;
            this.RejectDiloag = true;
        },

        Reject() {
            this.ruleFormReject.Id = Number(this.SelectedItem.id);
            if (!this.ruleFormReject.Resone) {
                this.$helper.showSwal('warning');
                return false;
            }

            Swal.fire({
                title: "هـل انت متأكد من رفض على عملية الانتقال  ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.RejectChangeRequest(this.ruleFormReject)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                            this.GetInfo();
                            this.RejectDiloag = false;
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية الحذف', err.response.data);
                        });
                    return;
                }
            });
        },

        OpenInfoDialog(item) {
            this.SelectedItem = item;
            this.InfoDialog = true;
        },

        Delete(Id) {
            Swal.fire({
                title: "هـل انت متأكد من عملية الحذف  ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.DeleteChangeRequest(Id)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                            this.GetInfo();
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية الحذف', err.response.data);
                        });
                    return;
                }
            });
        },






        //Attachment
        OpenAttachmentDiloag(item) {
            this.SelectedItem = item;
            this.AttachmentDilog = true;
        },

        SelectAttachment(file) {

            let str = file.raw.type;
            let type2 = str.substring(0, 5);

            if (str != "application/vnd.openxmlformats-officedocument.presentationml.presentation" &&
                str != "application/pdf" &&
                type2 != "image" &&
                type2 != "video") {
                this.$helper.ShowMessage('error', 'خطأ بالعملية', 'الرجاء التأكد من نوع الملف');
            }
            var $this = this;
            var reader = new FileReader();
            reader.readAsDataURL(file.raw);
            reader.onload = function () {
                $this.ruleFormAttahcment.ImageName = file.raw.name;
                $this.ruleFormAttahcment.ImageType = file.raw.type;
                $this.ruleFormAttahcment.fileBase64 = reader.result;
            };

            this.ruleFormAttahcment.Id = this.SelectedItem.id;
        },

        AddAttachment() {
            this.ruleFormAttahcment.Id = this.SelectedItem.id;
            this.$blockUI.Start();
            this.$http.AddChangeRequestAttahcment(this.ruleFormAttahcment)
                .then(response => {
                    this.$blockUI.Stop();
                    this.ruleFormAttahcment.Id = '';
                    this.ruleFormAttahcment.ImageName = '';
                    this.ruleFormAttahcment.ImageType = '';
                    this.ruleFormAttahcment.fileBase64 = '';
                    this.GetChangeRequestAttachment();
                    this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$helper.ShowMessage('error', 'خطأ بعملية الإضافة', err.response.data);
                });
        },

        GetChangeRequestAttachment() {
            this.$blockUI.Start();
            this.$http
                .GetChangeRequestAttachment(this.SelectedItem.id)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Attachment = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        DeleteChangeRequestAttachment(Id) {
            Swal.fire({
                title: "هـل انت متأكد من عملية الحذف ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.DeleteChangeRequestAttachment(Id)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                            this.GetChangeRequestAttachment();
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية الحذف', err.response.data);
                        });
                    return;
                }
            });
        },

































        GetByFileNumber() {
            //   this.$blockUI.Start();
            //   return this.$http
            //     .GetByFileNumber(Number(this.FileNumber))
            //     .then((response) => {
            //       this.$blockUI.Stop();
            //       this.Info = response.data.info;
            //       this.pages = response.data.count;
            //     })
            //     .catch(() => {
            //       this.$blockUI.Stop();
            //     });
        },


        //submitFormAddChangeRequest(formName) {
        //    this.$refs[formName].validate((valid) => {
        //        if (valid) {
        //            this.AddItemChagenRequest(formName);
        //        } else {
        //            return false;
        //        }
        //    });
        //},



        //AddItemChagenRequest(formName) {
        //    this.$blockUI.Start();
        //    this.$http
        //        .AddItemChagenRequest(this.AddChangeRequestruleForm)
        //        .then((response) => {
        //            this.resetForm(formName);
        //            this.$blockUI.Stop();
        //            this.AddViewDilogRequest = false;
        //            this.AddChangeRequestruleForm.Note = "";
        //            Swal.fire({
        //                icon: "success",
        //                title: "..نجـاح العملية",
        //                // text: '<strong>Something went wrong!</strong>',
        //                html: response.data,
        //                // showCloseButton: true,
        //                showCancelButton: false,
        //                //confirmButtonText: `حـفظ`,
        //                //denyButtonText: `مواق`,
        //            }).then(() => {
        //                this.GetInfo(this.pageNo);
        //            });
        //        })
        //        .catch((err) => {
        //            this.AddViewDilogRequest = false;

        //            this.$blockUI.Stop();
        //            this.$helper.showWorning(err.response.data);
        //        });
        //},

        OpenAddViewDilogRequest() {
            this.AddViewDilogRequest = true;
        },

        ViewAttachmentDilog(item) {
            this.SelectedItem = item;
            this.AttachmentDilog = true;
            this.GetAttachment();
        },

        GetKidneyCenters() {
            this.$blockUI.Start();
            this.$http
                .GetKidneyCentersNames()
                .then((response) => {
                    this.$blockUI.Stop();
                    this.KidneyCenters = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        GetAttachment() {
            this.$blockUI.Start();
            this.$http
                .GetChangeAttachments(this.SelectedItem.id)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.InfoAttachments = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        beforeRemove(file) {
            return this.$confirm(`حذف الملف التالي :  ${file.name} ?`);
        },

        onBeforeUpload(file) {
            const isCorrectFormat =
                file.type === "image/jpg" ||
                "image/png" ||
                ".pdf" ||
                ".docx" ||
                ".xlsx" ||
                ".csv" ||
                ".jpg";

            if (!isCorrectFormat) {
                Swal.fire({
                    icon: "warning",
                    title: "..الرجاء التاكد من البيانات",
                    // text: '<strong>Something went wrong!</strong>',
                    html: "يجب ان يكون امتداد الملف png,jpg,docx,xlsx,.csv.pdf",
                    // showCloseButton: true,
                    showCancelButton: false,
                    //confirmButtonText: `حـفظ`,
                    //denyButtonText: `مواق`,
                }).then(() => {
                    return;
                });
                return;
            }
            return isCorrectFormat;
        },

        handleExceed() {
            this.$message.warning(`لايمكن تحميل أكتر من  ملف `);
        },
        handleRemoveFile(file) {
            var $this = this;
            var reader = new FileReader();
            reader.readAsDataURL(file.raw);
            reader.onload = function () {
                $this.ruleForm.fileList.splice(
                    $this.ruleForm.fileList.indexOf(reader.result),
                    1
                );
            };
        },
        FileChanged(file) {
            var $this = this;
            var reader = new FileReader();
            reader.readAsDataURL(file.raw);
            reader.onload = function () {
                var obj = {
                    fileName: file.raw.name,
                    fileBase64: reader.result,
                };
                $this.ruleForm.fileList.push(obj);
            };
        },

        //submitForm(formName) {
        //    this.$refs[formName].validate((valid) => {
        //        if (valid) {
        //            this.AddItem(formName);
        //        } else {
        //            return false;
        //        }
        //    });
        //},
        //resetForm(formName) {
        //    this.$refs[formName].resetFields();
        //    this.ruleForm.fileList = [];
        //},

        AddItem(formName) {
            this.ruleForm.PatientsChangeCenterId = this.SelectedItem.id;
            this.ruleForm.fileBase64 = this.ruleForm.fileList;

            this.$blockUI.Start();
            this.$http
                .AddChangeAttachments(this.ruleForm)
                .then((response) => {
                    this.resetForm(formName);
                    this.$blockUI.Stop();
                    this.AttachmentDilog = false;
                    Swal.fire({
                        icon: "success",
                        title: "..نجـاح العملية",
                        // text: '<strong>Something went wrong!</strong>',
                        html: response.data,
                        // showCloseButton: true,
                        showCancelButton: false,
                        //confirmButtonText: `حـفظ`,
                        //denyButtonText: `مواق`,
                    }).then(() => {
                        this.GetAttachment();
                    });
                })
                .catch((err) => {
                    this.AttachmentDilog = false;
                    this.$blockUI.Stop();
                    this.$helper.showWorning(err.response.data);
                });
        },

        ViewInfo(item) {
            this.SelectedItem = item;
            this.ViewInfoDialog = true;
        },

        RejectApp() {
            this.ViewDilog = false;

            Swal.fire({
                title: "هـل انت متأكد من عملية الرفض ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http
                        .RejectChangeRequest(this.Reject)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.FormPorgress = 100;
                            Swal.fire({
                                icon: "success",
                                title: "..نجـاح العملية",
                                html: response.data,
                                showCancelButton: false,
                                //confirmButtonText: `حـفظ`,
                                denyButtonText: `خروج`,
                            }).then(() => {
                                this.$blockUI.Stop();
                                this.GetInfo(this.pageNo);
                                this.Reject.PatientsChangeCenterId = "";
                                this.Reject.Resone = "";
                                this.SelectedItem = "";
                            });
                        })
                        .catch((err) => {
                            this.ViewDilog = false;

                            this.$blockUI.Stop();
                            this.$notify({
                                title: "خطأ بعملية الرفض",
                                dangerouslyUseHTMLString: true,
                                type: "error",
                                message: err.response.data,
                            });
                        });
                    return;
                }
            });
        },

        

        ViewDialogIfno(item) {
            this.ViewDilog = true;
            this.SelectedItem = item;
            this.Reject.PatientsChangeCenterId = item.id;
        },

        

        deleteItem(id) {
            Swal.fire({
                title: "هـل انت متأكد من عملية الحذف ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http
                        .RemoveCahngeAttachments(id)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.FormPorgress = 100;
                            Swal.fire({
                                icon: "success",
                                title: "..نجـاح العملية",
                                html: response.data,
                                showCancelButton: false,
                                //confirmButtonText: `حـفظ`,
                                denyButtonText: `خروج`,
                            }).then(() => {
                                this.$blockUI.Stop();
                                this.GetInfo();
                            });
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.showWorning(err.response.data);
                        });
                    return;
                }
            });
        },

        deleteItemAttachment(id) {
            Swal.fire({
                title: "هـل انت متأكد من عملية الحذف ؟",
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http
                        .deleteItemAttachment(id)
                        .then((response) => {
                            this.$blockUI.Stop();
                            this.FormPorgress = 100;
                            Swal.fire({
                                icon: "success",
                                title: "..نجـاح العملية",
                                html: response.data,
                                showCancelButton: false,
                                //confirmButtonText: `حـفظ`,
                                denyButtonText: `خروج`,
                            }).then(() => {
                                this.$blockUI.Stop();
                                this.GetInfo();
                            });
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.showWorning(err.response.data);
                        });
                    return;
                }
            });
        },
    },
};
