<template>
  <VcApp
    :is-ready="isReady"
    :logo="logoImage"
    title="One Shell"
    :version="version"
    show-search
  >
    <template #menu="{ expanded, searchQuery }">
      <MainMenu
        :expanded
        :loading="isLoading"
        :search-query
        :menu
        :active-item-id="activeMenuItemId"
        @item-click="onMenuItemClick"
      />
    </template>
  </VcApp>
</template>

<script lang="ts" setup>
import { useUser } from "@vc-shell/framework";
import { computed, onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
// eslint-disable-next-line import/no-unresolved
import logoImage from "/assets/logo.svg";
import { MainMenu, type MenuSection } from "../shared/main-menu";
import { useMainMenu, useRecentMenu } from "../composables";
import { useI18n } from "vue-i18n";

const isReady = ref(false);
const version = import.meta.env.PACKAGE_VERSION;

const { isAuthenticated } = useUser();
const route = useRoute();
const router = useRouter();
const { t } = useI18n();

const { menu: defaultMenu, loadMenu, loading: isMenuLoading, recordClick } = useMainMenu();
const { recentItems, loadRecentMenu, loading: isRecentLoading } = useRecentMenu(5);

const menu = computed<MenuSection[]>(() => {
  const sections: MenuSection[] = [];

  if (recentItems.value.length) {
    sections.push({ id: "recent", title: t("SHELL.MENU.RECENT"), items: recentItems.value });
  }

  sections.push(...defaultMenu.value);

  return sections;
});

const isLoading = computed(() => {
  return isMenuLoading.value || isRecentLoading.value;
});

const activeMenuItemId = computed(() => {
  const currentUrl = route.query.url as string | undefined;
  if (!currentUrl) return undefined;

  for (const section of menu.value) {
    for (const item of section.items) {
      if (item.url === currentUrl) return item.id;
      if (item.children) {
        const child = item.children.find((c) => c.url === currentUrl);
        if (child) return child.id;
      }
    }
  }
  return undefined;
});

async function onMenuItemClick({ id, url }: { id: string; url: string }) {
  router.push({ name: "Platform", query: { url } });
  await recordClick(id);
  void loadRecentMenu();
}

onMounted(async () => {
  try {
    if (isAuthenticated.value) {
      isReady.value = true;
    }
    await Promise.all([loadMenu(), loadRecentMenu()]);
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
