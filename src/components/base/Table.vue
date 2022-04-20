<template>
  <v-data-table
    :headers="headers"
    :items="items"
    :search="search"
    :loading="loading"
    :item-class="itemClass"
    :loading-text="$t('common.table.loading')"
    :no-data-text="$t('common.table.noData')"
    :no-results-text="$t('common.table.noResult')"
    :footer-props="{
      //showFirstLastPage: true,
      itemsPerPageOptions: showAll ? [-1] : [10, 50, 100, -1],
      itemsPerPageAllText: $t('common.table.pageAll'),
      itemsPerPageText: $t('common.table.page'),
    }"
    v-bind="$attrs"
    v-on="$listeners"
  >
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

    <template v-slot:footer.page-text="props">
      {{
        $t("common.table.records", {
          start: props.pageStart,
          stop: props.pageStop,
          length: props.itemsLength,
        })
      }}
    </template>

    <template slot="footer">
      <v-btn
        icon
        :style="`position: absolute; left: 10px; bottom: ${
          showAll ? 0 : 10
        }px;`"
        @click="load()"
      >
        <v-icon>fa-spinner</v-icon>
      </v-btn>
    </template>

    <template v-for="(h, i) in rowFormat" v-slot:[`item.${h.value}`]="{ item }">
      <div :key="i">
        <div v-if="h.format == 'date'">
          {{ item[h.value] | date }}
        </div>
        <div v-else-if="h.format == 'sum'">
          {{ item[h.value] | sum }}
        </div>
        <div v-else>
          {{ item[h.value] }}
        </div>
      </div>
    </template>

    <!-- Pass on all named slots -->
    <slot v-for="slot in Object.keys($slots)" :name="slot" :slot="slot" />

    <!-- Pass on all scoped slots -->
    <template
      v-for="slot in Object.keys($scopedSlots)"
      :slot="slot"
      slot-scope="scope"
      ><slot :name="slot" v-bind="scope"
    /></template>
  </v-data-table>
</template>

<script>
/**
 * Base table component
 */
export default {
  name: "BaseTable",
  inheritAttrs: false,

  props: {
    /** table headers array */
    headers: Array,

    /** search string */
    search: String,

    /** function to get data from api */
    dataTable: Function,

    /** function to set table row class */
    itemClass: {
      type: Function,
      default: () => {},
    },

    /** show all records */
    showAll: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    /** table data is loading from api */
    loading: true,

    /** error while loading table data from api */
    error: null,

    /** array of table items */
    items: [],
  }),

  mounted() {
    this.load();
  },

  methods: {
    /** load table items from api */
    load() {
      this.loading = true;
      this.dataTable()
        .then(({ data }) => {
          this.error = null;
          if (data.items) this.items = data.items;
          else this.items = [];
          this.loading = false;
        })
        .catch((err) => {
          this.items = [];
          this.error = err.Error;
          this.loading = false;
        });
    },
  },

  computed: {
    /** get row format from headers */
    rowFormat: function () {
      return this.headers.filter((h) => h.format);
    },
  },
};
</script>
