angular.module('pick-me-app').controller('SearchController', ['$scope', '$routeParams', 'SearchService', '$location', '$cookies', '$timeout', function ($scope, $routeParams, SearchService, $location, $cookies, $timeout) {
    $scope.loaded = false;
    $scope.points = [];
    $scope.date = new Date();
    $scope.time = new Date().getTime();

    function init() {
        
        SearchService.GetPoints().then(function (data) {

            $scope.points = data.points;
            $scope.loaded = true;
        });        
    }

    $scope.search = function () {
        $location.path('/searchresults').search({
            time: new Date($scope.time),
            date: $scope.date,
            start: $scope.startPoint.pointId,
            end: $scope.endPoint.pointId
        });
    }

    init();


}]);