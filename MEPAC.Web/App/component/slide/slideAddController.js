//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['$scope', '$sce', '$compile',
        'apiService', '$interval', '$filter', '$ngBootbox',
        '$state', 'notificationService', 'commonService', 'authenticationService'];

    function slideAddController($scope, $sce, $compile, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService, commonService, authenticationService) {

        $scope.slideInfo = {};

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slideInfo.Image = fileUrl;
                    $("#spanImage").attr("data-title", fileUrl);
                })
               
            }
            finder.popup();
        }

        $scope.AddSlide = AddSlide;
        function AddSlide() {
            var validate = ValidateData();
            if (validate)
            {
                var url = '/api/slide/create';
                $scope.promise = apiService.post(url, $scope.slideInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('slides');
                    }, function (result) {
                        notificationService.displayError(result.data);
                    });
                    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
            }
        }

        function ValidateData() {
            var textError = '';
            var arrayError = [];
            if (IsNotNull($scope.slideInfo.Image) == false)
            {
                arrayError.push("Vui lòng chọn file ảnh");              
            }

            if (IsNotNull($scope.slideInfo.Content) == true) {
                if ($scope.slideInfo.Content.length > 200)
                {
                    arrayError.push("Mô tả ngắn không được nhập quá 200 ký tự");
                }
               
            }

            if (arrayError.length > 0)
            {
                textError = arrayError.join(". ");
                notificationService.displayError(textError);
                return false;
            }

            return true;
        }

        function IsNotNull(obj) {
            if (obj == null || obj == '' || undefined)
                return false;
            return true;
        }

    }
})(angular.module('sms.slide'));

