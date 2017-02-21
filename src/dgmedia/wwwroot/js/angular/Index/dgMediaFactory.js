(function () {
    'use strict';

    angular
        .module('dgmediaApp')
        .factory('dgMediaFactory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getActionTypes: getActionTypes,
            getTenants: getTenants,
            getEarnTypes: getEarnTypes,
            getIPS: getIPS,
            getUserIDs: getUserIDs,
            getChartData: getChartData,
            getChartIntervals: getChartIntervals
        };

        return service;

        function getActionTypes(onSuccess, onError) {

            $http.get('/Home/GetActionTypes').then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }

        function getEarnTypes(onSuccess, onError) {
            $http.get('/Home/GetEarnTypes').then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }

        function getIPS(onSuccess, onError) {
            $http.get('/Home/GetIPS').then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }
        function getUserIDs(onSuccess, onError) {
            $http.get('/Home/GetUserIDs').then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }

        function getTenants(onSuccess, onError) {

            $http.get('/Home/GetTenants').then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }

        function getChartIntervals(onSuccess, onError) {
            $http.get('/Home/GetChartIntervals').then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }

        function getChartData(chartParameters, onSuccess, onError) {
            $http.post('/Home/GenerateChart', JSON.stringify(chartParameters)).then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }
    }

})();