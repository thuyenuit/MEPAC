//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('projectsAddController', projectsAddController);

    projectsAddController.$inject = ['$scope', '$sce', '$compile',
        'apiService', '$interval', '$filter', '$ngBootbox',
        '$state', 'notificationService', 'commonService', 'authenticationService'];

    function projectsAddController($scope, $sce, $compile, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService, commonService, authenticationService) {

        angular.element("input[name='quantity']").on('input', function () {
            this.value = commonService.inputNumber(this.value);
        });
        angular.element("input[name='existMinimum']").on('input', function () {
            this.value = commonService.inputNumber(this.value);
        });
        angular.element("input[name='existMaximum']").on('input', function () {
            this.value = commonService.inputNumber(this.value);
        });
        angular.element("input[name='pricePromotion']").on('input', function () {
            this.value = commonService.inputNumber(this.value);
        });
        angular.element("input[name='priceCost']").on('input', function () {
            this.value = commonService.inputNumber(this.value);
        });
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
   
        
        $scope.projectInfo.Tags = "";
        $scope.AddProject = AddProject;
        function AddProject() {
            var error = ValidateData();
            if(true)
            {
                var url = '/api/projects/create';
                $scope.promise = apiService.post(url, $scope.projectInfo, function (result) {
                        notificationService.displaySuccess(result.data);
                        if ($scope.typeAction == 0) {
                            $state.go('projects');
                        }
                        //$scope.projectInfo = {};

                    }, function (result) {
                        notificationService.displayError(result.data);
                    });
                    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
            }
        }

        function ValidateData() {
            var textError = '';
            var arrayError = [];
            if ($scope.projectInfo.Display == null || $scope.projectInfo.Display == '' 
            || $scope.projectInfo.Display == undefined)
            {
                arrayError.push("Vui lòng nhập tên sản phẩm");              
            }
            else if ($scope.projectInfo.Display.length > 200)
            {
                arrayError.push("Tên sản phẩm không vượt quá 200 ký tự");
            }

            if (($scope.projectInfo.FromDate == null || $scope.projectInfo.FromDate == ''
                || $scope.projectInfo.FromDate == undefined)
                || ( $scope.projectInfo.ToDate == null || $scope.projectInfo.ToDate == ''
                || $scope.projectInfo.ToDate == undefined)) {
                arrayError.push("Vui lòng nhập thời gian thực hiện");
            }

            if ($scope.projectInfo.LinkImage == null || $scope.projectInfo.LinkImage == '')
            {
                arrayError.push("Vui lòng chọn file ảnh");
            }

            if (arrayError.length > 0)
            {
                textError = arrayError.join(", ");
                notificationService.displayError(textError);
                return false;
            }

            return true;
        }

    }
})(angular.module('sms.projects'));

