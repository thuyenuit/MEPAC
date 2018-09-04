 //<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('categoryListController', categoryListController);

    categoryListController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', 'notificationService'];

    function categoryListController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService) {

        $scope.fnAddCategory = function () {
            $state.go('categoryAdd');
        };

        // show
        $scope.options = [
            { name: 10, value: 10 },
            { name: 25, value: 25 },
            { name: 50, value: 50 },
            { name: 100, value: 100 }];
        $scope.valueShow = $scope.options[0].value;
        $scope.changeShow = function () {
            ListCategory();
        };

        $scope.onChangeStatus = function () {
            ListCategory();
        };

        $scope.keyWord = '';
        $scope.onSearch = function () {
            ListCategory();
        };

        $scope.listStatus = [];
        $scope.listStatus.StatusID = 0;
        function LoadStatus() {
            apiService.get('/api/other/getListStatus', null, function (result) {
                $scope.listStatus = result.data;
                $scope.listStatus.StatusID = 0;
            }, function (result) {
                //notificationService.displayError('Không thể tải danh sách trạng thái');
            });
        }
        LoadStatus();

        // list
        $scope.lstProductCategory = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.showFrom = 0;
        $scope.showTo = 0;
        $scope.checkAllDelete = true;

        $scope.ListCategory = ListCategory;
        function ListCategory(page) {
          
            page = page || 0;

            var statusId = $scope.listStatus.StatusID;
            var consfig = {
                params: {
                    page: page,
                    pageSize: $scope.valueShow,
                    keyWord: $scope.keyWord,
                    status: statusId
                }
            };
            var url = '/api/category/search';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.lstProductCategory = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

                if (result.data.Items.length == 0)
                {
                    $scope.checkAllDelete = false;
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy');
                }
                else
                {
                    $scope.checkAllDelete = true;
                }
            }, function (result) {
                //notificationService.displayError('Không thể tải danh sách thể loại');
            });

            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        $scope.ListCategory();

        $scope.sortColumn = 'Display';
        $scope.reverse = true; // sắp xếp giảm dần
        $scope.sortData = function (column) {
            if ($scope.sortColumn === column)
                $scope.reverse = !$scope.reverse;
            else
                $scope.reverse = true;
            $scope.sortColumn = column;
        };
        $scope.getSortClass = function (column) {
            if ($scope.sortColumn === column) {
                return $scope.reverse ? 'arrow-down' : 'arrow-up';
            }

            return '';
        };

        // select multi
        $scope.selectDelete = false; 
        $scope.isAll = false;
        $scope.selectAll = selectAll;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.lstProductCategory, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
                $scope.selectDelete = true;
            }
            else {
                angular.forEach($scope.lstProductCategory, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
                $scope.selectDelete = false;
            }
        }

        // Lắng nghe sự thay đổi của lstProductCategory,
        // co 2 tham so: 1 - lang nghe ten bien lstProductCategory
        //               2 - function (new, old) va filter nhung gia tri moi la true thi vao danh sach da dc checked
        $scope.$watch("lstProductCategory", function (newCheck, old) {
            var checked = $filter("filter")(newCheck, { checked: true });
            if (checked.length) {
                $scope.selected = checked;             
                $scope.selectDelete = true;
                $('#btnDeleteMulti').removeAttr('disabled');
            } else {              
                $scope.selectDelete = false;
                $('#btnDeleteMulti').attr('disabled', 'disabled');
            }

            angular.forEach(newCheck, function (item) {
                if (item.checked === false)
                {
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
                    listId.push(item.ProductCategoryID);
                });
                if (listId.length > 0)
                {
                    var consfigs = {
                        params: {
                            jsonlistId: JSON.stringify(listId)
                        }
                    };
                    var url = '/api/category/deletemulti';
                    $scope.promise = apiService.del(url, consfigs, function (result) {
                        if (result.status === 400)
                            notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                        else
                            notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                        ListCategory();
                    }, function () {
                        notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                    });

                    $scope.$parent.MethodShowLoading("Đang xử lý", $scope.promise);

                }
                else
                {
                    notificationService.displayError('Không có bản ghi nào được lựa chọn! Vui lòng kiểm tra lại.');
                }
            });
        }

        // phục hồi thể loại
        $scope.RefreshProductCategory = RefreshProductCategory;
        function RefreshProductCategory(productCategoryId, productCategoryName) {
            var name = '<strong> ' + productCategoryName + '</strong>';
            $ngBootbox.confirm(' Bạn có muốn phục hồi thể loại' + name + '?').then(function () {

                alert('ok ' + productCategoryName);
            });
        }
    }
})(angular.module('sms.category'));