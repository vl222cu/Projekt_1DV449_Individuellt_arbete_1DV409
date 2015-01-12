using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Webservices
{
    public interface IWeatherWebservice
    {
        IEnumerable<Forecast> GetLocationForcast(Location location);
        IEnumerable<Location> LookupLocation(string locationName);
    }
}
