import Vue from "vue";
import Vuex from "vuex";

import auth from "./auth.module";
import dictionary from "./dictionary.module";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    drawer: null,
    minify: true,
    loading: false,
    overlay: false,
    appLoad: true,
  },
  mutations: {
    SET_DRAWER(state, payload) {
      state.drawer = payload;
    },
    SET_MINIFY(state, payload) {
      state.minify = payload;
    },
    SET_LOADING(state, payload) {
      state.loading = payload;
    },
    SET_OVERLAY(state, payload) {
      state.overlay = payload;
    },
    APP_LOADED(state) {
      state.appLoad = false;
    },
  },
  modules: {
    auth,
    dictionary,
  },
});
