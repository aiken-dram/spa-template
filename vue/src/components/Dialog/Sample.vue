<template>
  <base-dialog-fullscreen v-model="show" :title="formTitle" @close="close">
    <template v-slot:buttons>
      <v-btn text :disabled="!valid" @click.stop="save">
        {{ $t("common.save") }}
      </v-btn>
      <v-divider vertical></v-divider>
      <v-btn icon :to="`/sample/get/${id}`">
        <v-icon>fa-external-link-alt</v-icon>
      </v-btn>
    </template>

    <template v-slot:extension>
      <v-tabs align-with-title v-model="tab">
        <v-tab :key="1">{{ $t("forms.sample.dataTab") }}</v-tab>
        <v-tab :key="2">{{ $t("forms.sample.historyTab") }}</v-tab>
      </v-tabs>
    </template>

    <v-tabs-items v-model="tab">
      <v-tab-item :key="1">
        <sample-form
          v-model="valid"
          :id="id"
          @close="close"
          @refresh="onRefresh"
          ref="Form"
        />
      </v-tab-item>
      <v-tab-item :key="2">
        <audit-view :idSample="id"></audit-view>
      </v-tab-item>
    </v-tabs-items>
  </base-dialog-fullscreen>
</template>

<script>
import SampleService from "@/api/sample";

import BaseDialogFullscreen from "@/components/base/Dialog/DialogFullscreen";
import SampleForm from "@/components/Form/Sample";
import AuditView from "@/components/Display/Audit";

/** Dialog for editing sample */
export default {
  name: "SampleDialog",

  data() {
    return {
      id: null,
      show: false,
      tab: null,
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
      if (this.$refs.History) this.$refs.Form.open();
    },

    save() {
      this.$refs.Form.save();
    },

    del(id) {
      return SampleService.delete(id)
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
      if (this.id == null) return this.$i18n.t("forms.sample.newSample");
      else return this.$i18n.t("forms.sample.editSample");
    },
  },

  components: {
    BaseDialogFullscreen,
    SampleForm,
    AuditView,
  },
};
</script>
