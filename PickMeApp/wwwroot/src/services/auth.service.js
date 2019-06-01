angular.module('pick-me-app').factory('AuthService', ['$http', function ($http) {

    var service = {};

    service.user = {};

    service.Login = function (data) {

        return $http.post('api/Auth/Login', data).then(handleSuccess, handleError);
    }

    service.Register = function (data) {

        return $http.post('api/Auth/Register', data).then(handleSuccess, handleError);
    }


    function handleSuccess(response) {
        return response.data;
    }

    function handleError(response) {
        FlashService.Error(response.error);
    }
    return service;

}]);