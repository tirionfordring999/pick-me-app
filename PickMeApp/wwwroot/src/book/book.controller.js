angular.module('pick-me-app').controller('BookController', ['$scope', 'SearchService', 'FlashService', 'OfferService', '$location', '$routeParams', '$timeout', function ($scope, SearchService, FlashService, OfferService, $location, $routeParams, $timeout) {

    $scope.step = 1;
    $scope.seats = 1;

    $scope.seatsChanged = function () {

        if ($scope.seats > ($scope.ride.numberOfSeats - $scope.ride.seatsBooked)) {
            $scope.seats = ($scope.ride.numberOfSeats - $scope.ride.seatsBooked);
        }
    }

    $scope.loaded = false;
    function init() {

        var id = $routeParams['id'];
        var start = $routeParams['start'];
        var end = $routeParams['end'];

        $location.search('id', null);
        $location.search('start', null);
        $location.search('end', null);

        OfferService.GetRideForBook({ id, start, end }).then(function (data) {
            if (!data.ride) {
                $location.path('/search');
            }
            $scope.ride = data.ride;
            $scope.displayDate = moment($scope.ride.date).format('DD/MM');
            $scope.displayTime = moment($scope.ride.time, 'HH:mm:ss').format('HH:mm');
            $scope.loaded = true;
        });
    }
    init();

    $scope.goToStep2 = function () {
        OfferService.BookRide({
            seats: $scope.seats,
            start: $scope.ride.startPoint.pointId,
            end: $scope.ride.endPoint.pointId,
            id: $scope.ride.routeId
        }).then(function (data) {
            if (data.message === "Success") {
                $scope.step++;
            }
        });
        
    }

}]);