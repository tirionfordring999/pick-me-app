angular.module('pick-me-app').factory('OfferService', ['$http', '$location', function ($http, $location) {

    var service = {};

    service.GetData = function () {
        return $http.get('api/Profile/GetData').then(handleSuccess, handleError);
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