angular.module('pick-me-app').controller('HomeController', ['$scope', 'AuthService', '$location', '$anchorScroll', function ($scope, AuthService, $location, $anchorScroll) {

    $scope.scroll = function () {
        $location.hash('bottom');
        $anchorScroll();
    }

}]);