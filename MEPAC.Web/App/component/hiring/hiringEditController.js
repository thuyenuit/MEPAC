//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('hiringEditController', hiringEditController);

    hiringEditController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', '$stateParams', 'notificationService'];

    function hiringEditController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, $stateParams, notificationService) {

        $scope.hiringInfo = {};

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.hiringInfo.LinkImage = fileUrl;
                    $("#spanImage").attr("data-title", fileUrl);
                })

            }
            finder.popup();
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '400px',
            toolbarCanCollapse: true
        }


        $scope.LoadHiringInfo = LoadHiringInfo;
        function LoadHiringInfo() {
            var consfig = {
                params: {
                    hiringId: $stateParams.hiringId
                }
            };

            var url = '/api/hiring/getbyid';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.hiringInfo = result.data;
                if (result.data != null) {
                    $("#spanImage").attr("data-title", $scope.hiringInfo.LinkImage);
                }

            }, function () {
                notificationService.displayError('Không tìm thấy thông tin dự án! Vui lòng kiểm tra lại');
            })
            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        LoadHiringInfo();


        $scope.EditHiring = EditHiring;
        function EditHiring() {
            //var validate = ValidateData();
            //if (validate) {
            $scope.hiringInfo.PostBy = "";

                var url = '/api/hiring/update';
                console.log($scope.hiringInfo);
                $scope.promise = apiService.post(url, $scope.hiringInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('hiring');
                }, function (result) {
                    if (result.status == 500) {
                        notificationService.displayError('Hệ thống đang bảo trì. Vui lòng thao tác lại sau!');
                    }
                    else {
                        notificationService.displayError(result.data);
                    }
                });
                $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
            //}
        }

        function ValidateData() {
            var textError = '';
            var arrayError = [];
            if (IsNotNull($scope.projectInfo.Display) == false) {
                arrayError.push("Vui lòng nhập tên dự án");
            }
            else if ($scope.projectInfo.Display.length > 200) {
                arrayError.push("Tên dự án không vượt quá 200 ký tự");
            }

            if (IsNotNull($scope.projectInfo.FromDate) == false) {
                arrayError.push("Vui lòng nhập ngày bắt đầu dự án");
            }

            if (IsNotNull($scope.projectInfo.ToDate) == false) {
                arrayError.push("Vui lòng nhập ngày kết thúc dự án");
            }

            if (IsNotNull($scope.projectInfo.FromDate) == true
                && IsNotNull($scope.projectInfo.ToDate) == true) {
                var fromDate = new Date($scope.projectInfo.FromDate);
                var toDate = new Date($scope.projectInfo.ToDate);

                if (fromDate > toDate) {
                    arrayError.push("Ngày bắt đầu không được lớn hơn ngày kết thúc dự án");
                }
            }

            if (IsNotNull($scope.projectInfo.LinkImage) == false) {
                arrayError.push("Vui lòng chọn ảnh đại diện dự án");
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
})(angular.module('sms.hiring'));


