
(function () {
    "use strict";
    angular.module("app-bookshelves")
        .controller("bookshelveEditorController", bookshelveEditorController);

    function bookshelveEditorController($routeParams, $scope, $route, $http) {

        $scope.reloadRoute = function () {
            $route.reload();
        }

        var vm = this;
        vm.bookshelveName = $routeParams.bookshelveName;
        vm.newBook = {};
        vm.books = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        var url = "/api/bookshelves/" + vm.bookshelveName + "/books";

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.books);
            }, function (err) {
                //failure
                vm.errorMessage = "Failed to load books";
            })
            .finally(function () {
                vm.isBusy = false;
            });


        vm.addBook = function () {

            //Add to server
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post(url, vm.newBook)
             .then(function (response) {
                 //success
                 vm.books.push(response.data);
                 vm.newBook = {};
             }, function (err) {
                 //failure
                 vm.errorMessage = "Failed to save new book"
             })
            .finally(function () {
                vm.isBusy = false;
            });
        };

        vm.deleteBook = function (Id) {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.delete("/api/bookshelves/" + vm.bookshelveName + "/books/" + Id)
              .then(function (response) {
                  //success                  
                  vm.books.status = "Deleted";
                  $scope.reloadRoute();

              }, function () {
                  //failure
                  vm.errorMessage = "Failed to delete bookshelve";
              })
               .finally(function () {
                   vm.isBusy = false;


               });

        };




        
    }

})();