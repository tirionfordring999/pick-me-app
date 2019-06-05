angular.module('pick-me-app').controller('OfferController', ['$scope', 'ProfileService', '$location', '$cookies', '$timeout', function ($scope, ProfileService, $location, $cookies, $timeout) {

    $scope.points = [{ id: 1, name: 'Lviv' }, { id: 2, name: 'Ternopil' }, { id: 3, name: 'Chortkiv' }]; 

    $scope.date = new Date();

    $scope.time = new Date().getTime();

    $scope.cars = [{ model: '1', make: 'Toyota' }, { model: '2', make: 'BMW' }];

    $scope.step = 1;

    $scope.goToStep2 = function () {
        $scope.step++;
    }

    $scope.goToStep3 = function () {
        $scope.step++;
    }

    $scope.goToStep4 = function () {
        $scope.step++;
    }


    $scope.loaded = false;
    function init() {
        $scope.loaded = true;
        
    }
    init();

}]);