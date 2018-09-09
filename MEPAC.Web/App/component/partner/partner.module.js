/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.partner', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('partner', {
                parent: 'baseView',
                url: '/partner',
                templateUrl: '/app/component/partner/_partnerListView.html',
                controller: 'partnerListController'
            })
            .state('partnerAdd', {
                parent: 'baseView',
                url: '/add-partner',
                templateUrl: '/app/component/partner/_partnerAddView.html',
                controller: 'partnerAddController'
            })
            .state('partnerEdit', {
                parent: 'baseView',
                url: '/edit-partner?partnerId',
                templateUrl: '/app/component/partner/_partnerEditView.html',
                controller: 'partnerEditController'
            });
    }
})();