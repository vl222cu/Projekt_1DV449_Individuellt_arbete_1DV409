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
        public Location(JToken locationToken)
            : this()
        {
           GeonameId = locationToken.Value<string>("geonameId");
           CountryName = locationToken.Value<string>("countryName");
           GeoName = locationToken.Value<string>("name");
        }
    }
}
