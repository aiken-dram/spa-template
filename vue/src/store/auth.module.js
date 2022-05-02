import ApiService from "@/api";
import JwtService from "@/plugins/jwt";
import { LOGIN, LOGOUT, CHECK_AUTH } from "./actions.type";
import { SET_AUTH, PURGE_AUTH, UPDATE_AUTH } from "./mutations.type";

const state = {
  currentUser: {},
  isAuthenticated: !!JwtService.getToken(),
};

const getters = {
  currentUser(state) {
    return state.currentUser;
  },
  isAuthenticated(state) {
    return state.isAuthenticated;
  },
};

const actions = {
  [LOGIN](context, credentials) {
    return new Promise((resolve, reject) => {
      ApiService.post("auth/login", {
        login: credentials.login,
        password: credentials.password,
      })
        .then(({ data }) => {
          context.commit(SET_AUTH, data);
          resolve(data);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },
  [LOGOUT](context) {
    context.commit(PURGE_AUTH);
  },
  [CHECK_AUTH](context) {
    return new Promise((resolve, reject) => {
      if (JwtService.getToken()) {
        ApiService.setHeader();
        if (!state.currentUser.userID) {
          //no user data, get from auth
          ApiService.get("auth")
            .then(({ data }) => {
              context.commit(UPDATE_AUTH, data);
              resolve(data);
            })
            .catch(({ response }) => {
              context.commit(PURGE_AUTH);
              reject(response);
            });
        } else {
          //have user in storage, just check token validation to see if it's not expired and user is active
          ApiService.get("auth/validate")
            .then(() => {
              resolve("Validation successful");
            })
            .catch(({ response }) => {
              context.commit(PURGE_AUTH);
              reject(response);
            });
        }
      } else {
        context.commit(PURGE_AUTH);
        reject("No token in local storage");
      }
    });
  },
};

const mutations = {
  [SET_AUTH](state, data) {
    state.isAuthenticated = true;
    state.currentUser = data.user;
    JwtService.saveToken(data.token);
  },
  [UPDATE_AUTH](state, data) {
    state.isAuthenticated = true;
    state.currentUser = data.user;
  },
  [PURGE_AUTH](state) {
    state.isAuthenticated = false;
    state.currentUser = {};
    JwtService.destroyToken();
  },
};

export default {
  state,
  actions,
  mutations,
  getters,
};
