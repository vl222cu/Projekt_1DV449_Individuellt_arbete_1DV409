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
        public IEnumerable<Location> LookupLocation(string cityName)
        {
           var rawJson = string.Empty;

 //           #region JSON from api.geonames.org

           var requestUriString = new Uri(String.Format("http://api.geonames.org/searchJSON?q={0}&maxRows=10&username=vl222cu", cityName));
           var request = (HttpWebRequest)WebRequest.Create(requestUriString);
           request.Method = "GET";

           using (var response = request.GetResponse())
           using (var reader = new StreamReader(response.GetResponseStream()))
           {
                rawJson = reader.ReadToEnd();
            }

//           #endregion

           var cityList = new List<Location>();
           var obj = JObject.Parse(rawJson);
           
           foreach (var child in obj["geonames"])
           {
               string id = (string)child["geonameId"];
               string countryName = (string)child["countryName"];
               string city = (string)child["name"];

               cityList.Add(new Location(id, countryName, city));
           }
           return cityList;
        }

        public IEnumerable<Forecast> GetLocationForcast(Location location)
        {
            var requestUriString = String.Format("http://www.yr.no/place/Sweden/{0}/{1}/forecast.xml", location, location);
            XDocument X = XDocument.Load(requestUriString);

            var forecast = X.Element("weatherdata").Element("forecast");
            var locationName = forecast.Descendants("location").Attributes("name").FirstOrDefault().Value;
            var tempData = forecast.Element("tabular").Elements("time");

            var data = tempData.Select(item =>
                        new Forecast
                        {
                            DateFrom = Convert.ToDateTime(item.Attribute("from").Value),
                            DateTo = Convert.ToDateTime(item.Attribute("to").Value),
                            SymbolNumber = item.Element("symbol").Attribute("number").Value,
                            Temperature = item.Element("temperature").Attribute("value").Value
                        })
                        .ToList();

            return data;
        }
    }
}
