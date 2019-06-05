angular.module('pick-me-app').controller('SearchController', ['$scope', 'ProfileService', '$location', '$cookies', '$timeout', function ($scope, ProfileService, $location, $cookies, $timeout) {
    $scope.loaded = false;
    $scope.user = {};


    function init() {

        $scope.loaded = true;
        
    }
    init();

}]);