<template>
  <v-container id="user-profile" fluid tag="section">
    <v-row justify="center">
      <v-col cols="12" md="4">
        <v-card class="v-card-profile">
          <v-overlay :value="overlay">
            <v-progress-circular indeterminate size="64"></v-progress-circular>
          </v-overlay>
          <v-card-title>
            {{ $t("profile.login") }}: {{ user.login }}
          </v-card-title>
          <v-form v-model="valid">
            <base-modelstate v-model="modelstate"> </base-modelstate>

            <current-user-form v-model="user">
              <v-col cols="12" class="text-right">
                <v-btn
                  :disabled="!valid"
                  color="success"
                  class="mr-0"
                  @click.stop="save"
                >
                  {{ $t("profile.update") }}
                </v-btn>
              </v-col>
            </current-user-form>
          </v-form>
        </v-card>

        <v-card class="mt-4">
          <v-card-title> {{ $t("profile.settings") }} </v-card-title>

          <v-form class="v-card-profile">
            <v-container class="py-0">
              <v-row>
                <v-col cols="12">
                  <v-switch
                    v-model="disableSignalR"
                    :label="$t('profile.disableSignalR')"
                  ></v-switch>
                  <v-switch
                    v-model="darkTheme"
                    @change="toggleDark"
                    :label="$t('profile.darkTheme')"
                  ></v-switch>
                </v-col>

                <v-col cols="12" class="text-right">
                  <v-btn color="success" class="mr-0" @click.stop="persist">
                    {{ $t("common.save") }}
                  </v-btn>
                </v-col>
              </v-row>
            </v-container>
          </v-form>
        </v-card>
      </v-col>

      <v-col cols="12" md="8">
        <audit-view
          v-if="user.idUser"
          :idUser="user.idUser"
          current-user
        ></audit-view>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import UserService from "@/api/user";

import BaseModelstate from "@/components/base/Modelstate";
import CurrentUserForm from "@/components/Form/CurrentUser";

import AuditView from "@/components/Display/Audit";

export default {
  name: "UserProfile",

  data: () => ({
    overlay: false,
    valid: true,
    modelstate: {},
    user: {},

    /** local config */
    disableSignalR: false,
    darkTheme: false,
  }),

  mounted() {
    UserService.current().then(({ data }) => {
      this.user = data;
      //alright here we should call
    });

    if (localStorage.disableSignalR) {
      this.disableSignalR = localStorage.disableSignalR == "true";
    }
    if (localStorage.darkTheme) {
      this.darkTheme = localStorage.darkTheme == "true";
    }
  },

  methods: {
    save() {
      this.overlay = true;
      UserService.update(this.user)
        .then(() => {
          this.modelstate = {};
          this.$root.$message(this.$i18n.t("common.editSaved"), "success");
        })
        .catch((error) => {
          this.modelstate = error;
        })
        .finally(() => {
          this.overlay = false;
        });
    },

    persist() {
      localStorage.disableSignalR = this.disableSignalR;
      localStorage.darkTheme = this.darkTheme;
    },

    toggleDark() {
      this.$vuetify.theme.dark = !this.$vuetify.theme.dark;
    },
  },

  computed: {
    ...mapGetters(["dict", "currentUser"]),
  },

  components: {
    BaseModelstate,
    CurrentUserForm,
    AuditView,
  },
};
</script>
