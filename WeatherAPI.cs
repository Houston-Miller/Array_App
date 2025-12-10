using System.Text.Json;

public class WeatherAPI
{
    public async Task<GeoResponse?> GetLocationAsync(string zipcode)
    {
        string url = $"https://geocoding-api.open-meteo.com/v1/search?name={zipcode}&count=10&language=en&format=json&countryCode=US";

        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);

        string content = await response.Content.ReadAsStringAsync();
        var location = JsonSerializer.Deserialize<GeoResponse>(content);

        return location;
    }

    public async Task<WeatherResponse?> GetForecastAsync(float lat, float lon)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&daily=temperature_2m_max&timezone=America%2FNew_York&temperature_unit=fahrenheit";
        using HttpClient client = new HttpClient();

        var response = await client.GetAsync(url);

        string content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WeatherResponse>(content);

        if (response.IsSuccessStatusCode)
        {
            return weatherData;
        }
        else
        {
            Console.WriteLine("Error fetching weather data: " + response.StatusCode);
            return null;
        }
    }
}