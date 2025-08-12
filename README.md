RESTful API developed in C# for a video-sharing platform, allowing users to create and manage their favorite videos.

### Features

- **Video Management:**
    - Creation of playlists with video title, URL, and description.
    - Addition and removal of videos using ID.
    - Listing of all videos.
    - Search for a video by ID.
- **Video Management:**
    - Addition of videos to the platform through links (URLs).
    - Listing of videos in a playlist.

### Technologies Used

- **Programming Language:** C#
- **Framework:** .NET 8
- **Database:** SQL Server
- **ORM:** Entity Framework
- **Tools:**
    - Visual Studio
    - Git

### How to Run the Project

1.  **Prerequisites:**
    - Have the .NET 8.0 SDK installed.
2.  **Clone the Repository:**
    ```bash
    git clone https://github.com/Matchiga/ChigaFlix.git
    ```
3.  **Configure the Database:**
    - Update the database connection string in the `appsettings.json` file.
4.  **Run Migrations (if applicable):**
    - Open the Package Manager Console and navigate to the project folder.
    - Run the `Update-Database` command to apply the migrations.
5.  **Start the Application:**
    - Run the `dotnet run` command in the project folder.

### API Routes

**Playlists:**

- `GET /Videos`: Returns all playlists of the authenticated user.
- `GET /Videos/{id}`: Returns a specific playlist by ID.
- `POST /Videos`: Creates a new playlist.
- `PUT /Videos`: Updates an existing playlist.
- `DELETE /Videos/{id}`: Deletes a playlist.

### Author
