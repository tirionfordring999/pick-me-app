angular.module('pick-me-app').factory('SearchService', ['$http', '$location', function ($http, $location) {

    var service = {};

    service.user = {};

    service.GetPoints = function () {
        return $http.get('api/Ride/Points').then(handleSuccess, handleError);
    }

    service.SearchRides = function (data) {
        return $http.post('api/Ride/SearchRides', data).then(handleSuccess, handleError);
    }

    function handleSuccess(response) {
        return response.data;
    }

    function handleError(response) {
        console.log(response);
        return Promise.reject();
    }
    return service;

}]);