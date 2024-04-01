////import Swal from "sweetalert2";
import moment from "moment";
import flatPickr from "vue-flatpickr-component";
import HelperMixin from '../../../Shared/HelperMixin.vue'

export default {
    name: "Add",
    mixins: [HelperMixin],
    async created() {
        await this.CheckLoginStatus();
        this.GetInfo();

        
    },
    components: {
        flatPickr,
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

            Info: '',
            Calculat: '',

            ruleForm: {

                Descriptions: '',
                MaxValue: '',
                PriceOfDay: '',

                PrisonersCount:'',
                valueOfPay: '',
                valueOfDay: '',
                valueOfHour: '',
                
            },
            rules: {
                
                Descriptions: this.$helper.DynamicArabicEnterRequired('وصف عملية الصرف'),
                MaxValue: this.$helper.RequiredInput('القيمة التي سيتم صرفها  '),
                PriceOfDay: this.$helper.RequiredInput('سعر اليوم   '),

            },
            
        };
    },
    
    methods: {


        GetInfo() {
            this.$blockUI.Start();
            this.$http
                .GetCalculatStatic()
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Info = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },


        CalculatInfo() {
            this.$blockUI.Start();
            this.$http
                .CalculatToPay(this.ruleForm.MaxValue, this.ruleForm.PriceOfDay)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.ruleForm.PrisonersCount = response.data.info.prisonersCount;
                    this.ruleForm.valueOfPay = response.data.info.valueOfPay;
                    this.ruleForm.valueOfDay = response.data.info.valueOfDay;
                    this.ruleForm.valueOfHour = response.data.info.valueOfHour;
                                   
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },


        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.ruleForm.MaxValue = Number(this.ruleForm.MaxValue);
                    this.ruleForm.PriceOfDay = Number(this.ruleForm.PriceOfDay);

                    this.$blockUI.Start();
                    this.$http.Pay(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            this.resetForm(formName);
                            this.GetInfo();
                            this.ruleForm.PrisonersCount = '';
                            this.ruleForm.valueOfPay = '';
                            this.ruleForm.valueOfDay ='';
                            this.ruleForm.valueOfHour = '';
                            this.$helper.ShowMessage('success', 'عملية ناجحة', response.data);
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$helper.ShowMessage('error', 'خطأ بعملية الاضافة', err.response.data);
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


    },
};
