angular.module('pick-me-app').controller('MainController', ['$scope', 'AuthService', '$location', '$cookies', function ($scope, AuthService, $location, $cookies) {
    $scope.greet = AuthService.user.token || 'Hello!';
    $scope.menuOpened = false;

    $scope.toggleMenu = function () {

        $scope.menuOpened = !$scope.menuOpened;
    }

    
    AuthService.user.token = $cookies.get('token');

    $scope.$on('$locationChangeStart', function () {
        if ($scope.menuOpened) {
            $scope.toggleMenu();
        }
    });
}]);