import Vue from "vue";
import Vuetify from "vuetify/lib";
import InputFacade from "vue-input-facade";

Vue.use(Vuetify);
Vue.use(InputFacade);

export default new Vuetify({
  icons: {
    iconfont: "fa",
  },
});
