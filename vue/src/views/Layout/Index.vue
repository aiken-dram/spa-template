<template>
  <v-app>
    <layout-app-bar />

    <layout-navigation />

    <layout-view />

    <error ref="error"></error>
    <message ref="message"></message>
  </v-app>
</template>

<script>
import Error from "@/components/base/DialogError";
import Message from "@/components/base/DialogMessage";

export default {
  name: "LayoutIndex",

  data: () => ({
    expandOnHover: false,
  }),

  components: {
    LayoutAppBar: () => import("./AppBar"),
    LayoutNavigation: () => import("./Navigation"),
    LayoutView: () => import("./ContentView"),
    Error,
    Message,
  },

  mounted() {
    this.$root.$error = this.$refs.error.open;
    this.$root.$message = this.$refs.message.open;
  },
};
</script>
