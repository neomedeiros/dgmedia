(function () {
    'use strict';

    angular
        .module('dgmediaApp')
        .factory('dgMediaFactory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getActionTypes: getActionTypes,
            getEarnTypes: getEarnTypes,
            getIPS: getIPS,
            getUserIDs: getUserIDs,
            getChartData: getChartData
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

        function getChartData(chartParameters, onSuccess, onError) {
            $http.post('/Home/GenerateChart', JSON.stringify(chartParameters)).then(
                function (response) { onSuccess(response.data) },
                function (response) { onError(response.data || 'Request failed') }
            );
        }
    }

})();