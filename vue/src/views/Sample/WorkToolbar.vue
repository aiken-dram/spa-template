<template>
  <search-toolbar :search="extended" ref="extended">
    <v-toolbar dense flat>
      <v-icon color="blue" :disabled="!active">fa-search</v-icon>
      <v-toolbar-items class="mt-4">
        <base-select
          v-model="search.district"
          :label="$t('sample.toolbar.district')"
          :dictionary="districts"
          toolbar
          clearable
          class="shrink ml-2"
          style="width: 300px"
        ></base-select>
      </v-toolbar-items>
    </v-toolbar>

    <template v-slot:actions>
      <v-btn class="ml-2" @click="applyFilter">{{ $t("common.search") }}</v-btn>
      <v-btn class="ml-2" text @click="resetFilter">
        {{ $t("common.cancel") }}
      </v-btn>
      <slot></slot>
    </template>
  </search-toolbar>
</template>

<script>
import { mapGetters } from "vuex";
import { DICT } from "@/common/config";
import BaseSelect from "@/components/base/Select";
import SearchToolbar from "@/components/base/Toolbar/SearchToolbar";

export default {
  name: "SampleWorkToolbar",

  props: {
    districts: String,
  },

  data() {
    return {
      active: false,
      search: {
        district: null,
      },
      extended: [
        {
          title: "sample.toolbar.sample",
          children: [
            {
              title: "sample.toolbar.dictionary",
              name: "IdDict",
              type: "dict",
              dict: DICT.SampleDicts,
            },
          ],
        },
        {
          title: "sample.toolbar.children",
          children: [
            {
              title: "sample.toolbar.text",
              name: "childrenText",
              type: "text",
            },
          ],
        },
      ],
    };
  },

  mounted() {
    if (
      this.districts == DICT.EditableDistricts &&
      this.currentUser.userDistricts.length > 0
    ) {
      //set initial district
      this.search.district = this.currentUser.userDistricts[0];
      this.applyFilter();
    }
  },

  methods: {
    applyFilter() {
      this.active = true;
      var filters = [];
      if (this.search.district)
        filters.push(`idDistrict|==|${this.search.district}`);
      this.$emit("set-search", filters, this.$refs.extended.getFilter());
    },
    resetFilter() {
      this.active = false;
      this.search = {
        district: null,
      };
      this.$refs.extended.clear();
      this.$emit("set-search", [], []);
    },
  },

  computed: {
    ...mapGetters(["currentUser"]),
  },

  beforeCreate() {
    this.DICT = DICT;
  },

  components: {
    BaseSelect,
    SearchToolbar,
  },
};
</script>

<style></style>
