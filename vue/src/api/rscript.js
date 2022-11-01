import ApiService from "./index";

export default {
  tree: {
    get(id) {
      return ApiService.get("r/rscripttree/get", id);
    },
    upsert(data) {
      return ApiService.post(`r/rscripttree/upsert`, data);
    },
    delete(id) {
      return ApiService.delete(`r/rscripttree/delete/${id}`);
    },
  },
  rscript: {
    get(id) {
      return ApiService.get("r/rscript/get", id);
    },
    upsert(data) {
      return ApiService.post(`r/rscript/upsert`, data);
    },
    delete(id) {
      return ApiService.delete(`r/rscript/delete/${id}`);
    },
  },
};
