using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Webservices
{
    public class WeatherWebservice : IWeatherWebservice
    {
        public Location LookupLocation(string cityName)
        {
            var rawJson = string.Empty;

            #region JSON from api.geonames.org

            var requestUriString = String.Format("http://api.geonames.org/searchJSON?q={0}&maxRows=10&username=vivlam", cityName);
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "GET";

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            #endregion

            return JArray.Parse(rawJson).Select(l => new Location(l)).SingleOrDefault();
        }

        public IEnumerable<Forecast> GetLocationForcast(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
