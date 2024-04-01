////import Swal from "sweetalert2";
import moment from "moment";
import HelperMixin from '../../../../Shared/HelperMixin.vue';

export default {
    name: "Transfer",
    mixins: [HelperMixin],
    async created() {
        await this.CheckLoginStatus();
        await this.GetCities();

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

            CityId: '',
            MunicipalitiesId: '',
            OfficeId: '',

            Prisoners: [],
            PrisonersId: '',
            PrisonerStatus: '',

            SelectedItem: '',

            
        };
    },
    methods: {

        async GetMunicipalitiesInfo() {
            this.Municipalities = [];
            this.MunicipalitiesId = '';
            this.Offices = [];
            this.OfficeId = '';
            this.GetInfo();
            await this.GetMunicipalities(this.CityId)
        },

        async GetOfficesInfo() {
            this.Offices = [];
            this.OfficeId = '';
            this.GetInfo();
            await this.GetOfficesById(this.MunicipalitiesId)
        },


        FilterByNid() {
            this.Prisoners = [];
            let code = "";
            if (document.getElementById('selectInputNid') != null) {

                if (document.getElementById('selectInputNid').value == null || document.getElementById('selectInputNid').value == '')
                    return;

                code = document.getElementById('selectInputNid').value;
            }
            if (code.length <= 3)
                return;

            this.$blockUI.Start();
            this.$http.GetPrisonersById(code)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Prisoners = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    //console.error(err);
                });
        },

        FilterByPass() {
            this.Prisoners = [];
            let code = "";
            if (document.getElementById('selectInputPass') != null) {

                if (document.getElementById('selectInputPass').value == null || document.getElementById('selectInputPass').value == '')
                    return;

                code = document.getElementById('selectInputPass').value;
            }
            if (code.length <= 3)
                return;

            this.$blockUI.Start();
            this.$http.GetPrisonersById(code)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Prisoners = response.data.info;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                    //console.error(err);
                });
        },



        GetInfo(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }
            this.$blockUI.Start();
            this.$http
                .PayInfoTransactions(this.pageNo, this.pageSize, this.CityId, this.MunicipalitiesId, this.OfficeId, this.PrisonerStatus, this.PrisonersId)
                .then((response) => {
                    this.$blockUI.Stop();
                    this.Info = response.data.info;
                    this.pages = response.data.count;
                })
                .catch(() => {
                    this.$blockUI.Stop();
                });
        },


        Refresh() {
            this.PrisonerStatus = '';
            this.CityId = '';
            this.Municipalities = '';
            this.MunicipalitiesId = '';
            this.Offices = '';
            this.OfficeId = '';
            this.PrisonersId = '';
            this.Prisoners = [];
            this.GetInfo();
        },

    },
};
