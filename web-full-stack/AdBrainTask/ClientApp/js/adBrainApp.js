; (function () {
    var adBrainApp = angular.module('adBrainApp', ['ngRoute']);

    adBrainApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/home', {
                templateUrl: 'ClientApp/views/home.html',
                controller: 'adBrainCtrl'
            }).
            when('/about', {
                templateUrl: 'ClientApp/views/about.html'
            }).
            otherwise({
                redirectTo: '/home'
            });
        }]);

    adBrainApp.controller('adBrainCtrl', ['$scope', '$http',  function ($scope, $http) {
        $scope.sportPostDomain = '';

        $scope.filterData = function () {
            $scope.loading = true;
            $http.get('/sports?domain=' + $scope.sportPostDomain).then(function (response) {
                $scope.sportPostGroups = response.data;
                $scope.loading = false;
            });
        };

        $scope.filterData();
    }]);

})();