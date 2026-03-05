1 Tạo Database
CREATE DATABASE BATTLEGAME;
GO
USE BATTLEGAME;
2⃣ Tạo bảng Player
CREATE TABLE Player (
    PlayerId INT IDENTITY(1,1) PRIMARY KEY,
    PlayerName NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(200),
    Age INT,
    CurrentLevel INT,
    CreatedDate DATETIME DEFAULT GETDATE()
);
3️Tạo bảng Asset
CREATE TABLE Asset (
    AssetId INT IDENTITY(1,1) PRIMARY KEY,
    AssetName NVARCHAR(200) NOT NULL,
    AssetType NVARCHAR(100),
    Description NVARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE()
);
4️Tạo bảng PlayerAsset
CREATE TABLE PlayerAsset (
    PlayerAssetId INT IDENTITY(1,1) PRIMARY KEY,
    PlayerId INT FOREIGN KEY REFERENCES Player(PlayerId),
    AssetId INT FOREIGN KEY REFERENCES Asset(AssetId)
);
II. Tạo Azure Function Project
Bước 1: Tạo Project
func init BattleGameFunction --dotnet
cd BattleGameFunction
func new

Chọn:

HTTP Trigger

.NET 8 (hoặc .NET 6)

Authorization level: Anonymous

Cài Nuget:

dotnet add package Microsoft.Data.SqlClient
III. API 1 – registerplayer 
File: RegisterPlayer.cs
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

public class RegisterPlayer
{
    private readonly string _connectionString = 
        Environment.GetEnvironmentVariable("SqlConnectionString");

    [Function("registerplayer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonSerializer.Deserialize<PlayerModel>(requestBody);

        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();

        string query = @"INSERT INTO Player 
                        (PlayerName, FullName, Age, CurrentLevel) 
                        VALUES (@PlayerName, @FullName, @Age, @CurrentLevel)";

        using SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@PlayerName", data.PlayerName);
        cmd.Parameters.AddWithValue("@FullName", data.FullName);
        cmd.Parameters.AddWithValue("@Age", data.Age);
        cmd.Parameters.AddWithValue("@CurrentLevel", data.CurrentLevel);

        cmd.ExecuteNonQuery();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Player registered successfully");
        return response;
    }
}

public class PlayerModel
{
    public string PlayerName { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public int CurrentLevel { get; set; }
}
IV. API 2 – createasset
File: CreateAsset.cs
public class CreateAsset
{
    private readonly string _connectionString = 
        Environment.GetEnvironmentVariable("SqlConnectionString");

    [Function("createasset")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonSerializer.Deserialize<AssetModel>(requestBody);

        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();

        string query = @"INSERT INTO Asset 
                        (AssetName, AssetType, Description) 
                        VALUES (@AssetName, @AssetType, @Description)";

        using SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@AssetName", data.AssetName);
        cmd.Parameters.AddWithValue("@AssetType", data.AssetType);
        cmd.Parameters.AddWithValue("@Description", data.Description);

        cmd.ExecuteNonQuery();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Asset created successfully");
        return response;
    }
}

public class AssetModel
{
    public string AssetName { get; set; }
    public string AssetType { get; set; }
    public string Description { get; set; }
}
V. API 3 – getassetsbyplayer 
File: GetAssetsByPlayer.cs
public class GetAssetsByPlayer
{
    private readonly string _connectionString =
        Environment.GetEnvironmentVariable("SqlConnectionString");

    [Function("getassetsbyplayer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();

        string query = @"
        SELECT 
            ROW_NUMBER() OVER (ORDER BY p.PlayerName) AS No,
            p.PlayerName,
            p.CurrentLevel,
            p.Age,
            a.AssetName
        FROM Player p
        JOIN PlayerAsset pa ON p.PlayerId = pa.PlayerId
        JOIN Asset a ON pa.AssetId = a.AssetId";

        using SqlCommand cmd = new SqlCommand(query, conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        List<object> result = new List<object>();

        while (reader.Read())
        {
            result.Add(new
            {
                No = reader["No"],
                PlayerName = reader["PlayerName"],
                Level = reader["CurrentLevel"],
                Age = reader["Age"],
                AssetName = reader["AssetName"]
            });
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);
        return response;
    }
}
VI. Frontend – ReactJS 
1️Tạo project
npx create-react-app battlegame-ui
cd battlegame-ui
npm install axios
2️File App.js
import React, { useEffect, useState } from "react";
import axios from "axios";

function App() {
  const [data, setData] = useState([]);

  useEffect(() => {
    axios.get("https://yourfunctionapp.azurewebsites.net/api/getassetsbyplayer")
      .then(res => setData(res.data))
      .catch(err => console.log(err));
  }, []);

  return (
    <div>
      <h2>Assets By Player</h2>
      <table border="1">
        <thead>
          <tr>
            <th>No</th>
            <th>Player Name</th>
            <th>Level</th>
            <th>Age</th>
            <th>Asset Name</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => (
            <tr key={index}>
              <td>{item.No}</td>
              <td>{item.PlayerName}</td>
              <td>{item.Level}</td>
              <td>{item.Age}</td>
              <td>{item.AssetName}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;