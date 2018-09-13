/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.news', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('news', {
                parent: 'baseView',
                url: '/news',
                templateUrl: '/app/component/news/_newsListView.html',
                controller: 'newsListController'
            })
            .state('newsAdd', {
                parent: 'baseView',
                url: '/add-news',
                templateUrl: '/app/component/news/_newsAddView.html',
                controller: 'newsAddController'
            })
            .state('newsEdit', {
                parent: 'baseView',
                url: '/edit-news?newsId',
                templateUrl: '/app/component/news/_newsEditView.html',
                controller: 'newsEditController'
            });
    }
})();