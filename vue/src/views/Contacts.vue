<template>
  <v-row no-gutters>
    <v-col
      v-for="contact in contacts.list"
      :key="contact.type"
      cols="4"
      class="mr-4"
    >
      <v-card>
        <v-card-title v-text="contact.type"></v-card-title>

        <v-card-text>
          <div class="my-4 subtitle-1" v-text="contact.name"></div>

          <div v-text="contact.dept"></div>
        </v-card-text>

        <v-divider class="mx-4"></v-divider>

        <v-list two-line>
          <v-list-item v-for="p in contact.phone" :key="p.type">
            <v-list-item-icon>
              <v-icon color="indigo">mdi-phone</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title v-text="p.phone"></v-list-item-title>
              <v-list-item-subtitle v-text="p.type"></v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>

          <v-divider inset></v-divider>

          <v-list-item v-for="m in contact.mail" :key="m.type">
            <v-list-item-icon>
              <v-icon color="indigo">mdi-email</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title v-text="m.address"></v-list-item-title>
              <v-list-item-subtitle v-text="m.type"></v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import { JsonService } from "@/plugins/api";

export default {
  name: "ContactsPage",
  data: () => ({
    contacts: {},
  }),

  mounted() {
    JsonService.contacts().then((response) => {
      this.contacts = response.data;
    });
  },
};
</script>
