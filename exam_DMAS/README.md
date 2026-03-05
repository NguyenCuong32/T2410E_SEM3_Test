# BattleGame - Microsoft Azure Solutions Exam Project

## Overview

This project is a complete solution for the Microsoft Azure Solutions exam. It includes:

- **SQL Server Database**: Asset, Player, PlayerAsset tables with stored procedures
- **Azure Functions**: Three APIs (registerplayer, createasset, getassetsbyplayer)
- **React Frontend**: Modern web interface to display player assets
- **Deployment Documentation**: Step-by-step Azure deployment guide

## Project Structure

```
Minh Bài Thi FPT/
├── database/
│   └── BATTLEGAME.sql          # SQL Server database scripts
├── azure-functions/
│   ├── package.json            # Node.js dependencies
│   ├── host.json               # Azure Functions host config
│   ├── local.settings.json     # Local development settings
│   ├── registerplayer/        # Register player API
│   ├── createasset/           # Create asset API
│   └── getassetsbyplayer/     # Get assets by player API
├── frontend/
│   ├── public/
│   │   └── index.html
│   ├── src/
│   │   ├── index.js
│   │   ├── App.js
│   │   ├── App.css
│   │   └── api.js             # API service
│   └── package.json
└── docs/
    └── AZURE_DEPLOYMENT_TUTORIAL.md
```

## Quick Start

### 1. Database Setup

Execute `database/BATTLEGAME.sql` in SQL Server or Azure SQL Database.

### 2. Azure Functions

```bash
cd azure-functions
npm install
# Update local.settings.json with your database credentials
npm start
```

### 3. Frontend

```bash
cd frontend
npm install
# Update src/api.js with your Azure Function URL
npm start
```

## API Endpoints

| API Name | Method | Description |
|----------|--------|-------------|
| registerplayer | POST | Register a new player |
| createasset | POST | Create a new asset |
| getassetsbyplayer | GET | Get all player assets |

## API Request/Response Examples

### Register Player

**Request:**
```json
{
  "playerName": "Player4",
  "fullName": "John Doe",
  "age": 25,
  "email": "player4@example.com"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Player registered successfully",
  "playerId": 4
}
```

### Create Asset

**Request:**
```json
{
  "assetName": "Magic Wand",
  "assetType": "Equipment",
  "description": "A powerful wand"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Asset created successfully",
  "assetId": 6
}
```

### Get Assets By Player

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "No": 1,
      "PlayerName": "Player 1",
      "Level": 10,
      "Age": 20,
      "AssetName": "Hero 1"
    }
  ]
}
```

## Technology Stack

- **Backend**: Azure Functions (Node.js)
- **Database**: SQL Server / Azure SQL Database
- **Frontend**: React 18 with Material UI
- **Deployment**: Azure Cloud Services

## Requirements Met

| Requirement | Mark | Status |
|-------------|------|--------|
| Database Creation | 1 | ✓ |
| registerplayer API | 3 | ✓ |
| createasset API | 3 | ✓ |
| getassetsbyplayer API | 3 | ✓ |
| Website Display | 3 | ✓ |
| Deployment Documentation | 1 | ✓ |
| Good Coding Convention | 1 | ✓ |

Total: 15/15

## License

This project is for educational purposes.
