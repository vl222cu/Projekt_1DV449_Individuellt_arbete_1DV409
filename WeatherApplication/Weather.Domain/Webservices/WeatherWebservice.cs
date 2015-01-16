using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Weather.Domain.Webservices
{
    public class WeatherWebservice : IWeatherWebservice
    {
        // Hämtning av data för plats via geonames API
        public IEnumerable<Location> LookupLocation(string cityName)
        {
           var rawJson = string.Empty;

            #region JSON från api.geonames.org

           var requestUriString = new Uri(String.Format("http://api.geonames.org/searchJSON?name={0}&username=vl222cu&featureClass=P", cityName));
           var request = (HttpWebRequest)WebRequest.Create(requestUriString);
           request.Method = "GET";

           using (var response = request.GetResponse())
           using (var reader = new StreamReader(response.GetResponseStream()))
           {
                rawJson = reader.ReadToEnd();
           }

           #endregion

           var cityList = new List<Location>();
           var obj = JObject.Parse(rawJson);
           
           foreach (var child in obj["geonames"])
           {
               string id = (string)child["geonameId"];
               string countryName = (string)child["countryName"];
               string countyName = (string)child["adminName1"];
               string city = (string)child["name"];

               cityList.Add(new Location(id, countryName, countyName, city));
           }

           return cityList;
        }

        // Hämtning av data för vädret på vald plats via yr.no API
        public IEnumerable<Forecast> GetLocationForcast(Location location)
        {
            XDocument xDoc;

            #region XML från api.yr.no

            var requestUriString = String.Format("http://www.yr.no/place/{0}/{1}/{2}/forecast.xml", location.CountryName, location.AdminName1, location.GeoName);

            var request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "GET";

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                xDoc = XDocument.Load(reader);
            }

            #endregion

            var data = (from time in xDoc.Descendants("time")
                        where Int32.Parse(time.Attribute("period").Value) >= 2 // Periodens värde väljs ut
                        group time by DateTime.Parse(time.Attribute("to").Value).Date into g // Perioderna grupperas... 
                        select (from t in g select t).First()).Take(5).Select(item => // ...och första väljs sedan ut i varje par
                        new Forecast
                        {
                            SymbolNumber = item.Element("symbol").Attribute("number").Value,
                            Temperature = item.Element("temperature").Attribute("value").Value,
                            LocationId = location.LocationId,
                            Location = location
                        })
                        .ToList();

            return data;
        }
    }
}
