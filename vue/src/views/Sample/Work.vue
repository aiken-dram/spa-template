<template>
  <v-card>
    <confirm ref="confirm"></confirm>
    <sample-dialog ref="Dialog" @refresh="load" />
    <sample-batch-dialog ref="SampleBatchDialog" @finished="load" />
    <sample-mass-dialog ref="SampleMassDialog" />
    <base-table-api
      item-key="idSample"
      :headers="headers"
      :actions="actions"
      :data-table="dataTable"
      export-page-icon="fa-file-excel"
      @export-page="exportPageXLS"
      @click:row="edit"
      @selected="batch"
      show-select
      ref="Table"
    >
      <work-toolbar :districts="DICT.Districts" @set-search="setSearch">
        <v-divider class="mx-2" vertical></v-divider>
        <v-btn color="primary" @click.stop="create()">
          {{ $t("common.add") }}...
        </v-btn>
      </work-toolbar>

      <template v-slot:toolbar-buttons>
        <base-select-menu
          title="common.massOperations"
          :items="mass"
          @selected="onMass"
        />
      </template>

      <template v-slot:item.cmd="{ item }">
        <base-table-action
          icon="fa-trash"
          :tooltip="$t('common.delete')"
          @click="del(item)"
        ></base-table-action>
      </template>

      <template v-slot:item.flags="{ item }">
        <base-table-flag
          :value="item.flags"
          flag-name="sampleFlags"
        ></base-table-flag>
      </template>
    </base-table-api>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import { DICT } from "@/common/config";
import { download } from "@/common/file";
import SampleService from "@/api/sample";

import BaseTableApi from "@/components/base/Table/TableAPI";
import BaseTableAction from "@/components/base/Table/Action";
import BaseTableFlag from "@/components/base/Table/Flag";
import BaseSelectMenu from "@/components/base/Table/SelectMenu";
import Confirm from "@/components/base/Dialog/Confirm";

import WorkToolbar from "./WorkToolbar";
import SampleDialog from "@/components/Dialog/Sample";
import SampleBatchDialog from "@/components/Dialog/SampleBatch";
import SampleMassDialog from "@/components/Dialog/SampleMass";

export default {
  name: "SampleWork",

  data() {
    return {
      search: [],
      extended: [],

      headers: [
        {
          text: this.$i18n.t("common.actions"),
          align: "start",
          value: "cmd",
          sortable: false,
          filterable: false,
          width: 120,
        },
        { text: this.$i18n.t("sample.table.district"), value: "idDistrict" },
        { text: this.$i18n.t("sample.table.text"), value: "text" },
        { text: this.$i18n.t("sample.table.number"), value: "number" },
        {
          text: this.$i18n.t("sample.table.date"),
          value: "date",
          format: "date",
        },
      ],

      actions: [
        {
          title: "sample.title",
          items: [
            { title: "sample.typeA", action: "type_a" },
            { title: "sample.typeB", action: "type_b" },
          ],
        },
      ],

      mass: [
        {
          title: "sample.title",
          items: [
            { title: "sample.typeA", action: "type_a" },
            { title: "common.delete", action: "delete" },
          ],
        },
      ],
    };
  },

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),

    /** Table */
    setSearch(search, extended) {
      this.search = search;
      this.extended = extended;
      this.load();
    },
    dataTable(params) {
      params["Search"] = this.search;
      params["Extended"] = this.extended;
      return SampleService.table(params);
    },
    load(resetPage = false) {
      //need to refresh table from here
      this.$refs.Table.load(resetPage);
    },

    /** CRUD */
    create() {
      //new form
      this.$refs.Dialog.create();
    },
    edit(item) {
      //edit form
      this.$refs.Dialog.open(item.idSample);
    },
    del(item) {
      //deleting user
      this.$refs.confirm
        .open(
          this.$i18n.t("common.delete"),
          this.$i18n.t("sample.deleteSample"),
          { color: "red" }
        )
        .then((confirm) => {
          if (confirm) {
            this.setOverlay(true);
            this.$refs.Dialog.del(item.idSample).finally(() => {
              this.setOverlay(false);
            });
          }
        });
    },

    exportPageXLS(params) {
      //exporting current page
      this.setOverlay(true);
      SampleService.tablexls(params)
        .then((response) => {
          download(response);
          this.$root.$message(this.$i18n.t("common.download"), "primary");
        })
        .catch((error) => {
          this.$root.$error(error);
        })
        .finally(() => {
          this.setOverlay(false);
        });
    },

    batch(action) {
      var selected = this.$refs.Table.selected.map((p) => p.idSample);
      this.$refs.SampleBatchDialog.start(selected, action);
    },

    onMass(action) {
      var params = {
        Filters: this.$refs.Table.getFilters(),
        Search: this.search,
        Extended: this.extended,
      };
      //console.log(params);
      this.$refs.SampleMassDialog.open(params, action);
    },
  },

  beforeCreate() {
    this.DICT = DICT;
  },

  components: {
    SampleDialog,
    SampleBatchDialog,
    SampleMassDialog,
    WorkToolbar,
    Confirm,
    BaseTableApi,
    BaseTableAction,
    BaseTableFlag,
    BaseSelectMenu,
  },
};
</script>

<style></style>
