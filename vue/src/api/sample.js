import ApiService from "./index";

export default {
  table(params) {
    return ApiService.query("sample/sample/table", {
      params: params,
    });
  },
  tablecsv(params) {
    return ApiService.download("sample/sample/tablecsv", params);
  },
  get(id) {
    return ApiService.get("sample/sample/get", id);
  },
  upsert(data) {
    return ApiService.post(`sample/sample/upsert`, data);
  },
  delete(id) {
    return ApiService.delete(`sample/sample/delete/${id}`);
  },
  batch(data) {
    return ApiService.post(`sample/sample/batch`, data);
  },
  audit(id) {
    return ApiService.get("sample/sample/get", id);
  },
};
