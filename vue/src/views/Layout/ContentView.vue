<template>
  <!-- Sizes your content based upon application components -->
  <v-main>
    <!-- Provides the application the proper gutter -->
    <v-container fluid>
      <v-overlay :value="overlay">
        <v-progress-circular indeterminate size="64"></v-progress-circular>
      </v-overlay>
      <v-progress-linear v-if="loading" indeterminate absolute top>
      </v-progress-linear>
      <!-- If using vue-router -->
      <router-view></router-view>
    </v-container>
  </v-main>
</template>

<script>
export default {
  name: "ContentView",

  computed: {
    overlay: {
      get() {
        return this.$store.state.overlay;
      },
    },
    loading: {
      get() {
        return this.$store.state.loading;
      },
    },
  },
};
</script>
