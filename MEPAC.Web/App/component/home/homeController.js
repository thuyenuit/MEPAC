//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('homeController',
        ['$scope', '$state', 'authenticationService', 
            function ($scope, $state, authenticationService) {
                //$scope.$parent.LoadResources();
                //var authen = authenticationService.getTokenInfo();
                //if (authen == null
                //   || authen == ''
                //   || authen == undefined) {
                //    authenticationService.removeToken();
                //    $state.go('login');
                //}
    }]);
})(angular.module('sms'));