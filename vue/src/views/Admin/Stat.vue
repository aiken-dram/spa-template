<template>
  <v-row>
    <v-col cols="4">
      <v-card flat>
        <v-card-title>{{ $t("nav.stat") }}</v-card-title>
        <base-overlay v-model="overlay" />
        <v-treeview ref="Tree" :items="tree" transition open-all>
          <template v-slot:prepend="{ item }">
            <v-icon :color="item.color">
              {{ item.icon }}
            </v-icon>
          </template>
          <template v-slot:append="{ item }">
            <v-btn icon small @click="edit(item)">
              <v-icon small>fa-edit</v-icon>
            </v-btn>
            <v-btn :disabled="cannotChild(item)" icon small @click="add(item)">
              <v-icon small>fa-folder-plus</v-icon>
            </v-btn>
            <v-btn :disabled="cannotDelete(item)" icon small @click="del(item)">
              <v-icon small>fa-times-circle</v-icon>
            </v-btn>
          </template>
          <template v-slot:label="{ item }">
            <span :class="active == item.id ? 'font-weight-bold' : null">
              {{ item.name }}
            </span>
          </template>
        </v-treeview>
        <v-btn icon @click="add()">
          <v-icon>fa-plus-square</v-icon>
        </v-btn>
      </v-card>
    </v-col>
    <v-col>
      <r-script-form @save="load" ref="RScriptForm"></r-script-form>
    </v-col>
  </v-row>
</template>

<script>
import StatService from "@/api/stat";
import RScriptService from "@/api/rscript";

import BaseOverlay from "@/components/base/Overlay";
import RScriptForm from "@/components/Form/RScriptTree";

export default {
  name: "StatPage",

  data: () => ({
    overlay: true,
    active: null,
    tree: [],
  }),

  methods: {
    load() {
      this.overlay = true;
      this.active = null;
      StatService.tree()
        .then(({ data }) => {
          this.tree = data.items;
          this.overlay = false;
        })
        .catch((error) => {
          this.$root.$error(error);
        })
        .finally(() => {
          this.$refs.Tree.updateAll(true);
          this.overlay = false;
        });
    },

    add(item) {
      this.$refs.RScriptForm.add(item);
    },
    edit(item) {
      this.active = item.id;
      this.$refs.RScriptForm.edit(item);
    },
    del(item) {
      //delete node
      this.overlay = true;
      RScriptService.tree
        .delete(item.id)
        .catch((error) => {
          this.$root.$error(error);
        })
        .finally(() => {
          this.load();
        });
    },

    /*searchElement(element, id) {
      if (element.id == id) {
        return element;
      } else if (element.children != null) {
        var i;
        var result = null;
        for (i = 0; result == null && i < element.children.length; i++) {
          result = this.searchElement(element.children[i], id);
        }
        return result;
      }
      return null;
    },
    searchTree(tree, id) {
      var i;
      var result = null;
      for (i = 0; result == null && i < tree.length; i++) {
        result = this.searchElement(tree[i], id);
      }
      return result;
    },*/

    cannotDelete(item) {
      //dont delete nodes with children
      return item.children && item.children.length > 0;
    },
    cannotChild(item) {
      //cant add children to rscript nodes
      return item.idRScript ? true : false;
    },
  },

  mounted() {
    this.load();
  },

  components: {
    BaseOverlay,
    RScriptForm,
  },
};
</script>

<style></style>
