<template>
  <v-card>
    <confirm ref="confirm"></confirm>
    <v-card-title>
      {{
        hasUsername
          ? $t("audit.titleUser", { username: username })
          : $t("audit.title")
      }}
    </v-card-title>
    <base-table-api
      :headers="cHeaders"
      :data-table="dataTable"
      :top-toolbar="false"
      item-key="idEvent"
      show-expand
      single-expand
      export-all-icon="fa-file-export"
      @export-all="exportAllCSV"
      ref="AuditTable"
    >
      <!--
      <template v-slot:item.target="{ item }">
        <base-table-flag
          :value="item.target"
          flag-name="auditTarget"
        ></base-table-flag>
      </template>-->
      <template v-slot:item.data-table-expand="{ item, expand, isExpanded }">
        <v-icon
          v-show="item.eventData && item.eventData.length > 0"
          small
          @click="expand(!isExpanded)"
        >
          {{ isExpanded ? "fa-angle-up" : "fa-angle-down" }}
        </v-icon>
      </template>
      <template v-slot:expanded-item="{ headers, item }">
        <td :colspan="headers.length">
          <v-row no-gutters class="mt-2">
            <audit-data-display :item="item" />
          </v-row>
        </td>
      </template>
    </base-table-api>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import { DICT } from "@/common/config";
import UserService from "@/api/user";
import MQService from "@/api/mq";

import BaseTableApi from "@/components/base/Table/TableAPI";
import Confirm from "@/components/base/Dialog/Confirm";

import AuditDataDisplay from "@/components/Display/AuditData";

export default {
  name: "AuditPage",

  props: {
    id: Number,
    currentUser: Boolean,
  },

  data: function () {
    return {
      username: null,

      headers: [
        {
          text: this.$i18n.t("audit.table.user"),
          value: "login",
          width: 150,
          filter: {
            name: "login",
            label: this.$i18n.t("audit.table.userFilter"),
            type: "text",
          },
        },
        {
          text: this.$i18n.t("audit.table.stamp"),
          value: "stamp",
          format: "datetime",
          width: 160,
          filter: {
            name: "stamp",
            label: this.$i18n.t("audit.table.stampFilter"),
            type: "date",
          },
        },
        {
          text: this.$i18n.t("audit.table.target"),
          value: "targetDesc",
          width: 160,
          filter: {
            name: "idTarget",
            label: this.$i18n.t("audit.table.targetFilter"),
            type: "dict",
            dictionary: DICT.EventTargets,
          },
        },
        {
          text: this.$i18n.t("audit.table.action"),
          value: "action",
          width: 160,
          filter: {
            name: "idAction",
            label: this.$i18n.t("audit.table.actionFilter"),
            type: "dict",
            dictionary: DICT.EventActions,
          },
        },
        {
          text: this.$i18n.t("audit.table.targetName"),
          value: "targetName",
          width: 160,
          filter: {
            name: "targetName",
            label: this.$i18n.t("audit.table.targetNameFilter"),
            type: "text",
          },
        },
        { text: "", value: "data-table-expand" },
      ],
    };
  },

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),
    dataTable(params) {
      //console.log(this.id);
      if (this.id) params["id"] = this.id;
      return UserService.audittable(params);
    },
    load() {
      this.$refs.AuditTable.load();
    },

    exportAllCSV(params, total) {
      //need warning here if count > some number
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
            if (this.id) params["id"] = this.id;
            var data = {
              type: "TABLE_EXPORT_AUDIT",
              json: JSON.stringify(params),
            };
            //console.log(data);
            MQService.create(data)
              .then(() => {
                this.$root.$message(
                  this.$t("common.requestCreated"),
                  "success"
                );
              })
              .catch((error) => {
                this.$root.$error(error);
              })
              .finally(() => {
                this.setOverlay(false);
                this.$root.$MessageQuery();
              });
          }
        });
    },
  },

  mounted() {
    //get username from userservice with id
    if (this.id && !this.currentUser) {
      UserService.get(this.id)
        .then(({ data }) => {
          this.username = data.name;
        })
        .catch((error) => {
          this.$root.$error(error);
        });
    }
  },

  computed: {
    cHeaders() {
      if (this.id) return this.headers.filter((p) => p.value != "login");
      else return this.headers;
    },

    hasUsername() {
      if (this.currentUser) return true;
      else return !!this.username;
    },
  },

  components: {
    BaseTableApi,
    AuditDataDisplay,
    Confirm,
  },
};
</script>

<style></style>
