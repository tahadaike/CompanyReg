//import Swal from "sweetalert2";
import moment from "moment";
import HelperMixin from '../../../Shared/HelperMixin.vue'


export default {
    name: "Add",
    mixins: [HelperMixin],
    async created() {
        await this.CheckLoginStatus();
        await this.GetCities();
        await this.GetBank();

        
    },
    
    filters: {
        moment: function (date) {
            if (date === null) {
                return "فارغ";
            }
             //return moment(date).format('MMMM Do YYYY, h:mm:ss a');
            return moment(date).format("MMMM Do YYYY");
        },
    },
    data() {
        return {


            

            dialogImageUrl: '',
            dialogVisible: false,
            disabled: false,


            ruleForm: {
                Id: 0,

                Name: '',
                MotharName: '',
                IsLive: 1,
                IsFinsh: 1,
                
                MaritalStatus:'',
                ChildrenCount:'',
                NID: '',
                Passport: '',

                Gender: '',
                BirthDate: '',
                Address: '',
                Workplace: '',

                IsLiveMothar: '',
                IsLiveFather: '',
                Qualification: '',

                WakelName: '',
                WakelNID: '',
                WakelPhone: '',

                FatherNID: '',
                MotherNID: '',
                WifeNID: '',
                WifeNID1: '',
                WifeNID2: '',
                WifeNID3: '',
                Child1: '',
                Child2: '',
                Child3: '',
                Child4: '',
                Child5: '',
                Child6: '',
                Child7: '',
                Child8: '',
                Child9: '',
                Child10: '',


                HealthIssues: '',
                Disease: '',


                Phone: '',
                BankId:'',
                BankBranchesId:'',
                AcountNumber: '',

                BankId1: '',
                BankBranchesId1:'',
                AcountNumber1: '',

                CityCenterId: '',
                MunicipalitiesId: '',
                OfficeId: '',

                WitnessName: '',
                WitnessNID: '',
                WitnessName1: '',
                WitnessNID1: '',


                StartDate: '',
                EndDate: '',
                StartDate1: null,
                EndDate1: null,
                StartDate2: null,
                EndDate2: null,
                
                //BarCode: '',
                Notes: '',

                Attachments: [],

                ImageName: '',
                ImageType: '',
                fileBase64: '',
            },



            rules: {
                
                Name: this.$helper.DynamicArabicEnterRequired('الاسم الاول'),
                MotharName: this.$helper.DynamicArabicEnterRequired('إسم الاب'),

                MaritalStatus: this.$helper.RequiredInput('الحالة الاجتماعية '),
                ChildrenCount: this.$helper.RequiredInput(' عدد الأطفال '),
                NID: this.$helper.NID(),
                Passport: this.$helper.RequiredInput('رقم جواز السفر او رقم الهوية'),

                Gender: this.$helper.RequiredInput('الجنس'),
                BirthDate: this.$helper.RequiredInput('تاريخ الميلاد'),
                Address: this.$helper.RequiredInput('مكان السكن'),
                Workplace: this.$helper.RequiredInput('مكان العمل'),

                Qualification: this.$helper.RequiredInput('المؤهل العلمي '),

                WakelName: this.$helper.RequiredInput(' الاسم '),
                WakelNID: this.$helper.NID(),
                WakelPhone: this.$helper.RequiredInput(' رقم الهاتف '),

                //FatherNID: this.$helper.NID(),
                //MotherNID: this.$helper.NID(),
                //Child1: this.$helper.NID(),
                //Child2: this.$helper.NID(),
                //Child3: this.$helper.NID(),
                //Child4: this.$helper.NID(),
                //Child5: this.$helper.NID(),
                //Child6: this.$helper.NID(),
                //Child7: this.$helper.NID(),
                //Child8: this.$helper.NID(),
                //Child9: this.$helper.NID(),
                //Child10: this.$helper.NID(),

                HealthIssues: this.$helper.RequiredInput('  مشاكل صحية'),
                Disease: this.$helper.RequiredInput('   المرض'),

                Phone: this.$helper.Phone(),
                BankId: this.$helper.RequiredInput('  المصرف'),
                BankBranchesId: this.$helper.RequiredInput(' فرع المصرف '),
                AcountNumber: this.$helper.RequiredInput('  رقم الحساب '),

                CityCenterId: this.$helper.RequiredInput(' المدينة'),
                MunicipalitiesId: this.$helper.RequiredInput(' البلدية'),
                OfficeId: this.$helper.RequiredInput(' المركز '),

                WitnessName: this.$helper.DynamicArabicEnterRequired(' اسم الشاهد الاول '),
                WitnessNID: this.$helper.RequiredInput(' الرقم الوطني او اتبات الهوية  '),
                WitnessName1: this.$helper.DynamicArabicEnterRequired(' اسم الشاهد التاني '),
                WitnessNID1: this.$helper.RequiredInput(' الرقم الوطني او اتبات الهوية  '),
                
                StartDate: this.$helper.RequiredInput('تاريخ الأسر '),
                EndDate: this.$helper.RequiredInput('تاريخ الخروج '),

            },
        };
    },
    
    methods: {

        async GetMunicipalitiesInfo() {
            this.ruleForm.MunicipalitiesId = '';
            await this.GetMunicipalities(this.ruleForm.CityCenterId)
        },

        async GetOfficesInfo() {
            this.ruleForm.OfficeId = '';
            await this.GetOfficesById(this.ruleForm.MunicipalitiesId)
        },


        async GetBankBranchesByBankIdInfo() {
            this.ruleForm.BankBranchesId = '';
            await this.GetBankBranchesByBankId(this.ruleForm.BankId)
        },

        async GetBankBranchesByBankIdInfo1() {
            this.ruleForm.BankBranchesId1 = '';
            await this.GetBankBranchesByBankId1(this.ruleForm.BankId1)
        },
        



        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.ruleForm.Id = Number(this.ruleForm.Id);
                    if (this.loginDetails.userType != 1) {
                        this.ruleForm.OfficeId = 0;
                    }

                    if (!this.ruleForm.ChildrenCount) {
                        this.ruleForm.ChildrenCount = Number(0);
                    } else {
                        this.ruleForm.ChildrenCount = Number(this.ruleForm.ChildrenCount);
                    }

                    if (!this.ruleForm.IsLiveFather)
                        this.ruleForm.IsLiveFather = false;
                    if (!this.ruleForm.IsLiveMothar)
                        this.ruleForm.IsLiveMothar = false;

                    if (!this.ruleForm.Disease)
                        this.ruleForm.Disease = Number(0);

                    if (!this.ruleForm.BankId1)
                        this.ruleForm.BankBranchesId1 == Number(0);
                        

                    this.$blockUI.Start();
                    this.$http.AddPrisoners(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            this.resetForm(formName);
                            this.RemoveAllAttachment();
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
            this.ruleForm.IsLiveFather = '';
            this.ruleForm.IsLiveMothar = '';
        },


        SelectCoverAttachment(file) {
            let str = file.raw.type;
            str = str.substring(0, 5);

            if (str != "image") {
                this.$helper.ShowMessage('error', 'خطأ بالعملية', 'الرجاء التأكد من نوع الملف');
            }

            var $this = this;
            var reader = new FileReader();
            reader.readAsDataURL(file.raw);
            reader.onload = function () {
                $this.ruleForm.ImageName = file.raw.name;
                $this.ruleForm.ImageType = file.raw.type;
                $this.ruleForm.fileBase64 = reader.result;
            };

        },

        SelectCoverAttachment1(file) {

            let str = file.raw.type;
            str = str.substring(0, 5);


            if (str != "image" && str != "appli" && str != "text/" && str != "video") {
                this.$helper.ShowMessage('error', 'خطأ بالعملية', 'الرجاء التأكد من نوع الملف');
            }

            if (str != "image")
                file.url = this.ServerUrl + '/assets/img/small-logos/file.png';


            //var reader1 = new FileReader();
            //reader.readAsDataURL(file.raw);

            var $this = this;
            var reader = new FileReader();
            reader.readAsDataURL(file.raw);
            reader.onload = function () {

                if ($this.ruleForm && $this.ruleForm.Attachments.length !== 0) {
                    if (!$this.ruleForm.Attachments.some(item => item.ImageName === file.raw.name)) {
                        const obj = {
                            ImageName: file.raw.name,
                            ImageType: file.raw.type,
                            Url: file.url,
                            fileBase64: reader.result,
                        }
                        $this.ruleForm.Attachments.push(obj);
                    } else {
                        $this.$helper.ShowMessage('error', 'خطأ بالعملية', 'اسم الملف موجود مسبقا');
                        const imageUrl = file.url;
                        const imageElement = document.querySelector(`img[src="${imageUrl}"]`);
                        const parentElement = imageElement.parentNode;
                        const grandparentElement = parentElement.parentNode;

                        if (imageElement) {
                            grandparentElement.remove();
                        }
                    }
                } else {
                    const obj = {
                        ImageName: file.raw.name,
                        Url: file.url,
                        ImageType: file.raw.type,
                        fileBase64: reader.result,
                    }
                    $this.ruleForm.Attachments.push(obj);
                }

            };

        },

        handleRemove(file) {

            if (this.ruleForm && this.ruleForm.Attachments.length !== 0) {

                if (this.ruleForm.Attachments.some(item => item.ImageName === file.name)) {
                    const indexToDelete = this.ruleForm.Attachments.findIndex(item => item.ImageName === file.name);

                    if (indexToDelete !== -1) {
                        this.ruleForm.Attachments.splice(indexToDelete, 1);
                    }
                }
            }

            const imageUrl = file.url;
            const imageElement = document.querySelector(`img[src="${imageUrl}"]`);
            const parentElement = imageElement.parentNode;
            const grandparentElement = parentElement.parentNode;

            if (imageElement) {
                grandparentElement.remove();
            }
        },

        handlePictureCardPreview(file) {
            this.dialogImageUrl = file.url;
            this.dialogVisible = true;
        },

        handleDownload(file) {
            const link = document.createElement('a');
            link.href = file.url;
            link.target = '_blank';
            link.download = '';
            link.click();
        },

        RemoveAllAttachment() {
            const elements = document.querySelectorAll('.el-upload-list--picture-card .el-upload-list__item');
            elements.forEach(element => element.remove());
            this.ruleForm.Attachments = [];
        }













    },
};
