using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class PlayerAsset
{
    public string PlayerName { get; set; }
    public int Level { get; set; }
    public int Age { get; set; }
    public string AssetName { get; set; }
}

public class PlayerAssetsTable
{
    private List<PlayerAsset> data = new List<PlayerAsset>();
    private static readonly HttpClient client = new HttpClient();

    public async Task LoadDataAsync()
    {
        try
        {
            var response = await client.GetAsync("http://localhost:7071/api/getassetsbyplayer");
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                // Parse JSON and populate data list
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading assets: {ex.Message}");
        }
    }

    public List<PlayerAsset> GetData()
    {
        return data;
    }
}