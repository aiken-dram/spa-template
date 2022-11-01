<template>
  <div>
    <v-tabs v-model="tab">
      <v-tab v-for="t of tabs" :key="t.id" :to="t.route" exact ripple>
        {{ t.name }}
      </v-tab>
    </v-tabs>

    <v-tabs-items v-model="tab">
      <v-tab-item v-for="t of tabs" :key="t.id" :value="t.route">
        <router-view v-if="tab == t.route" />
      </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
export default {
  name: "DictPage",

  data() {
    return {
      tab: "/admin/dict",
      tabs: [
        {
          id: 1,
          name: this.$i18n.t("admin.dict.districts.title"),
          route: "/admin/dict/districts",
        },
        {
          id: 2,
          name: this.$i18n.t("admin.dict.sample.title"),
          route: "/admin/dict/sample",
        },
      ],
    };
  },
};
</script>
