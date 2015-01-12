using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {
        // Metod som returnerar frågeobjekt för Forecast
        protected abstract IQueryable<Forecast> QueryForecasts();

        public IEnumerable<Forecast> FindAllForecast()
        {
            return QueryForecasts().ToList();
        }

        public Forecast FindForecastById(int id)
        {
            return QueryForecasts().SingleOrDefault(f => f.ForecastId == id);
        }

        public abstract void AddForecast(Forecast forecast);
        public abstract void RemoveForecast(int id);

        // Metod som returnerar frågeobjekt för Location
        protected abstract IQueryable<Location> QueryLocations();

        public IEnumerable<Location> FindAllLocation()
        {
            return QueryLocations().ToList();
        }

        public Location FindLocationById(int id)
        {
            return QueryLocations().SingleOrDefault(l => l.LocationId == id);
        }

        public IEnumerable<Location> FindLocationByCityName(string cityName)
        {
            return QueryLocations().Where(l => l.GeoName == cityName);
        }

        public abstract void AddLocation(Location location);
        public abstract void RemoveLocation(int id);

        public abstract void Save();

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
