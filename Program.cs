
using System.Text.Json;

/*
//For next steps I am adding in user input to find thier location - walked this back to satisfy current requirements
static async Task<GeoResponse> GeoLocation()
{
    Console.WriteLine("Enter a US Zipcode for Weather Data -");
    string ZipInput = Console.ReadLine();

    string url = $"https://geocoding-api.open-meteo.com/v1/search?name={ZipInput}&count=10&language=en&format=json&countryCode=US";
    using HttpClient client = new HttpClient();

    var response = await client.GetAsync(url);

    string content = await response.Content.ReadAsStringAsync();
    var location = JsonSerializer.Deserialize<GeoResponse>(content);

    Console.WriteLine(location);
    return location;
}
*/

static async Task<WeatherResponse> Forcast(double lat, double lon)
{
    //double latitude = LocationData.latitude;
    //double longitude = LocationData.longitude;

    string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&daily=temperature_2m_max&timezone=America%2FNew_York&temperature_unit=fahrenheit";
    using HttpClient client = new HttpClient();

    var response = await client.GetAsync(url);

    string content = await response.Content.ReadAsStringAsync();
    var weatherData = JsonSerializer.Deserialize<WeatherResponse>(content);

    if (response.IsSuccessStatusCode)
        
        return weatherData;

    else
    {
        Console.WriteLine("Error fetching weather data: " + response.StatusCode);
        return null;
    }
}


static async Task GetWeather()
{
    //currently hardcoded - expanding in next deliverable
    double GetLat = 37.9884;
    double GetLon = -85.71579;

    var weatherData = await Forcast(GetLat, GetLon);
    
    if (weatherData != null)
    {
        for (int i = 0; i < weatherData!.daily.time.Length; i++)
        {
            string date = weatherData!.daily.time[i];
            float highTemp = weatherData!.daily.temperature_2m_max[i];
            
            if (highTemp < 60)
            {
                Console.WriteLine($"On {date} the High will be: {highTemp}°F - Bring a Coat!");
            }
            else if (highTemp > 90)
            {
                Console.WriteLine($"On {date} the High will be: {highTemp}°F - Bring a water!");
            }
            else
            {
                Console.WriteLine($"On {date} the High will be: {highTemp}°F");
            }
        }
    }

}


//await GeoLocation();
await GetWeather();