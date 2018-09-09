﻿//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('partnerEditController', partnerEditController);

    partnerEditController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', '$stateParams', 'notificationService'];

    function partnerEditController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, $stateParams, notificationService) {

        $scope.partnerInfo = {};

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.partnerInfo.Logo = fileUrl;
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


        $scope.LoadPartnerInfo = LoadPartnerInfo;
        function LoadPartnerInfo() {
            var consfig = {
                params: {
                    partnerId: $stateParams.partnerId
                }
            };

            var url = '/api/partner/getbyid';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.partnerInfo = result.data;
                if (result.data != null) {
                    $("#spanImage").attr("data-title", $scope.partnerInfo.Logo);
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
        LoadPartnerInfo();


        $scope.EditPartner = EditPartner;
        function EditPartner() {
            //var validate = ValidateData();
            //if (validate) {
                //$scope.partnerInfo.PostBy = JSON.stringify($scope.moreImages);
                var url = '/api/partner/update';
                $scope.promise = apiService.post(url, $scope.partnerInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('partner');
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
})(angular.module('sms.partner'));


