public record DailyData(string[] time, float[] temperature_2m_max);
public record WeatherResponse(DailyData daily);
public record LocationData(double latitude, double longitude);
public record GeoResponse(LocationData[] results);