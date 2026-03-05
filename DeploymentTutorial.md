# Tutorial: Deploying Azure Functions to the Azure Cloud

This tutorial will guide you through the process of deploying a .NET Azure Functions project to Microsoft Azure using two common methods: Visual Studio / Visual Studio Code and the Azure CLI.

## Prerequisites
- An active Microsoft Azure account.
- The project is built and running locally.
- **[Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)** installed on your machine.
- **[Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)** installed.

---

## Method 1: Deploy using Visual Studio Code

1. **Install Extensions**: Open VS Code and install the `Azure Resources` and `Azure Functions` extensions.
2. **Sign In**: Click on the Azure icon in the left activity bar. Click **Sign in to Azure...** and follow the browser prompts.
3. **Deploy to Function App**:
   - In the Azure pane under the *Workspace* section, click the **Deploy** button (cloud icon with an up arrow).
   - Select **Deploy to Function App**.
4. **Create New Function App**:
   - Select **+ Create new Function App in Azure (Advanced)**.
   - Enter a unique name for your Function App (e.g., `battlegame-api-2026`).
   - Select the runtime stack: **.NET 10 (Isolated)**.
   - Select your preferred OS (Windows or Linux).
   - Choose a Resource Group and a Region near you.
   - Select a hosting plan (Consumption is usually best for testing).
   - Choose or create a new Application Insights resource for monitoring (optional).
5. **Wait and Verify**: The deployment will begin. Once finished, you will see a notification in VS Code with the function URL.

---

## Method 2: Deploy using Azure CLI and Azure Functions Core Tools

1. **Login to Azure**:
   Open a terminal and run:
   ```bash
   az login
   ```
2. **Create a Resource Group**:
   ```bash
   az group create --name BattleGameResourceGroup --location eastus
   ```
3. **Create a Storage Account** (Required for Azure Functions):
   ```bash
   az storage account create --name battlegamestorage2024 --location eastus --resource-group BattleGameResourceGroup --sku Standard_LRS
   ```
4. **Create the Function App**:
   ```bash
   az functionapp create --resource-group BattleGameResourceGroup --consumption-plan-location eastus --runtime dotnet-isolated --functions-version 4 --name battlegame-api-2024 --storage-account battlegamestorage2024
   ```
5. **Publish the Project**:
   In the terminal, navigate to your Azure Function project folder (`c:\project\new\T2410E_SEM3_Test`) and run:
   ```bash
   func azure functionapp publish battlegame-api-2024
   ```

---

## Post-Deployment Setup (Important)

Your application uses a SQL Database connection string. You must configure this in Azure so the deployed Function App can connect to your database. Note: Make sure your Azure SQL Server firewall allows connections from Azure services.

1. In the Azure Portal, go to your **Function App**.
2. Click on **Environment variables** (under Settings).
3. Under the **App settings** tab, click **+ Add**.
4. Set the **Name** to `SqlConnectionString` and the **Value** to your production Azure SQL connection string.
5. Save and apply the changes.

Your APIs are now live and accessible over the internet! Update your frontend `index.html` URL to point to `https://<YOUR_APP_NAME>.azurewebsites.net/api/getassetsbyplayer`.
