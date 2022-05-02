import ApiService from "./index";

export default {
  toolbar() {
    return ApiService.get("messagequery/request/toolbar");
  },
  table(params) {
    return ApiService.query("messagequery/request/table", {
      params: params,
    });
  },
  download(id) {
    return ApiService.download(`messagequery/request/download/${id}`);
  },
  create(data) {
    return ApiService.post("messagequery/request/create", data);
  },
  delete(id) {
    return ApiService.delete(`messagequery/request/delete/${id}`);
  },
  queue() {
    return ApiService.get("messagequery/request/queue");
  },
};
