<template>
  <div>
    <v-tabs v-model="tab">
      <v-tab :key="1">{{ $t("admin.dict.districts.title") }}</v-tab>
    </v-tabs>
    <v-tabs-items v-model="tab">
      <v-tab-item :key="1"> <dict-districts /> </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
import DictDistricts from "./Dict/Districts";

export default {
  name: "DictPage",
  data() {
    return {
      tab: null,
    };
  },

  components: {
    DictDistricts,
  },
};
</script>
