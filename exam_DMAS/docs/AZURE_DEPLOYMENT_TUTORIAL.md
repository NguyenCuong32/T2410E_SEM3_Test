# Azure Function Deployment Tutorial

This document provides step-by-step instructions for deploying Azure Functions to Microsoft Azure cloud.

## Prerequisites

Before deploying, ensure you have:

1. **Azure Account**: Create a free account at [azure.com](https://azure.com)
2. **Visual Studio Code** with Azure Functions extension
3. **Node.js** (v18 or later) installed
4. **Azure Functions Core Tools** installed
5. **Azure CLI** installed

## Database Setup (Azure SQL Database)

### Step 1: Create Azure SQL Database

1. Log in to Azure Portal: https://portal.azure.com
2. Click "Create a resource" > "Databases" > "SQL Database"
3. Fill in the details:
   - **Subscription**: Select your subscription
   - **Resource Group**: Create new or select existing
   - **Database name**: BATTLEGAME
   - **Server**: Create new server with credentials
   - **Pricing tier**: Basic (or select appropriate tier)
4. Click "Review + Create" > "Create"

### Step 2: Execute Database Script

1. Open Azure Data Studio or SQL Server Management Studio
2. Connect to your Azure SQL Database
3. Run the `BATTLEGAME.sql` script from the `database` folder
4. This will create:
   - Asset table
   - Player table
   - PlayerAsset table
   - Sample data
   - Stored procedure GetAssetsByPlayer

## Azure Functions Deployment

### Option 1: Deploy from Visual Studio Code

#### Step 1: Install Required Extensions

1. Open VS Code
2. Go to Extensions (Ctrl+Shift+X)
3. Install:
   - Azure Functions
   - Azure Account

#### Step 2: Configure local.settings.json

Edit `local.settings.json` with your Azure SQL Database connection:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "node",
    "DB_SERVER": "your-server.database.windows.net",
    "DB_NAME": "BATTLEGAME",
    "DB_USER": "your-username",
    "DB_PASSWORD": "your-password"
  }
}
```

#### Step 3: Test Locally

1. Open terminal in the `azure-functions` folder
2. Run: `npm install`
3. Start the function: `npm start`
4. Test APIs:
   - POST http://localhost:7071/api/registerplayer
   - POST http://localhost:7071/api/createasset
   - GET http://localhost:7071/api/getassetsbyplayer

#### Step 4: Deploy to Azure

1. In VS Code, press F1
2. Type "Azure Functions: Deploy to Function App"
3. Select your subscription
4. Click "Create Function App in Azure"
5. Enter a unique name (e.g., `battlegame-functions`)
6. Select runtime: Node.js 18
7. Select location

#### Step 5: Configure Application Settings

1. Go to Azure Portal > Your Function App
2. Click "Configuration"
3. Add the following Application Settings:
   - `DB_SERVER`: your-server.database.windows.net
   - `DB_NAME`: BATTLEGAME
   - `DB_USER`: your-username
   - `DB_PASSWORD`: your-password
4. Save settings

### Option 2: Deploy using Azure CLI

#### Step 1: Login to Azure

```bash
az login
```

#### Step 2: Create Resource Group

```bash
az group create --name battlegame-rg --location eastus
```

#### Step 3: Create Storage Account

```bash
az storage account create --name battlegamestorage --resource-group battlegame-rg --location eastus --sku Standard_LRS
```

#### Step 4: Create Function App

```bash
az functionapp create --resource-group battlegame-rg --name battlegame-functions --storage-account battlegamestorage --consumption-plan-location eastus --runtime node --runtime-version 18
```

#### Step 5: Configure App Settings

```bash
az functionapp config appsettings set --resource-group battlegame-rg --name battlegame-functions --settings DB_SERVER="your-server.database.windows.net" DB_NAME="BATTLEGAME" DB_USER="your-username" DB_PASSWORD="your-password"
```

#### Step 6: Deploy the Function

```bash
cd azure-functions
func azure functionapp publish battlegame-functions
```

## Frontend Deployment

### Option 1: Deploy to Azure Static Web Apps

#### Step 1: Create Static Web App

1. Go to Azure Portal
2. Click "Create a resource" > "Static Web Apps"
3. Fill in details:
   - **Name**: battlegame-frontend
   - **Resource Group**: battlegame-rg
   - **Plan Type**: Free
   - **Source**: Other (or GitHub)

#### Step 2: Configure Build Settings

1. In the Static Web App settings
2. Go to "Build presets" > "React"
3. Set:
   - App location: `/frontend`
   - Output location: `build`
   - Api location: `/azure-functions`

#### Step 3: Set API URL

In `frontend/src/api.js`, update the API_BASE_URL:

```javascript
const API_BASE_URL = 'https://battlegame-functions.azurewebsites.net/api';
```

Or use environment variable:

```javascript
const API_BASE_URL = process.env.REACT_APP_API_URL || 'https://your-function-app.azurewebsites.net/api';
```

#### Step 4: Deploy

If using GitHub:
1. Push your code to GitHub
2. Connect your repository to Azure Static Web Apps
3. The deployment will happen automatically

If manual deployment:
1. Build the React app: `npm run build`
2. Use Azure CLI: `az staticwebapp browse`

### Option 2: Deploy using Visual Studio Code

1. Install "Azure Static Web Apps" extension
2. Right-click on `frontend` folder
3. Select "Open in Static Web Apps Extension"
4. Follow the prompts

## Testing Your Deployed Application

1. Get your Static Web App URL from Azure Portal
2. Open the URL in a browser
3. You should see the Player Assets table
4. Test registering a new player
5. Test creating a new asset
6. Refresh to see the data from Azure SQL Database

## API Endpoints

After deployment, your APIs will be available at:

- **Register Player**: `https://your-function-app.azurewebsites.net/api/registerplayer`
- **Create Asset**: `https://your-function-app.azurewebsites.net/api/createasset`
- **Get Assets by Player**: `https://your-function-app.azurewebsites.net/api/getassetsbyplayer`

## Troubleshooting

### Common Issues

1. **CORS Errors**: Configure CORS in Azure Function App
   - Go to your Function App > CORS
   - Add your frontend URL to allowed origins

2. **Database Connection Issues**:
   - Verify firewall rules allow Azure services
   - Check connection string in Application Settings
   - Ensure SQL Server allows Azure IPs

3. **Function Not Starting**:
   - Check logs in Azure Portal > Functions > Monitor
   - Verify all required packages are in package.json

## Clean Up

To avoid charges, delete resources when done:

```bash
az group delete --name battlegame-rg
```

---

**Note**: Make sure to replace placeholder values (your-server, your-username, your-password) with your actual Azure credentials.
