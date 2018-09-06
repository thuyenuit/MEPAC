/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.slide', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('slides', {
                parent: 'baseView',
                url: '/slides',
                templateUrl: '/app/component/slide/_slideListView.html',
                controller: 'slideListController'
            })
            .state('slideAdd', {
                parent: 'baseView',
                url: '/add-slide',
                templateUrl: '/app/component/slide/_slideAddView.html',
                controller: 'slideAddController'
            })
            .state('slideEdit', {
                parent: 'baseView',
                url: '/edit-slide?slideId',
                templateUrl: '/app/component/slide/_slideEditView.html',
                controller: 'slideEditController'
            });
    }
})();