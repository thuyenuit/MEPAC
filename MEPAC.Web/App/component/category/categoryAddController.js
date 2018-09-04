//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('categoryAddController', categoryAddController);

    categoryAddController.$inject = ['$http', '$scope', 'apiService',
        'commonService', '$state', 'notificationService', 'authenticationService'];

    function categoryAddController($http, $scope, apiService, commonService,
        $state, notificationService, authenticationService) {

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        angular.element("input[name='order']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '');
        });

        // add productCategory
        $scope.pcInfo = {};      
        $scope.typeAction = 0;

        $scope.GetSEOTitle = GetSEOTitle;
        function GetSEOTitle() {
            $scope.pcInfo.Alias = commonService.getSeoTitle($scope.pcInfo.Display);
        }

        $scope.SaveContinue = SaveContinue;
        function SaveContinue(){
            $scope.typeAction = 1;
            AddProductCategory();
        }

        $scope.Save = Save;
        function Save() {
            $scope.typeAction = 0;
            AddProductCategory();
        }

        $scope.CancelAction = CancelAction;
        function CancelAction(){
            $state.go('categories');
        }

        //$scope.click = function () {
        //    setTimeout(function () {
        //        $('#btnSubmit').click()
        //        $scope.clicked = true;
        //    }, 0);
        //};
       
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {

            if ($scope.pcInfo.Display == null || $scope.pcInfo.Display == ''
                && ($scope.pcInfo.Alias == null || $scope.pcInfo.Alias == ''))
            {
                notificationService.displayError('Dữ liệu nhập không hợp lệ! Vui lòng kiểm tra lại.');
            }
            else
            {
                var url = '/sms/productcategory/create';
                $scope.promise = apiService.post(url, $scope.pcInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    if ($scope.typeAction == 0)
                    {
                        $state.go('categories');
                    }    

                    $scope.pcInfo = {};

                }, function (result) {
                    notificationService.displayError(result.data);
                });
                $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
            }
        }

    }
})(angular.module('sms.category'));