<template>
  <base-dialog v-model="show" :title="formTitle">
    <base-overlay v-model="overlay"></base-overlay>
    <v-form v-model="valid">
      <base-modelstate v-model="modelstate"> </base-modelstate>
      <user-form v-model="user"></user-form>
    </v-form>

    <template v-slot:buttons>
      <v-btn color="blue darken-1" text @click.stop="close">
        {{ $t("common.cancel") }}
      </v-btn>
      <v-btn color="blue darken-1" text :disabled="!valid" @click.stop="save">
        {{ $t("common.save") }}
      </v-btn>
    </template>
  </base-dialog>
</template>

<script>
import { UserService } from "@/plugins/api";

import BaseDialog from "@/components/base/Dialog";
import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import UserForm from "@/components/Form/User";

export default {
  name: "UserDialog",

  props: {
    refreshTable: Function,
  },

  data() {
    return {
      show: false,
      overlay: false,
      valid: true,
      modelstate: {},

      user: {
        login: null,
        password: null,
        name: null,
        desc: null,
        isActive: null,
        isExpired: null,
        passDate: null,
        groups: [],
        roles: [],
      },
    };
  },

  methods: {
    close() {
      this.show = false;
      this.modelstate = {};
    },

    create() {
      this.user = {};
      this.modelstate = {};
      this.show = true;
    },

    open(idUser) {
      return UserService.get(idUser)
        .then(({ data }) => {
          this.user = data;
          this.modelstate = {};
          this.show = true;
        })
        .catch((error) => {
          this.$root.$error(error);
        });
    },

    save() {
      this.overlay = true;
      UserService.edit(this.user)
        .then(() => {
          this.close();
          this.refreshTable();
          this.$root.$message(this.$i18n.t("common.editSaved"), "success");
        })
        .catch((error) => {
          console.log(error);
          this.modelstate = error;
        })
        .finally(() => {
          this.overlay = false;
        });
    },

    del(idUser) {
      UserService.delete(idUser)
        .then(() => {
          this.$root.$message(this.$i18n.t("common.deleted"), "success");
        })
        .catch((error) => {
          this.$root.$error(error);
        })
        .finally(() => {
          this.refreshTable();
        });
    },
  },

  computed: {
    formTitle() {
      if (!this.user.idUser) return this.$i18n.t("forms.user.newUser");
      else return this.$i18n.t("forms.user.editUser");
    },
    isNew() {
      return !this.user.idUser;
    },
  },

  components: {
    BaseDialog,
    BaseOverlay,
    BaseModelstate,
    UserForm,
  },
};
</script>
