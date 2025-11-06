
using System.Text.Json;

Console.WriteLine("Fetching weather data for the next 7 days - Louisville, KY:");
await Forcast();

static async Task Forcast()
{
    string url = "https://api.open-meteo.com/v1/forecast?latitude=38.2542&longitude=-85.7594&daily=temperature_2m_max&timezone=America%2FNew_York&temperature_unit=fahrenheit";
    using HttpClient client = new HttpClient();

    var response = await client.GetAsync(url);

    var content = await response.Content.ReadAsStringAsync();
    var weatherData = JsonSerializer.Deserialize<WeatherResponse>(content);

    if (response.IsSuccessStatusCode)
    // This was originally a for loop that printed both the date and the high, but I changed it to adhere better to the assignment
    {
        //for (int i = 0; i < weatherData!.daily.time.Length; i++)
        foreach (float highTemp in weatherData!.daily.temperature_2m_max)
        {
            //string date = weatherData!.daily.time[i];
            //float highTemp = weatherData!.daily.temperature_2m_max[i];
            Console.WriteLine($"High: {highTemp}°F");
        }
    }

    else
    {
        Console.WriteLine("Error fetching weather data: " + response.StatusCode);
    }
}