export interface ApiMenuItem {
  id: string;
  title: string;
  description?: string;
  permission?: string;
  icon?: string;
  iconUrl?: string;
  type?: string;
  app?: { url: string };
  children?: ApiMenuItem[];
}

export interface ApiMenuGroup {
  id: string;
  title: string;
  items: ApiMenuItem[];
}

export interface ApiMenuResponse {
  title: string;
  groups: ApiMenuGroup[];
}
