public record DailyData(string[] time, 
    float[] temperature_2m_max, 
    float[] precipitation_probability_mean, 
    WeatherType[] weather_code);
public record WeatherResponse(DailyData daily);
public record LocationData(string name, float latitude, float longitude);
public record GeoResponse(LocationData[] results);
public enum WeatherType
{
    ClearSky = 0,
    MainlyClear = 1,
    PartlyCloudy = 2,
    Overcast = 3,
    
    Fog = 45,
    DepositingRimeFog = 48,
    
    DrizzleLight = 51,
    DrizzleModerate = 53,
    DrizzleDense = 55,
    
    RainSlight = 61,
    RainModerate = 63,
    RainHeavy = 65,
    
    SnowSlight = 71,
    SnowModerate = 73,
    SnowHeavy = 75,
    
    Thunderstorm = 95,
    ThunderstormHeavy = 99
}