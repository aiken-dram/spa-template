<template>
  <div>
    <v-tabs v-model="tab">
      <v-tab :key="1">{{ $t("admin.access.users.title") }}</v-tab>
      <v-tab :key="2">{{ $t("admin.access.package.title") }}</v-tab>
      <v-tab :key="3">{{ $t("admin.access.file.title") }}</v-tab>
    </v-tabs>
    <v-tabs-items v-model="tab">
      <v-tab-item :key="1"> <access-users></access-users> </v-tab-item>
      <v-tab-item :key="2"> <access-package></access-package> </v-tab-item>
      <v-tab-item :key="3"> <access-file></access-file> </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
import AccessUsers from "./Access/Users";
import AccessPackage from "./Access/Package";
import AccessFile from "./Access/File";

export default {
  name: "AccessPage",
  data() {
    return {
      tab: null,
    };
  },

  components: {
    AccessUsers,
    AccessPackage,
    AccessFile,
  },
};
</script>
