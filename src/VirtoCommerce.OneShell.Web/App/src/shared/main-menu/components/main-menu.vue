<template>
  <div class="main-menu">
    <template v-if="loading">
      <div
        v-for="s in 2"
        :key="s"
        class="main-menu__section"
        :class="{ 'main-menu__section--bordered': s > 1 }"
      >
        <div class="main-menu__skeleton-header">
          <VcSkeleton
            :rows="1"
            animated
            class="main-menu__skeleton-header-line"
          />
        </div>
        <div class="main-menu__items">
          <div
            v-for="i in 2"
            :key="i"
            class="main-menu__skeleton-item"
          >
            <VcSkeleton
              :rows="1"
              animated
              class="main-menu__skeleton-label"
            />
          </div>
        </div>
      </div>
    </template>

    <template v-else>
      <CollapsibleRoot
        v-for="(section, index) in menu"
        :key="section.id"
        class="main-menu__section"
        :class="{ 'main-menu__section--bordered': index > 0 }"
        :open="expanded.includes(section.id)"
        @update:open="toggleExpanded(section.id, $event)"
      >
        <template #default="{ open }">
          <div class="menu-item">
            <CollapsibleTrigger class="menu-item__trigger">
              <span>{{ section.title }}</span>
              <!-- TODO: Change lucide icons -->
              <VcIcon
                class="menu-item__chevron"
                :class="{ 'menu-item__chevron--open': open }"
                icon="lucide-chevron-up"
                size="m"
              />
            </CollapsibleTrigger>
          </div>

          <CollapsibleContent>
            <div class="main-menu__items">
              <template
                v-for="item in section.items"
                :key="item.id"
              >
                <CollapsibleRoot
                  v-if="item.children?.length"
                  :open="expanded.includes(item.id)"
                  @update:open="toggleExpanded(item.id, $event)"
                >
                  <template #default="{ open: nestedOpen }">
                    <CollapsibleTrigger class="menu-item__element">
                      <VcIcon
                        :icon="item.icon"
                        size="xs"
                      />
                      <span class="menu-item__text">{{ item.label }}</span>
                      <VcIcon
                        class="menu-item__chevron"
                        :class="{ 'menu-item__chevron--open': nestedOpen }"
                        icon="lucide-chevron-up"
                        size="xs"
                      />
                    </CollapsibleTrigger>
                    <CollapsibleContent>
                      <div class="menu-item__nested-items-wrapper">
                        <button
                          v-for="child in item.children"
                          :key="child.id"
                          class="menu-item__element menu-item__element--nested"
                          @click="child.url && emit('itemClick', { id: child.id, url: child.url })"
                        >
                          <VcIcon
                            :icon="child.icon"
                            size="xs"
                          />
                          <span class="menu-item__text">{{ child.label }}</span>
                        </button>
                      </div>
                    </CollapsibleContent>
                  </template>
                </CollapsibleRoot>

                <button
                  v-else
                  class="menu-item__element"
                  @click="item.url && emit('itemClick', { id: item.id, url: item.url })"
                >
                  <VcIcon
                    :icon="item.icon"
                    size="xs"
                  />
                  <span class="menu-item__text">{{ item.label }}</span>
                </button>
              </template>
            </div>
          </CollapsibleContent>
        </template>
      </CollapsibleRoot>
    </template>
  </div>
</template>

<script setup lang="ts">
import { CollapsibleContent, CollapsibleRoot, CollapsibleTrigger } from "../../ui/collapsible";
import type { MenuSection } from "../types";

interface Props {
  menu: MenuSection[];
  loading?: boolean;
}

defineProps<Props>();

const emit = defineEmits<{
  itemClick: [item: { id: string; url: string }];
}>();

const expanded = defineModel<string[]>("expanded", { default: () => [] });

function toggleExpanded(id: string, isOpen: boolean) {
  if (isOpen) {
    if (!expanded.value.includes(id)) {
      expanded.value = [...expanded.value, id];
    }
  } else {
    expanded.value = expanded.value.filter((item) => item !== id);
  }
}
</script>

<style lang="scss">
.main-menu {
  &__section {
    &--bordered {
      @apply tw-border-t tw-border-[--neutrals-200] tw-pt-3 tw-mt-3;
    }
  }

  &__items {
    @apply tw-flex tw-flex-col tw-gap-0.5;
  }

  &__skeleton {
    &-header {
      @apply tw-pl-1 tw-py-1;
    }

    &-header-line {
      @apply tw-h-3 tw-w-2/5;
    }

    &-item {
      @apply tw-flex tw-items-center tw-gap-1.5 tw-p-1.5;
    }

    &-label {
      @apply tw-h-3;
    }
  }

  .menu-item {
    &__trigger {
      @apply tw-pl-1 tw-py-1 tw-flex tw-justify-between tw-items-center tw-w-full tw-uppercase tw-text-[--neutrals-500] tw-text-xs tw-font-medium;
    }

    &__element {
      @apply tw-p-1.5 tw-flex tw-rounded-md tw-w-full tw-text-xs tw-items-center tw-gap-1.5 tw-font-medium;

      &:not(&--nested) {
        @apply tw-font-semibold;
      }

      &:hover {
        @apply tw-bg-[--secondary-50];
      }
    }

    &__nested-items-wrapper {
      @apply tw-pl-2 tw-flex tw-flex-col tw-gap-0.5;
    }

    &__text {
      @apply tw-flex-1 tw-text-left;
    }

    &__chevron {
      @apply tw-transition-transform tw-duration-200 tw-rotate-180;

      &--open {
        @apply tw-rotate-0;
      }
    }
  }
}
</style>
