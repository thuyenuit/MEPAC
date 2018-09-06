//<reference path="/Assets/Admin/libs/angular/angular.js" />



(function (app) {
    app.controller('rangeListController', rangeListController);

    rangeListController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', 'notificationService'];

    function rangeListController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService) {

        $scope.onClickAddRange = function () {
            $state.go('rangeAdd');
        };

        // show
        $scope.options = [
            { name: 10, value: 10 },
            { name: 25, value: 25 },
            { name: 50, value: 50 },
            { name: 100, value: 100 }];
        $scope.valueShow = $scope.options[0].value;
        $scope.changeShow = function () {
            ListRange();
        };

        $scope.onChangeStatus = function () {
            ListRange();
        };

        $scope.keyWord = '';
        $scope.onSearch = function () {
            ListRange();
        };

        $scope.listStatus = [];
        $scope.listStatus.StatusID = 1;
        function LoadStatus() {
            apiService.get('/api/other/getListStatus', null, function (result) {
                $scope.listStatus = result.data;
                $scope.listStatus.StatusID = 1;
            }, function (result) {
                // notificationService.displayError('Không thể tải danh sách trạng thái');
            });
        }
        LoadStatus();

        // list
        //$scope.editFastProduct = [];

        $scope.lstRange = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.showFrom = 0;
        $scope.showTo = 0;
        $scope.checkAllDelete = false;

        $scope.ListRange = ListRange;
        function ListRange(page) {

            page = page || 0;

            var statusId = $scope.listStatus.StatusID;
            //var categoryID = $scope.categories.ProductCategoryID;
            //if (categoryID === undefined || categoryID === 'undefined' || categoryID === null)
            //    categoryID = 0;

            var keyword = $scope.keyword;
            if (keyword == undefined || keyword == null)
                keyword = "";

            var consfig = {
                params: {
                    page: page,
                    pageSize: $scope.valueShow,
                    keyword: keyword,
                    status: statusId
                }
            };

            var url = '/api/range/search';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.lstRange = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

                if (result.data.Items.length == 0) {
                    $scope.checkAllDelete = false;
                    //notificationService.displayWarning('Không có bản ghi nào được tìm thấy');
                }
                else {
                    $scope.checkAllDelete = true;
                }
            }, function (result) {
                //notificationService.displayError('Không thể tải danh sách sản phẩm');
            });

            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        $scope.ListRange();

        // select multi
        $scope.selectDelete = true;
        $scope.isAll = false;
        $scope.selectAll = selectAll;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.lstRange, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.lstRange, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }

            if ($("input[name='all']").is(":checked")) {
                $scope.selectDelete = false;
            }
            else {
                $scope.selectDelete = true;
            }
        }

        // Lắng nghe sự thay đổi của lstProductCategory,
        // co 2 tham so: 1 - lang nghe ten bien lstProductCategory
        //               2 - function (new, old) va filter nhung gia tri moi la true thi vao danh sach da dc checked
        $scope.$watch("lstRange", function (newCheck, old) {
            var checked = $filter("filter")(newCheck, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $scope.selectDelete = false;
            } else {
                $scope.selectDelete = true;
            }

            angular.forEach(newCheck, function (item) {
                if (item.checked === false) {
                    $("input[name='all']").attr('checked', false);
                    $scope.isAll = false;
                    return;
                }
            });
        }, true);

        $scope.deleteMulti = deleteMulti;
        function deleteMulti() {
            $ngBootbox.confirm('Bạn có muốn chắc xóa những mục đã chọn?').then(function () {

                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.SubMenuID);
                });
                if (listId.length > 0) {
                    var consfigs = {
                        params: {
                            jsonlistId: JSON.stringify(listId)
                        }
                    };
                    var url = '/api/range/deletemulti';
                    $scope.promise = apiService.del(url, consfigs, function (result) {
                        if (result.status === 400)
                            notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                        else
                            notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                        ListRange();
                    }, function (result) {
                        notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                    });

                    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);

                }
                else {
                    notificationService.displayError('Không có bản ghi nào được lựa chọn! Vui lòng kiểm tra lại.');
                }
            });
        }

        ////   
        //$scope.fnStopProduct = function (ProductID, ProductName) {
        //    var name = '<strong> ' + ProductName + '</strong>';
        //    $ngBootbox.confirm(' Bạn có chắc chắn ngừng kinh doanh sản phẩm ' + name + '?').then(function () {
        //        //UpdateIsAcitve(ProductID, 1);

        //        var configs = {
        //            params: {
        //                productID: ProductID,
        //                TypeID: 1
        //            }
        //        };
        //        var url = '/api/product/updateIsActive';
        //        $scope.promise = apiService.post(url, configs, function (result) {
        //            notificationService.displaySuccess(result.data);
        //            ListProduct();
        //        }, function (result) {
        //            console.log(result);
        //            if (result.status == 500) {
        //                notificationService.displayError('Hệ thống đang bảo trì. Vui lòng thao tác lại sau!');
        //            }
        //            else {
        //                notificationService.displayError(result.data);
        //            }
        //        });
        //        $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
        //    });
        //}
        //$scope.fnRecoverProduct = function (ProductID, ProductName) {
        //    var name = '<strong> ' + ProductName + '</strong>';
        //    $ngBootbox.confirm(' Bạn có muốn cho phép kinh doanh sản phẩm ' + name + '?').then(function () {
        //        UpdateIsAcitve(ProductID, 2);
        //    });
        //}
        //function UpdateIsAcitve(productID, typeID) {
        //    var configs = {
        //        params: {
        //            productID: productID,
        //            typeID: typeID
        //        }
        //    };
        //    var url = '/api/product/updateActive';
        //    $scope.promise = apiService.postParam(url, configs, function (result) {
        //        notificationService.displaySuccess(result.data);
        //        ListProduct();
        //    }, function (result) {
        //        console.log(result);
        //        if (result.status == 500) {
        //            notificationService.displayError('Hệ thống đang bảo trì. Vui lòng thao tác lại sau!');
        //        }
        //        else {
        //            notificationService.displayError(result.data);
        //        }
        //    });
        //    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);
        //}
    }
})(angular.module('sms.range'));
