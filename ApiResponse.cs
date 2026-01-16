public record DailyData(string[] time, float[] temperature_2m_max, float[] precipitation_probability_mean);
public record WeatherResponse(DailyData daily);
public record LocationData(string name, float latitude, float longitude);
public record GeoResponse(LocationData[] results);