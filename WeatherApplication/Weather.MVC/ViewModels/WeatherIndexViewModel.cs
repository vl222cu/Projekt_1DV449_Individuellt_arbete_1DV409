using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weather.Domain;

namespace Weather.MVC.ViewModels
{
    public class WeatherIndexViewModel
    {
        [DisplayName("Sök stad: ")]
        public string CityName { get; set; }

        public bool HasForecasts
        {
            get { return Forecasts != null && Forecasts.Any(); }
        }

        public IEnumerable<Forecast> Forecasts 
        {
            get { return Location != null ? Location.Forecasts : null; } 
        }

        public Location Location { get; set; }

        public string Name
        {
            get { return Location != null ? Location.GeoName : "[Unknown]"; }
        }
    }
}