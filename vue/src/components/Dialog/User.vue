<template>
  <base-dialog v-model="show" :title="formTitle">
    <user-form
      v-model="valid"
      :id="id"
      @close="close"
      @refresh="onRefresh"
      ref="Form"
    />

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
import UserService from "@/api/user";

import BaseDialog from "@/components/base/Dialog/Dialog";
import UserForm from "@/components/Form/User";

/** Dialog for editing user in account admin */
export default {
  name: "UserDialog",

  data() {
    return {
      id: null,

      show: false,
      valid: true,
    };
  },

  methods: {
    close() {
      this.show = false;
    },

    create() {
      this.id = null;
      this.show = true;
      if (this.$refs.Form) this.$refs.Form.create();
    },

    open(id) {
      this.id = id;
      this.show = true;
      if (this.$refs.Form) this.$refs.Form.open();
    },

    save() {
      this.$refs.Form.save();
    },

    del(idUser) {
      return UserService.delete(idUser)
        .then(() => {
          this.$root.$message(this.$i18n.t("common.deleted"), "success");
          this.$emit("refresh");
        })
        .catch((error) => {
          this.$root.$error(error);
        });
    },

    onRefresh() {
      this.$emit("refresh");
    },
  },

  computed: {
    formTitle() {
      if (this.id == null) return this.$i18n.t("forms.user.newUser");
      else return this.$i18n.t("forms.user.editUser");
    },
  },

  components: {
    BaseDialog,
    UserForm,
  },
};
</script>
