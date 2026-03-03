<template>
  <div
    :data-state="isOpen ? 'open' : 'closed'"
    :data-disabled="disabled ? '' : undefined"
  >
    <slot :open="isOpen" />
  </div>
</template>

<script lang="ts" setup>
import { computed, provide } from "vue";
import { COLLAPSIBLE_INJECTION_KEY } from "./types";

const isOpen = defineModel<boolean>("open", { default: false });

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
  }>(),
  {
    disabled: false,
  },
);

function toggle() {
  if (!props.disabled) {
    isOpen.value = !isOpen.value;
  }
}

provide(COLLAPSIBLE_INJECTION_KEY, {
  open: isOpen,
  disabled: computed(() => props.disabled),
  toggle,
});
</script>
