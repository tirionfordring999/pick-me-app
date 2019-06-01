﻿angular.module('pick-me-app').config(['$routeProvider','$locationProvider',
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
            .otherwise({
                redirectTo: '#!/home'
            });
    }
]);