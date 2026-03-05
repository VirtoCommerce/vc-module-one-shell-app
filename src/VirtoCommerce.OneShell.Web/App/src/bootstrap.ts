import { addMenuItem, registerDashboardWidget } from "@vc-shell/framework";
import { App, markRaw } from "vue";
import Welcome from "./components/dashboard-widgets/Welcome.vue";

export function bootstrap(_app: App) {
  // Add Dashboard to main menu item
  addMenuItem({
    title: "SHELL.MENU.DASHBOARD",
    icon: "lucide-home",
    priority: 0,
    url: "/",
  });

  // Register Dashboard Welcome Widget
  registerDashboardWidget({
    id: "welcome-widget",
    name: "Welcome",
    component: markRaw(Welcome),
    size: { width: 6, height: 6 },
    position: { x: 0, y: 0 },
  });
}
