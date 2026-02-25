// Call this to register your module to main application
var moduleName = 'VirtoCommerce.OneShell';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.OneShellState', {
                    url: '/one-shell',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'VirtoCommerce.OneShell.helloWorldController',
                                template: 'Modules/$(VirtoCommerce.OneShell)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/one-shell',
                icon: 'fa fa-cube',
                title: 'OneShell',
                priority: 100,
                action: function () { $state.go('workspace.OneShellState'); },
                permission: 'one-shell:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
