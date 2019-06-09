angular.module('pick-me-app').factory('OfferService', ['$http', '$location', function ($http, $location) {

    var service = {};

    service.SaveRide = function (data) {
        return $http.post('api/Ride/SaveRide', data).then(handleSuccess, handleError);
    }

    service.GetRideForBook = function (data) {
        return $http.post('api/Ride/GetRideForBook', data).then(handleSuccess, handleError);
    }

    service.BookRide = function (data) {
        return $http.post('api/Ride/BookRide', data).then(handleSuccess, handleError);
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