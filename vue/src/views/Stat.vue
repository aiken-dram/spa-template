<template>
  <v-card flat>
    <v-row>
      <v-col cols="3">
        <v-card-title>{{ $t("nav.stat") }}</v-card-title>
        <v-skeleton-loader
          v-if="loading"
          type="list-item-three-line, list-item-three-line, list-item-three-line"
        ></v-skeleton-loader>
        <v-treeview
          v-else
          :items="tree"
          activatable
          transition
          open-on-click
          @update:active="select"
        >
          <template v-slot:prepend="{ item }">
            <v-icon :color="item.color">
              {{ item.icon }}
            </v-icon>
          </template>
        </v-treeview>
      </v-col>
      <v-col>
        <stat-form v-show="selected" ref="StatForm"></stat-form>
      </v-col>
    </v-row>
  </v-card>
</template>

<script>
import StatService from "@/api/stat";

import StatForm from "@/components/Form/Stat";

export default {
  name: "StatPage",

  data: () => ({
    selected: false,
    loading: true,

    active: [],
    tree: [],
  }),

  methods: {
    find(array, id) {
      var result;
      array.some(
        (o) => (result = o.id === id ? o : this.find(o.children || [], id))
      );
      return result;
    },

    select(id) {
      if (!id.length) return;
      const node = this.find(this.tree, id[0]);
      this.selected = true;
      this.$refs.StatForm.open(node);
    },
  },

  mounted() {
    //load tree from API
    StatService.tree()
      .then(({ data }) => {
        this.tree = data.items;
        this.loading = false;
      })
      .catch((error) => {
        this.$root.$error(error);
      });
  },

  components: {
    StatForm,
  },
};
</script>

<style></style>
