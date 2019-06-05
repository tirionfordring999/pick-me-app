angular.module('pick-me-app').config(['$routeProvider','$locationProvider',
    function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/home', {
                templateUrl: 'src/home/home.html',
                controller: 'HomeController'
            })
            .when('/login', {
                templateUrl: 'src/login/login.html',
                controller: 'LoginController'
            })
            .when('/register', {
                templateUrl: 'src/register/register.html',
                controller: 'RegisterController'
            })
            .when('/profile', {
                templateUrl: 'src/profile/profile.html',
                controller: 'ProfileController'
            })
            .when('/offer', {
                templateUrl: 'src/offer/offer.html',
                controller: 'OfferController'
            })
            .when('/search', {
                templateUrl: 'src/search/search.html',
                controller: 'SearchController'
            })
            .when('/searchresults', {
                templateUrl: 'src/searchresults/searchresults.html',
                controller: 'SearchResultsController'
            })
            .otherwise({
                redirectTo: '/home'
            });
    }
]);