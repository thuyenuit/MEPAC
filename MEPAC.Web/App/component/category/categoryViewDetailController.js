/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('categoryViewDetailController', categoryViewDetailController);

    categoryViewDetailController.$inject = ['$scope', 'apiService', '$stateParams', 'notificationService'];

    function categoryViewDetailController($scope, apiService, $stateParams, notificationService) {
        $scope.pcInfo = {};
  
        // thong tin the loai
        $scope.LoadProCategoryInfo = LoadProCategoryInfo;
        function LoadProCategoryInfo() {
            var consfig = {
                params: {
                    categoryId: $stateParams.categoryId
                }
            }
            var url = '/api/productcategory/getbyid';
            apiService.get(url, consfig, function (result) {
                $scope.pcInfo = result.data;
            }, function () {
                notificationService.displayError('Không tìm thấy thông tin thể loại! Vui lòng kiểm tra lại');
            })
        }
        LoadProCategoryInfo();
    }
})(angular.module('sms.category'));