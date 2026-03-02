<template>
  <iframe
    v-if="url"
    class="platform"
    :src="url"
  />
</template>

<script lang="ts" setup>
import { computed } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

const url = computed(() => route.query.url as string | undefined);
</script>

<style lang="scss">
.platform {
  @apply tw-h-full tw-w-full;
}
</style>
