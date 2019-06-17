angular.module('pick-me-app').controller('RequestController', ['$scope', 'SearchService', '$location', '$cookies', '$timeout', 'FlashService', function ($scope, SearchService, $location, $cookies, $timeout, FlashService) {
    $scope.loaded = false;
    $scope.user = {};


    function init() {

        SearchService.GetRequests().then(function (data) {
            $scope.requests = data.requests;
            $scope.loaded = true;
        });
        
    }
    init();

    $scope.showDate = function (date) {
        return moment(date).format('YYYY-MM-DD');
    }

    $scope.showTime = function (time) {
        return moment(time,'HH:mm:ss').format('HH:mm');
    }

    $scope.calculateAge = function (birthday) {
        var ageDifMs = Date.now() - new Date(birthday).getTime();
        var ageDate = new Date(ageDifMs);
        return Math.abs(ageDate.getUTCFullYear() - 1970);
    }

    $scope.deleteRequest = function (requestId) {
        var loading = FlashService.Loading();
        SearchService.CancelRequest({ requestId: requestId })
            .then(function (data) {
                loading.dismiss();
                init();
            });
    }


}]);