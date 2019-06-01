angular.module('pick-me-app').controller('LoginController', ['$scope', 'AuthService', '$location', '$cookies', function ($scope, AuthService, $location, $cookies) {

    
    $scope.greet = "Hello!";

    $scope.login = function () {
        AuthService.Login({ login: "1", pass: "1" }).then(function (data) {

            $cookies.put('token', data.token);
            AuthService.user.token = data.token;
            $location.path("/home");
        });
    }

}]);