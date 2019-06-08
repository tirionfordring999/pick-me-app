angular.module('pick-me-app').controller('LoginController', ['$scope', 'AuthService', '$location', '$cookies', '$http', 'FlashService', function ($scope, AuthService, $location, $cookies, $http, FlashService) {

    
    $scope.username = '';
    $scope.password = '';

    $scope.login = function () {

        var loading = FlashService.Loading();
        if ($scope.username !== '' && $scope.password !== '') {
            AuthService.Login({ login: $scope.username, pass: $scope.password }).then(function (data) {

                $cookies.put('token', data.token);
                AuthService.user.token = data.token;
                $http.defaults.headers.common.Authorization = data.token;
                $location.path("/home");
                loading.dismiss();
            });
        }
        else {
            $scope.error = 'Wrong username or password!';
        }
    }

    $scope.onModelChange = function () {

        $scope.error = '';
    }

}]);