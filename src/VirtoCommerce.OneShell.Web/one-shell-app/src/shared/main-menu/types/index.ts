export interface CustomMenuItem {
  id: string;
  label: string;
  icon: string;
  url?: string;
  children?: CustomMenuItem[];
}

export interface MenuSection {
  id: string;
  title: string;
  items: CustomMenuItem[];
}
