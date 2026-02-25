angular.module('VirtoCommerce.OneShell')
    .factory('VirtoCommerce.OneShell.webApi', ['$resource', function ($resource) {
        return $resource('api/one-shell');
    }]);
