<template>
  <div>
    <div v-if="item.idTarget == TARGETS.Auth">
      <!-- Authorization audit -->
      {{ $t("audit.data.remoteSystem") }}: <kbd>{{ item.message }}</kbd>
    </div>

    <div v-else-if="item.idTarget == TARGETS.MessageQueryRequest">
      <!-- Message query audit -->
      <strong>{{ $t("audit.data.requestParams") }}:</strong>&nbsp;
      <kbd>{{ parse(item.auditData)[0].json.Value }}</kbd>
    </div>

    <div v-else>
      <!-- Creating and Editing -->

      <div v-for="(d, i) in parse(item.auditData)" :key="i">
        <div v-if="d.idType == DATATYPES.FieldOperationValue">
          <strong>{{ $t(translateField(d.json.Field)) }}:</strong>&nbsp;
          <kbd>{{ $t(translateOperation(d.json.Operation)) }}</kbd> &nbsp;
          <kbd>{{ d.json.Value }}</kbd>
        </div>

        <div v-if="d.idType == DATATYPES.FieldOldNew">
          <strong>{{ $t(translateField(d.json.Field)) }}:</strong>&nbsp;
          <kbd>{{ d.json.Old }}</kbd> &nbsp;
          {{ $t("audit.data.history.replacedWith") }}&nbsp;
          <kbd>{{ d.json.New }}</kbd>
        </div>

        <div v-if="d.idType == DATATYPES.FieldValue">
          <strong>{{ $t(translateField(d.json.Field)) }}:</strong>&nbsp;
          <kbd>{{ d.json.Value }}</kbd>
        </div>

        <div v-if="d.idType == DATATYPES.Value">
          {{ $t("audit.data.history.field") }}
          <strong>{{ $t(translateField(d.json.Field)) }}</strong>
          {{ $t("audit.data.history.updated") }}
        </div>
      </div>
    </div>

    <div v-if="item.idAction == ACTIONS.Delete">
      <!-- Creating -->
    </div>
  </div>
</template>

<script>
/**
 * Display for audit data
 * this would be perfect opportunity to use js vue component generation
 * or not, no idea how complex this will become, maybe i'll use a v-list or something?
 * */
export default {
  name: "AuditDataDisplay",

  props: {
    item: Object,
  },

  data: () => ({
    TARGETS: {
      Auth: 1,
      AccountUser: 2,
      MessageQueryRequest: 3,
      DictionaryDistrict: 4,
    },

    ACTIONS: {
      Create: 1,
      Edit: 2,
      Delete: 3,

      Login: 4,
      WrongPassword: 5,
      Expired: 6,
      Lock: 7,

      UpdatePassword: 8,
    },

    DATATYPES: {
      Value: 1,
      FieldValue: 2,
      FieldOldNew: 3,
      FieldOperationValue: 4,
    },
  }),

  methods: {
    parse(data) {
      var res = data.map((item) => ({
        idType: item.idType,
        json: JSON.parse(item.json),
      }));
      //console.log(res);
      return res;
    },

    translateField(field) {
      return `audit.fields.${this.item.target}.${field}`;
    },
    translateOperation(operation) {
      return `audit.operations.${operation}`;
    },
  },
};
</script>

<style></style>
