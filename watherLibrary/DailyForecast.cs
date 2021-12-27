using System;

namespace weatherLibrary
{
    public class DailyForecast : IEquatable<DailyForecast>
    {
        private Weather dailyWeather;
        private DateTime dailyDate;

        public Weather DailyWeather { get => dailyWeather; set => dailyWeather = value; }
        public DateTime DailyDate { get => dailyDate; set => dailyDate = value; }

        public DailyForecast(DateTime dailyDate, Weather dailyWeather)
        {
            this.dailyDate = dailyDate;
            this.DailyWeather = dailyWeather;
        }
        public static bool operator >(DailyForecast left, DailyForecast right)
        {
            return (left.DailyWeather.GetTemperature() > right.DailyWeather.GetTemperature());
        }

        public static bool operator <(DailyForecast left, DailyForecast right)
        {
            return (left.DailyWeather.GetTemperature() < right.DailyWeather.GetTemperature());
        }

        public static bool operator !=(DailyForecast left, DailyForecast right)
        {
            return !(left == right);
        }

        public static bool operator ==(DailyForecast left, DailyForecast right)
        {
            if (left == null) return right == null;
            return left.DailyDate == right.dailyDate && left.DailyWeather.Equals(right.DailyWeather);
        }

        public static bool operator >(double left, DailyForecast right)
        {
            return (left > right.DailyWeather.GetTemperature());
        }
        public static bool operator <(double left, DailyForecast right)
        {
            return (left < right.DailyWeather.GetTemperature());
        }

        public override string ToString()
        {
            return DailyDate.ToString("dd/MM/yyyy HH:mm:ss") + " " + DailyWeather.ToString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(dailyDate, dailyWeather);
        }

        public override bool Equals(object obj)
        {
            if (obj is DailyForecast == false) return false;
            return this.Equals((DailyForecast)obj);
        }

        public bool Equals(DailyForecast dailyForecast)
        {
            return dailyDate == dailyForecast.DailyDate && dailyWeather.Equals(dailyForecast.DailyWeather);
        }
    }
}
