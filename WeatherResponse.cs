public record DailyData(string[] time, float[] temperature_2m_max);
public record WeatherResponse(DailyData daily);