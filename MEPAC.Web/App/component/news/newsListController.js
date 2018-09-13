//<reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('newsListController', newsListController);

    newsListController.$inject = ['$scope',
        'apiService', '$interval', '$filter', '$ngBootbox', '$state', 'notificationService'];

    function newsListController($scope, apiService, $interval,
        $filter, $ngBootbox, $state, notificationService) {

        $scope.onClickAddNews = function () {
            $state.go('newsAdd');
        };

        // show
        $scope.options = [
            { name: 10, value: 10 },
            { name: 25, value: 25 },
            { name: 50, value: 50 },
            { name: 100, value: 100 }];
        $scope.valueShow = $scope.options[0].value;
        $scope.changeShow = function () {
            ListProject();
        };

        $scope.onChangeStatus = function () {
            ListProject();
        };

        $scope.onChangeTypeNews = function () {
            ListProject();
        };
     
        $scope.keyWord = '';
        $scope.onSearch = function () {
            ListProject();
        };

        $scope.lstTypeNews = [];
        function getTypeNews() {
            apiService.get('/api/other/getTypeNews', null, function (result) {
                $scope.lstTypeNews = result.data;
                console.log($scope.lstTypeNews);
            }, function (result) {
                // notificationService.displayError('Không thể tải danh sách trạng thái');
            });
        }
        getTypeNews();

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
        $scope.lstProject = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.showFrom = 0;
        $scope.showTo = 0;
        $scope.checkAllDelete = false;

        $scope.ListProject = ListProject;
        function ListProject(page) {

            page = page || 0;

            var statusId = $scope.listStatus.StatusID;
            var typeNewsID = $scope.lstTypeNews.SubMenuID;
            if (typeNewsID === undefined || typeNewsID === 'undefined' || typeNewsID === null)
                typeNewsID = 0;

            var keyword = $scope.keyword;
            if (keyword == undefined || keyword == null)
                keyword = "";

            var consfig = {
                params: {
                    page: page,
                    pageSize: $scope.valueShow,
                    keyword: keyword,
                    status: statusId,
                    typeNewsID: typeNewsID
                }
            };           

            var url = '/api/news/search';
            $scope.promise = apiService.get(url, consfig, function (result) {
                $scope.lstProject = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.StrDate = result.data.StrDate
                $scope.StrHour = result.data.StrHour;
                $scope.StrUser = result.data.StrUser;
                $scope.showTitle = false;

                if (result.data.Items.length == 0) {
                    $scope.checkAllDelete = false;
                    //notificationService.displayWarning('Không có bản ghi nào được tìm thấy');
                }
                else {
                    $scope.showTitle = true;;
                    $scope.checkAllDelete = true;                
                }
            }, function (result) {
                //notificationService.displayError('Không thể tải danh sách sản phẩm');
            });

            $scope.$parent.MethodShowLoading("Đang tải dữ liệu", $scope.promise);
        }
        $scope.ListProject();

        // select multi
        $scope.selectDelete = true;
        $scope.isAll = false;
        $scope.selectAll = selectAll;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.lstProject, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.lstProject, function (item) {
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
        $scope.$watch("lstProject", function (newCheck, old) {
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
                        ListProject();
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
})(angular.module('sms.news'));
