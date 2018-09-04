//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productImportController', productImportController);

    productImportController.$inject = ['apiService',
        '$http',
        'authenticationService',
        '$scope',
        'notificationService',
        '$state', 
        'commonService'];

    function productImportController(apiService, $http, authenticationService,
        $scope, notificationService, $state, commonService) {

        //var authen = authenticationService.getTokenInfo();
        //if (authen == null || authen == ''|| authen == undefined) {
        //    authenticationService.removeToken();
        //    $state.go('login');
        //}

        // tải file mẫu
        $scope.DownloadTemplate = DownloadTemplate;
        function DownloadTemplate() {
            var url = '/api/product/downloadTemplate';
            //$("#btnCloseImportExcel").click();
            $scope.promise = apiService.get(url, null, function (result) {
                window.open(result.data.Message);
            }, function (result) {
                notificationService.displayError(result.data);
            });
            $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
        }

        //import excel
        $scope.linkFileImport = 'Chưa chọn file';

        $scope.files = [];
        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.linkFileImport = $("#fileId").val();
                $scope.files.push(args.file);
            });
        });

        $scope.typeImport = 1;
        $scope.changeTypeInsert = changeTypeInsert;
        function changeTypeInsert() {
            if ($("input[class='typeInsert']").is(":checked")) {
                $scope.typeImport = 1;
            }
            else {
                $scope.typeImport = 2;
            }
        }

        $scope.changeTypeUpdate = changeTypeUpdate;
        function changeTypeUpdate() {
            if ($("input[class='typeUpdate']").is(":checked")) {
                $scope.typeImport = 2;
            }
            else {
                $scope.typeImport = 1;
            }
        }


        $scope.listErrores = {};
        $scope.filename = '';
        $scope.ImportProduct = ImportProduct;
        function ImportProduct() {
            if ($scope.files.length <= 0) {
                notificationService.displayError("Vui lòng chọn File excel cần import");
            }
            else {

                var ext = angular.element("input[name='file']").val().match(/\.([^\.]+)$/)[1];

                if (ext === 'xls' || ext === 'xlsx') {
                    authenticationService.setHeader();
                    var url = '/api/product/importExcel';
                    $scope.promise = $http({
                        method: "POST",
                        url: url,
                        headers: { 'Content-Type': undefined },
                        transformRequest: function (data) {
                            var formData = new FormData();
                            formData.append("typeImportExcel", $scope.typeImport);

                            for (var i = 0; i < data.files.length; i++) {
                                formData.append("file" + i, data.files[i]);
                            }
                            return formData;
                        },
                        data: { files: $scope.files }
                    }).then(function (result, status, headers, config) {
                        notificationService.displaySuccess(result.data);
                    }, function (error, status, headers, config) {

                        console.log("1:", error);
                        if (result.status === 502) {
                            $scope.listErrores = result.data;
                            // $("button[name='btnShowError']").click();
                            notificationService.displayError(result.data);
                        }
                        else {
                            notificationService.displayError(result.data.ExceptionMessage);
                        }

                    });

                    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
                }
                else {
                    notificationService.displayError("File không hợp lệ. Vui lòng chọn file excel");
                }
            }

            $scope.files = [];
            angular.element("input[name='file']").val('');
        }

    }
})(angular.module('sms.product'));

