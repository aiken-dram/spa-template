<template>
  <div>
    <v-tabs v-model="tab">
      <v-tab :key="1">{{ $t("admin.dict.raions.title") }}</v-tab>
    </v-tabs>
    <v-tabs-items v-model="tab">
      <v-tab-item :key="1"> <dict-raions></dict-raions> </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
import DictRaions from "./Dict/Raions";

export default {
  name: "DictPage",
  data() {
    return {
      tab: null,
    };
  },

  components: {
    DictRaions,
  },
};
</script>
