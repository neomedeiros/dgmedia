(function () {
    'use strict';

    app.controller('dgmediaController', ['$scope', '$http', 'dgMediaFactory', function ($scope, $http, $dgMediaFactory) {

        $scope.actionTypes = [];
        $scope.earnTypes = [];
        $scope.IPS = [];
        $scope.userIDs = [];
        $scope.tenants = [];
        $scope.chartIntervals = [];

        $scope.selectedActionStart = { startDate: moment("2017-01-01"), endDate: moment("2017-01-31") };
        $scope.selectedActionTypes = null;
        $scope.selectedEarnTypes = null;
        $scope.selectedStartDate = { startDate: null, endDate: null };
        $scope.selectedIPS = null;
        $scope.actionUrl = null;
        $scope.selectedUserIDs = null;
        $scope.selectedTenants = null;
        $scope.selectedResultChartType = 0;
        $scope.selectedChartInterval = 0;

        $scope.isLoadingActionTypes = true;
        $scope.isLoadingEarnTypes = true;
        $scope.isLoadingIPS = true;
        $scope.isLoadingUserIDs = true;
        $scope.isLoadingTenants = true;
        $scope.isLoadingChartIntervals = true;

        $scope.isLoadingChart = false;

        $scope.chartParameters = null;

        $scope.chartData = [];

        $scope.init = function () {

            $scope.getActionTypes();
            $scope.getEarnTypes();
            $scope.getIPS();
            $scope.getUserIDs();
            $scope.getTenants();
            $scope.getChartIntervals();

            var propertiesToObserve = [
                        'selectedActionStart',
                        'selectedActionTypes',
                        'selectedEarnTypes',
                        'selectedStartDate',
                        'selectedIPS',
                        'selectedUserIDs',
                        'selectedTenants',
                        'selectedResultChartType',
                        'selectedChartInterval']

            $scope.subscribeProperties(propertiesToObserve);

        }

        $scope.getActionTypes = function () {
            $dgMediaFactory.getActionTypes(
            (result) => {
                $scope.actionTypes = result;
                $scope.isLoadingActionTypes = false;
            },
            (error) => {
                console.log(error);
            });
        }

        $scope.getEarnTypes = function () {
            $dgMediaFactory.getEarnTypes(
            (result) => {
                $scope.earnTypes = result;
                $scope.isLoadingEarnTypes = false;
            },
            (error) => {
                console.log(error);
            });
        }

        $scope.getIPS = function () {
            $dgMediaFactory.getIPS(
            (result) => {
                $scope.IPS = result;
                $scope.isLoadingIPS = false;
            },
            (error) => {
                console.log(error);
            });
        }

        $scope.getUserIDs = function () {
            $dgMediaFactory.getUserIDs(
            (result) => {
                $scope.userIDs = result;
                $scope.isLoadingUserIDs = false;
            },
            (error) => {
                console.log(error);
            });
        }

        $scope.getTenants = function () {
            $dgMediaFactory.getTenants(
            (result) => {
                $scope.tenants = result;
                $scope.isLoadingTenants = false;
            },
            (error) => {
                console.log(error);
            });
        }

        $scope.getChartIntervals = function () {
            $dgMediaFactory.getChartIntervals(
            (result) => {
                $scope.chartIntervals = result;
                $scope.isLoadingChartIntervals = false;
            },
            (error) => {
                console.log(error);
            });
        }

        $scope.subscribeProperties = function (propertyNames) {
            $.each(propertyNames, function () {
                $scope.$watch(this.toString(), function (newValue, oldValue) {
                    if (newValue === oldValue) return;
                    $scope.updateChartParams();
                });
            });
        }

        $scope.updateChartParams = function () {
            $scope.chartParameters = {
                SelectedActionStart: toServerDateRange($scope.selectedActionStart),
                SelectedActionTypes: $scope.selectedActionTypes,
                SelectedEarnTypes: $scope.selectedEarnTypes,
                SelectedStartDate: toServerDateRange($scope.selectedStartDate),
                SelectedIPS: $scope.selectedIPS,
                ActionUrl: $scope.actionUrl,
                SelectedUserIDs: $scope.selectedUserIDs,
                ResultChartType: $scope.selectedResultChartType,
                ResultChartInterval: $scope.selectedChartInterval
            }
        }

        $scope.getChart = function () {
            $scope.isLoadingChart = true;

            $dgMediaFactory.getChartData(
                $scope.chartParameters,
                (result) => {

                    $scope.chartData = [];

                    var jsonResult = JSON.parse(result);

                    var userIds = _.map(jsonResult, function (i) {
                        return i._id.userId;
                    });

                    var uniqueUserIds = _.uniq(userIds);

                    $.each(uniqueUserIds, function (i) {
                        var currentUserId = this;
                        var userData = _.filter(jsonResult, function (i) {
                            return i._id.userId === currentUserId;
                        });

                        var uData = _.map(userData, function (n) {

                            var result = {};

                            if ($scope.selectedChartInterval == 0)
                                result = {
                                    x: new Date(n._id.year, n._id.month - 1, n._id.day, n._id.hour),
                                    y: n.total
                                }
                            else if ($scope.selectedChartInterval == 1) {
                                result = {
                                    x: new Date(n._id.year, n._id.month - 1, n._id.day),
                                    y: n.total
                                }
                            }
                            else if ($scope.selectedChartInterval == 2) {
                                result = {
                                    x: new Date(n._id.year, n._id.month - 1),
                                    y: n.total
                                }
                            }

                            return result;
                        });


                        $scope.chartData.push({ userId: currentUserId, userData: uData });
                    });

                    bindChart();
                    $scope.isLoadingChart = false;
                },
                (error) => {
                    console.log(error);
                }
             );
        }

        function bindChart() {

            var series = [];
            var resultType = $scope.selectedResultChartType == 0 ? "# of events" : "earned nectar";

            $.each($scope.chartData, function (i) {

                var orderedUserData = _.sortBy(this.userData, 'x');

                series.push(
                    {
                        name: this.userId,
                        data: orderedUserData
                    }
                )
            });

            Highcharts.chart('chartContainer', {
                chart: {
                    type: 'spline',
                    zoomType: 'x'
                },

                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    type: 'datetime'
                },
                yAxis: {
                    title: {
                        text: resultType
                    },
                    min: 0
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' +
                            Highcharts.dateFormat('%Y-%m-%d %H:%M',
                                                  new Date(this.x))
                        + '  <br/>' + this.y;
                    }
                },
                plotOptions: {
                    spline: {
                        marker: {
                            enabled: true
                        }
                    }
                },
                credits: {
                    enabled: false
                },
                series: series
            });
        }

        function toServerDateRange(dateRange) {
            if (!dateRange || !dateRange.startDate || !dateRange.endDate) return null;

            return {
                StartDate: dateRange.startDate.toDate(),
                EndDate: dateRange.endDate.toDate()
            }
        }
    }]);

    $(document).on('click', '.panel-heading span.clickable', function (e) {
        var $this = $(this);
        if (!$this.hasClass('panel-collapsed')) {
            $this.parents('.panel').find('.panel-body').slideUp();
            $this.addClass('panel-collapsed');
            $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
            $('.panel-footer').hide();
        } else {
            $this.parents('.panel').find('.panel-body').slideDown();
            $this.removeClass('panel-collapsed');
            $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
            $('.panel-footer').show();
        }
    });
})();