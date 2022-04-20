<template>
  <v-data-table
    v-model="selected"
    :headers="headers"
    :loading="loading"
    :items="items"
    :options.sync="options"
    :server-items-length="totalLength"
    :item-class="itemClass"
    :loading-text="$t('common.table.loading')"
    :no-data-text="$t('common.table.noData')"
    :no-results-text="$t('common.table.noResult')"
    :footer-props="{
      //showFirstLastPage: true,
      itemsPerPageOptions: [10, 50, 100],
      itemsPerPageText: $t('common.table.page'),
    }"
    v-bind="$attrs"
  >
    <!-- Displaying error from API -->
    <template v-if="error" v-slot:no-data>
      <v-alert
        border="top"
        colored-border
        type="error"
        class="mt-2"
        elevation="2"
      >
        {{ error }}
      </v-alert>
    </template>

    <!-- Displaying table filter in headers that have filter enabled -->
    <template
      v-for="h in filterHeaders"
      v-slot:[`header.${h.value}`]="{ header }"
    >
      <base-table-filter
        :key="h.name"
        :filter="h.filter"
        @set-filter="setFilter"
        @remove-filter="removeFilter"
      ></base-table-filter>
      {{ header.text }}
    </template>

    <!-- Displaying footer with page information -->
    <template v-slot:footer.page-text="props">
      {{
        $t("common.table.records", {
          start: props.pageStart,
          stop: props.pageStop,
          length: props.itemsLength,
        })
      }}
    </template>

    <!-- Displaying button for refreshing data from API -->
    <template slot="footer">
      <div style="position: absolute; margin-left: 10px; margin-top: 10px">
        <v-btn icon @click="load()"><v-icon>fa-spinner</v-icon></v-btn>
      </div>
    </template>

    <template v-slot:top>
      <!-- Default slot for displaying content above table -->
      <slot></slot>

      <!-- Optional toolbar that appears at top right above table
           and contains actions for selected rows, along with download buttons -->
      <v-toolbar
        v-if="topToolbar"
        flat
        floating
        dense
        style="position: absolute; right: 10px; z-index: 3"
        class="mt-n10"
      >
        <!-- Slot 'selected-actions' to display in toolbar if any rows have been selected -->
        <div v-show="selected.length > 0" class="mr-2">
          <slot name="selected-actions"></slot>
        </div>

        <!-- Download table data button -->
        <v-btn icon @click="downloadCSV()">
          <v-icon v-text="downloadIcon"> </v-icon
        ></v-btn>
      </v-toolbar>
    </template>

    <!-- Supported formats for columns -->
    <!-- date filter -->
    <template v-for="h in formatDate" v-slot:[`item.${h.value}`]="{ item }">
      {{ item[h.value] | date }}
    </template>

    <!-- datetime filter -->
    <template v-for="h in formatDatetime" v-slot:[`item.${h.value}`]="{ item }">
      {{ item[h.value] | datetime }}
    </template>

    <!-- Pass on all named slots -->
    <slot v-for="slot in Object.keys($slots)" :name="slot" :slot="slot" />

    <!-- Pass on all scoped slots -->
    <template
      v-for="slot in Object.keys($scopedSlots)"
      :slot="slot"
      slot-scope="scope"
    >
      <slot :name="slot" v-bind="scope" />
    </template>
  </v-data-table>
</template>

<script>
import BaseTableFilter from "./TableFilter";
import { mapMutations } from "vuex";

/**
 * Base api table component
 *
 * Supports column header filters:
 *
 * to use, add filter object to headers array element:
 * filter: {
 *  name: "FieldName"
 *  label: "Placeholder text in search field"
 *  type: "filterType"
 * },
 * where filterType can be:
 * - 'text':
 *  text field input that adds 'FieldName|like|value' to Filters array in API query
 * - 'mask'
 *  text field with mask input that adds 'FieldName|==|value' to Filters array in API query
 * - 'date'
 *  BaseDateRagePicker that adds 'FieldName|date|value' to Filters array in API query
 * - 'dict'
 *  BaseSelect that adds 'FieldName|==|value' to Filters array in API query
 * - 'flags'
 *  TableFilterFlagGroup that adds 'FieldName|in|value' to Filters array in API query
 *
 *  2D: write about remaining fuctionality
 *  also add example
 */
export default {
  name: "BaseTableAPI",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** table headers array */
    headers: Array,

    /** function to get data from api */
    dataTable: Function,

    /** function to set table row class */
    itemClass: {
      type: Function,
      default: () => {},
    },

    /** Show top toolbar */
    topToolbar: {
      type: Boolean,
      default: true,
    },

    /** icon for downloading records */
    downloadIcon: {
      type: String,
      default: "fa-file-csv",
    },
  },

  data: () => ({
    /** array of table items */
    items: [],

    /** array of selected table items */
    selected: [],

    /** total amount of unpaginated records from api */
    totalLength: null,

    /** error while loading items from api */
    error: null,

    /** table data is loading from api */
    loading: true,

    /** sync for table options */
    options: {},

    /** table filters */
    filters: {},
  }),

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),

    /** load table items from api */
    load(resetPage = false) {
      this.loading = true;
      this.selected = [];
      if (resetPage) {
        this.options.page = 1;
      }
      var params = {
        Page: this.options.page,
        ItemsPerPage: this.options.itemsPerPage,
        SortBy: this.options.sortBy.find(Boolean),
        SortDesc: this.options.sortDesc.find(Boolean),
        Filters: this.getFilters(),
      };
      this.dataTable(params)
        .then(({ data }) => {
          //console.log(data.items);
          this.error = null;
          this.items = data.items;
          this.totalLength = data.total;
          this.loading = false;
        })
        .catch((err) => {
          this.items = [];
          this.error = err.Error;
          this.loading = false;
        });
    },

    /**
     * Set filter
     * @param {string} filterName - name of filter
     * @param {Object} filterBody - filter object
     */
    setFilter(filterName, filterBody) {
      this.filters[filterName] = filterBody;
      this.load();
    },

    /**
     * Remove filter
     * @param {string} filterName - name of filter
     */
    removeFilter(filterName) {
      this.filters[filterName] = null;
      this.load();
    },

    /** get filters */
    getFilters() {
      //need to transform values of properties of filters object into array
      var res = [];
      this.headers
        .filter((h) => h.filter)
        .forEach((h) => {
          if (this.filters[h.filter.name])
            res.push(this.filters[h.filter.name]);
        });
      return res;
    },

    /** download csv with table */
    downloadCSV() {
      var params = {
        SortBy: this.options.sortBy.find(Boolean),
        SortDesc: this.options.sortDesc.find(Boolean),
        Filters: this.getFilters(),
      };
      this.$emit("download-csv", params, this.totalLength);
    },
  },

  mounted() {},

  watch: {
    /** table options watch */
    options: {
      handler() {
        //reload table from api on option changes
        this.load();
      },
      deep: true,
    },
  },

  computed: {
    /** return headers which have filter property  */
    filterHeaders: function () {
      return this.headers.filter((h) => h.filter);
    },

    /** get row formats from headers */
    formatDate: function () {
      return this.headers.filter((h) => h.format == "date");
    },
    formatDatetime: function () {
      return this.headers.filter((h) => h.format == "datetime");
    },
  },

  components: {
    BaseTableFilter,
  },
};
</script>
