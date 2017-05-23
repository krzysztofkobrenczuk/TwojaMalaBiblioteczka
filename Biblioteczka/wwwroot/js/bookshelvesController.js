
(function () {
    "use strict";

    // Getting the existing module 
    angular.module("app-bookshelves")
    .controller("bookshelvesController", bookshelvesController);

    //call the server -> like contrustor injector
    function bookshelvesController($http) {

        var vm = this;

        vm.bookshelves = [];

        vm.newBookshelve = {};

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
                 vm.errorMessage = "Failed to save new bookshelve"
             })
            .finally(function () {
                vm.isBusy = false;
            });
        };

        vm.deleteBookshelve = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            var id = vm.bookshelves.id;

            $http.delete("/api/bookshelves/" + id)
            .then(function (response) {
                //success          
                vm.bookshelves.push(response.data)
                
            }, function () {
                //failure
                vm.errorMessage = "Failed to delete bookshelve"
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };
    
    }
})();