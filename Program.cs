
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
    {
        for (int i = 0; i < weatherData!.daily.time.Length; i++)
        {
            string date = weatherData!.daily.time[i];
            float highTemp = weatherData!.daily.temperature_2m_max[i];
            Console.WriteLine($"Date: {date} High: {highTemp}°F");
        }
    }

    else
    {
        Console.WriteLine("Error fetching weather data: " + response.StatusCode);
    }
}