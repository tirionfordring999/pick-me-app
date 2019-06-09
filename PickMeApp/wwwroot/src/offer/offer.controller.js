angular.module('pick-me-app').controller('OfferController', ['$scope', 'SearchService', 'FlashService', 'OfferService', '$location', '$cookies', '$timeout', function ($scope, SearchService, FlashService, OfferService, $location, $cookies, $timeout) {

    $scope.points = []; 

    $scope.routePoints = [
        {},
        {}
    ];

    $scope.addPoint = function () {
        $scope.routePoints.push({});
    }
    $scope.removePoint = function () {
        if ($scope.routePoints.length > 2) {
            $scope.routePoints.splice(1, 1);
        }
    }

    $scope.date = new Date();

    $scope.time = new Date();

    $scope.step = 1;

    $scope.goToStep2 = function () {
        $scope.step++;
        $scope.displayDate = moment($scope.date).format('DD/MM');
        $scope.displayTime = moment($scope.time).format('HH:mm');
    }

    $scope.goToStep3 = function () {
        $scope.step++;
    }

    $scope.goToStep4 = function () {
        var loading = FlashService.Loading();
        OfferService.SaveRide({
            date: $scope.date,
            time: $scope.time,
            points: $scope.routePoints
        }).then(function (data) {
            loading.dismiss();
            $scope.step++;
        });
    }

    $scope.loaded = false;
    function init() {
        SearchService.GetPoints().then(function (data) {
            $scope.points = data.points;
            $scope.loaded = true;
        });
    }
    init();

}]);