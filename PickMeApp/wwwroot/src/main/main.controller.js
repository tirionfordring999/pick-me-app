angular.module('pick-me-app').controller('MainController', ['$scope', 'AuthService', '$location', '$cookies', '$timeout', '$http', function ($scope, AuthService, $location, $cookies, $timeout, $http) {
    $scope.menuOpened = false;

    $scope.toggleMenu = function () {

        $scope.menuOpened = !$scope.menuOpened;
    }

    AuthService.user.token = $cookies.get('token');
    $scope.user = AuthService.user;

    $scope.openProfile = function () {
        if ($scope.user && $scope.user.token) {
            $location.path('/profile');
        }
    }

    $scope.logout = function () {
        $cookies.put('token', '');
        $http.defaults.headers.common.Authorization = '';
        AuthService.user = {};
        $scope.user = AuthService.user;
        $location.path('/login');
        $timeout(() => $scope.$apply(), 1000);
    }


    $scope.$on('$locationChangeStart', function () {
        if ($scope.menuOpened) {
            $scope.toggleMenu();
        }
    });
}]);