export interface CustomMenuItem {
  id: string;
  label: string;
  description?: string;
  icon?: string;
  iconUrl?: string;
  type?: string;
  url?: string;
  children?: CustomMenuItem[];
}

export interface MenuSection {
  id: string;
  title: string;
  items: CustomMenuItem[];
}
