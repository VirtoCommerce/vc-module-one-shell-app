<template>
  <iframe
    v-if="url"
    ref="iframeEl"
    class="platform"
    :src="url"
  />
</template>

<script lang="ts" setup>
import { computed, shallowRef, watch } from "vue";
import { useRoute } from "vue-router";
import { useEmbeddedAppBridge } from "../composables";

const route = useRoute();
const url = computed(() => route.query.url as string | undefined);

const iframeEl = shallowRef<HTMLIFrameElement | null>(null);
const { reset } = useEmbeddedAppBridge(iframeEl);

watch(url, () => {
  reset();
});
</script>

<style lang="scss">
.platform {
  @apply tw-h-full tw-w-full;
  border: none;
}
</style>
