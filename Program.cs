
using System.Text.Json;


static async Task<GeoResponse?> GeoLocation()
{
    try
    {
    Console.WriteLine("Enter a 5 digit US Zipcode for Weather Data -");
    // This 'Null Coalescing Operator' was the only way I could satisfy the compiler warming - it was an AI assisted suggestion, unfortunately.
    string ZipInput = Console.ReadLine() ?? "";
    int.Parse(ZipInput);

    string url = $"https://geocoding-api.open-meteo.com/v1/search?name={ZipInput}&count=10&language=en&format=json&countryCode=US";
    using HttpClient client = new HttpClient();

    var response = await client.GetAsync(url);

    string content = await response.Content.ReadAsStringAsync();
    var location = JsonSerializer.Deserialize<GeoResponse>(content);

    
    return location;
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Zipcode Format. Please enter a 5 digit US Zipcode.");
        return null;
    }
}


static async Task<WeatherResponse?> Forcast(float lat, float lon)
{

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
    var GetLocation = await GeoLocation();

    if (GetLocation == null)
    {
        Console.WriteLine("Unable to retrieve location data, oops! Exiting...");
        return;
    }

    string LocationName = GetLocation.results[0].name;
    Console.WriteLine($"Weather Forecast for {LocationName}:");

    float GetLat = GetLocation.results[0].latitude;
    float GetLon = GetLocation.results[0].longitude;

    var weatherData = await Forcast(GetLat, GetLon);
    
    if (weatherData != null)
    {
        for (int i = 0; i < weatherData!.daily.time.Length; i++)
        {
            string date = weatherData.daily.time[i];
            float highTemp = weatherData.daily.temperature_2m_max[i];
            
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

await GetWeather();