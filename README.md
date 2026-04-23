# Mod Madness
Mod Madness is a web application designed for managing games, DLCs and mods, including compatibility tracking across different game versions and platforms.

The system is built using ASP.NET Core and follows the Onion Architecture pattern to ensure clean separation of concerns and scalability.

### Features
- Manage Games (CRUD operations)
- Manage Mods (Enable/Disable functionality)
- Manage DLCs (Install simulation)
- Platform management (PC, PlayStation, XBox..)
- Game Version tracking
- Compatibility handling between Mods/DLCs and Game Versions
- Integration with external API (RAWG)
- Authentication - User registration & login via ASP.NET Identity

#

### Architecture

The project follows the Onion Architecture:
- ModMadnessDomain → Core entities and domain models
- ModMadnessRepository → Data access layer (Entity Framework Core)
- ModMadnessService → Business logic layer
- ModMadnessWeb → Presentation layer (Controllers & UI)

#

### Technologies Used

| Technology | Purpose |
|---|---|
| ASP.NET Core (.NET 8) | Web framework |
| Entity Framework Core | ORM / data access |
| SQL Server | Database |
| ASP.NET Identity | Authentication & authorization |
| RAWG API | External game data provider |
| Razor Views + HTML/CSS/JS | Frontend UI |

#

### Domain Models

- Game
- DLC
- Mod
- Platform
- GameVersion
- MadnessUser

#

### External API Integration

The application integrates with the **[RAWG API](https://rawg.io/apidocs)** to retrieve real-world game data.
 
Key integration points:
- Fetching games and metadata from RAWG
- Transforming external data into internal domain models
- Displaying only relevant and compatible content to the user

#

## MVC Routes & Actions

### Games — `/Games`

| Method | Route | Description |
|---|---|---|
| `GET` | `/Games` | List all games |
| `GET` | `/Games/Details/{id}` | View game details |
| `GET` | `/Games/Create` | Create game form |
| `POST` | `/Games/Create` | Submit new game |
| `GET` | `/Games/Edit/{id}` | Edit game form |
| `POST` | `/Games/Edit/{id}` | Submit game edit |
| `GET` | `/Games/Delete/{id}` | Delete confirmation |
| `POST` | `/Games/Delete/{id}` | Confirm delete |
| `POST` | `/Games/Import` | Import game from RAWG API by title |

### Mods - `/Mods`

| Method | Route | Description |
|---|---|---|
| `GET` | `/Mods` | List all mods |
| `GET` | `/Mods?gameId={id}` | Filter mods by game |
| `GET` | `/Mods/Details/{id}` | View mod details |
| `GET` | `/Mods/Create` | Create mod form |
| `POST` | `/Mods/Create` | Submit new mod |
| `GET` | `/Mods/Edit/{id}` | Edit mod form |
| `POST` | `/Mods/Edit/{id}` | Submit mod edit |
| `GET` | `/Mods/Delete/{id}` | Delete confirmation |
| `POST` | `/Mods/Delete/{id}` | Confirm delete |

### DLCs - `DLCs`

| Method | Route | Description |
|---|---|---|
| `GET` | `/DLCs` | List all DLCs |
| `GET` | `/DLCs?gameId={id}` | Filter DLCs by game |
| `GET` | `/DLCs/Details/{id}` | View DLC details |
| `GET` | `/DLCs/Create` | Create DLC form |
| `POST` | `/DLCs/Create` | Submit new DLC |
| `GET` | `/DLCs/Edit/{id}` | Edit DLC form |
| `POST` | `/DLCs/Edit/{id}` | Submit DLC edit |
| `GET` | `/DLCs/Delete/{id}` | Delete confirmation |
| `POST` | `/DLCs/Delete/{id}` | Confirm delete |

### Platflorms - `/Platforms`

| Method | Route | Description |
|---|---|---|
| `GET` | `/Platforms` | List all platforms |
| `GET` | `/Platforms/Details/{id}` | View platform details |
| `GET` | `/Platforms/Create` | Create platform form |
| `POST` | `/Platforms/Create` | Submit new platform |
| `GET` | `/Platforms/Edit/{id}` | Edit platform form |
| `POST` | `/Platforms/Edit/{id}` | Submit platform edit |
| `GET` | `/Platforms/Delete/{id}` | Delete confirmation |
| `POST` | `/Platforms/Delete/{id}` | Confirm delete |

### Game Versions - `/GameVersions`

| Method | Route | Description |
|---|---|---|
| `GET` | `/GameVersions` | List all versions |
| `GET` | `/GameVersions?gameId={id}` | Filter versions by game |
| `GET` | `/GameVersions/Details/{id}` | View version details |
| `GET` | `/GameVersions/Create` | Create version form |
| `POST` | `/GameVersions/Create` | Submit new version |
| `GET` | `/GameVersions/Edit/{id}` | Edit version form |
| `POST` | `/GameVersions/Edit/{id}` | Submit version edit |
| `GET` | `/GameVersions/Delete/{id}` | Delete confirmation |
| `POST` | `/GameVersions/Delete/{id}` | Confirm delete |
