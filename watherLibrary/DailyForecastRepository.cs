using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace weatherLibrary
{
    public class DailyForecastRepository : IEnumerable<DailyForecast>
    {
        private List<DailyForecast> forecasts;

        public DailyForecastRepository() { forecasts = new List<DailyForecast>(); }
        
        public DailyForecastRepository(DailyForecastRepository forecastsRepository)
        {
            forecasts = new List<DailyForecast>();
            foreach(DailyForecast dailyForecast in forecastsRepository)
            {
                this.Add(dailyForecast);
            }
        }

        public void Add(DailyForecast newForecast) //ubacuje u listu tako da ona ostane sortirana, svako dodavanje se svodi na ovu funkciju 
        {
            if (forecasts.Count > 0)
            {
                foreach (DailyForecast dailyForecast in forecasts)//provjerava postoji li prognoza za isti datum ako postoji brise ju 
                {
                    if (dailyForecast.DailyDate.Date == newForecast.DailyDate.Date)
                    {
                        this.Remove(dailyForecast.DailyDate);
                        break;
                    }
                }
            }
            if (forecasts.Count == 0)//ubacivanje nove prognoze na odgovarajuce mjesto
                forecasts.Add(newForecast);
            else
            {
                int index = 0;
                while (index < forecasts.Count && forecasts[index].DailyDate < newForecast.DailyDate)
                {
                    index++;
                }
                if (index == forecasts.Count)
                    forecasts.Add(newForecast);
                else
                    forecasts.Insert(index, newForecast);
            }
        }

        public void Add(DateTime date, WeatherGenerator weatherGenerator)
        {
            this.Add(new DailyForecast(date, weatherGenerator.Generate()));
        }

        public void Add(List<DailyForecast> newForecasts)
        {
            foreach(DailyForecast dailyForecast in newForecasts)
            {
                this.Add(dailyForecast);
            }
        }

        public void Remove(DateTime date)
        {
            foreach (DailyForecast dailyForecast in forecasts)
            {
                if (dailyForecast.DailyDate.Date == date.Date)
                {
                    forecasts.Remove(dailyForecast);                  
                    return;
                }
            }
            throw new NoSuchDailyWeatherException("No daily forecast for " + date.Date);
        }

        public IEnumerator<DailyForecast> GetEnumerator()
        {
            return forecasts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(DailyForecast dailyForecast in forecasts)
            {
                stringBuilder.AppendLine(dailyForecast.ToString());
            }
            return stringBuilder.ToString();
        }

    }
}
