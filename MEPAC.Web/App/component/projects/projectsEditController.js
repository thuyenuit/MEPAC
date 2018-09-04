//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', '$stateParams', 'notificationService'];

    function productEditController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, $stateParams, notificationService) {

        angular.element("input[name='quantity']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');
        });
        angular.element("input[name='existMinimum']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');
        });
        angular.element("input[name='existMaximum']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');
        });
        angular.element("input[name='pricePromotion']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');
        });
        angular.element("input[name='priceCost']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');
        });
        angular.element("input[name='priceSell']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');
            if (this.value <= 0) {
                $scope.pcEditInfo.PricePromotion = null;
                $scope.percent = null;
            }
        });

        angular.element("input[name='percent']").on('input', function () {
            this.value = this.value.replace(/[^\d\.\-]/g, '').replace('-', '');

            if (this.value > 100) {
                this.value = 100;
            }
        });

        $scope.GetSEOTitle = GetSEOTitle;
        function GetSEOTitle() {
            $scope.pcEditInfo.Alias = commonService.getSeoTitle($scope.pcEditInfo.ProductName);
        }

        $scope.GetPriceSale = GetPriceSale;
        function GetPriceSale() {
            if ($scope.percent > 0) {
                var priceSell = $scope.pcEditInfo.PriceSell;
                $scope.pcEditInfo.PricePromotion = ($scope.percent * priceSell) / 100;
            }
            else {
                $scope.pcEditInfo.PricePromotion = null;
            }
        }

        $scope.categories = {};
        function LoadCategory() {
            apiService.get('/api/productcategory/getallNoPage', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                notificationService.displayError('Không thể tải danh sách thể loại');
            });
        }
        LoadCategory();


        $scope.pcEditInfo = {};
        $scope.LoadProductInfo = LoadProductInfo;
        function LoadProductInfo() {
            var consfig = {
                params: {
                    productId: $stateParams.productID
                }
            };

            var url = '/api/product/getbyid';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.pcEditInfo = result.data;
            }, function () {
                notificationService.displayError('Không tìm thấy thông tin sản phẩm! Vui lòng kiểm tra lại');
            })
            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        LoadProductInfo();


        $scope.EditProduct = EditProduct;
        function EditProduct() {
            var validate = ValidateData();
            if (validate) {
                var url = '/sms/product/update';
                $scope.promise = apiService.put(url, $scope.pcEditInfo, function (result) {
                    notificationService.displaySuccess(result.data);
                    $state.go('products');
                }, function (result) {
                    console.log(result);
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
            if ($scope.pcEditInfo.ProductName == null || $scope.pcEditInfo.ProductName == ''
            || $scope.pcEditInfo.ProductName == undefined) {
                arrayError.push("Vui lòng nhập tên sản phẩm");
            }
            else if ($scope.pcEditInfo.ProductName.length > 255) {
                arrayError.push("Tên sản phẩm không vượt quá 255 ký tự");
            }

            if ($scope.pcEditInfo.ProductCategoryID == null || $scope.pcEditInfo.ProductCategoryID == ''
            || $scope.pcEditInfo.ProductCategoryID == undefined) {
                arrayError.push("Vui lòng chọn thể loại");
            }

            if ($scope.pcEditInfo.ProductCode != null && $scope.pcEditInfo.ProductCode.length > 255) {
                arrayError.push("Mã sản phẩm không vượt quá 255 ký tự");
            }

            if ($scope.pcEditInfo.ExistMaximum > 0 && $scope.pcEditInfo.ExistMinimum > 0) {
                if ($scope.pcEditInfo.ExistMaximum <= $scope.pcEditInfo.ExistMinimum) {
                    arrayError.push("Giá trị tồn tối thiểu không được lớn hơn tồn tối đa");
                }
            }

            if (arrayError.length > 0) {
                textError = arrayError.join(", ");
                notificationService.displayError(textError);
                return false;
            }

            return true;
        }


    }
})(angular.module('sms.product'));


