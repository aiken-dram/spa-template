<template>
  <v-menu
    v-model="show"
    :close-on-content-click="false"
    :nudge-width="300"
    offset-x
    offset-y
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn icon x-small @click.stop="" v-bind="attrs" v-on="on">
        <v-icon :color="ButtonColor" x-small>fa-filter</v-icon>
      </v-btn>
    </template>

    <v-list dense>
      <v-list-item v-if="filter.type == 'text'">
        <v-text-field
          v-model="text"
          :label="filter.label"
          placeholder=" "
          persistent-placeholder
        ></v-text-field>
      </v-list-item>

      <v-list-item v-if="filter.type == 'mask'">
        <v-text-field
          v-model="text"
          :label="filter.label"
          v-facade="filter.mask"
          placeholder=" "
          persistent-placeholder
        ></v-text-field>
      </v-list-item>

      <v-list-item v-if="filter.type == 'date'">
        <base-date-range-picker
          v-model="dateRange"
          :label="filter.label"
        ></base-date-range-picker>
      </v-list-item>

      <v-list-item v-if="filter.type == 'dict'">
        <base-select
          v-model="dict"
          :label="filter.label"
          :dictionary="filter.dictionary"
        ></base-select>
      </v-list-item>

      <v-list-item v-if="filter.type == 'checkbox'">
        <base-checkbox v-model="checkbox" :label="filter.label"></base-checkbox>
      </v-list-item>

      <div v-if="filter.type == 'flags'">
        <div v-for="(f, i) in filter.flags" :key="i">
          <base-table-filter-flag-group
            v-if="f.group"
            v-model="flags[f.name]"
            :flag="f"
          ></base-table-filter-flag-group>
          <base-table-filter-flag
            v-else
            v-model="flags[f.name]"
            :flag="f"
          ></base-table-filter-flag>
        </div>
      </div>

      <v-list-item>
        <v-spacer></v-spacer>
        <v-btn small @click="applyFilter">{{ $t("common.search") }}</v-btn>
        <v-btn small text @click="cancelFilter">
          {{ $t("common.cancel") }}
        </v-btn>
      </v-list-item>
    </v-list>
  </v-menu>
</template>

<script>
import FLAGS from "@/common/flags";

import BaseDateRangePicker from "./DateRangePicker";
import BaseSelect from "./Select";
import BaseCheckbox from "./Checkbox";
import BaseTableFilterFlag from "./TableFilterFlag";
import BaseTableFilterFlagGroup from "./TableFilterFlagGroup";

/**
 * Base api table filter component
 * This is used by TableAPI to display and apply filters in column headers
 */
export default {
  name: "TableFilter",

  props: {
    /** filter object */
    filter: Object,
  },

  data: () => ({
    /** show filter menu */
    show: false,

    /** filter is active */
    active: false,

    /** filter value for text */
    text: null,

    /** filter value for dictionary */
    dict: null,

    /** filter value for checkbox */
    checkbox: null,

    /** filter value for date range */
    dateRange: [],

    /** filter value for flags */
    flags: {},
  }),

  methods: {
    getFlag(name, value) {
      //value can be undefined or array?
      var f = FLAGS[name];

      if (value && value.length > 0)
        return value.map((v) => f[v].value).join(",");

      return null;
    },
    /** get filter */
    getFilter() {
      switch (this.filter.type) {
        case "text":
          return `${this.filter.name}|like|${this.text}`;
        case "date":
          return `${this.filter.name}|date|${this.dateRange.join(" ~ ")}`;
        case "mask":
          return `${this.filter.name}|==|${this.text}`;
        case "dict":
          return `${this.filter.name}|==|${this.dict}`;
        case "checkbox":
          return `${this.filter.name}|==|${this.checkbox}`;
        case "flags":
          return this.filter.flags.map((p) => {
            if (p.group)
              return `${p.group
                .map((q) => q.field)
                .join(",")}|group|${this.flags[p.name].join(",")}`;
            else
              return `${p.field}|in|${this.getFlag(
                p.name,
                this.flags[p.name]
              )}`;
          });
        default:
          return null;
      }
    },

    /** applies this filter to parent table component */
    applyFilter() {
      this.active = true;
      this.show = false;
      this.$emit("set-filter", this.filter.name, this.getFilter());
    },

    /** cancel this filter from parent table component */
    cancelFilter() {
      this.active = false;
      this.show = false;
      this.$emit("remove-filter", this.filter.name);
    },
  },

  computed: {
    /** filter button icon color, based on if filter is active or not */
    ButtonColor() {
      return this.active ? "black" : "grey lighten-1";
    },
  },

  components: {
    BaseDateRangePicker,
    BaseCheckbox,
    BaseSelect,
    BaseTableFilterFlag,
    BaseTableFilterFlagGroup,
  },
};
</script>
