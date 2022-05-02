<template>
  <base-dialog v-model="show" :title="formTitle">
    <base-overlay v-model="overlay" />
    <v-form v-model="valid">
      <base-modelstate v-model="modelstate" />
      <district-form v-model="district" />
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
import DistrictService from "@/api/district";

import BaseDialog from "@/components/base/Dialog/Dialog";
import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import DistrictForm from "@/components/Form/District";

export default {
  name: "DistrictDialog",

  props: {
    refreshTable: Function,
  },

  data() {
    return {
      show: false,
      overlay: false,
      valid: true,
      modelstate: {},

      district: {
        idDistrict: null,
        name: null,
      },
    };
  },

  methods: {
    close() {
      this.show = false;
      this.modelstate = {};
    },

    create() {
      this.district = {};
      this.modelstate = {};
      this.show = true;
    },

    open(id) {
      return DistrictService.get(id)
        .then(({ data }) => {
          this.district = data;
          this.modelstate = {};
          this.show = true;
        })
        .catch((error) => {
          this.$root.$error(error);
        });
    },

    save() {
      this.overlay = true;
      DistrictService.upsert(this.district)
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

    del(id) {
      DistrictService.delete(id)
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
      if (!this.district.idDistrict)
        return this.$i18n.t("forms.district.newDistrict");
      else return this.$i18n.t("forms.district.editDistrict");
    },
    isNew() {
      return !this.district.idDistrict;
    },
  },

  components: {
    BaseDialog,
    BaseOverlay,
    BaseModelstate,
    DistrictForm,
  },
};
</script>
