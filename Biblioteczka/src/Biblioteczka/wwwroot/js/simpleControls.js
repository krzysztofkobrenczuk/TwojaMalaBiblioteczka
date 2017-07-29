(function () {

    "use strict";

    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                 show: "=displaywhen"
            },
            restrict: "E",
          templateUrl: "/views/waitCursor.html"
        };
    }
})();