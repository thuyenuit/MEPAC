/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('sms',
        ['sms.projects', 'sms.range',
        'sms.common']).config(config).config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];

    function config($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
             .state('baseView', {             
                 templateUrl: '/app/shared/views/baseView.html',
                 abstract: true
             })
             .state('error404', {
                 parent: 'baseView',
                 url: '/error404',
                 templateUrl: '/app/component/error/_error404.html',
                 controller: 'error404Controller'
             })
            .state('login', {
                url: '/login',
                templateUrl: '/app/component/login/loginView.html',
                controller: 'loginController'
            })
            .state('home', {    
                parent: 'baseView',
                url: '/dashboard',
                templateUrl: '/app/component/home/homeView.html',
                controller: 'homeController'
            });

       
       $urlRouterProvider.otherwise('/login');
      // $locationProvider.html5Mode(true);
       $locationProvider.hashPrefix('!');      
    }

    configAuthentication.$inject = ['$httpProvider'];
    function configAuthentication($httpProvider){
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function(rejection){
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401")
                    {
                        $location.path('/login');
                    }
                     
                    if (response.status == "404")
                        $location.path('/error404');

                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401")
                    {
                        $location.path('/login');
                    }
                     
                    if (response.status == "404")
                        $location.path('/error404');
                    return $q.reject(rejection);
                }
            }
        })
    }
})();