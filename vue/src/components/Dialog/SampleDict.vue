<template>
  <base-dialog v-model="show" :title="formTitle">
    <sample-dict-form
      v-model="valid"
      :item="item"
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
import { SampleDictService } from "@/api/dictionary";

import BaseDialog from "@/components/base/Dialog/Dialog";
import SampleDictForm from "@/components/Form/SampleDict";

/** Dialog for editing sample dictionary */
export default {
  name: "SampleDictDialog",

  data() {
    return {
      item: {},
      show: false,
      valid: true,
    };
  },

  methods: {
    close() {
      this.show = false;
    },

    create() {
      this.item = {};
      this.show = true;
      if (this.$refs.Form) this.$refs.Form.create();
    },

    open(item) {
      this.item = item;
      this.show = true;
      if (this.$refs.Form) this.$refs.Form.open();
    },

    save() {
      this.$refs.Form.save();
    },

    del(id) {
      return SampleDictService.delete(id)
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
      if (Object.keys(this.item).length === 0)
        return this.$i18n.t("forms.common.new");
      else return this.$i18n.t("forms.common.edit");
    },
  },

  components: {
    BaseDialog,
    SampleDictForm,
  },
};
</script>
