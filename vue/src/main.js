import Vue from "vue";
import App from "./App.vue";
import i18n from "./plugins/i18n";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";

import "roboto-fontface/css/roboto/roboto-fontface.css";
import "@mdi/font/css/materialdesignicons.css";
import "@fortawesome/fontawesome-free/css/all.css";

import filters from "./common/filters";
import ApiService from "./api";

Vue.config.productionTip = false;
Object.keys(filters).forEach((key) => Vue.filter(key, filters[key]));

ApiService.init();

new Vue({
  i18n,
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
