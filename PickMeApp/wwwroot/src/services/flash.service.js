angular.module('pick-me-app').factory('FlashService', ['$http', '$location', '$uibModal', function ($http, $location, $uibModal) {

    var service = {};

    service.Loading = function () {
        return $uibModal.open({
            templateUrl: 'src/directives/loading.html',
            windowClass: 'empty-modal',
            backdrop: true
        });
    }

    service.Error = function (message) {
        return $uibModal.open({
            template: '<div class="alert alert-danger margin-0" role="alert">' + message  +'</div>'});
    }

    return service;

}]);