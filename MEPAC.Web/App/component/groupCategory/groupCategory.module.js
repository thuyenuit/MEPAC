/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.groupCategory', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('categories', {
                parent: 'baseView',
                url: '/list-category',
                templateUrl: '/app/component/groupCategory/_groupCategoryListView.html',
                controller: 'groupCategoryListController'
            })
            .state('categoryAdd', {
                parent: 'baseView',
                url: '/add-category',
                templateUrl: '/app/component/groupCategory/_groupCategoryAddView.html',
                controller: 'groupCategoryAddController'
            })
            .state('categoryEdit', {
                parent: 'baseView',
                url: '/edit-category?Id',
                templateUrl: '/app/component/groupCategory/_groupCategoryEditView.html',
                controller: 'groupCategoryEditController'
            });
    }
})();