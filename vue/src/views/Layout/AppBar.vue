<template>
  <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app dark>
    <v-app-bar-nav-icon @click.stop="switchDrawer()" />

    <v-toolbar-title class="hidden-sm-and-down font-weight-light mr-2">
      {{ $t("common.appName") }}
    </v-toolbar-title>

    <v-spacer />

    <v-btn small text :href="HELP.root + doc.href" target="_blank" class="mr-2">
      <v-icon left small>fa-question-circle</v-icon> {{ $t(doc.name) }}
    </v-btn>

    <mq-toolbar ref="MessageQuery" />

    <v-menu
      bottom
      left
      offset-y
      origin="top right"
      transition="scale-transition"
    >
      <template v-slot:activator="{ attrs, on }">
        <v-btn text v-bind="attrs" v-on="on">
          <v-icon left>fa-user</v-icon>
          {{ currentUser.userName }}
        </v-btn>
      </template>

      <v-list dense>
        <v-list-item to="/user">
          <v-list-item-icon>
            <v-icon>fa-user</v-icon>
          </v-list-item-icon>
          <v-list-item-title>{{ $t("layout.profile") }}</v-list-item-title>
        </v-list-item>

        <v-list-item @click="logout()">
          <v-list-item-icon>
            <v-icon>fa-sign-out-alt</v-icon>
          </v-list-item-icon>
          <v-list-item-title>{{ $t("layout.logOff") }}</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
  </v-app-bar>
</template>

<script>
// Utilities
import { mapState, mapMutations, mapGetters } from "vuex";
import { LOGOUT } from "@/store/actions.type";
import { HELP } from "@/common/config";

import MqToolbar from "@/components/MQToolbar";

export default {
  name: "LayoutAppBar",

  computed: {
    ...mapState(["drawer"]),
    ...mapState(["minify"]),
    ...mapGetters(["currentUser", "isAuthenticated"]),
    doc() {
      return HELP.routes.find(
        (p) => p.route == this.$route.path || p.route == null
      );
    },
  },

  methods: {
    ...mapMutations({
      setDrawer: "SET_DRAWER",
      setMinify: "SET_MINIFY",
    }),
    switchDrawer() {
      if (this.drawer && this.minify) {
        this.setDrawer(!this.drawer);
        this.setMinify(!this.minify);
        return;
      }
      if (this.drawer && !this.minify) {
        this.setMinify(!this.minify);
        return;
      }
      if (!this.drawer) {
        this.setDrawer(!this.drawer);
        return;
      }
    },
    logout: function () {
      this.$store.dispatch(LOGOUT).then(() => {
        this.$router.push("/login");
      });
    },
  },

  beforeCreate() {
    this.HELP = HELP;
  },

  components: {
    MqToolbar,
  },
};
</script>
