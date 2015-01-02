using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    interface IWeatherRepository : IDisposable
    {
        IEnumerable<Forecast> FindAllForecast();
        Forecast FindForecastById(int id);
        void AddForecast(Forecast forecast);
        void DeleteForecast(Forecast forecast);

        IEnumerable<Location> FindAllLocation();
        Location FindLocationById(int id);
        Location FindLocationByCityName(string cityName);
        void AddLocation(Location location);
        void DeleteLocation(Location location);

        void Save();
    }
}
