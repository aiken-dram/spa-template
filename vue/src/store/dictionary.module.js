//import Vue from "vue";
//import i18n from "@/plugins/i18n";

import DictionaryService from "@/api/dictionary";
import { DICT } from "@/common/config";
import { DICT_FETCH } from "./actions.type";
import { SET_DICT } from "./mutations.type";

const initialState = {
  dict: {
    [DICT.AccessGroups]: [],
    [DICT.AccessRoles]: [],
    [DICT.EventActions]: [],
    [DICT.EventTargets]: [],
    [DICT.Districts]: [],
    [DICT.UserDistricts]: [],
  },
};

export const state = { ...initialState };

function parseDictData(context, dict, data) {
  //parsing dictionary data
  context.commit(SET_DICT, { dict, data });
}

export const actions = {
  [DICT_FETCH](context, dict) {
    //if dictionary already filled dont do api request
    var cur_dict = context.getters.dict(dict);
    if (cur_dict.length > 0) return;

    return DictionaryService.get(dict).then(({ data }) => {
      parseDictData(context, dict, data);
    });
  },
};

export const mutations = {
  [SET_DICT](state, { dict, data }) {
    state.dict[dict] = data;
  },
};

const getters = {
  dict: (state) => (dict) => {
    return state.dict[dict];
  },
};

export default {
  state,
  actions,
  mutations,
  getters,
};
