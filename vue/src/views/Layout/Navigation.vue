<template>
  <v-navigation-drawer
    v-model="drawer"
    app
    :expand-on-hover="minify"
    :mini-variant="minify && drawer"
    :clipped="$vuetify.breakpoint.lgAndUp"
    width="290"
  >
    <v-list dense nav>
      <template v-for="item in access(items)">
        <v-list-group v-if="item.children" :key="item.name">
          <template v-slot:activator>
            <v-list-item-icon>
              <v-icon v-text="item.icon" />
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title v-text="item.name" />
            </v-list-item-content>
          </template>

          <v-list-item
            v-for="child in access(item.children)"
            :key="child.name"
            :to="child.route"
            link
          >
            <v-list-item-icon>
              <v-avatar
                v-if="minify"
                size="24"
                v-text="child.avatar"
              ></v-avatar>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title v-text="child.name" />
            </v-list-item-content>
          </v-list-item>
        </v-list-group>

        <v-list-item
          v-else-if="item.route"
          :key="item.name"
          :to="item.route"
          link
        >
          <v-list-item-icon>
            <v-icon v-text="item.icon" />
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.name" />
          </v-list-item-content>
        </v-list-item>

        <v-list-item
          v-else
          :key="item.name"
          :href="item.href"
          :target="item.target"
        >
          <v-list-item-icon>
            <v-icon v-text="item.icon" />
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.name" />
          </v-list-item-content>
        </v-list-item>
      </template>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
import { mapGetters } from "vuex";
import { MODULES } from "@/common/config";

export default {
  name: "NavigationPage",

  data: function () {
    return {
      items: [
        {
          name: this.$t("nav.home"),
          route: "/",
          icon: "fa-home",
        },
        {
          name: this.$t("nav.sample"),
          icon: "fa-globe",
          children: [
            {
              name: this.$t("nav.sampleWork"),
              route: "/sample",
              avatar: this.$t("nav.sampleWorkAvatar"),
            },
          ],
        },
        {
          name: this.$t("nav.stat"),
          route: "/stat",
          icon: "fa-chart-bar",
        },
        {
          name: this.$t("nav.mq"),
          route: "/mq",
          icon: "fa-mail-bulk",
        },
        {
          name: this.$t("nav.admin"),
          icon: "fa-cog",
          children: [
            {
              name: this.$i18n.t("nav.adminAccess"),
              route: "/admin/access",
              avatar: this.$i18n.t("nav.adminAccessAvatar"),
              role: MODULES.SecurityAdmin,
            },
            {
              name: this.$i18n.t("nav.adminAudit"),
              route: "/admin/audit",
              avatar: this.$i18n.t("nav.adminAuditAvatar"),
              role: MODULES.SecurityAdmin,
            },
            {
              name: this.$i18n.t("nav.adminDict"),
              route: "/admin/dict",
              avatar: this.$i18n.t("nav.adminDictAvatar"),
            },
            {
              name: this.$i18n.t("nav.adminStat"),
              route: "/admin/stat",
              avatar: this.$i18n.t("nav.adminStatAvatar"),
              role: MODULES.ConfigurationAdmin,
            },
          ],
        },
        {
          name: this.$t("nav.doc"),
          href: "/doc/",
          target: "_blank",
          icon: "fa-question-circle",
        },
        {
          name: this.$t("nav.contacts"),
          route: "/contacts",
          icon: "fa-envelope",
        },
      ],
    };
  },

  methods: {
    access(items) {
      var modules = this.currentUser.userModules;
      if (modules) {
        return items.filter(
          (p) =>
            (!p.role || modules.includes(p.role)) &&
            (!p.deny || !modules.includes(p.deny))
        );
      } else return items;
    },
  },

  computed: {
    ...mapGetters(["currentUser"]),
    drawer: {
      get() {
        return this.$store.state.drawer;
      },
      set(val) {
        this.$store.commit("SET_DRAWER", val);
      },
    },
    minify: {
      get() {
        return this.$store.state.minify;
      },
      set(val) {
        this.$store.commit("SET_MINIFY", val);
      },
    },
  },
};
</script>
