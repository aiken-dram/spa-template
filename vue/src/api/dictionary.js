import ApiService from "./index";

export default {
  get(dict) {
    return ApiService.get("admin/dictionary/get", dict);
  },
};
