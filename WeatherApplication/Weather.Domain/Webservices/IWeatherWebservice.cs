using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Webservices
{
    interface IWeatherWebservice
    {
        IEnumerable<Forecast> GetLocationForcast(Location location);
        Location LookupLocation(string locationName);
    }
}
