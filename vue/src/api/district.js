import ApiService from "./index";

export default {
  list() {
    return ApiService.get("admin/district/list");
  },
  get(id) {
    return ApiService.get("admin/district/get", id);
  },
  upsert(data) {
    return ApiService.post("admin/district/upsert", data);
  },
  delete(id) {
    return ApiService.delete(`admin/district/delete/${id}`);
  },
};
