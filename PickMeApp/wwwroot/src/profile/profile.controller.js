angular.module('pick-me-app').controller('ProfileController', ['$scope', 'ProfileService', '$location', '$cookies', '$timeout', function ($scope, ProfileService, $location, $cookies, $timeout) {
    $scope.loaded = false;
    $scope.user = {};


    function init() {

        ProfileService.GetData().then(function (data) {
            $scope.user = data.user;
            $scope.user.birthDate = new Date($scope.user.birthDate);
            $scope.user.age = calculateAge($scope.user.birthDate);
            $scope.loaded = true;
        });
        
    }
    init();

    function calculateAge(birthday) { 
        var ageDifMs = Date.now() - birthday.getTime();
        var ageDate = new Date(ageDifMs); 
        return Math.abs(ageDate.getUTCFullYear() - 1970);
    }

}]);