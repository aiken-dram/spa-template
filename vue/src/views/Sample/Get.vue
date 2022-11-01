<template>
  <v-card>
    <v-toolbar color="primary" dark>
      <v-toolbar-title>{{ formTitle }}</v-toolbar-title>
      <v-spacer />
      <v-btn text @click="save()" :disabled="!valid">
        {{ $t("common.save") }}
      </v-btn>
      <template v-slot:extension>
        <v-tabs align-with-title v-model="tab">
          <v-tab :key="1">{{ $t("forms.sample.dataTab") }}</v-tab>
          <v-tab :key="2">{{ $t("forms.sample.historyTab") }}</v-tab>
        </v-tabs>
      </template>
    </v-toolbar>

    <v-tabs-items v-model="tab">
      <v-tab-item :key="1">
        <v-container>
          <sample-form ref="Form" v-model="valid" :id="id" />
        </v-container>
      </v-tab-item>
      <v-tab-item :key="2">
        <audit-view :idSample="id"></audit-view>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
import SampleForm from "@/components/Form/Sample";
import AuditView from "@/components/Display/Audit";

export default {
  name: "SampleGet",

  props: {
    id: Number,
  },

  data: () => ({
    valid: true,
    tab: null,
  }),

  methods: {
    save() {
      this.$refs.Form.save();
    },
  },

  mounted() {
    if (this.$refs.Form) this.$refs.Form.open();
  },

  computed: {
    formTitle() {
      if (this.id == null) return this.$i18n.t("forms.sample.newSample");
      else return this.$i18n.t("forms.sample.editSample");
    },
  },

  components: {
    SampleForm,
    AuditView,
  },
};
</script>

<style></style>
