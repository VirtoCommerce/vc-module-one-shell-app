import type { InjectionKey, Ref } from "vue";

export interface CollapsibleRootProps {
  disabled?: boolean;
}

export interface CollapsibleContext {
  open: Ref<boolean>;
  disabled: Ref<boolean>;
  toggle: () => void;
}

export const COLLAPSIBLE_INJECTION_KEY: InjectionKey<CollapsibleContext> = Symbol("CollapsibleContext");
