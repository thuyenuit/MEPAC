/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms.projects', ['sms.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('projects', {
                parent: 'baseView',
                url: '/projects',
                templateUrl: '/app/component/projects/_projectsListView.html',
                controller: 'projectsListController'
            })
            .state('projectAdd', {
                parent: 'baseView',
                url: '/add-project',
                templateUrl: '/app/component/projects/_projectsAddView.html',
                controller: 'projectsAddController'
            })
            .state('projectEdit', {
                parent: 'baseView',
                url: '/edit-project?projectId',
                templateUrl: '/app/component/projects/_projectsEditView.html',
                controller: 'projectsEditController'
            });
    }
})();