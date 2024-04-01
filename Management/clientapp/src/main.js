import Vue from 'vue';
import VueI18n from 'vue-i18n'
import VueRouter from 'vue-router';
import ElementUI from 'element-ui';
import Vuetify from 'vuetify'
import locale from 'element-ui/lib/locale/lang/en'
import BlockUIService from './Shared/BlockUIService.js';
import App from './App.vue';
import Layout from './components/Layout/Layout.vue';
import Login from './components/Login/Login.vue';
import Home from './components/Home/Home.vue';
import DataService from './Shared/DataService';
import Helper from './Shared/Helper';

import Users from './components/Users/Users.vue';
import Profile from './components/Users/EditUsersProfile/EditUsersProfile.vue'


import Prisoners from "./components/Prisoners/Prisoners.vue"
import AddPrisoners from "./components/Prisoners/Add/Add.vue"
import Pay from "./components/Prisoners/Pay/Pay.vue"
import PayInfo from "./components/Prisoners/Pay/PayInfo/PayInfo.vue"
import PayInfoTransactions from "./components/Prisoners/Pay/PayInfoTransactions/PayInfoTransactions.vue"
import company from "./components/company/company.vue"
import Issuse from "./components/Issuse/Issuse.vue"
import Transactions from "./components/Transactions/Transactions.vue"



//import PatientTransacitons from "./components/Prisoners/Transactions/Transactions.vue"
//import PatientTransfer from './components/Prisoners/TransferRequest/TransferInfo.vue';
//import PatientTransferRequest from './components/Prisoners/TransferRequest/Add/TransferRequest.vue';
//import PatientTransferRequestCenter from './components/Prisoners/TransferRequest/CenterChangeRequest/TransferRequest.vue';
//import PatientDailyUsed from "./components/Prisoners/DailyUsed/DailyUsed.vue";
//import PatientDailyUsedReport from "./components/Prisoners/DailyUsedReport/DailyUsedReport.vue";
//import Required from "./components/Prisoners/DailyUsedRequired/DailyUsedRequired.vue";



import Offices from './components/Dictionaries/Offices/Offices.vue';
import Municipalities from './components/Dictionaries/Municipalities/Municipalities.vue';
import Cities from './components/Dictionaries/Cities/Cities.vue';
import Bank from './components/Dictionaries/Bank/Bank.vue';
import BankBranches from './components/Dictionaries/BankBranches/BankBranches.vue';






import VueEllipseProgress from 'vue-ellipse-progress';

Vue.use(VueEllipseProgress);

Vue.use(Vuetify);
Vue.use(VueI18n);
Vue.use(VueRouter);
Vue.use(ElementUI, { locale });

Vue.config.productionTip = false;

Vue.prototype.$http = DataService;
Vue.prototype.$blockUI = BlockUIService;
Vue.prototype.$helper = Helper;

export const eventBus = new Vue();

//const i18n = new VueI18n({
//    locale: 'ar', // set locale
//    messages, // set locale messages
//})

const router = new VueRouter({
    mode: "history",
    base: __dirname,
    linkActiveClass: "active",
    routes: [
        {
            path: "/Login",
            component: Login,
        },
        {
            path: "/",
            component: App,
            children: [
                {
                    path: "",
                    component: Layout,
                    children: [
                        { path: "", component: Home },
                        { path: "Prisoners", component: Prisoners },
                        { path: "AddPrisoners", component: AddPrisoners },
                        { path: "Pay", component: Pay },
                        { path: "PayInfo", component: PayInfo },
                        { path: "PayInfoTransactions", component: PayInfoTransactions },


                        //{ path: "PatientTransacitons", component: PatientTransacitons },
                        //{ path: "PatientTransfer", component: PatientTransfer },
                        //{ path: "PatientTransferRequest", component: PatientTransferRequest },
                        //{ path: "PatientTransferRequestCenter", component: PatientTransferRequestCenter },
                        //{ path: "PatientDailyUsed", component: PatientDailyUsed },
                        //{ path: "PatientDailyUsedReport", component: PatientDailyUsedReport },
                        //{ path: "Required", component: Required, },

                        
                        { path: "company", component: company },
                        { path: "Issuse", component: Issuse },
                        { path: "Transactions", component: Transactions },

                        
                        { path: "Offices", component: Offices },
                        { path: "Municipalities", component: Municipalities },
                        { path: "Cities", component: Cities },
                        { path: "Bank", component: Bank },
                        { path: "BankBranches", component: BankBranches },

                        { path: "Users", component: Users },
                        { path: "Profile", component: Profile },

                        
                    ],
                },
            ],
        },
    ],
});

Vue.filter("toUpperCase", function (value) {
    if (!value) return "";
    return value.toUpperCase();
});

new Vue({
    router,
    render: (h) => {
        return h(App);
    },
}).$mount("#cpanel-management");
