<template>
  <div class="main-menu">
    <template v-if="loading">
      <template
        v-for="s in 2"
        :key="s"
      >
        <hr
          v-if="s > 1"
          class="main-menu__divider"
        />
        <div class="main-menu__section">
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
    </template>

    <template v-else>
      <template
        v-for="(section, index) in menu"
        :key="section.id"
      >
        <hr
          v-if="index > 0"
          class="main-menu__divider"
        />
        <CollapsibleRoot
          class="main-menu__section"
          :open="expanded.includes(section.id)"
          @update:open="toggleExpanded(section.id, $event)"
        >
          <template #default="{ open }">
            <div class="menu-item">
              <CollapsibleTrigger class="menu-item__trigger">
                <span>{{ section.title }}</span>
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
                      <CollapsibleTrigger
                        class="menu-item__element"
                        :class="{ 'menu-item__element--active': isParentActive(item) }"
                      >
                        <VcIcon
                          :icon="item.icon"
                          :custom-size="16"
                          class="menu-item__icon"
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
                            :class="{ 'menu-item__element--active': activeItemId === child.id }"
                            @click="child.url && emit('itemClick', { id: child.id, url: child.url })"
                          >
                            <VcIcon
                              :icon="child.icon"
                              :custom-size="12"
                              class="menu-item__nested-icon"
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
                    :class="{ 'menu-item__element--active': activeItemId === item.id }"
                    @click="item.url && emit('itemClick', { id: item.id, url: item.url })"
                  >
                    <VcIcon
                      :icon="item.icon"
                      :custom-size="16"
                      class="menu-item__icon"
                    />
                    <span class="menu-item__text">{{ item.label }}</span>
                  </button>
                </template>
              </div>
            </CollapsibleContent>
          </template>
        </CollapsibleRoot>
      </template>
    </template>
  </div>
</template>

<script setup lang="ts">
import { CollapsibleContent, CollapsibleRoot, CollapsibleTrigger } from "../../ui/collapsible";
import type { MenuSection } from "../types";

interface Props {
  menu: MenuSection[];
  loading?: boolean;
  activeItemId?: string;
}

const props = defineProps<Props>();

function isParentActive(item: MenuSection["items"][number]): boolean {
  return !!item.children?.some((child) => child.id === props.activeItemId);
}

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
  @apply -tw-mr-[18px] -tw-ml-[10px];

  &__divider {
    @apply tw-border-t tw-border-neutrals-200 tw-ml-1 tw-mr-3 tw-my-2;
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
      @apply tw-pl-2.5 tw-pr-[18px] tw-h-10 tw-flex tw-justify-between tw-items-center tw-w-full tw-uppercase tw-text-neutrals-400 tw-text-sm;
    }

    &__element {
      @apply tw-pl-2.5 tw-pr-3 tw-py-1.5 tw-flex tw-w-full tw-text-xs tw-items-center tw-gap-1.5;

      &--nested {
        @apply tw-rounded;
      }

      &:not(&--nested) {
        @apply tw-rounded-l-md;
      }

      &:hover,
      &--active {
        @apply tw-bg-[--secondary-100];
      }
    }

    &__icon {
      @apply tw-min-w-4 tw-min-h-4 tw-text-secondary-700;
    }

    &__nested-icon {
      @apply tw-min-w-3 tw-min-h-3 tw-text-secondary-700;
    }

    &__nested-items-wrapper {
      @apply tw-pl-2.5 tw-pr-2 tw-py-1.5 tw-flex tw-flex-col tw-gap-0.5;
    }

    &__text {
      @apply tw-flex-1 tw-text-left;
    }

    &__chevron {
      @apply tw-transition-transform tw-duration-200 tw-rotate-180 tw-text-neutrals-400;

      &--open {
        @apply tw-rotate-0;
      }
    }
  }
}
</style>
