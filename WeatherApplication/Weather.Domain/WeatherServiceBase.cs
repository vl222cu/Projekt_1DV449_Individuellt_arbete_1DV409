using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public abstract class WeatherServiceBase : IWeatherService
    {
        public abstract IEnumerable<Location> GetLocation(string cityName);
        public abstract void RefreshForecasts(Location location);

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
