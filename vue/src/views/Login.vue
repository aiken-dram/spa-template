<template>
  <v-app id="inspire">
    <v-main>
      <v-container class="fill-height" fluid>
        <v-row align="center" justify="center">
          <v-col cols="12" sm="8" md="4">
            <v-card class="elevation-12">
              <v-overlay :value="overlay">
                <v-progress-circular
                  indeterminate
                  size="64"
                ></v-progress-circular>
              </v-overlay>
              <v-toolbar dark flat>
                <v-toolbar-title> {{ $t("login.pageTitle") }} </v-toolbar-title>
                <v-spacer></v-spacer>
                <v-progress-circular
                  v-if="health === null"
                  indeterminate
                ></v-progress-circular>
                <v-icon v-if="health === true" color="success">fa-check</v-icon>
                <v-icon v-if="health === false" color="error">fa-times</v-icon>
              </v-toolbar>
              <v-card-text>
                <v-form @submit.prevent="onSubmit(login, password)">
                  <v-alert type="error" v-show="modelstate['Error']">
                    {{ modelstate["Error"] }}
                  </v-alert>

                  <v-text-field
                    required
                    id="login"
                    name="login"
                    :label="$t('login.username')"
                    v-model="login"
                    :rules="[
                      (v) =>
                        !!v ||
                        $t('forms.common.mustNotBeEmpty', {
                          field: $t('login.username'),
                        }),
                    ]"
                    prepend-icon="fa-user"
                    type="text"
                    autocomplete="login"
                    placeholder=" "
                    persistent-placeholder
                  ></v-text-field>

                  <v-text-field
                    required
                    id="password"
                    name="password"
                    :label="$t('login.password')"
                    v-model="password"
                    :rules="[
                      (v) =>
                        !!v ||
                        $t('forms.common.mustNotBeEmpty', {
                          field: $t('login.password'),
                        }),
                    ]"
                    prepend-icon="fa-lock"
                    type="password"
                    autocomplete="password"
                    placeholder=" "
                    persistent-placeholder
                  ></v-text-field>

                  <div>
                    <v-spacer></v-spacer>
                    <v-btn dark color="primary" block type="submit">
                      {{ $t("login.login") }}
                    </v-btn>
                  </div>
                </v-form>
              </v-card-text>
            </v-card>

            <v-card flat class="mt-6">
              <v-card-text>
                {{ $t("login.applicationAdmin") }}: <br />
                {{ adminContact.name }} {{ $t("common.phone") }}:
                {{ adminContact.phone }}
                <div v-show="accessContact.isDiff">
                  {{ $t("login.accessAdmin") }}: <br />
                  {{ accessContact.name }} {{ $t("common.phone") }}:
                  {{ accessContact.phone }}
                </div>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import { LOGIN } from "@/store/actions.type";
import { JsonService } from "@/api";
import { HealthService } from "@/api";

export default {
  name: "LoginPage",

  data() {
    return {
      overlay: false,
      health: null,

      modelstate: {},
      adminContact: {},
      accessContact: {},

      login: null,
      password: null,
    };
  },

  mounted() {
    JsonService.contacts().then((response) => {
      this.adminContact = response.data.admin;
      this.accessContact = response.data.access;
    });
    HealthService.health()
      .then(() => (this.health = true))
      .catch(() => (this.health = false));
  },

  methods: {
    onSubmit(login, password) {
      this.overlay = true;
      this.$store
        .dispatch(LOGIN, { login, password })
        .then(() => this.$router.push("/"))
        .catch((error) => {
          this.modelstate = error;
        })
        .finally(() => {
          this.overlay = false;
        });
    },
  },
};
</script>
