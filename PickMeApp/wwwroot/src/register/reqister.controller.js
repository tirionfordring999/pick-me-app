angular.module('pick-me-app').controller('RegisterController', ['$scope', function ($scope) {

    $scope.step = 1;

    $scope.goToStep2 = function () {

    }

    $scope.goToStep3 = function () {

    }

    $scope.modelChanged = function () {
        $scope.error = '';
    }

}]);