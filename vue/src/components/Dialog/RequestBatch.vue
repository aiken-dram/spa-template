<template>
  <v-dialog v-model="show" persistent width="500">
    <v-card>
      <v-card-text>
        <base-signal-r
          :api="batchApi"
          :subject="SUBJECTS.BatchProcessRequest"
          start-on-mount
          bar-only
          @state="onState"
          @finished="onFinished"
          ref="BatchSignalR"
        >
          <template v-slot:finished>
            <v-btn
              v-if="finished & (state == 'error')"
              color="primary"
              @click="close()"
            >
              {{ $t("common.close") }}
            </v-btn>
          </template>
        </base-signal-r>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { SUBJECTS } from "@/common/config";
import MQService from "@/api/mq";

import BaseSignalR from "@/components/base/SignalR";

export default {
  name: "RequestBatchDialog",

  data: () => ({
    show: false,
    finished: false,
    state: null,

    items: [],
    action: null,
  }),

  methods: {
    start(items, action) {
      this.finished = false;
      this.state = null;
      this.items = items;
      this.action = action;
      this.show = true;
      if (this.$refs.BatchSignalR) this.$refs.BatchSignalR.start();
    },

    /** upload file api */
    batchApi(idConnection) {
      var params = {
        idConnection: idConnection,
        items: this.items,
        action: this.action,
      };
      return MQService.batch(params);
    },

    close() {
      this.show = false;
      this.$emit("finished");
    },

    onState(state) {
      this.state = state;
    },
    onFinished() {
      this.finished = true;
      if (this.state == "result") this.close();
    },
  },

  created() {
    this.SUBJECTS = SUBJECTS;
  },

  components: {
    BaseSignalR,
  },
};
</script>

<style></style>
