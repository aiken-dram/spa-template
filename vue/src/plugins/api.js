import Vue from "vue";
import axios from "axios";
import qs from "qs";
import VueAxios from "vue-axios";
import JwtService from "@/plugins/jwt";

import { API_URL } from "@/common/config";

//function to process error server response
function catchModelState(error) {
  if (error.response) {
    if (error.response.status == 400) {
      //BadResponse()
      //console.log(error.response.data);
      throw error.response.data;
    } else if (error.response.status == 403) {
      //403 forbidden
      if (error.response.data && error.response.data.error)
        throw { Error: `Error 403: ${error.response.data.error}` };
      else throw { Error: `Error 403: No access` };
    } else if (error.response.status == 404) {
      //404 not found
      if (error.response.data && error.response.data.error)
        throw { Error: `Error 404: ${error.response.data.error}` };
      else throw { Error: `Error 404: Web API not found` };
    } else if (error.response.status == 500) {
      //500 internal server error
      //if download the response data will be blob so error wont display
      //if (error.response.data.toString() === "[object Blob]") {
      throw { Error: `Error 500: ${error.response.data.error}` };
    } else {
      //something else
      throw {
        Error: `Error ${error.response.status}: ${error.response.statusText}`,
      };
    }
  } else if (error.request) {
    // The request was made but no response was received
    throw { Error: `Error while awaiting server response: ${error.message}` };
  } else {
    // Something happened in setting up the request that triggered an Error
    throw { Error: `Error while executing request: ${error.message}` };
  }
}

const ApiService = {
  init() {
    Vue.use(VueAxios, axios);
    Vue.axios.defaults.baseURL = API_URL;
    Vue.axios.defaults.paramsSerializer = (p) => {
      return qs.stringify(p, { arrayFormat: "repeat", encode: false });
    };
  },

  setHeader() {
    Vue.axios.defaults.headers.common[
      "Authorization"
    ] = `Bearer ${JwtService.getToken()}`;
  },

  query(resource, params) {
    return Vue.axios.get(resource, params).catch(catchModelState);
  },

  get(resource, id = "") {
    return Vue.axios.get(`${resource}/${id}`).catch(catchModelState);
  },

  post(resource, params) {
    return Vue.axios.post(`${resource}`, params).catch(catchModelState);
  },

  update(resource, id, params) {
    return Vue.axios.put(`${resource}/${id}`, params).catch(catchModelState);
  },

  put(resource, params) {
    return Vue.axios.put(`${resource}`, params).catch(catchModelState);
  },

  delete(resource) {
    return Vue.axios.delete(resource).catch(catchModelState);
  },

  download(resource, params) {
    return Vue.axios
      .get(resource, {
        params: params,
        responseType: "blob", // important
      })
      .catch(catchModelState);
  },

  upload(resource, data) {
    return Vue.axios
      .post(resource, data, {
        headers: { "Content-Type": "multipart/form-data" },
      })
      .catch(catchModelState);
  },
};

export default ApiService;

export const JsonService = {
  versions() {
    return Vue.axios({ url: "versions.json", baseURL: "/app/json/" });
  },
  contacts() {
    return Vue.axios({ url: "contacts.json", baseURL: "/app/json/" });
  },
};

export const HealthService = {
  health() {
    return Vue.axios({ url: "health", baseURL: "/" });
  },
};

export const DictionaryService = {
  get(dict) {
    return ApiService.get("admin/dictionary/get", dict);
  },
};

export const UserService = {
  table(params) {
    return ApiService.query("admin/user/table", {
      params: params,
    });
  },
  tablecsv(params) {
    return ApiService.download("admin/user/tablecsv", params);
  },
  authtable(params) {
    return ApiService.query("admin/user/authtable", {
      params: params,
    });
  },
  get(id) {
    return ApiService.get("admin/user/get", id);
  },
  current() {
    return ApiService.get("admin/user/current");
  },
  edit(data) {
    return ApiService.post(`admin/user/upsert`, data);
  },
  update(data) {
    return ApiService.post("admin/user/update", data);
  },
  delete(id) {
    return ApiService.delete(`admin/user/delete/${id}`);
  },
  upload(data) {
    return ApiService.upload("admin/user/upload", data);
  },
};
