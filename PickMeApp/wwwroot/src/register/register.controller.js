angular.module('pick-me-app').controller('RegisterController', ['$scope', 'AuthService', function ($scope, AuthService) {
    $scope.step = 1;
    $scope.username = 'aaaa';
    $scope.password = '';
    $scope.repeatPassword = '';
    $scope.fullName = '';
    $scope.birthDate = '';

    $scope.goToStep2 = function () {
        
        if ($scope.username !== '' && $scope.password !== '' && $scope.repeatPassword !== '') {
            if ($scope.password !== $scope.repeatPassword) {
                $scope.error = 'Password not match';
                return;
            }
            else {
                AuthService.ValidateLogin({ Login: $scope.username }).then(function (data) {
                    if (data.valid) {
                        $scope.step = 2;
                    }
                    else {
                        $scope.error = 'Login already exist';
                        return;
                    }
                });
            }
        }
        else {
            $scope.error = 'Wrong username or password!'
            return;
        }
    }

    $scope.onModelChange = function () {

        $scope.error = '';
    }

    $scope.goToStep3 = function () {

        if ($scope.fullName !== '' && $scope.birthDate !== '') {
            AuthService.Register(
                {
                    Login: $scope.username,
                    Password: $scope.password,
                    BirthDate: $scope.birthDate,
                    FullName: $scope.fullName
                }
            ).then(function (data) {
                if (data.success) {
                    $location.path('#!/login');
                }
            });
        }
        else {
            $scope.error = 'Please, add more info about you'
            return;
        }

    }

}]);