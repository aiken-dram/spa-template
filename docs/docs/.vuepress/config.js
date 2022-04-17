module.exports = {
  /**
   * Ref：https://v1.vuepress.vuejs.org/config/#title
   */
  title: "ApplicationName",
  /**
   * Ref：https://v1.vuepress.vuejs.org/config/#description
   */
  description: "Documentation for ApplicationName",

  base: "/doc/",
  dest: "../publish/doc/",
  head: [["link", { rel: "icon", href: "/favicon.ico" }]],

  locales: {
    // The key is the path for the locale to be nested under.
    // As a special case, the default locale can use '/' as its path.
    "/": {
      lang: "en-US", // this will be set as the lang attribute on <html>
      title: "%Single page application%",
      description: "Documentation for %single page application%",
    },
  },

  /**
   * Theme configuration, here is the default theme configuration for VuePress.
   *
   * ref：https://v1.vuepress.vuejs.org/theme/default-theme-config.html
   */
  themeConfig: {
    locales: {
      "/": {
        repo: "",
        editLinks: false,
        docsDir: "",
        editLinkText: "",
        lastUpdated: false,
        smoothScroll: false,
        sidebarDepth: 2,
        sidebar: "auto",
        nav: [
          {
            text: "User manual",
            link: "/manuals/user",
          },
          {
            text: "Admin manuals",
            ariaLabel: "Language Menu",
            items: [
              {
                text: "Supervisor",
                link: "/manuals/supervisor",
              },
              { text: "Application admin", link: "/manuals/admin" },
              { text: "Access admin", link: "/manuals/access" },
            ],
          },
          {
            text: "Swagger",
            link: "/../swagger/",
          },
          {
            text: "%AppicationName%",
            link: "/../app/",
          },
        ],
      },
    },
  },
};
