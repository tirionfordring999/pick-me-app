angular.module('pick-me-app').controller('LoginController', ['$scope', 'AuthService', '$location', function ($scope, AuthService, $location) {

    
    $scope.greet = "Hello!";

    $scope.login = function () {
        AuthService.Login({ login: "1", pass: "1" }).then(function (data) {

            AuthService.user.token = data.token;
            $location.path("/home");
        });
    }

}]);