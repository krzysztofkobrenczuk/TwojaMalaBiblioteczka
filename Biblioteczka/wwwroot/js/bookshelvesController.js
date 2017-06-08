
(function () {
    "use strict";

    // Getting the existing module 
    angular.module("app-bookshelves")
    .controller("bookshelvesController", bookshelvesController);

    //call the server -> like contrustor injector
    function bookshelvesController($scope, $route, $http) {


        $scope.reloadRoute = function () {
            $route.reload();
        }

        var vm = this;

        vm.bookshelves = [];

        vm.newBookshelve = {};

        vm.bookShelveId = {};

        vm.errorMessage = "";

        vm.isBusy = true;

        //call to the server
        $http.get("/api/bookshelves")
           .then(function (response) {
               //Success
               angular.copy(response.data, vm.bookshelves);

           }, function (error) {
               //Failure
               vm.errorMessage = "Failed to load data: " + error;
           })
            .finally(function () {
                vm.isBusy = false;
            });


        vm.addBookshelve = function () {
            // add on page not on server
            //vm.bookshelves.push({ name: vm.newBookshelve.name, created: new Date() });
            //vm.newBookshelve = {};

            //Add to server
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/bookshelves", vm.newBookshelve)
             .then(function (response) {
                 //success
                 vm.bookshelves.push(response.data);
                 vm.newBookshelve = {};
             }, function () {
                 //failure
                 vm.errorMessage = "Failed to save new bookshelve";
             })
            .finally(function () {
                vm.isBusy = false;
            });
        };

        vm.deleteBookshelves = function (Id) {
            vm.isBusy = true;
            vm.errorMessage = "";


            $http.delete("/api/bookshelves/" + Id)
                .then(function (response) {
                    //success                  
                    vm.bookshelves.status = "Deleted";
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