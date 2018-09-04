/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.category', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('categories', {
                parent: 'baseView',
                url: '/categories',
                templateUrl: '/app/component/category/_categoryListView.html',
                controller: 'categoryListController'
            })
            .state('categoryAdd', {
                parent: 'baseView',
                url: '/add-category',
                templateUrl: '/app/component/category/_categoryAddView.html',
                controller: 'categoryAddController'
            })
            .state('categoryEdit', {
                parent: 'baseView',
                url: '/edit-category?categoryId',
                templateUrl: '/app/component/category/_categoryEditView.html',
                controller: 'categoryEditController'
            })
            .state('categoryView', {
                parent: 'baseView',
                url: '/view-category?categoryId',
                templateUrl: '/app/component/category/_categoryViewDetailView.html',
                controller: 'categoryViewDetailController'
            });
    }
})();