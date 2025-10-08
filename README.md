# Book Catalog Microservice (Minimal API)

This project is a simple cloud-native REST microservice built with ASP.NET Core Minimal APIs.  
It manages a catalog of books with full CRUD operations over an in-memory list.  
Endpoints include GET, POST, PUT, and DELETE with proper status codes (200, 201, 204, 404, 400).  

## How to Run
1. Open the folder in VS Code.
2. Run the API with:
   ```bash
   dotnet run

## How To Test
Use test.http with the REST Client extension in VS Code.
Click "Send Request" above each section to verify all endpoints.


---

## 🧪 test.http


Contents:

```http
### Get all books
GET http://localhost:5189/api/books
Accept: application/json

###

### Get one book by ID
GET http://localhost:5189/api/books/1
Accept: application/json

###

### Create a new book
POST http://localhost:5189/api/books
Content-Type: application/json

{
  "title": "Refactoring",
  "author": "Martin Fowler",
  "price": 50.00,
  "stock": 7
}

###

### Update an existing book
PUT http://localhost:5189/api/books/1
Content-Type: application/json

{
  "title": "Clean Code (Updated Edition)",
  "author": "Robert C. Martin",
  "price": 37.50,
  "stock": 15
}

###

### Delete a book
DELETE http://localhost:5189/api/books/2

###

### Try to get a deleted or non-existent book (should return 404)
GET http://localhost:5189/api/books/99
Accept: application/json

```

### Screenshots
<img width="1899" height="1139" alt="image" src="https://github.com/user-attachments/assets/3f971b4e-ea6b-4c41-8bdd-fb7657f747ec" />
<img width="568" height="427" alt="image" src="https://github.com/user-attachments/assets/c8c87c13-fb71-426e-a1ec-97a35cc6bae7" />



