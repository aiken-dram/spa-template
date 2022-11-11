<template>
  <v-card>
    <confirm ref="confirm" />
    <user-dialog ref="Dialog" @refresh="load" />
    <base-table-api
      :headers="headers"
      :search="search"
      :data-table="dataTable"
      export-page-icon="fa-file-csv"
      @export-page="exportPageCSV"
      @click:row="edit"
      ref="Table"
    >
      <v-card flat>
        <v-toolbar flat>
          <v-btn color="primary" @click.stop="create()">
            {{ $t("common.add") }}...
          </v-btn>
          <v-divider class="mx-4" inset vertical />
          <v-text-field
            v-model="search"
            append-icon="fa-search"
            :label="$t('common.search')"
            @keydown.enter="load"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer />
        </v-toolbar>
      </v-card>

      <template v-slot:item.idUser="{ item }">
        <base-table-action
          icon="fa-trash"
          :tooltip="$t('common.delete')"
          @click="del(item)"
        ></base-table-action>
        <base-table-action
          icon="fa-history"
          :tooltip="$t('admin.access.users.table.activity')"
          @click="$router.push(`audit/${item.idUser}`)"
        ></base-table-action>
      </template>

      <template v-slot:item.isActive="{ item }">
        <v-simple-checkbox
          :value="item.isActive == 'T'"
          disabled
        ></v-simple-checkbox>
      </template>

      <template v-slot:item.groups="{ item }">
        <v-list-item dense v-for="(i, index) in item.groups" :key="index">
          {{ i }}
        </v-list-item>
      </template>

      <template v-slot:item.districts="{ item }">
        <v-chip v-for="(i, index) in item.districts" :key="index">{{
          i
        }}</v-chip>
      </template>
    </base-table-api>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import { download } from "@/common/file";
import UserService from "@/api/user";

import BaseTableApi from "@/components/base/Table/TableAPI";
import BaseTableAction from "@/components/base/Table/Action";
import Confirm from "@/components/base/Dialog/Confirm";

import UserDialog from "@/components/Dialog/User";

export default {
  name: "AccessUsers",

  data() {
    return {
      search: "", //search in table
      headers: [
        {
          text: "common.actions",
          align: "start",
          value: "idUser",
          sortable: false,
          filterable: false,
          width: 120,
        },
        {
          text: "admin.access.users.table.login",
          value: "login",
          filter: {
            name: "login",
            type: "text",
          },
        },
        {
          text: "admin.access.users.table.isActive",
          value: "isActive",
          filter: {
            name: "isActive",
            label: "admin.access.users.table.isActive",
            type: "checkbox",
          },
        },
        {
          text: "admin.access.users.table.userName",
          value: "name",
          filter: {
            name: "name",
            type: "text",
          },
        },
        {
          text: "admin.access.users.table.districts",
          value: "districts",
          sortable: false,
        },
        {
          text: "admin.access.users.table.groups",
          value: "groups",
          sortable: false,
        },
        {
          text: "admin.access.users.table.desc",
          value: "description",
          filter: {
            name: "description",
            type: "text",
          },
        },
      ],
    };
  },

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),

    /** Table */
    dataTable(params) {
      params["FullSearch"] = this.search;
      return UserService.table(params);
    },
    load(resetPage = false) {
      //need to refresh table from here
      this.$refs.Table.load(resetPage);
    },

    /** CRUD */
    create() {
      //new user form
      this.$refs.Dialog.create();
    },
    edit(item) {
      //edit user form
      this.$refs.Dialog.open(item.idUser);
    },
    del(item) {
      //deleting user
      this.$refs.confirm
        .open(
          this.$i18n.t("common.delete"),
          this.$i18n.t("admin.access.users.deleteUser"),
          { color: "red" }
        )
        .then((confirm) => {
          if (confirm) {
            this.setOverlay(true);
            //console.log("users.del");
            this.$refs.Dialog.del(item.idUser).finally(() => {
              this.setOverlay(false);
            });
          }
        });
    },

    exportPageCSV(params) {
      //exporting current page
      this.setOverlay(true);
      UserService.tablecsv(params)
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
  },

  components: {
    UserDialog,
    Confirm,
    BaseTableApi,
    BaseTableAction,
  },
};
</script>
