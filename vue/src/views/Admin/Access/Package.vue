<template>
  <v-card>
    <v-overlay :value="overlay">
      <v-progress-circular indeterminate size="64"></v-progress-circular>
    </v-overlay>
    <v-card-title>
      {{ $t("admin.access.package.accessTable") }}
      <v-btn color="primary" dark small absolute right fab @click="addPackage">
        <v-icon>fa-plus</v-icon>
      </v-btn>
    </v-card-title>
    <v-toolbar flat dense v-for="p in packages" :key="p.id">
      <v-btn icon @click="removePackage(p.id)">
        <v-icon color="error">fa-minus-square</v-icon>
      </v-btn>
      <v-text-field
        :label="$t('admin.access.package.table.login')"
        v-model="p.login"
        :disabled="p.processed"
        single-line
        solo
        dense
        hide-details
        class="shrink"
      ></v-text-field>
      <v-text-field
        :label="$t('admin.access.package.table.password')"
        v-model="p.pass"
        :disabled="p.processed"
        single-line
        solo
        dense
        hide-details
        class="shrink ml-2"
      ></v-text-field>
      <v-text-field
        :label="$t('admin.access.package.table.userName')"
        v-model="p.name"
        :disabled="p.processed"
        single-line
        solo
        dense
        hide-details
        class="shrink ml-2"
      ></v-text-field>
      <v-select
        :label="$t('admin.access.package.table.groups')"
        v-model="p.group"
        :items="dict(DICT.AccessGroups)"
        :disabled="p.processed"
        single-line
        solo
        dense
        hide-details
        class="shrink ml-2"
        style="width: 200px"
      ></v-select>

      <v-icon v-if="p.processed" color="success" class="ml-2">fa-check</v-icon>

      <v-tooltip v-if="p.error" left>
        <template v-slot:activator="{ on, attrs }">
          <v-icon v-bind="attrs" v-on="on" class="ml-2" color="error">
            fa-exclamation-triangle
          </v-icon>
        </template>
        <span v-text="p.error" />
      </v-tooltip>

      <v-toolbar-title v-show="p.result" v-text="p.result" />
    </v-toolbar>
    <v-card-actions>
      <v-btn class="ml-2" @click="processPackages">
        {{ $t("common.process") }}
      </v-btn>
      <v-btn class="ml-2" text @click="resetPackages">
        {{ $t("common.cancel") }}
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { DICT } from "@/common/config";
import { mapGetters } from "vuex";
import UserService from "@/api/user";

export default {
  name: "AccessPackage",

  data: () => ({
    id: 1,
    packages: [],
    overlay: false,
  }),

  methods: {
    addPackage() {
      //
      var p = {
        id: this.id++,
        processed: false,
        error: null,

        login: null,
        password: null,
        name: null,
        district: null,
        group: null,
      };
      this.packages.push(p);
    },
    processPackages() {
      //lets check errors first
      this.packages
        .filter(
          (p) =>
            p.processed == false && (!p.login || !p.pass || !p.name || !p.group)
        )
        .forEach(
          (p) =>
            (p.error = this.$i18n.t(
              "admin.access.package.requiredFieldsNotSet"
            ))
        );

      //tricky part here
      var toProcess = this.packages.filter(
        (p) =>
          p.processed == false && !!p.login && !!p.pass && !!p.name && !!p.group
      );
      this.overlay = true;
      Promise.all(
        toProcess.map(function (p) {
          var data = {
            login: p.login,
            password: p.pass,
            isActive: "T",
            name: p.name,
            desc: null,
            phone: null,
            groups: [p.group],
            roles: [],
            districts: p.district ? [p.district] : [],
          };
          return UserService.edit(data)
            .then(() => {
              p.processed = true;
            })
            .catch((error) => {
              p.error = error;
            });
        })
      ).finally(() => {
        this.overlay = false;
      });
    },
    removePackage(id) {
      for (var i = 0; i < this.packages.length; i++) {
        if (this.packages[i].id === id) {
          this.packages.splice(i, 1);
        }
      }
    },
    resetPackages() {
      //
      this.packages = [];
      this.id = 1;
    },
  },

  computed: {
    ...mapGetters(["dict"]),
  },

  beforeCreate() {
    this.DICT = DICT;
  },
};
</script>
