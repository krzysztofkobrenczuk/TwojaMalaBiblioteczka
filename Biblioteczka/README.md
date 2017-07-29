# Biblioteczka
Web application to store your favourite books in separate bookshelves. Add bookshelves and manage with adding some books.

## Getting Started
These instruction will help you to run this project on your local machine.

### Initialize Database 
After open project in Visual Studio you need to initialize database. There is a class in Models folder called LibraryContextSeedData.cs. which is responsible for creating initial database with example data.

```
In Visual Studio open Package Manager Console then write
* Enable-Migrations
* Add-Migration Initial
* Update-Database
```

After that commands, first user with example bookshelf and book on it are created.

### Images
![IndexPage](http://i.imgur.com/VenKLV6.png)
![BookshelvesPage](http://i.imgur.com/pmfZ74d.png)
![BooksPage](http://i.imgur.com/XnGc9XW.png)
