//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', 'notificationService'];

    function slideListController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService) {

        $scope.onClickAddSlide = function () {
            $state.go('slideAdd');
        };

        // show
        $scope.options = [
            { name: 3, value: 3 },
            { name: 6, value: 6 },
            { name: 12, value: 12 }];
        $scope.valueShow = $scope.options[0].value;
        $scope.changeShow = function () {
            ListSlide();
        };

        $scope.onChangeStatus = function () {
            ListSlide();
        };

        $scope.keyWord = '';
        $scope.onSearch = function () {
            ListSlide();
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

        $scope.lstSlide = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.showFrom = 0;
        $scope.showTo = 0;
        $scope.checkAllDelete = false;

        $scope.ListSlide = ListSlide;
        function ListSlide(page) {

            page = page || 0;
            var statusId = $scope.listStatus.StatusID;
            var consfig = {
                params: {
                    page: page,
                    pageSize: $scope.valueShow,
                    status: statusId
                }
            };           

            var url = '/api/slide/search';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.lstSlide = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.StrDate = result.data.StrDate
                $scope.StrHour = result.data.StrHour;
                $scope.StrUser = result.data.StrUser;
                $scope.showTitle = false;

                if (result.data.Items.length == 0) {
                    $scope.checkAllDelete = false;
                }
                else {
                    $scope.showTitle = true;;
                    $scope.checkAllDelete = true;                
                }
            }, function (result) {

            });

            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        $scope.ListSlide();

        // select multi
        $scope.selectDelete = true;
        $scope.isAll = false;
        $scope.selectAll = selectAll;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.lstSlide, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.lstSlide, function (item) {
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
        $scope.$watch("lstSlide", function (newCheck, old) {
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
                    listId.push(item.ProjectID);
                });
                if (listId.length > 0) {
                    var consfigs = {
                        params: {
                            jsonlistId: JSON.stringify(listId)
                        }
                    };
                    var url = '/api/projects/deletemulti';
                    $scope.promise = apiService.del(url, consfigs, function (result) {
                        if (result.status === 400)
                            notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                        else
                            notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                        ListSlide();
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

        
    }
})(angular.module('sms.slide'));
