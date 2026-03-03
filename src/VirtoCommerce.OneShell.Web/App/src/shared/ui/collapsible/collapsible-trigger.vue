<template>
  <button
    :aria-expanded="context.open.value"
    :data-state="context.open.value ? 'open' : 'closed'"
    :data-disabled="context.disabled.value ? '' : undefined"
    :disabled="context.disabled.value"
    @click="context.toggle"
  >
    <slot />
  </button>
</template>

<script lang="ts" setup>
import { inject } from "vue";
import { COLLAPSIBLE_INJECTION_KEY } from "./types";

const context = inject(COLLAPSIBLE_INJECTION_KEY)!;
</script>
