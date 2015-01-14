using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Repositories;
using Weather.Domain.Webservices;


namespace Weather.Domain
{
    public class WeatherService : WeatherServiceBase
    {
        private IWeatherRepository _repository;
        private IWeatherWebservice _webservice;

        // Defaultkonstruktor
        public WeatherService()
            :this(new WeatherRepository(), new WeatherWebservice())
        { 
            // Empty!
        }


        public WeatherService(IWeatherRepository repository, IWeatherWebservice webservice)
        {
            _repository = repository;
            _webservice = webservice;
        }

        public override IEnumerable<Location> GetLocation(string cityName)
        {
            // Försöker hämta location från databasen
            var locations = _repository.FindLocationByCityName(cityName);

            // Om location saknas i databasen...
            if (locations == null || locations.Count() == 0)
            {
                // ...hämtas location från webservicen och...
                locations = _webservice.LookupLocation(cityName);

                // ...sparas i databasen 
                foreach (var location in locations) 
                {
                    _repository.AddLocation(location);
                    _repository.Save();
                }
            }

            return locations;
        }

        public override IEnumerable<Forecast> GetForecast(Location location)
        {
            // Om forecast saknas eller om det är dags för uppdatering...
            if (location.Forecasts.Count() == 0 || location.Forecasts.Any() || location.NextUpdate < DateTime.Now)
            {
                // ...tar bort gamla prognoser om det finns några...
                foreach (var forecast in location.Forecasts.ToList())
                {
                    _repository.RemoveForecast(forecast.ForecastId);
                }

                // ...och ny prognos hämtas från webservicen som läggs till...
                foreach (var forecast in _webservice.GetLocationForcast(location))
                {
                    _repository.AddForecast(forecast);
                }

                // ...sätter tiden för nästa uppdatering och...
                location.NextUpdate = DateTime.Now.AddHours(7);

                // ...sparas i databasen
                _repository.Save();
            }

            return location.Forecasts.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
