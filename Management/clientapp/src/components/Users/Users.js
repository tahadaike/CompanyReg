﻿import Swal from "sweetalert2";
import moment from "moment";
import HelperMixin from '../../Shared/HelperMixin.vue';

export default {
    name: "Transfer",
    mixins: [HelperMixin],
    async created() {
        await this.CheckLoginStatus();
        await this.GetCities();

        this.GetInfo();

        var today = new Date();
        var dd = String(today.getDate()).padStart(2, "0");
        var mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
        var yyyy = today.getFullYear();
        this.dateNow = mm + "/" + dd + "/" + yyyy;

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

        momentTime: function (date) {
            if (date === null) {
                return "فارغ";
            }
            // return moment(date).format('MMMM Do YYYY, h:mm:ss a');
            return moment(date).format('DD/MM/YYYY || h:mm a');
        },
    },
    data() {
        var validatePass = (rules, value, callback) => {
            if (value === '') {
                callback(new Error('الرجاء إدخال كلمة المرور'));
            } else {
                if (this.ruleForm.ConfimPassword !== '') {
                    this.$refs.ruleForm.validateField('ConfimPassword');
                }
                callback();
            }
        };
        var validatePass2 = (rrulesule, value, callback) => {
            if (value === '') {
                callback(new Error('الرجاء كتابه اعاده كلمه المرور'));
            } else if (value !== this.ruleForm.Password) {
                callback(new Error('الرجاء التأكد من تطابق كلمة المرور'));
            } else {
                callback();
            }
        };
        return {
            pageNo: 1,
            pageSize: 10,
            pages: 0,
            state: 0,
            Info: [],

            CityId: '',
            MunicipalitiesId: '',
            OfficeId: '',
            UserType: '',


            AddDialog: false,
            EditDialog: false,
            InfoDialog: false,

            SelectedItem: '',


            ruleForm: {
                Id: '',
                CityId: '',
                MunicipalitiesId: '',
                OfficeId: '',
                Name: '',
                Phone: '',
                LoginName: '',
                Email: '',
                Password: '',
                ConfimPassword: '',
                UserType: '',
            },
            rules: {
                CityId: this.$helper.Required(),
                MunicipalitiesId: this.$helper.Required(),
                OfficeId: this.$helper.Required(),
                Name: this.$helper.DynamicArabicEnterRequired('الاسم '),
                Phone: this.$helper.Phone(),
                LoginName: this.$helper.LoginName(),
                Email: this.$helper.Email(),
                Password: [
                    { validator: validatePass, trigger: 'blur' },
                    { required: true, pattern: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]){8,}.*$/, message: '  تتكون كلمة المرور من حروف صغيرة وكبيرو وأرقام', trigger: 'blur' }
                ],
                ConfimPassword: [
                    { validator: validatePass2, trigger: 'blur' },
                    { required: true, pattern: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]){8,}.*$/, message: ' تتكون كلمة المرور من حروف صغيرة وكبيرو وأرقام', trigger: 'blur' }
                ],
                UserType: this.$helper.Required(),
                KidneyCentersId: this.$helper.Required(),
            },


            
        };
    },
    methods: {

        async GetMunicipalitiesInfo() {
            this.MunicipalitiesId = '';
            this.OfficeId = '';
            this.Offices = [];
            this.GetInfo();
            await this.GetMunicipalities(this.CityId);
        },


        async GetOffices() {
            this.OfficeId = '';
            this.GetInfo();
            await this.GetOfficesById(this.MunicipalitiesId);
        },


        async GetMunicipalities_Add() {
            this.ruleForm.MunicipalitiesId = '';
            this.ruleForm.OfficeId = '';
            this.Offices = [];
            await this.GetMunicipalities(this.ruleForm.CityId);
            
        },

        async GetOffices_Add() {
            this.ruleForm.OfficeId = '';
            await this.GetOfficesById(this.ruleForm.MunicipalitiesId)
        },


        async GetMunicipalities_Edit() {
            this.ruleForm.MunicipalitiesId = '';
            this.ruleForm.OfficeId = '';
            this.Offices = [];
            await this.GetMunicipalitiesByCiteisID(this.ruleForm.CityId);

        },

        async GetOffice_Edit() {
            this.ruleForm.OfficeId = '';
            await this.GetOfficesById(this.ruleForm.MunicipalitiesId);

        },



        GetInfo(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http
                .GetUsers(this.pageNo, this.pageSize, this.UserType,this.CityId,this.MunicipalitiesId, this.OfficeId)
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

        OpenAddDialog() {
            this.state = 1;
            this.ruleForm.Id = '';
            this.ruleForm.CityId = '';
            this.ruleForm.MunicipalitiesId = '';
            this.ruleForm.OfficeId = '';
            this.ruleForm.Name = '';
            this.ruleForm.Phone = '';
            this.ruleForm.LoginName = '';
            this.ruleForm.Email = '';
            this.ruleForm.Password = '';
            this.ruleForm.ConfimPassword = '';
            this.ruleForm.UserType = '';
        },

        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {

                    if (this.ruleForm.Password != this.ruleForm.ConfimPassword) {
                        this.$helper.ShowMessage('error', 'خطأ بالعملية', 'الرجاء التأكد من تطابق كلمة المرور');
                        return;
                    } else {

                        if (this.ruleForm.UserType != 2) {
                            this.ruleForm.OfficeId = Number(1);
                        }
                        this.ruleForm.Id = Number(0);
                        this.$blockUI.Start();
                        this.$http.AddUser(this.ruleForm)
                            .then(response => {
                                this.$blockUI.Stop();
                                this.resetForm(formName);
                                this.$helper.ShowMessage('success', ' تمت عملية التسجيل بنجاح', response.data);
                                this.GetInfo();
                                this.state = 0;
                            })
                            .catch((err) => {
                                this.$blockUI.Stop();
                                this.$helper.ShowMessage('error', 'خطأ بعملية الاضافة', err.response.data);
                            });
                    }

                } else {
                    this.$helper.showSwal('warning');
                    return false;
                }
            });
        },

        resetForm(formName) {
            this.$refs[formName].resetFields();
        },

        OpentEditDialog(item) {
            this.SelectedItem = item;
            this.ruleForm.Id = item.id;
            this.ruleForm.Name = item.name;
            this.ruleForm.LoginName = item.loginName;
            this.ruleForm.Phone = '0' + item.phone;
            this.ruleForm.Email = item.email;
            this.ruleForm.UserType = item.userType;
            this.ruleForm.OfficeId = item.officeId;
            if (this.ruleForm.OfficeId > 0) {
                this.$blockUI.Start();
                this.$http
                    .GetOfficesById(this.ruleForm.OfficeId)
                    .then((response) => {
                        this.$blockUI.Stop();
                        this.ruleForm.CityId = response.data.info.cityId;
                        this.ruleForm.MunicipalitiesId = response.data.info.municipalityId;
                        this.GetMunicipalities_Edit();
                        this.GetOffice_Edit();
                    })
                    .catch(() => {
                        this.$blockUI.Stop();
                    });
            }
            
            
            this.EditDialog = true;
        },

        submitEditForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.ruleForm.Id = Number(this.ruleForm.Id);
                    if (this.ruleForm.UserType != 2) {
                        this.ruleForm.OfficeId = Number(1);
                    }
                    this.$blockUI.Start();
                    this.$http.EditUser(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            this.resetForm(formName);
                            this.$helper.ShowMessage('success', ' تمت عملية التعديل بنجاح', response.data);
                            this.GetInfo();
                            this.EditDialog = false;
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
                    this.$http.DeleteUser(Id)
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

        ResetPassword(Id) {
            Swal.fire({
                title: 'هـل انت متأكد من تهيئة كلمة المرور ؟',
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.RestePassword(Id)
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

        DeactivateUser(Id) {
            Swal.fire({
                title: 'هـل انت متأكد من ايقاف تفعيل المستخدم ؟',
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.ChangeStatusUser(Id)
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

        ActivateUser(Id) {
            Swal.fire({
                title: 'هـل انت متأكد من  تفعيل المستخدم ؟',
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: `تأكيد العملية`,
                denyButtonText: `الغاء العملية`,
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$blockUI.Start();
                    this.$http.ChangeStatusUser(Id)
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






        Back() {
            this.state = 0;
            this.Refresh();
        },

        Refresh() {
            this.CityId = '';
            this.MunicipalitiesId = '';
            this.OfficeId = '';
            this.ruleForm.CityId = '';
            this.ruleForm.MunicipalitiesId = '';
            this.ruleForm.OfficeId = '';
            this.UserType = '';
            this.GetInfo();
        },

    },
};
