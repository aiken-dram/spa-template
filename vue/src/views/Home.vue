<template>
  <v-container fluid>
    <base-dialog v-model="show" :title="$t('home.newVersion')">
      <v-row class="pt-1">
        <v-col cols="2">
          <strong v-text="versions[0].date"></strong>
        </v-col>
        <v-col cols="10">
          <strong v-text="versions[0].version"></strong>
          <div
            class="caption"
            v-for="(ch, j) in versions[0].changes"
            :key="j"
            v-text="ch"
          ></div>
        </v-col>
      </v-row>
      <template v-slot:buttons>
        <v-btn color="blue darken-1" text @click.stop="show = false">ОK</v-btn>
      </template>
    </base-dialog>

    <v-alert border="top" colored-border type="warning" elevation="2">
      <p>
        {{ $t("home.recommendedBrowser") }}:
        <a href="/Software/ChromeStandaloneSetup.exe"> Google Chrome </a>
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
                <strong v-text="v.date" />
              </v-col>
              <v-col cols="10">
                <strong v-text="v.version" />
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
import VersionService from "@/plugins/version";
import BaseDialog from "@/components/base/Dialog/Dialog";

export default {
  name: "HomePage",

  data: () => ({
    show: false,
    versions: [
      {
        color: "teal lighten-3",
        date: "??.??.2022",
        version: "v0.1-beta",
        changes: ["new version"],
      },
    ],
  }),

  mounted() {
    //check last version and if hasnt been notified, open dialog box with version information
    var version = VersionService.getVersion();
    var current = this.versions[0].version;
    if (version == null) {
      VersionService.saveVersion(current);
    } else {
      if (version != current) {
        this.show = true;
        VersionService.saveVersion(current);
      }
    }
  },

  components: {
    BaseDialog,
  },
};
</script>
