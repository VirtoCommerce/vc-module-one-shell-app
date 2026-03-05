<template>
  <VcApp
    :is-ready="isReady"
    :logo="logoImage"
    title="One Shell"
    :version="version"
  >
    <template #menu>
      <div class="search">
        <VcInput :placeholder="$t('SHELL.MENU.SEARCH')" />
      </div>
      <MainMenu
        v-model:expanded="expandedMenuItems"
        :loading="isLoading"
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
import { useRouter } from "vue-router";
// eslint-disable-next-line import/no-unresolved
import logoImage from "/assets/logo.svg";
import { MainMenu, type MenuSection } from "../shared/main-menu";
import { useMainMenu, useRecentMenu } from "../composables";
import { useI18n } from "vue-i18n";

const isReady = ref(false);
const version = import.meta.env.PACKAGE_VERSION;

const { isAuthenticated } = useUser();
const router = useRouter();
const { t } = useI18n();

const expandedMenuItems = ref<string[]>(["activity"]);
const activeMenuItemId = ref<string>();

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

async function onMenuItemClick({ id, url }: { id: string; url: string }) {
  activeMenuItemId.value = id;
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

.search {
  @apply -tw-mx-[18px] tw-px-[18px] tw-py-3 tw-bg-secondary-50 tw-border-b tw-border-neutrals-200 tw-mb-2;
}
</style>
