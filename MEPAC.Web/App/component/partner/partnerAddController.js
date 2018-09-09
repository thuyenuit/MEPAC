//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('partnerAddController', partnerAddController);

    partnerAddController.$inject = ['$scope', '$sce', '$compile',
        'apiService', '$interval', '$filter', '$ngBootbox',
        '$state', 'notificationService', 'commonService', 'authenticationService'];

    function partnerAddController($scope, $sce, $compile, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService, commonService, authenticationService) {

        //angular.element("input[name='quantity']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value);
        //});
        //angular.element("input[name='existMinimum']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value);
        //});
        //angular.element("input[name='existMaximum']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value);
        //});
        //angular.element("input[name='pricePromotion']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value);
        //});
        //angular.element("input[name='priceCost']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value);
        //});
        //angular.element("input[name='priceSell']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value); 
        //    if( this.value <= 0)
        //    {
        //        $scope.projectInfo.PricePromotion = null;
        //        $scope.percent = null;
        //    }
        //});

        //angular.element("input[name='percent']").on('input', function () {
        //    this.value = commonService.inputNumber(this.value);
        //    if(this.value > 100)
        //    {
        //        this.value = 100;
        //    }
        //});

        //$scope.indexTag = 0;
        //$scope.inputTags = null;
        //$scope.myFunct = function (e) {
        //    if (e.which === 13)
        //    {
        //        var val = $scope.inputTags;
        //        if (val != null && val != '' && val.length <= 100)
        //        {
        //            $scope.indexTag++;                  
        //            var html = '<span class="tag ' + 'tag-' + $scope.indexTag + '"><span>' + val + '</span><button type="button" ng-click="removeTag(' + $scope.indexTag + ')" class="close">×</button></span>';
        //            var $el = $(html).appendTo('div.content-tags');
        //            $compile($el)($scope);                 
        //            $scope.projectInfo.Tags += val + 'Ⓐ';
        //            $scope.inputTags = null;
        //        }
        //    }               
        //}

        //$scope.removeTag = function(item)
        //{
        //    var tag = "span.tag-" + item;
        //    $(tag).remove();
        //    $scope.projectInfo.Tags = "";
        //    $('div.content-tags').find("span span").each(function () {
        //        $scope.projectInfo.Tags += $(this).text() + 'Ⓐ';
        //    });
        //}



        //$scope.GetSEOTitle = GetSEOTitle;
        //function GetSEOTitle() {
        //    $scope.projectInfo.Alias = commonService.getSeoTitle($scope.projectInfo.ProductName);
        //}
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

        $("#uploadAvatar").click(function () {
            $("#selectFileAvatar").click();
        });

        $scope.SaveContinue = SaveContinue;
        function SaveContinue() {
            $scope.typeAction = 1;
            AddPartner();
        }

        $scope.Save = Save;
        function Save() {
            $scope.typeAction = 0;
            AddPartner();
        }


        //$scope.projectInfo.Tags = "";
        $scope.AddPartner = AddPartner;
        function AddPartner() {
            //var validate = ValidateData();
            //if (validate) {
                //$scope.projectInfo.PostBy = JSON.stringify($scope.moreImages);
                var url = '/api/partner/create';
                $scope.promise = apiService.post(url, $scope.partnerInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('partner');
                    //if ($scope.typeAction == 0) {
                    //    $state.go('projects');
                    //}
                    //else {
                    //    $state.go('projectAdd');
                    //    $scope.projectInfo = {};
                    //    $scope.moreImages = [];
                    //    $("#spanImage").attr("data-title", "Chưa chọn ảnh...");
                    //    $scope.showImage = false;
                    //    $("input[name='display']").focus();
                    //}

                }, function (result) {
                    notificationService.displayError(result.data);
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

