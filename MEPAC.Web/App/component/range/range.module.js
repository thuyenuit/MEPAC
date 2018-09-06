

(function () {
	angular.module('sms.range', ['sms.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {

		$stateProvider
            .state('range', {
            	parent: 'baseView',
            	url: '/range',
            	templateUrl: '/app/component/range/_rangeListView.html',
            	controller: 'rangeListController'
            })
            //.state('rangeAdd', {
            //	parent: 'baseView',
            //	url: '/add-range',
            //	templateUrl: '/app/component/range/_rangeAddView.html',
            //	controller: 'rangeAddController'
            //})
            .state('rangeEdit', {
            	parent: 'baseView',
            	url: '/edit-range?rangeId',
            	templateUrl: '/app/component/range/_rangeEditView.html',
            	controller: 'rangeEditController'
		    });
	}
})();