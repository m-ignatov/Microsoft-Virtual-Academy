using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingWithValueConverters
{
  public class DayForecast
  {
    public DateTime Date { get; set; }
    public Weather Weather { get; set; }
    public ObservableCollection<TimeForecast> HourlyForecast { get; set; }
  }

  public class TimeForecast
  {
    public int Hour { get; set; }
    public int Temperature { get; set; }
  }

  public enum Weather
  {
    Sunny,
    Cloudy,
    Rainy,
    Snowy
  }

  public class FiveDayForecast
  {
    public static ObservableCollection<DayForecast> GetForecast()
    {
      var forecast = new ObservableCollection<DayForecast>();

      forecast.Add(new DayForecast()
      {
        Date = new DateTime(2015, 12, 7),
        Weather = Weather.Rainy,
        HourlyForecast = generateRandomTimeForecast(7)
      });

      forecast.Add(new DayForecast()
      {
        Date = new DateTime(2015, 12, 8),
        Weather = Weather.Cloudy,
        HourlyForecast = generateRandomTimeForecast(8)
      });

      forecast.Add(new DayForecast()
      {
        Date = new DateTime(2015, 12, 9),
        Weather = Weather.Sunny,
        HourlyForecast = generateRandomTimeForecast(9)
      });

      forecast.Add(new DayForecast()
      {
        Date = new DateTime(2015, 12, 10),
        Weather = Weather.Snowy,
        HourlyForecast = generateRandomTimeForecast(10)
      });

      return forecast;
    }

    private static ObservableCollection<TimeForecast> generateRandomTimeForecast(int seed)
    {
      var random = new System.Random(seed);
      var forecast = new ObservableCollection<TimeForecast>();

      for (int i = 0; i < 24; i++)
      {
        var timeForecast = new TimeForecast();
        timeForecast.Hour = i;
        timeForecast.Temperature = random.Next(105);
        forecast.Add(timeForecast);
      }
      return forecast;
    }
  }
}
