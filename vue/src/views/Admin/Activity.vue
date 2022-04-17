<template>
  <v-card>
    <v-card-title>{{
      $t("admin.activity.title", { username: username })
    }}</v-card-title>
    <base-table-api
      :headers="headers"
      :data-table="dataTable"
      :top-toolbar="false"
      ref="ActivityTable"
    >
      <template v-slot:item.source="{ item }">
        <base-table-flag
          :value="item.source"
          flag-name="activitySource"
        ></base-table-flag>
      </template>

      <template v-slot:item.message="{ item }">
        <div v-html="item.message"></div>
      </template>
    </base-table-api>
  </v-card>
</template>

<script>
import { DICT } from "@/common/config";
import { UserService } from "@/plugins/api";

import BaseTableApi from "@/components/base/TableAPI";
import BaseTableFlag from "@/components/base/TableFlag";

export default {
  name: "UserActivity",

  data: function () {
    return {
      username: null,

      headers: [
        {
          text: this.$i18n.t("admin.activity.table.type"),
          value: "source",
          width: 120,
          sortable: false,
          filter: {
            type: "flags",
            flags: [
              {
                title: this.$i18n.t("admin.activity.table.type"),
                field: "source",
                name: "activitySource",
              },
            ],
          },
        },
        {
          text: this.$i18n.t("admin.activity.table.stamp"),
          value: "stamp",
          format: "datetime",
          width: 160,
          filter: {
            name: "stamp",
            label: this.$i18n.t("admin.activity.table.stampFilter"),
            type: "date",
          },
        },
        {
          text: this.$i18n.t("admin.activity.table.action"),
          value: "action",
          filter: {
            name: "idAction",
            label: this.$i18n.t("admin.activity.table.actionFilter"),
            type: "dict",
            dictionary: DICT.AuthActions,
          },
        },
        {
          text: this.$i18n.t("admin.activity.table.subject"),
          value: "subject",
          filter: {
            name: "subject",
            label: this.$i18n.t("admin.activity.table.subjectFilter"),
            type: "text",
          },
        },
        {
          text: this.$i18n.t("admin.activity.table.message"),
          value: "message",
          filter: {
            name: "message",
            label: this.$i18n.t("admin.activity.table.messageFilter"),
            type: "text",
          },
        },
      ],
    };
  },

  methods: {
    dataTable(params) {
      params["id"] = this.$route.params.id;
      return UserService.authtable(params);
    },
    load() {
      this.$refs.ActivityTable.load();
    },

    actionText(action) {
      //2D: replace this with dictionary column in headers
      switch (action) {
        case "LOGIN":
          return this.$i18n.t("admin.activity.login");
        case "WRONGPASS":
          return this.$i18n.t("admin.activity.wrongPass");
      }
      return action;
    },
  },

  mounted() {
    //get username from userservice with route id
    UserService.get(this.$route.params.id)
      .then(({ data }) => {
        this.username = data.name;
      })
      .catch((error) => {
        this.$root.$error(error);
      });
  },

  components: {
    BaseTableApi,
    BaseTableFlag,
  },
};
</script>

<style></style>
