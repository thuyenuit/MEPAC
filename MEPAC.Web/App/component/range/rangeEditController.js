//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('rangeEditController', rangeEditController);

    rangeEditController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', '$stateParams', 'notificationService'];

    function rangeEditController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, $stateParams, notificationService) {

        $scope.rangeInfo = {};

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.rangeInfo.LinkImage = fileUrl;
                    $("#spanImage").attr("data-title", fileUrl);
                })

            }
            finder.popup();
        }

        $scope.showImage = false;
        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                    //$("#spanImage").attr("data-title", fileUrl);
                    if ($scope.moreImages.length > 0) {
                        $scope.showImage = true;
                    }
                    else {
                        $scope.showImage = false;
                    }

                })
            }
            finder.popup();
        }

        $scope.RemoveAllImage = function () {
            $scope.moreImages = [];
            $scope.showImage = false;
        }

        $scope.RemoveImage = function (image) {
            var index = $scope.moreImages.indexOf(image);
            if (index > -1) {
                $scope.moreImages.splice(index, 1);
            }

            if ($scope.moreImages.length > 0) {
                $scope.showImage = true;
            }
            else {
                $scope.showImage = false;
            }
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '400px',
            toolbarCanCollapse: true
        }


        $scope.LoadRangeInfo = LoadRangeInfo;
        function LoadRangeInfo() {
            var consfig = {
                params: {
                    rangeId: $stateParams.rangeId
                }
            };

            var url = '/api/range/getbyid';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.rangeInfo = result.data;
                if (result.data != null) {
                    $("#spanImage").attr("data-title", $scope.rangeInfo.LinkImage);
                    if (result.data.ListMoreImage != null && result.data.ListMoreImage.length > 0) {
                        $.each(result.data.ListMoreImage, function (index, value) {
                            $scope.moreImages.push(value)
                        });
                        $scope.showImage = true;
                    }
                }

            }, function () {
                notificationService.displayError('Không tìm thấy thông tin dự án! Vui lòng kiểm tra lại');
            })
            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        LoadRangeInfo();


        $scope.EditRange = EditRange;
        function EditRange() {
            //var validate = ValidateData();
            //if (validate) {
                //$scope.projectInfo.PostBy = JSON.stringify($scope.moreImages);
                var url = '/api/range/update';
                $scope.promise = apiService.post(url, $scope.rangeInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('range');
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
})(angular.module('sms.range'));


