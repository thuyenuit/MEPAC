//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('inforController', inforController);

    inforController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', '$stateParams', 'notificationService'];

    function inforController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, $stateParams, notificationService) {

        $scope.projectInfo = {};

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.projectInfo.LinkImage = fileUrl;
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

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '400px',
            toolbarCanCollapse: true
        }


        $scope.LoadInfo = LoadInfo;
        $scope.showAddress3 = false;
        $scope.showAddress4 = false;
        function LoadInfo() {
            var url = '/api/other/getInforCompany';
            $scope.promise = apiService.get(url, null, function (result) {
                $scope.projectInfo = result.data;

                if ($scope.projectInfo.Address4 != null && $scope.projectInfo.Address4.length > 0) {
                    $scope.showAddress4 = true;
                }
                if ($scope.projectInfo.Address3 != null && $scope.projectInfo.Address3.length > 0)
                {
                    $scope.showAddress3 = true;
                }

            }, function () {
                notificationService.displayError('Không tìm thấy thông tin công ty! Vui lòng kiểm tra lại');
            })
            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        LoadInfo();


        $scope.EditProject = EditProject;
        function EditProject() {
            var validate = ValidateData();
            if (validate) {                
                var url = '/api/other/updateInforCompany';
                $scope.promise = apiService.post(url, $scope.projectInfo, function (result) {
                    notificationService.displaySuccess(result.data);                   
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
            if (IsNotNull($scope.projectInfo.Display) == false) {
                arrayError.push("Vui lòng nhập tên công ty");
            }
            else if ($scope.projectInfo.Display.length > 300) {
                arrayError.push("Tên dự án không vượt quá 300 ký tự");
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
})(angular.module('sms.infor'));


