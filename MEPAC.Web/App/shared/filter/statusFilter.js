﻿(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return 'Đang hiển thị';
            else
                return 'Đã hủy';
        }
    })

    //app.filter('statusFilter', statusFilter);

    //function statusFilter() {
    //    return {
    //        statusProduct: statusProduct         
    //    }

    //    function statusProduct(input) {
    //        if (input == true)
    //            return 'Đang kinh doanh';
    //        else
    //            return 'Ngừng kinh doanh';
    //    }
    //}
})(angular.module('sms.common'));