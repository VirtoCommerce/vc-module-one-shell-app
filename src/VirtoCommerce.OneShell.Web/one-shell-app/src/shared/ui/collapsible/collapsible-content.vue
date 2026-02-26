<template>
  <div
    v-if="forceMount || context.open.value"
    :data-state="context.open.value ? 'open' : 'closed'"
    :data-disabled="context.disabled.value ? '' : undefined"
    :hidden="forceMount && !context.open.value ? true : undefined"
  >
    <slot />
  </div>
</template>

<script lang="ts" setup>
import { inject } from "vue";
import { COLLAPSIBLE_INJECTION_KEY } from "./types";

defineProps<{
  forceMount?: boolean;
}>();

const context = inject(COLLAPSIBLE_INJECTION_KEY)!;
</script>
