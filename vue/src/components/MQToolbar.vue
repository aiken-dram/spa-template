<template>
  <v-menu
    open-on-hover
    bottom
    offset-y
    :disabled="disabled"
    max-width="490px"
    min-width="490px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-badge
        color="amber darken-1"
        :value="cnt > 0 ? cnt : null"
        :content="cnt"
        overlap
      >
        <v-btn
          :color="getColor()"
          icon
          to="/mq"
          :loading="loading"
          v-bind="attrs"
          v-on="on"
        >
          <v-icon>fa-envelope</v-icon>
        </v-btn>
      </v-badge>
    </template>

    <v-list dense>
      <template v-for="(item, i) in items">
        <v-list-item :key="i">
          <v-list-item-avatar>
            <v-icon v-if="item.state == 'QUEUE'" color="info">
              fa-hourglass-start
            </v-icon>
            <v-icon v-if="item.state == 'PROCESSING'" color="success">
              fa-cog fa-spin
            </v-icon>
            <v-icon v-if="item.state == 'READY'" color="amber">
              fa-envelope
            </v-icon>
            <v-icon v-if="item.state == 'ERROR'" color="error">
              fa-exclamation-circle
            </v-icon>
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title v-text="item.typeDesc" />

            <v-list-item-subtitle>
              <span>
                {{ item.created | datetime }}
              </span>
              <span v-if="item.state == 'READY'" class="float-right">
                <strong>{{ $t("common.requestProcessed") }}:</strong>
                {{ item.processed | datetime }}
              </span>
            </v-list-item-subtitle>

            <v-list-item-subtitle
              v-if="item.message"
              v-text="item.message"
            ></v-list-item-subtitle>
          </v-list-item-content>

          <v-list-item-action>
            <v-btn
              icon
              v-if="item.state == 'READY'"
              color="primary"
              align-self="end"
              @click="download(item)"
            >
              <v-icon>fa-save</v-icon>
            </v-btn>
          </v-list-item-action>
        </v-list-item>

        <v-divider
          v-if="i < items.length - 1"
          :key="`divider-${i}`"
        ></v-divider>
      </template>
    </v-list>
  </v-menu>
</template>

<script>
import { mapMutations } from "vuex";
import { download } from "@/common/file";
import MQService from "@/api/mq";

export default {
  name: "MQToolbar",

  data: () => ({
    cnt: 0,
    cntTotal: 0,
    cntInterval: "",
    loading: false,
    disabled: true,

    items: [],
  }),

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),

    getColor() {
      if (this.cnt > 0) return "amber";
      if (this.cnt < 0) return "grey";
      return "";
    },

    /** update mq state for current user from api */
    update() {
      this.loading = true;
      MQService.toolbar()
        .then(({ data }) => {
          //console.log(data);
          this.cnt = data.countReady;
          this.cntTotal = data.countTotal;
          this.items = data.items;
          this.disabled = !(this.items && this.items.length > 0);
        })
        .catch(() => {
          //error while retrieving count of ready documents
          this.cnt = -1;
          this.cntTotal = -1;
          this.items = [];
          this.disabled = true;
        })
        .finally(() => {
          this.loading = false;
        });
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
          this.update();
        });
    },

    createInterval() {
      this.cntInterval = setInterval(() => {
        this.update();
      }, 60000); //every 60 seconds
    },
  },

  mounted() {
    //lets update now
    this.update();
    this.$root.$MessageQuery = this.update;
  },

  created() {
    this.createInterval();
  },

  beforeDestroy() {
    clearInterval(this.cntInterval);
  },
};
</script>

<style></style>
