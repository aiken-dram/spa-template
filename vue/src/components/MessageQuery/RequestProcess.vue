<template>
  <div class="text-center">
    <v-col class="text-subtitle-1 text-center" cols="6">
      {{ $t(state) }}
      <base-modelstate v-model="error"> </base-modelstate>
    </v-col>
    <v-col cols="6">
      <v-progress-linear
        color="deep-purple accent-4"
        indeterminate
        rounded
        height="6"
      ></v-progress-linear>
    </v-col>
  </div>
</template>

<script>
import signalr from "@/plugins/signalr";
import { SUBJECTS } from "@/common/config";
import MQService from "@/api/mq";

import BaseModelstate from "@/components/base/Modelstate";

export default {
  name: "RequestProcess",

  data: () => ({
    id: null, //id of request
    state: null, //state
    error: {}, //error, if any

    connection: null, //Connection to SignalR hub
    disabled: false, //SignalR is disabled in local storage
  }),

  methods: {
    start(data) {
      this.id = null;
      this.state = null;
      this.error = {};

      //start tracking process
      if (this.disabled) this.startNoSignalR(data);
      else this.startSignalR(data);
    },

    startSignalR(data) {
      //1. connect to signalR hub
      this.state = "signalr.connectingToServer";
      this.connection
        .start()
        .then(() => {
          this.state = "signalr.connectionEstablished";
          MQService.create(data)
            .then(({ data }) => {
              this.id = data;
              this.state = "messagequery.queue";
            })
            .catch((error) => {
              this.error = error;
              this.state = "signalR.error";
            });
        })
        .catch((err) => {
          this.error = err;
          this.state = "signalR.error";
        });
    },
    startNoSignalR(data) {
      this.state = "messagequery.signalrDisabled";
      MQService.create(data)
        .then(({ data }) => {
          this.$root.$message(this.$t("common.requestCreated"), "success");
          this.id = data;
        })
        .catch((error) => {
          this.error = error;
          this.state = "signalR.error";
        });
    },

    /** Intercepts message from SignalR server*/
    signalr(data) {
      if (data.subject == SUBJECTS.MessageQuery && data.id == this.id) {
        var e = JSON.parse(data.body);
        this.state = `messagequery.request.state${e.state}`;
        if (e.state == "READY") this.$emit("on-finished", this.id);
      }
    },
  },

  created() {
    //build hub connection on created
    this.connection = signalr.build();
  },

  mounted() {
    this.disabled = signalr.isDisabled();
    //add listener on mounted
    if (!this.disabled) {
      this.connection.on("notification", (data) => this.signalr(data));
    }
  },

  components: {
    BaseModelstate,
  },
};
</script>

<style></style>
