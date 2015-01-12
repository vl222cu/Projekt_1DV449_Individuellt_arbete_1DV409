using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public partial class Location
    {
        public Location(string id, string countryName, string cityName)
            : this()
        {
           GeonameId = id;
           CountryName = countryName;
           GeoName = cityName;
        }
    }
}
