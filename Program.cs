using System.Text.Json;

WeatherAPI weatherCall = new WeatherAPI();

Console.WriteLine("Enter a 5 digit US Zipcode for Weather Data -");
string zipInput = Console.ReadLine() ?? "";
int.Parse(zipInput);

var location = await weatherCall.GetLocationAsync(zipInput);

if (location != null && location.results != null)
{
    float lat = location.results[0].latitude;
    float lon = location.results[0].longitude;

    var weatherData = await weatherCall.GetForecastAsync(lat, lon);
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
