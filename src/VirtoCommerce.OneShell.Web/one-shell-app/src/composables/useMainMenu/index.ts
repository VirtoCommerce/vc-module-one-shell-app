import { ref, computed } from "vue";
import { useApiClient, useAsync } from "@vc-shell/framework";
import { MenuItemClickEvent, OneShellClient } from "../../api_client/virtocommerce.oneshell";
import type { CustomMenuItem, MenuSection } from "../../shared/main-menu/types";

interface ApiMenuItem {
  id: string;
  title: string;
  icon: string;
  app?: { url: string };
  children?: ApiMenuItem[];
}

interface ApiMenuGroup {
  id: string;
  title: string;
  items: ApiMenuItem[];
}

interface ApiMenuResponse {
  title: string;
  groups: ApiMenuGroup[];
}

function mapMenuItem(item: ApiMenuItem): CustomMenuItem {
  return {
    id: item.id,
    label: item.title,
    icon: item.icon,
    url: item.app?.url,
    children: item.children?.length ? item.children.map(mapMenuItem) : undefined,
  };
}

function mapMenuResponse(response: ApiMenuResponse): MenuSection[] {
  return response.groups.map((group) => ({
    id: group.id,
    title: group.title,
    items: group.items.map(mapMenuItem),
  }));
}

const { getApiClient } = useApiClient(OneShellClient);

export function useMainMenu() {
  const menu = ref<MenuSection[]>([]);

  const { loading, action: loadMenu } = useAsync<string>(async (cultureName = "en-US") => {
    const client = await getApiClient();
    const result = await client.getMainMenu(cultureName);
    menu.value = mapMenuResponse(result as unknown as ApiMenuResponse);
  });

  const { action: recordClick } = useAsync<string>(async (menuItemId) => {
    const client = await getApiClient();
    await client.recordClickEvent(new MenuItemClickEvent({ menuItemId }));
  });

  return {
    menu: computed(() => menu.value),
    loading: computed(() => loading.value),
    loadMenu,
    recordClick,
  };
}
