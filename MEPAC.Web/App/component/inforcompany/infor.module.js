/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.infor', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('inforCompany', {
                parent: 'baseView',
                url: '/info-company',
                templateUrl: '/app/component/inforcompany/_inforView.html',
                controller: 'inforController'
            });
    }
})();