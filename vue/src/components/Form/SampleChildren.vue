<template>
  <div>
    <div class="text-h5">{{ $t("forms.sample.children") }}</div>

    <v-card v-for="(c, i) in val" :key="i" class="mt-2">
      <v-toolbar flat color="grey lighten-4" dense>
        {{ $t("forms.sample.child") }}
        <v-spacer></v-spacer>

        <v-btn icon color="error" @click="del(c.idChild)">
          <v-icon>fa-times-circle</v-icon>
        </v-btn>
      </v-toolbar>

      <v-card-text>
        <base-text-field
          v-model="c.text"
          t-label="forms.sample.text"
          rule-preset
          :rule-max="120"
          counter="120"
        ></base-text-field>
      </v-card-text>
    </v-card>

    <v-btn color="primary" @click="add()" class="mt-2">
      <v-icon left>fa-plus</v-icon> {{ $t("common.add") }}
    </v-btn>
  </div>
</template>

<script>
import BaseTextField from "@/components/base/TextField";

export default {
  name: "SampleChildrenForm",

  props: {
    value: Array,
  },

  methods: {
    add() {
      var maxId =
        this.val.length > 0
          ? Math.max.apply(
              Math,
              this.val.map((p) => p.idChild)
            )
          : 0;
      var p = {
        idChild: maxId + 1,
        isNew: true,
      };
      this.val.push(p);
      this.$nextTick(() => {
        this.$emit("validate");
      });
    },
    del(nn) {
      //console.log(nn);
      for (var i = 0; i < this.val.length; i++) {
        if (this.val[i].idChild === nn) {
          this.val.splice(i, 1);
        }
      }
    },
  },

  computed: {
    /** get and set value */
    val: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },
  },

  components: {
    BaseTextField,
  },
};
</script>

<style></style>
