angular.module('VirtoCommerce.OneShell')
    .controller('VirtoCommerce.OneShell.helloWorldController', ['$scope', 'VirtoCommerce.OneShell.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'OneShell';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'OneShell.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
