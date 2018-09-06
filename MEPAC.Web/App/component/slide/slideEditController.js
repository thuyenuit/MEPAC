//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('slideEditController', slideEditController);

    slideEditController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', '$stateParams', 'notificationService'];

    function slideEditController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, $stateParams, notificationService) {

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

        $scope.LoadSlideInfo = LoadSlideInfo;
        function LoadSlideInfo() {
            var consfig = {
                params: {
                    projectId: $stateParams.slideId
                }
            };

            var url = '/api/slide/getbyid';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.slideInfo = result.data;
                if (result.data != null)
                {
                    $("#spanImage").attr("data-title", $scope.slideInfo.Image);
                }

            }, function () {
                notificationService.displayError('Không tìm thấy thông tin slide banner! Vui lòng kiểm tra lại');
            })
            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        LoadSlideInfo();


        $scope.EditSlide = EditSlide;
        function EditSlide() {
            var validate = ValidateData();
            if (validate) {
                var url = '/api/slide/update';
                $scope.promise = apiService.post(url, $scope.slideInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('slides');
                }, function (result) {                
                    if (result.status == 500) {
                        notificationService.displayError('Hệ thống đang bảo trì. Vui lòng thao tác lại sau!');
                    }
                    else {
                        notificationService.displayError(result.data);
                    }
                });
                $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
            }
        }

        function ValidateData() {
            var textError = '';
            var arrayError = [];
            if (IsNotNull($scope.slideInfo.Image) == false) {
                arrayError.push("Vui lòng chọn file ảnh");
            }

            if (IsNotNull($scope.slideInfo.Content) == true) {
                if ($scope.slideInfo.Content.length > 200) {
                    arrayError.push("Mô tả ngắn không được nhập quá 200 ký tự");
                }

            }

            if (arrayError.length > 0) {
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
})(angular.module('sms.projects'));


