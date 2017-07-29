(function () {
    "use strict";
    //Creating module for us / second parameter
    angular.module("app-bookshelves", ["simpleControls", "ngRoute"])

      .config(function ($routeProvider) {

          $routeProvider.when("/", {
              controller: "bookshelvesController",
              controllerAs: "vm",
              templateUrl: "/views/bookshelvesView.html"
          });

          $routeProvider.when("/editor/:bookshelveName", {
              controller: "bookshelveEditorController",
              controllerAs: "vm",
              templateUrl: "/views/bookshelveEditorView.html"
          });

          $routeProvider.otherwise({ redirectTo: "/" });
          
      });

   


})();