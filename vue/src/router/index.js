import Vue from "vue";
import VueRouter from "vue-router";

import store from "@/store";
import { CHECK_AUTH } from "@/store/actions.type";
import { MODULES } from "@/common/config";

import Index from "@/views/Layout/Index"; //this doesnt have i18n for some reason
import User from "@/views/User"; //but this does

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    component: Index,
    children: [
      // home
      {
        name: "Home page",
        path: "",
        component: () => import(/* webpackChunkName: "admin" */ "@/views/Home"),
      },
      // admin
      {
        name: "Admin",
        path: "admin",
        component: () =>
          import(/* webpackChunkName: "admin" */ "@/views/Admin"),
        children: [
          // admin/access
          {
            path: "access",
            component: () =>
              import(
                /* webpackChunkName: "admin.access" */ "@/views/Admin/Access"
              ),
            meta: { roles: [MODULES.SecurityAdmin] },
            children: [
              // admin/access
              {
                name: "Users",
                path: "",
                component: () =>
                  import(
                    /* webpackChunkName: "admin.access.users" */ "@/views/Admin/Access/Users"
                  ),
                meta: { roles: [MODULES.SecurityAdmin] },
              },
              // admin/access/package
              {
                name: "Package",
                path: "package",
                component: () =>
                  import(
                    /* webpackChunkName: "admin.access.package" */ "@/views/Admin/Access/Package"
                  ),
                meta: { roles: [MODULES.SecurityAdmin] },
              },
              // admin/access/file
              {
                name: "File",
                path: "file",
                component: () =>
                  import(
                    /* webpackChunkName: "admin.access.file" */ "@/views/Admin/Access/File"
                  ),
                meta: { roles: [MODULES.SecurityAdmin] },
              },
            ],
          },
          // admin/dict
          {
            path: "dict",
            component: () =>
              import(/* webpackChunkName: "admin.dict" */ "@/views/Admin/Dict"),
            children: [
              // admin/dict
              {
                name: "Districts",
                path: "",
                alias: "districts",
                component: () =>
                  import(
                    /* webpackChunkName: "admin.dict.districts" */ "@/views/Admin/Dict/Districts"
                  ),
              },
              // admin/access/package
              {
                name: "Sample",
                path: "sample",
                component: () =>
                  import(
                    /* webpackChunkName: "admin.access.package" */ "@/views/Admin/Dict/Sample"
                  ),
              },
            ],
          },
          // admin/stat
          {
            name: "Stat",
            path: "stat",
            component: () =>
              import(/* webpackChunkName: "admin.stat" */ "@/views/Admin/Stat"),
            meta: { roles: [MODULES.ConfigurationAdmin] },
          },
          // admin/audit
          {
            name: "Audit",
            path: "audit",
            component: () =>
              import(
                /* webpackChunkName: "admin.audit" */ "@/views/Admin/Audit"
              ),
            meta: { roles: [MODULES.SecurityAdmin] },
          },
          // admin/audit/{id}
          {
            name: "Audit user",
            path: "audit/:id",
            component: () =>
              import(
                /* webpackChunkName: "admin.audit" */ "@/views/Admin/Audit"
              ),
            meta: { roles: [MODULES.SecurityAdmin] },
            props: (route) => ({ idUser: Number(route.params.id) }),
          },
        ],
      },
      // sample
      {
        path: "sample",
        component: () =>
          import(/* webpackChunkName: "sample" */ "@/views/Sample"),
        children: [
          // sample/
          {
            name: "Sample work",
            path: "/",
            component: () =>
              import(
                /* webpackChunkName: "sample.work" */ "@/views/Sample/Work"
              ),
          },
          // admin/audit/{id}
          {
            name: "Sample entity",
            path: "get/:id",
            component: () =>
              import(
                /* webpackChunkName: "admin.audit" */ "@/views/Sample/Get"
              ),
            props: (route) => ({ id: Number(route.params.id) }),
          },
        ],
      },
      // message query
      {
        name: "Message query",
        path: "mq",
        component: () =>
          import(/* webpackChunkName: "messagequery" */ "@/views/MessageQuery"),
      },
      // statistics
      {
        name: "Statistics",
        path: "stat",
        component: () => import(/* webpackChunkName: "stat" */ "@/views/Stat"),
      },
      // contact
      {
        name: "Contacts",
        path: "contacts",
        component: () =>
          import(/* webpackChunkName: "contacts" */ "@/views/Contacts"),
      },
      // user
      {
        name: "User",
        path: "user",
        component: User,
      },
    ],
  },
  {
    name: "Login",
    path: "/login",
    component: () => import(/* webpackChunkName: "login" */ "@/views/Login"),
  },
];

const router = new VueRouter({
  base: "/app/",
  mode: "hash",
  routes,
});

// Ensure we checked auth before each page load.
router.beforeEach((to, from, next) => {
  store.commit("SET_LOADING", true);

  const { roles } = to.meta;
  const publicPages = ["/login"];
  const authRequired = !publicPages.includes(to.path);

  if (authRequired) {
    store
      .dispatch(CHECK_AUTH)
      .then(() => {
        if (!store.getters.isAuthenticated) {
          next({
            path: "/login",
          });
        } else if (roles) {
          // check if route is restricted by role
          if (
            roles.length &&
            !roles.some((r) =>
              store.getters.currentUser.userModules.includes(r)
            )
          ) {
            // role not authorised so redirect to home page
            next({ path: "/" });
          } else {
            //role authorized
            next();
          }
        } else {
          next();
        }
      })
      // eslint-disable-next-line no-unused-vars
      .catch((error) => {
        next({
          path: "/login",
        });
      });
  } else {
    //console.log("not required auth");
    next();
  }
});

router.afterEach(() => {
  store.commit("SET_LOADING", false);
  if (store.state.appLoad) store.commit("APP_LOADED");
});

export default router;
