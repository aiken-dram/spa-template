<template>
  <v-card>
    <confirm ref="confirm" />
    <district-dialog ref="DistrictDialog" :refresh-table="load" />
    <base-table
      :headers="headers"
      :search="search"
      :data-table="dataTable"
      ref="DistrictTable"
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
          icon="fa-edit"
          :tooltip="$t('common.edit')"
          @click="edit(item)"
        ></base-table-action>
        <base-table-action
          icon="fa-trash"
          :tooltip="$t('common.delete')"
          @click="del(item)"
        ></base-table-action>
      </template>
    </base-table>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import DistrictService from "@/api/district";

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

    dataTable() {
      return DistrictService.list();
    },
    load() {
      this.$refs.DistrictTable.load();
    },

    create() {
      //new form
      this.$refs.DistrictDialog.create();
    },
    edit(item) {
      //edit form
      this.setOverlay(true);
      this.$refs.DistrictDialog.open(item.idDistrict).finally(() => {
        this.setOverlay(false);
      });
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
            this.$refs.DistrictDialog.del(item.idDistrict);
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
