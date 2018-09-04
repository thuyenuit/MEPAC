/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('categoryEditController', categoryEditController);

    categoryEditController.$inject = ['$scope', 'apiService', 'commonService',
                                            '$stateParams', '$state', 'notificationService'];

    function categoryEditController($scope, apiService, commonService, $stateParams, 
                                            $state, notificationService) {

        angular.element("input[class='numbersOnly']").on('input', function () {
            //this.value = this.value.replace(/[^\d\.\-]/g, '');
            this.value = this.value.replace(/[^0-9\.]/g, '');
        });

        $scope.pcEditInfo = {};
         // thong tin the loai
        $scope.LoadCategoryInfo = LoadCategoryInfo;
        function LoadCategoryInfo() {
            var consfig = {
                params: {
                    categoryId: $stateParams.categoryId
                }
            };

            var url = '/sms/productcategory/getbyid';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.pcEditInfo = result.data;
            }, function () {
                notificationService.displayError('Không tìm thấy thông tin thể loại! Vui lòng kiểm tra lại');
            })
            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        LoadCategoryInfo();

        $scope.CancelAction = CancelAction;
        function CancelAction() {
            $state.go('categories');
        }

        $scope.GetSEOTitle = GetSEOTitle;
        function GetSEOTitle() {
            $scope.pcEditInfo.ProductCategoryAlias = commonService.getSeoTitle($scope.pcEditInfo.ProductCategoryName);
        }

        //cap nhat
        $scope.UpdateProductCategory = UpdateProductCategory;
        function UpdateProductCategory() {            
            var url = '/sms/productcategory/update';
            $scope.promise = apiService.put(url, $scope.pcEditInfo, function (result) {
                notificationService.displaySuccess(result.data);
                $state.go('categories');
            }, function (result) {
                notificationService.displayError(result.data);
            });
            $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
        }

    }
})(angular.module('sms.category'));