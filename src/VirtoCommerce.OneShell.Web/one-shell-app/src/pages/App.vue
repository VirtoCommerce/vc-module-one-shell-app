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
        :loading
        @item-click="onMenuItemClick"
      />
    </template>
  </VcApp>
</template>

<script lang="ts" setup>
import { useUser } from "@vc-shell/framework";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
// eslint-disable-next-line import/no-unresolved
import logoImage from "/assets/logo.svg";
import { MainMenu } from "../shared/main-menu";
import { useMainMenu } from "../composables";

const isReady = ref(false);
const version = import.meta.env.PACKAGE_VERSION;

const { isAuthenticated } = useUser();
const router = useRouter();

const expandedMenuItems = ref<string[]>(["activity"]);

const { menu, loadMenu, loading, recordClick } = useMainMenu();

function onMenuItemClick({ id, url }: { id: string; url: string }) {
  recordClick(id);
  router.push({ name: "Platform", query: { url } });
}

onMounted(async () => {
  try {
    if (isAuthenticated.value) {
      isReady.value = true;
    }
    await loadMenu();
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
