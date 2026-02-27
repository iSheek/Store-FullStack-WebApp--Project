# Store-FullStack-WebApp--Project
repo containing fullstack web application for store (solo project for learning purposes)

## Features
* **Some of CRUD operations**: Add, view, and delete products.
* **Image Uploading**: Upload physical image files that are saved to the server and linked to database records.
* **Relational Data**: Connected entities (Products and Categories).
* **Automated Database Setup**: Automatic migrations and database seeding on startup.

## Tech Stack
* **Frontend**: Blazor WebAssembly, HTML/CSS
* **Backend**: .NET 10, Minimal APIs
* **Database**: Postgresql Server (via Docker), Entity Framework Core

## Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/)
* [Docker](https://www.docker.com/)

## How to run the project locally

1. Clone this repository and open a terminal in the folder containing the `.slnx` file.
2. Start the database using Docker: 
  docker-compose up -d
3. Navigate to the API folder and run the backend: 
  cd ./Store.API
  dotnet run
4. Open a new terminal window (again from the folder with .slnx), navigate to the Frontend, and run it:
  cd ./Store.Frontend
  dotnet run
5. Check the terminal for the frontend port (e.g., http://localhost:xxxx) and paste it into your browser.
