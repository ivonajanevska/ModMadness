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

