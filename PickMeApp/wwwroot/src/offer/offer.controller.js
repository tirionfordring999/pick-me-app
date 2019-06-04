angular.module('pick-me-app').controller('ProfileController', ['$scope', 'ProfileService', '$location', '$cookies', '$timeout', function ($scope, ProfileService, $location, $cookies, $timeout) {
    $scope.loaded = false;
    $scope.user = {};


    function init() {

        ProfileService.GetData().then(function (data) {
            $scope.user = data;
            $scope.loaded = true;
        });
        
    }
    init();

}]);