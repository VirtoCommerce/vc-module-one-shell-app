import { ref, computed } from "vue";
import { useApiClient, useAsync } from "@vc-shell/framework";
import { OneShellClient } from "../../api_client/virtocommerce.oneshell";
import type { CustomMenuItem } from "../../shared/main-menu/types";
import type { ApiMenuItem } from "../../shared/main-menu/types/api";

function mapMenuItem(item: ApiMenuItem): CustomMenuItem {
  return {
    id: item.id,
    label: item.title,
    description: item.description,
    icon: item.icon,
    iconUrl: item.iconUrl,
    type: item.type,
    url: item.app?.url,
  };
}

const { getApiClient } = useApiClient(OneShellClient);

export function useRecentMenu(take = 10) {
  const recentItems = ref<CustomMenuItem[]>([]);
  const isInitialized = ref(false);

  const { loading, action: loadRecentMenu } = useAsync<string>(async (cultureName = "en-US") => {
    const client = await getApiClient();
    const result = await client.getMainMenu2(cultureName, take);
    recentItems.value = (result as unknown as ApiMenuItem[]).map(mapMenuItem);
    isInitialized.value = true;
  });

  return {
    recentItems: computed(() => recentItems.value),
    loading: computed(() => loading.value && !isInitialized.value),
    loadRecentMenu,
  };
}
