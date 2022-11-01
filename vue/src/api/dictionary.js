import ApiService from "./index";

export default {
  get(dict) {
    return ApiService.get("admin/dictionary/get", dict);
  },
};

export const DistrictService = {
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

export const SampleDictService = {
  list() {
    return ApiService.get("admin/sampledict/list");
  },
  get(id) {
    return ApiService.get("admin/sampledict/get", id);
  },
  upsert(data) {
    return ApiService.post("admin/sampledict/upsert", data);
  },
  delete(id) {
    return ApiService.delete(`admin/sampledict/delete/${id}`);
  },
};
