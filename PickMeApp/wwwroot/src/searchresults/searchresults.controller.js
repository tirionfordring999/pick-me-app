angular.module('pick-me-app').controller('SearchResultsController', ['$scope', 'SearchService', '$location', '$routeParams', '$timeout', function ($scope, SearchService, $location, $routeParams, $timeout) {
    $scope.loaded = false;
    $scope.user = {};

    function init() {

        var date = $routeParams['date'];
        var time = $routeParams['time'];
        var endPointId = $routeParams['end'];
        var startPointId = $routeParams['start'];

        $location.search('date', null);
        $location.search('time', null);
        $location.search('end', null);
        $location.search('start', null);

        if (!date || !time || !endPointId || !startPointId) {
            $location.path('/search');
            return;
        }

        SearchService.SearchRides({

            date: moment(date).format('YYYY-MM-DD'),
            time: moment(time).format('HH:mm'),
            end: endPointId,
            start: startPointId

        }).then(function (data) {
            data.rides.forEach(function (item) {
                item.date = moment(item.date,'YYYY-MM-DD').format('MM/DD'),
                item.time = moment(item.time, 'HH:mm:ss').format('HH:mm'),
                item.driver.birthDate = new Date(item.driver.birthDate)
                
            })


            $scope.rides = data.rides;
            $scope.loaded = true;
        });
        
    }
    init();

    $scope.calculateAge = function (birthday) {
        var ageDifMs = Date.now() - birthday.getTime();
        var ageDate = new Date(ageDifMs);
        return Math.abs(ageDate.getUTCFullYear() - 1970);
    }

}]);