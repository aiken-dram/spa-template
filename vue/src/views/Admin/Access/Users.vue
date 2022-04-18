<template>
  <v-card>
    <confirm ref="confirm"></confirm>
    <user-dialog ref="UserDialog" :refresh-table="load" />
    <base-table-api
      :headers="headers"
      :search="search"
      :data-table="dataTable"
      download-icon="fa-file-csv"
      @download-csv="downloadCSV"
      ref="UserTable"
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
          <v-spacer></v-spacer>
          <v-divider class="mx-4" inset vertical></v-divider>

          <v-btn color="primary" @click.stop="create()">
            {{ $t("common.add") }}...
          </v-btn>
        </v-toolbar>
      </template>

      <template v-slot:item.idUser="{ item }">
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
        <base-table-action
          icon="fa-history"
          :tooltip="$t('admin.access.users.table.activity')"
          @click="$router.push(`activity/${item.idUser}`)"
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
    </base-table-api>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import { download } from "@/common/file";
import { UserService } from "@/plugins/api";

import BaseTableApi from "@/components/base/TableAPI";
import BaseTableAction from "@/components/base/TableAction";
import Confirm from "@/components/base/DialogConfirm";

import UserDialog from "@/components/Dialog/User";

export default {
  name: "AccessUsers",
  data() {
    return {
      search: "", //search in table
      headers: [
        {
          text: this.$i18n.t("common.actions"),
          align: "start",
          value: "idUser",
          sortable: false,
          filterable: false,
          width: 120,
        },
        {
          text: this.$i18n.t("admin.access.users.table.login"),
          value: "login",
          filter: {
            name: "login",
            label: this.$i18n.t("admin.access.users.table.loginFilter"),
            type: "text",
          },
        },
        {
          text: this.$i18n.t("admin.access.users.table.isActive"),
          value: "isActive",
          filter: {
            name: "isActive",
            label: this.$i18n.t("admin.access.users.table.isActiveFilter"),
            type: "checkbox",
          },
        },
        {
          text: this.$i18n.t("admin.access.users.table.userName"),
          value: "name",
          filter: {
            name: "name",
            label: this.$i18n.t("admin.access.users.table.userNameFilter"),
            type: "text",
          },
        },
        {
          text: this.$i18n.t("admin.access.users.table.groups"),
          value: "groups",
          sortable: false,
        },
        {
          text: this.$i18n.t("admin.access.users.table.desc"),
          value: "description",
          filter: {
            name: "description",
            label: this.$i18n.t("admin.access.users.table.descFilter"),
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

    dataTable(params) {
      params["FullSearch"] = this.search;
      return UserService.table(params);
    },
    load(resetPage = false) {
      //need to refresh table from here
      this.$refs.UserTable.load(resetPage);
    },

    create() {
      //new user form
      this.$refs.UserDialog.create();
    },
    edit(item) {
      //edit user form
      this.setOverlay(true);
      this.$refs.UserDialog.open(item.idUser).finally(() => {
        this.setOverlay(false);
      });
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
            console.log("users.del");
            this.$refs.UserDialog.del(item.idUser);
          }
        });
    },

    downloadCSV(params, total) {
      //2D: probably need warning here if count > some number
      this.$refs.confirm
        .open(
          this.$i18n.t("common.exportCsv"),
          this.$i18n.t("common.exportCsvConfirm", { count: total }),
          {
            color: "primary",
          }
        )
        .then((confirm) => {
          if (confirm) {
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
          }
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
