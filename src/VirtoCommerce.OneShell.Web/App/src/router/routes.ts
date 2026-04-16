import { RouteRecordRaw } from "vue-router";
import App from "../pages/App.vue";
import Dashboard from "../pages/Dashboard.vue";
import Platform from "../pages/Platform.vue";
import {
  Invite,
  Login,
  ResetPassword,
  useBladeNavigation,
  ChangePasswordPage,
  ForgotPassword,
} from "@vc-shell/framework";
// eslint-disable-next-line import/no-unresolved
import whiteLogoImage from "/assets/logo-white.svg";
// eslint-disable-next-line import/no-unresolved
import bgImage from "/assets/background.jpg";

const version = import.meta.env.PACKAGE_VERSION;

export const routes: RouteRecordRaw[] = [
  {
    path: "/",
    component: App,
    name: "App",
    meta: {
      root: true,
    },
    children: [
      {
        name: "Dashboard",
        path: "",
        alias: `/`,
        component: Dashboard,
      },
      {
        name: "Platform",
        path: "platform",
        component: Platform,
      },
    ],
  },
  {
    name: "Login",
    path: "/login",
    component: Login,
    meta: {
      appVersion: version,
    },
    props: () => ({
      logo: whiteLogoImage,
      title: "One Shell",
    }),
  },
  {
    name: "ForgotPassword",
    path: "/forgot-password",
    component: ForgotPassword,
    meta: {
      appVersion: version,
    },
    props: () => ({
      logo: whiteLogoImage,
    }),
  },
  {
    name: "Invite",
    path: "/invite",
    component: Invite,
    props: (_route) => ({
      userId: _route.query.userId,
      token: _route.query.token,
      userName: _route.query.userName,
      logo: whiteLogoImage,
    }),
  },
  {
    name: "ResetPassword",
    path: "/resetpassword",
    component: ResetPassword,
    props: (_route) => ({
      userId: _route.query.userId,
      token: _route.query.token,
      userName: _route.query.userName,
      logo: whiteLogoImage,
    }),
  },
  {
    name: "ChangePassword",
    path: "/changepassword",
    component: ChangePasswordPage,
    meta: {
      forced: true,
    },
  },
];
