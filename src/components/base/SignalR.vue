<template>
  <div>
    <!-- Slot 'status' with binded :status for displaying component status -->
    <slot name="status" v-bind:status="status">
      <p>{{ status }}</p>
    </slot>

    <div v-show="showProgressBar" class="mt-2">
      <!-- Slot 'progress' with binded :bar for displaying progress bar -->
      <slot name="running" v-bind:bar="bar">
        <v-row justify="center" align="center">
          <v-progress-linear
            color="deep-purple accent-4"
            v-model="bar.value"
            :active="bar.show"
            :indeterminate="!bar.value"
            :query="true"
            height="25"
            class="mb-4"
          >
            <template v-slot:default="{ value }">
              <strong>{{ Math.ceil(value) }}%</strong>
            </template>
          </v-progress-linear>
        </v-row>
      </slot>
    </div>

    <div v-if="useItems">
      <div v-show="showProgress" class="mt-2">
        <div v-for="(p, i) in progress" :key="i">
          <!-- Slot 'item' with binded :p for displaying single message from SinglaR -->
          <slot name="item" v-bind:p="p">
            <v-alert dense :type="p.state">
              <div v-html="p.body"></div>
            </v-alert>
          </slot>
        </div>
      </div>
    </div>
    <div v-else>
      <div v-show="showProgress">
        <!-- Slot 'loop' with binded :progress for displaying list of message from SinglaR -->
        <slot name="loop" v-bind:progress="progress">
          <v-list flat dense>
            <v-list-item
              v-for="(p, i) in progress"
              :key="i"
              :color="p.state"
              dense
            >
              <v-list-item-icon v-if="p.state == 'success'">
                <v-icon color="success">fa-check</v-icon>
              </v-list-item-icon>
              <v-list-item-icon v-if="p.state == 'error'">
                <v-icon color="error">fa-exclamation-circle</v-icon>
              </v-list-item-icon>
              <v-list-item-icon v-if="p.state == 'warning'">
                <v-icon color="warning">fa-exclamation-triangle</v-icon>
              </v-list-item-icon>
              <v-list-item-icon v-if="p.state == 'info'">
                <v-icon color="info">fa-info-circle</v-icon>
              </v-list-item-icon>
              <div v-html="p.body"></div>
            </v-list-item>
          </v-list>
        </slot>
      </div>
    </div>

    <div v-show="showResult" class="mt-2">
      <!-- Slot 'result' with binded :result for displaying result from API -->
      <slot name="result" v-bind:result="result"></slot>
    </div>

    <!-- Displaying error from API -->
    <div v-show="state == 'error'">
      <v-alert type="error" class="mt-2">
        {{ error["Error"] }}
      </v-alert>
    </div>

    <div v-show="showFinished" class="mt-2">
      <!-- Slot 'finished' to display after api request has been finished with result/error -->
      <slot name="finished"></slot>
    </div>
  </div>
</template>

<script>
import signalr from "@/plugins/signalr";

/**
 * Component for displaying progress from SignalR server while processing api request
 *
 * Example:
 * Insert component with:
 *  :api calling method that returns api call from idConnection property
 *  :subject returning name of signalr subject to intercept during this api call
 *  ref reference to this component
 *
 * <base-signal-r :api="uploadFileApi" :subject="SUBJECTS.ProcessFile" ref="UploadFileSignalR">
 * </base-signal-r>
 *
 * Add api calling method from idConnection property:
 *
 *  uploadFileApi(idConnection) {
 *      let formData = new FormData();
 *       formData.append("file", this.file);
 *      formData.append("idConnection", idConnection);
 *      return UserService.upload(formData);
 *  },
 *
 * Now you can start api processing by calling start() method:
 *
 *  this.$refs.UploadFileSignalR.start();
 *
 *  Available properties in component for customization:
 *  keep-signalr - set if you want to keep displaying messages from signalr after api request finishes
 *  use-items - set if you want to use 'item' slot for each message in a loop of div
 *      instead of default 'loop' slot taking entire array
 *  different-result - set if api response returns different result than an array of
 *      signalr messages in 'items' property
 *
 *  Available slots in component:
 *  status - slot for displaying status of api request
 *  running - slot for displaying progress bar
 *  item - scoped slot with :p to display single message from server,
 *      only used if property use-items is set
 *  loop - scoped slot with :progress to display array of messages from server
 *  result - scoped slot with :result
 *  finished - slot for displaying elements for when api request has been finished
 *
 *  During processing api component emits events:
 *  @set-state - changes in state (started -> running -> error/result)
 *  @on-finished - emits after api request was finished
 *
 *  2D: add progress % support
 */
