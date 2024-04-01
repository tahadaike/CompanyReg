import Swal from "sweetalert2";
import moment from "moment";
import HelperMixin from '../../../Shared/HelperMixin.vue';

export default {
    name: "Transfer",
    mixins: [HelperMixin],
    async created() {
        await this.CheckLoginStatus();
        this.GetCompanies();
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

            Companies: [],
            Devices: [],

            CompanyId: '',
            DeviceId: '',

            AddDialog: false,
            EditDialog: false,

            SelectedItem: '',


            ruleForm: {
                Id: '',
                CompanyId: '',
                DeviceId: '',
                Name: '',
            },
            rules: {
                CompanyId: this.$helper.Required(),
                DeviceId: this.$helper.Required(),
                Name: this.$helper.Required(),
            },


            
        };
    },
    methods: {

        GetCompanies() {
            this.$blockUI.Start();
            this.$http
                .GetAllCompanies()
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Companies = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        GetDviceById() {
            this.Devices = [];
            this.DeviceId = '';
            this.$blockUI.Start();
            this.$http
                .GetDviceById(this.CompanyId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Devices = response.data.info;
                    this.GetInfo();
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        GetDviceById_Add() {
            this.Devices = [];
            this.ruleForm.DeviceId = '';
            this.$blockUI.Start();
            this.$http
                .GetDviceById(this.ruleForm.CompanyId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Devices = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },

        GetInfo(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http
                .GetFilters(this.pageNo, this.pageSize, this.CompanyId,this.DeviceId)
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
            this.AddDialog = true;
            this.ruleForm.Name = '';
            this.ruleForm.CompanyId = '';
            this.ruleForm.DeviceId = '';
            this.Devices = [];
            this.ruleForm.Id = '';
        },


        //Info
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.ruleForm.Id = Number(0);
                    this.$blockUI.Start();
                    this.$http.AddFilters(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            this.resetForm(formName);
                            this.GetInfo();
                            this.AddDialog = false;
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية الإظافة', err.response.data);
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

        OpentEditDialog(item) {
            this.SelectedItem = item;
            this.ruleForm.Id = item.id;
            this.ruleForm.CompanyId = item.companyId;
            this.ruleForm.DeviceId = item.deviceId;
            this.ruleForm.Name = item.name;
            this.EditDialog = true;
        },

        submitEditForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.ruleForm.Id = Number(this.ruleForm.Id);
                    this.$blockUI.Start();
                    this.$http.EditFilters(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            this.resetForm(formName);
                            this.GetInfo();
                            this.EditDialog = false;
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
                    this.$http.DeleteFilters(Id)
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





        Refresh() {
            this.CompanyId = '';
            this.DeviceId = '';
            this.ruleForm.CompanyId = '';
            this.ruleForm.DeviceId = '';
            this.GetInfo();
        },


    },
};

