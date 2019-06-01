angular.module('pick-me-app').controller('HomeController', ['$scope', 'AuthService', '$location', function ($scope, AuthService, $location) {
    $scope.greet = AuthService.user.token || 'Hello!';




    if (!AuthService.user.token) {
        $location.path('/login');
    }
}]);