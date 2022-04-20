<template>
  <div>
    <v-tabs v-model="tab">
      <v-tab v-for="t of tabs" :key="t.id" :to="t.route" exact ripple>
        {{ t.name }}
      </v-tab>
    </v-tabs>

    <v-tabs-items v-model="tab">
      <v-tab-item v-for="t of tabs" :key="t.id" :value="t.route">
        <router-view v-if="tab == t.route"></router-view>
      </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
export default {
  name: "AccessPage",

  data() {
    return {
      tab: "/admin/access",
      tabs: [
        {
          id: 1,
          name: this.$i18n.t("admin.access.users.title"),
          route: "/admin/access",
        },
        {
          id: 2,
          name: this.$i18n.t("admin.access.package.title"),
          route: "/admin/access/package",
        },
        {
          id: 3,
          name: this.$i18n.t("admin.access.file.title"),
          route: "/admin/access/file",
        },
      ],
    };
  },
};
</script>
