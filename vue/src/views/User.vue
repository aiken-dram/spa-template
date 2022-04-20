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
            <v-container class="py-0">
              <v-row>
                <v-col cols="12" v-show="modelstate['Error']">
                  <v-alert type="error">
                    {{ modelstate["Error"] }}
                  </v-alert>
                </v-col>

                <v-col cols="12">
                  <base-text-field
                    :label="$t('profile.userName')"
                    v-model="user.name"
                    :rules="[
                      (v) =>
                        (v || '').length <= 120 ||
                        $t('forms.common.maxLength', { length: '120' }),
                    ]"
                    counter="120"
                  ></base-text-field>
                </v-col>

                <v-col cols="12">
                  <base-text-field
                    :label="$t('profile.desc')"
                    v-model="user.desc"
                    :rules="[
                      (v) =>
                        (v || '').length <= 255 ||
                        $t('forms.common.maxLength', { length: '255' }),
                    ]"
                    counter="255"
                  ></base-text-field>
                </v-col>

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
              </v-row>
            </v-container>
          </v-form>
        </v-card>
      </v-col>

      <v-col cols="12" md="8"> </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { UserService } from "@/plugins/api";

import BaseTextField from "@/components/base/TextField";

export default {
  name: "UserProfile",

  data: () => ({
    overlay: false,
    valid: true,
    modelstate: {},
    user: {},
  }),

  mounted() {
    UserService.current().then(({ data }) => {
      this.user = data;
    });
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
  },

  computed: {
    ...mapGetters(["dict", "currentUser"]),
  },

  components: {
    BaseTextField,
  },
};
</script>
