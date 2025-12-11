public record DailyData(string[] time, float[] temperature_2m_max);
public record WeatherResponse(DailyData daily);
public record LocationData(string name, float latitude, float longitude);
public record GeoResponse(LocationData[] results);