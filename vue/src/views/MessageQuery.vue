<template>
  <v-card flat>
    <confirm ref="confirm" />
    <request-batch-dialog ref="RequestBatchDialog" @finished="load" />
    <v-navigation-drawer absolute permanent style="min-height: 200px">
      <v-list-item class="px-2">
        <v-list-item-avatar>
          <v-icon>fa-user</v-icon>
        </v-list-item-avatar>
        <v-list-item-title> {{ currentUser.userName }} </v-list-item-title>
      </v-list-item>

      <v-divider />

      <v-list>
        <v-list-item-group
          v-model="selectedFolder"
          :close-on-content-click="false"
          color="primary"
          mandatory
        >
          <v-list-item link>
            <v-list-item-icon>
              <v-icon>fa-book</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>
                {{ $t("messagequery.inbox") }}
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item link>
            <v-list-item-icon>
              <v-icon>fa-trash</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>
                {{ $t("messagequery.delivered") }}
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <div style="margin-left: 256px">
      <base-table-api
        :headers="cHeaders"
        :actions="actions"
        item-key="idRequest"
        :data-table="dataTable"
        @click:row="click"
        @selected="batch"
        show-select
        fixed-toolbar
        ref="MQTable"
      >
        <template v-slot:item.state="{ item }">
          <request-icon
            v-model="item.state"
            style="width: 50px; display: inline-block"
          ></request-icon>
          <v-progress-linear
            v-if="showProgress(item.state)"
            indeterminate
            rounded
            height="16"
            class="ml-2"
            style="width: 50px; display: inline-block"
          ></v-progress-linear>
        </template>
        <template v-slot:item.btn="{ item }">
          <base-table-action
            icon="fa-times-circle"
            color="error"
            :small="false"
            :tooltip="$t('common.delete')"
            @click="del(item)"
          ></base-table-action>
          <base-table-action
            v-if="showDownload(item.state)"
            icon="fa-save"
            :small="false"
            color="primary"
            :tooltip="$t('common.save')"
            @click="download(item)"
          ></base-table-action>
        </template>
      </base-table-api>
    </div>
  </v-card>
</template>

<script>
import signalr from "@/plugins/signalr";
import { SUBJECTS } from "@/common/config";
import { download } from "@/common/file";
import { mapMutations, mapGetters } from "vuex";
import MQService from "@/api/mq";

import Confirm from "@/components/base/Dialog/Confirm";
import BaseTableApi from "@/components/base/Table/TableAPI";
import BaseTableAction from "@/components/base/Table/Action";
import RequestIcon from "@/components/MessageQuery/RequestIcon";

import RequestBatchDialog from "@/components/Dialog/RequestBatch";

/**
 * MessageQuery inbox-like page
 */
export default {
  name: "MessageQueryPage",

  data: () => ({
    connection: null, //Connection to SignalR hub
    disabled: false, //SignalR is disabled in local storage

    selectedFolder: 0, //0 - income, 1 - delivered
    headers: [
      { text: "common.actions", value: "btn", width: 100, sortable: false },
      { text: "mq.table.state", value: "state", width: 140, sortable: false },
      {
        text: "mq.table.type",
        value: "typeDesc",
        filter: { name: "typeDesc", type: "text" },
      },
      {
        text: "mq.table.created",
        value: "created",
        width: 160,
        format: "datetime",
        filter: { name: "created", type: "date" },
      },
      {
        text: "mq.table.processed",
        value: "processed",
        width: 160,
        format: "datetime",
        filter: { name: "processed", type: "date" },
      },
      {
        text: "mq.table.delivered",
        value: "delivered",
        width: 160,
        format: "datetime",
        filter: { name: "delivered", type: "date" },
      },
    ],

    actions: [{ title: "common.delete", action: "delete" }],
  }),

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),
    load() {
      //need to refresh table from here
      this.$refs.MQTable.load();
    },
    dataTable(params) {
      params["selectedFolder"] = this.selectedFolder;
      return MQService.table(params);
    },

    click(item) {
      //console.log(item);
      if (this.showDownload(item.state)) this.download(item);
    },
    del(item) {
      //delete
      //console.log(item.idDocument);
      this.$refs.confirm
        .open(this.$i18n.t("common.delete"), this.$i18n.t("mq.deleteRequest"), {
          color: "red",
        })
        .then((confirm) => {
          if (confirm) {
            this.setOverlay(true);
            MQService.delete(item.idRequest)
              .then(() => {
                this.$root.$message(this.$i18n.t("common.deleted"), "success");
              })
              .catch((error) => {
                this.$root.$error(error);
              })
              .finally(() => {
                this.setOverlay(false);
                this.load();
              });
          }
        });
    },
    batch(action, selectAll) {
      console.log(selectAll);
      var selected = this.$refs.MQTable.selected.map((p) => p.idRequest);
      this.$refs.RequestBatchDialog.start(selected, action);
    },

    download(item) {
      this.setOverlay(true);
      MQService.download(item.idRequest)
        .then((response) => {
          download(response);
          this.$root.$message(this.$t("common.download"), "primary", true);
        })
        .catch((error) => {
          this.$root.$error(error);
        })
        .finally(() => {
          this.setOverlay(false);
          this.load();
        });
    },

    showDownload(state) {
      return state == "READY" || state == "DELIVERED";
    },

    showProgress(state) {
      return state == "PROCESSING";
    },

    /** Intercepts message from SignalR server and pushes body to progress array */
    signalr(data) {
      console.log("received signal:");
      console.log(data);
      if (
        data.subject == SUBJECTS.MessageQuery &&
        data.idUser == this.currentUser.userID
      ) {
        //need to pass update state into table?
        this.$refs.MQTable.signalr("idRequest", data.id, JSON.parse(data.body));
      }
    },
  },

  computed: {
    ...mapGetters(["currentUser"]),

    cHeaders() {
      if (this.selectedFolder == 0)
        return this.headers.filter((p) => p.value != "delivered");
      else return this.headers;
    },
  },

  watch: {
    // whenever selectedFolder changes, load new data to table
    selectedFolder() {
      this.load();
    },
  },

  created() {
    //build hub connection on created
    this.connection = signalr.build();
  },

  mounted() {
    this.disabled = signalr.isDisabled();
    //create connection here if not disabled
    if (!this.disabled) {
      this.connection.on("notification", (data) => this.signalr(data));

      this.connection.start().catch((err) => {
        this.$root.$error(err);
      });
    }
  },

  components: {
    Confirm,
    BaseTableApi,
    BaseTableAction,
    RequestIcon,
    RequestBatchDialog,
  },
};
</script>

<style></style>
