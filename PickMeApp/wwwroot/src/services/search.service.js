angular.module('pick-me-app').factory('SearchService', ['$http', '$location', function ($http, $location) {

    var service = {};

    service.user = {};

    service.GetPoints = function () {
        return $http.get('api/Ride/Points').then(handleSuccess, handleError);
    }

    service.SearchRides = function (data) {
        return $http.post('api/Ride/SearchRides', data).then(handleSuccess, handleError);
    }

    service.GetRequests = function () {
        return $http.get('api/Ride/GetRequests').then(handleSuccess, handleError);
    }

    service.GetRides = function () {
        return $http.get('api/Ride/GetRides').then(handleSuccess, handleError);
    }

    service.CancelRequest = function (data) {
        return $http.post('api/Ride/CancelRequest', data).then(handleSuccess, handleError);
    }

    service.AcceptRequest = function (data) {
        return $http.post('api/Ride/AcceptRequest', data).then(handleSuccess, handleError);
    }

    function handleSuccess(response) {
        return response.data;
    }

    function handleError(response) {
        console.log(response);
        if (response.status === 401) {
            $location.path('/login');
        }
        return Promise.reject();
    }
    return service;

}]);