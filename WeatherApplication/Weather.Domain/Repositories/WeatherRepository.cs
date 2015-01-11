using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.DataModels;

namespace Weather.Domain.Repositories
{
    public class WeatherRepository : WeatherRepositoryBase
    {
        private WP13_vl222cu_WeatherEntities _context = new WP13_vl222cu_WeatherEntities();

        //Forecast
        protected override IQueryable<Forecast> QueryForecasts()
        {
            return _context.Forecasts.AsQueryable();
        }

        public override void AddForecast(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);
        }

        public override void RemoveForecast(int id)
        {
            Forecast forecast = _context.Forecasts.Find(id);
            _context.Forecasts.Remove(forecast);
        }

        //Location
        protected override IQueryable<Location> QueryLocations()
        {
            return _context.Locations.AsQueryable();
        }

        public override void AddLocation(Location location)
        {
            _context.Locations.Add(location);
        }

        public override void RemoveLocation(int id)
        {
            Location location = _context.Locations.Find(id);
            _context.Locations.Remove(location);
        }

        public override void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposible

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;

            base.Dispose(disposing);
        }

        #endregion
    }
}
