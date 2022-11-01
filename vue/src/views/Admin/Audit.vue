<template>
  <audit-view
    :idUser="idUser"
    :idSample="idSample"
    :current-user="currentUser"
  ></audit-view>
</template>

<script>
import AuditView from "@/components/Display/Audit";

export default {
  name: "AuditPage",

  props: {
    idUser: Number,

    idSample: Number,

    currentUser: {
      type: Boolean,
      default: false,
    },
  },

  components: {
    AuditView,
  },
};
</script>

<style></style>
