//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('projectsAddController', projectsAddController);

    projectsAddController.$inject = ['$scope', '$sce', '$compile',
        'apiService', '$interval', '$filter', '$ngBootbox',
        '$state', 'notificationService', 'commonService', 'authenticationService'];

    function projectsAddController($scope, $sce, $compile, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService, commonService, authenticationService) {

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
           toolbarCanCollapse : true
        } 

        $("#uploadAvatar").click(function () {
            $("#selectFileAvatar").click();
        });

        $scope.SaveContinue = SaveContinue;
        function SaveContinue() {
            $scope.typeAction = 1;
            AddProject();
        }

        $scope.Save = Save;
        function Save() {
            $scope.typeAction = 0;
            AddProject();
        }
   
        
        //$scope.projectInfo.Tags = "";
        $scope.AddProject = AddProject;
        function AddProject() {
            var validate = ValidateData();
            if (validate)
            {
                $scope.projectInfo.JSonMoreImage = JSON.stringify($scope.moreImages);
                var url = '/api/projects/create';
                $scope.promise = apiService.post(url, $scope.projectInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('projects');

                    }, function (result) {
                        notificationService.displayError(result.data);
                    });
                    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
            }
        }

        function ValidateData() {
            var textError = '';
            var arrayError = [];
            if (IsNotNull($scope.projectInfo.Display) == false)
            {
                arrayError.push("Vui lòng nhập tên dự án");              
            }
            else if ($scope.projectInfo.Display.length > 200)
            {
                arrayError.push("Tên dự án không vượt quá 200 ký tự");
            }

            if (IsNotNull($scope.projectInfo.FromDate) == false) {
                arrayError.push("Vui lòng nhập ngày bắt đầu dự án");
            }

            if (IsNotNull($scope.projectInfo.ToDate) == false) {
                arrayError.push("Vui lòng nhập ngày kết thúc dự án");
            }

            if (IsNotNull($scope.projectInfo.FromDate) == true
                && IsNotNull($scope.projectInfo.ToDate) == true)
            {
                var fromDate = new Date($scope.projectInfo.FromDate);
                var toDate = new Date($scope.projectInfo.ToDate);

                if (fromDate > toDate) {
                    arrayError.push("Ngày bắt đầu không được lớn hơn ngày kết thúc dự án");
                }
            }

            if (IsNotNull($scope.projectInfo.LinkImage) == false) {
                arrayError.push("Vui lòng chọn ảnh đại diện dự án");
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
})(angular.module('sms.projects'));

