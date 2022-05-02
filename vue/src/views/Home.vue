<template>
  <v-container fluid>
    <v-alert border="top" colored-border type="warning" elevation="2">
      <p>
        {{ $t("home.recommendedBrowser") }}:
        <a href="https://www.google.com/chrome/"> Google Chrome </a>
      </p>
    </v-alert>

    <v-alert border="top" colored-border type="info" elevation="2">
      <p>
        <a href="/doc/" target="_blank">
          {{ $t("home.documentationLink") }}
        </a>
      </p>
    </v-alert>

    <v-card>
      <v-card-title>{{ $t("home.versionHistory") }}: </v-card-title>
      <v-card-text>
        <v-timeline align-top dense>
          <v-timeline-item v-for="(v, i) in versions" :key="i" :color="v.color">
            <v-row class="pt-1">
              <v-col cols="2">
                <strong v-text="v.date"></strong>
              </v-col>
              <v-col cols="10">
                <strong v-text="v.version"></strong>
                <div
                  class="caption"
                  v-for="(ch, j) in v.changes"
                  :key="j"
                  v-text="ch"
                ></div>
              </v-col>
            </v-row>
          </v-timeline-item>
        </v-timeline>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import { JsonService } from "@/api";

export default {
  name: "HomePage",

  data: () => ({
    versions: [],
  }),

  mounted() {
    JsonService.versions().then((response) => {
      this.versions = response.data;
    });
  },
};
</script>
