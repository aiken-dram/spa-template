<template>
  <v-card>
    <confirm ref="confirm" />
    <sample-dict-dialog ref="Dialog" @refresh="load" />
    <base-table
      :headers="headers"
      :search="search"
      :data-table="dataTable"
      @click:row="edit"
      ref="Table"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-text-field
            v-model="search"
            append-icon="fa-search"
            :label="$t('common.search')"
            @keydown.enter="load"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer />
          <v-divider class="mx-4" inset vertical />

          <v-btn color="primary" @click.stop="create()">
            {{ $t("common.add") }}...
          </v-btn>
        </v-toolbar>
      </template>

      <template v-slot:item.cmd="{ item }">
        <base-table-action
          icon="fa-trash"
          :tooltip="$t('common.delete')"
          @click.stop="del(item)"
        ></base-table-action>
      </template>
    </base-table>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";
import { SampleDictService } from "@/api/dictionary";

import BaseTable from "@/components/base/Table/Table";
import Confirm from "@/components/base/Dialog/Confirm";
import BaseTableAction from "@/components/base/Table/Action";

import SampleDictDialog from "@/components/Dialog/SampleDict";

export default {
  name: "DictSample",

  data() {
    return {
      search: "", //search in table
      headers: [
        {
          text: this.$i18n.t("common.actions"),
          align: "start",
          value: "cmd",
          sortable: false,
          filterable: false,
          width: 120,
        },
        {
          text: this.$i18n.t("admin.dict.sample.table.dict"),
          value: "dict",
        },
        {
          text: this.$i18n.t("admin.dict.sample.table.description"),
          value: "description",
        },
      ],
    };
  },

  methods: {
    ...mapMutations({
      setOverlay: "SET_OVERLAY",
    }),

    /** Table */
    dataTable() {
      return SampleDictService.list();
    },
    load() {
      this.$refs.Table.load();
    },

    /** CRUD */
    create() {
      //new form
      this.$refs.Dialog.create();
    },
    edit(item) {
      //edit form
      this.$refs.Dialog.open(item);
    },
    del(item) {
      //deleting user
      this.$refs.confirm
        .open(
          this.$i18n.t("common.delete"),
          this.$i18n.t("admin.dict.sample.deleteDict"),
          { color: "red" }
        )
        .then((confirm) => {
          if (confirm) {
            this.setOverlay(true);
            this.$refs.Dialog.del(item.idDict).finally(() => {
              this.setOverlay(false);
            });
          }
        });
    },
  },

  components: {
    Confirm,
    BaseTable,
    BaseTableAction,
    SampleDictDialog,
  },
};
</script>

<style></style>