export default {
  name: "BaseSignalR",

  props: {
    /** Api function that starts request */
    api: Function,

    /** Hub message subject to intercept here */
    subject: String,

    /** Keep displaying messages from server after api call was successfull */
    keepSignalr: {
      type: Boolean,
      default: false,
    },

    /** Use v-for of div with 'item' slots instead of 'loop' slot wrapping progress array */
    useItems: {
      type: Boolean,
      default: false,
    },

    /** Response from api is different from array of signalr messages in items property */
    differentResult: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    connection: null, //Connection to SignalR hub
    disabled: false, //SignalR is disabled in local storage

    state: null, //api request state: null, started, running, result, error
    status: null, //state of signalr connection
    progress: [], //array of messages from signalr
    result: {}, //result from api
    error: {}, //error while running api or signalr

    /** object for progress bar properties */
    bar: {
      value: null, //value
      show: false, //show progress bar
      query: true, //query
    },
  }),

  methods: {
    /** Start api request and monitoring messages from SignalR hub */
    start() {
      //reset
      this.state = null;
      this.status = null;
      this.progress = [];
      this.result = {};
      this.error = {};
      this.bar.value = null;
      this.bar.show = false;
      this.bar.query = true;

      //check if signalr has been disabled
      if (this.disabled) this.startNoSignalR();
      else this.startSignalR();
    },

    startSignalR() {
      //1. connect to signalR hub
      this.bar.show = true;
      this.status = this.$i18n.t("signalr.connectingToServer");
      this.connection
        .start()
        .then(() => {
          this.state = "started";
          this.status = this.$i18n.t("signalr.connectionEstablished");
          //2. after connected do api request
          this.api(this.connection.connectionId)
            .then(({ data }) => {
              this.result = data;
              if (!this.differentResult) this.progress = data.items;
              this.state = "result";
              this.status = this.$i18n.t("signalr.result");
              this.$emit("set-state", "result");
            })
            .catch((error) => {
              this.error = error;
              this.state = "error";
              this.status = this.$i18n.t("signalr.error");
              this.$emit("set-state", "error");
            })
            .finally(() => {
              this.connection.stop();
              this.bar.show = false;
              this.$emit("on-finished");
            });
        })
        .catch((err) => {
          this.error = err;
          this.state = "error";
          this.bar.show = false;
          this.$emit("set-state", "result");
        });
    },
    startNoSignalR() {
      this.state = "started";
      this.state = this.$i18n.t("signalr.disabled");
      this.bar.show = true;
      //2. after connected do api request
      this.api("disableSignalR")
        .then(({ data }) => {
          if (!this.differentResult) this.progress = data.items;
          this.result = data;
          this.state = "result";
          this.status = this.$i18n.t("signalr.result");
          this.$emit("set-state", "result");
        })
        .catch((error) => {
          this.error = error;
          this.state = "error";
          this.status = this.$i18n.t("signalr.error");
          this.$emit("set-state", "error");
        })
        .finally(() => {
          this.$emit("on-finished");
        });
    },
    /** Intercepts message from SignalR server and pushes body to progress array */
    signalr(data) {
      if (data.subject == this.subject) {
        if (this.state == "started") this.state = "running";
        if (data.bar) this.bar.value = data.bar;
        this.progress.push(data.body);
      }
    },
  },

  computed: {
    showProgressBar: function () {
      //show if state is 'started' or state is 'running' or api call is finished and keep-progress is set
      return (
        this.state == "started" ||
        this.state == "running" ||
        ((this.state == "error" || this.state == "result") && this.keepProgress)
      );
    },
    progressBar: function () {
      //only if api request is running
      return this.state == "started" || this.state == "running"
        ? "intermidiate"
        : "none";
    },
    showProgress: function () {
      //show progress if state is 'result' and result.items are same as progress
      if (!this.differentResult && this.state == "result") return true;
      //if signalr is disabled, dont show
      if (this.disabled) return false;
      //show if state is' running' or state is 'result' or 'error' and keep-signalr is set
      //else dont show
      return (
        this.state == "running" ||
        ((this.state == "result" || this.state == "error") && this.keepSignalr)
      );
    },
    showResult: function () {
      //if result.items same as progress, dont show
      if (!this.differentResult) return false;
      //if state is not 'result', dont show
      if (this.state != "result") return false;
      //else if state is 'result'
      //but signalr is not disabled and keep-signalr is set, dont show
      if (!this.disabled && this.keepSignalr) return false;
      //otherwise, show
      return true;
    },
    showFinished: function () {
      //show if state is 'error' or 'result'
      return this.state == "error" || this.state == "result";
    },
  },

  created() {
    //build hub connection on created
    this.connection = signalr.build();
  },

  mounted() {
    this.disabled = signalr.isDisabled();
    //add listener on mounted
    this.connection.on("notification", (data) => this.signalr(data));
  },
};
</script>

<style></style>
