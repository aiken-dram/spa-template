import ApiService from "./index";

export default {
  table(params) {
    return ApiService.query("admin/user/table", {
      params: params,
    });
  },
  tablecsv(params) {
    return ApiService.download("admin/user/tablecsv", params);
  },
  audittable(params) {
    return ApiService.query("admin/user/audittable", {
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
