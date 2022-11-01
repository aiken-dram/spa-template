import ApiService from "./index";

export default {
  tree() {
    return ApiService.get("r/rscripttree/tree");
  },
  form(id) {
    return ApiService.get("r/rscript/form", id);
  },
};
