<template>
  <div>
    <div id="circleG" v-if="$store.state.appLoad">
      <div id="circleG_1" class="circleG"></div>
      <div id="circleG_2" class="circleG"></div>
      <div id="circleG_3" class="circleG"></div>
    </div>
    <router-view v-else />
  </div>
</template>

<script>
export default {
  name: "SPA",
};
</script>

<style lang="sass">
@import '../node_modules/typeface-roboto/index.css'
</style>
