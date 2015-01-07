using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public interface IWeatherService : IDisposable
    {
        Location GetLocation(string cityName);
        void RefreshForecasts(Location location);
    }
}
