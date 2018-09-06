/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.hiring', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('hiring', {
                parent: 'baseView',
                url: '/hiring',
                templateUrl: '/app/component/hiring/_hiringListView.html',
                controller: 'hiringListController'
            })
            .state('hiringAdd', {
                parent: 'baseView',
                url: '/add-hiring',
                templateUrl: '/app/component/hiring/_hiringAddView.html',
                controller: 'hiringAddController'
            })
            .state('hiringEdit', {
                parent: 'baseView',
                url: '/edit-hiring?hiringId',
                templateUrl: '/app/component/hiring/_hiringEditView.html',
                controller: 'hiringEditController'
            });
    }
})();