angular.module('pick-me-app').controller('RideController', ['$scope', 'SearchService', '$location', '$cookies', '$timeout', 'FlashService', function ($scope, SearchService, $location, $cookies, $timeout, FlashService) {


    $scope.loaded = false;
    function init() {

        SearchService.GetRides().then(function (data) {
            $scope.routes = data.routes;
            $scope.loaded = true;
        });

    }
    init();

    $scope.showDate = function (date) {
        return moment(date).format('YYYY-MM-DD');
    }

    $scope.showTime = function (time) {
        return moment(time, 'HH:mm:ss').format('HH:mm');
    }

    $scope.calculateAge = function (birthday) {
        var ageDifMs = Date.now() - new Date(birthday).getTime();
        var ageDate = new Date(ageDifMs);
        return Math.abs(ageDate.getUTCFullYear() - 1970);
    }

    $scope.acceptRequest = function (requestId) {
        var loading = FlashService.Loading();
        SearchService.AcceptRequest({ requestId }).then(function (data) {
            loading.dismiss();
            init();
        });
    }
         
}]);