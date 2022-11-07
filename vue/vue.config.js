const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  configureWebpack: {
    devtool: "source-map",
  },

  transpileDependencies: ["vuetify"],
  publicPath: "/",
  outputDir: "../publish/frontend",

  pluginOptions: {
    i18n: {
      locale: "en",
      fallbackLocale: "en",
      localeDir: "locales",
      enableInSFC: false,
      enableBridge: false,
    },
  },
});
