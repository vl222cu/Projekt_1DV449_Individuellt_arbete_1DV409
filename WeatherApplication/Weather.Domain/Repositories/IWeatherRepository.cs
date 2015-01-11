using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public interface IWeatherRepository : IDisposable
    {
        IEnumerable<Forecast> FindAllForecast();
        Forecast FindForecastById(int id);
        void AddForecast(Forecast forecast);
        void RemoveForecast(int id);

        IEnumerable<Location> FindAllLocation();
        Location FindLocationById(int id);
        Location FindLocationByCityName(string cityName);
        void AddLocation(Location location);
        void RemoveLocation(int id);

        void Save();
    }
}
