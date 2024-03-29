<template>
  <v-card>
    <confirm ref="confirm" />
    <district-dialog ref="Dialog" @refresh="load" />
    <base-table
      :headers="headers"
      :search="search"
      :data-table="dataTable"
      @click:row="edit"
      ref="Table"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-text-field
            v-model="search"
            append-icon="fa-search"
            :label="$t('common.search')"
            @keydown.enter="load"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer />
          <v-divider class="mx-4" inset vertical />

          <v-btn color="primary" @click.stop="create()">
            {{ $t("common.add") }}...
          </v-btn>
        </v-toolbar>
      </template>

      <template v-slot:item.cmd="{ item }">
        <base-table-action
          icon="fa-trash"
          :tooltip="$t('common.delete')"
          @click.stop="del(item)"
        ></base-table-action>
      </template>
    </base-table>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import { DistrictService } from "@/api/dictionary";

import BaseTable from "@/components/base/Table/Table";
import Confirm from "@/components/base/Dialog/Confirm";
import BaseTableAction from "@/components/base/Table/Action";

import DistrictDialog from "@/components/Dialog/District";

export default {
  name: "DictDistricts",

  data() {
    return {
      search: "", //search in table
      headers: [
        {
          text: this.$i18n.t("common.actions"),
          align: "start",
          value: "cmd",
          sortable: false,
          filterable: false,
          width: 120,
        },
        {
          text: this.$i18n.t("admin.dict.districts.table.idDistrict"),
          value: "idDistrict",
          width: 160,
        },
        {
          text: this.$i18n.t("admin.dict.districts.table.name"),
          value: "name",
        },
      ],
    };
  },

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),

    /** Table */
    dataTable() {
      return DistrictService.list();
    },
    load() {
      this.$refs.Table.load();
    },

    /** CRUD */
    create() {
      //new form
      this.$refs.Dialog.create();
    },
    edit(item) {
      //edit form
      this.$refs.Dialog.open(item.idDistrict);
    },
    del(item) {
      //deleting user
      this.$refs.confirm
        .open(
          this.$i18n.t("common.delete"),
          this.$i18n.t("admin.dict.districts.deleteDistrict"),
          { color: "red" }
        )
        .then((confirm) => {
          if (confirm) {
            this.setOverlay(true);
            this.$refs.Dialog.del(item.idDistrict).finally(() => {
              this.setOverlay(false);
            });
          }
        });
    },
  },

  components: {
    Confirm,
    BaseTable,
    BaseTableAction,
    DistrictDialog,
  },
};
</script>

<style></style>
