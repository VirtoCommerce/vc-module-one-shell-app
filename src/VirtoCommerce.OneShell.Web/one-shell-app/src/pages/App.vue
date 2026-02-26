<template>
  <VcApp
    :is-ready="isReady"
    :logo="logoImage"
    title="One Shell"
    :version="version"
  >
    <template #menu>
      <MainMenu
        v-model:expanded="expandedMenuItems"
        :menu
      />
    </template>
  </VcApp>
</template>

<script lang="ts" setup>
import { useUser } from "@vc-shell/framework";
import { onMounted, ref } from "vue";
// eslint-disable-next-line import/no-unresolved
import logoImage from "/assets/logo.svg";
import type { MenuSection } from "../shared/main-menu/types";
import { MainMenu } from "../shared/main-menu";

const isReady = ref(false);
const version = import.meta.env.PACKAGE_VERSION;

const { isAuthenticated } = useUser();

const expandedMenuItems = ref<string[]>(["activity"]);

const menu: MenuSection[] = [
  {
    id: "activity",
    title: "Activity",
    items: [
      {
        id: "new-orders-act",
        label: "New Orders",
        icon: "lucide-file-text",
      },
      {
        id: "pending-reviews",
        label: "Pending Reviews",
        icon: "lucide-message-square",
      },
      {
        id: "low-stock",
        label: "Low Stock Alerts",
        icon: "lucide-alert-triangle",
      },
      {
        id: "returns",
        label: "Returns & Refunds",
        icon: "lucide-rotate-ccw",
      },
    ],
  },
  {
    id: "recent",
    title: "Recent",
    items: [
      {
        id: "recent-orders",
        label: "New Orders",
        icon: "lucide-file-text",
      },
      {
        id: "recent-returns",
        label: "Returns & Refunds",
        icon: "lucide-rotate-ccw",
      },
    ],
  },
  {
    id: "dashboard",
    title: "Dashboard",
    items: [
      {
        id: "whats-new",
        label: "What's new",
        icon: "lucide-sparkles",
      },
      {
        id: "kpi",
        label: "Key Performance Indicators",
        icon: "lucide-bar-chart-3",
      },
    ],
  },
  {
    id: "marketing",
    title: "Marketing",
    items: [
      {
        id: "promotions",
        label: "Promotions",
        icon: "lucide-megaphone",
      },
      {
        id: "dynamic-content",
        label: "Dynamic content",
        icon: "lucide-layout-grid",
      },
    ],
  },
  {
    id: "orders-customers",
    title: "Orders & Customers",
    items: [
      {
        id: "all-orders",
        label: "All orders",
        icon: "lucide-list",
      },
      {
        id: "new-orders",
        label: "New orders",
        icon: "lucide-file-plus",
        children: [
          {
            id: "accepted",
            label: "Accepted",
            icon: "lucide-check-circle",
          },
          {
            id: "declined",
            label: "Declined",
            icon: "lucide-x-circle",
          },
        ],
      },
      {
        id: "not-paid",
        label: "Not Paid Orders",
        icon: "lucide-credit-card",
      },
      {
        id: "awaiting",
        label: "Awaiting Fulfillment",
        icon: "lucide-truck",
      },
      {
        id: "customers",
        label: "Customers",
        icon: "lucide-users",
      },
    ],
  },
];

onMounted(async () => {
  try {
    if (isAuthenticated.value) {
      isReady.value = true;
    }
  } catch (e) {
    console.log(e);
    throw e;
  }
});

console.debug(`Initializing App`);
</script>

<style lang="scss">
@use "./../styles/index.scss";
</style>
