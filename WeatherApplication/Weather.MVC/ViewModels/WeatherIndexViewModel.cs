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
        [DisplayName("Location: ")]
        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(50, ErrorMessage = "The search field may not contain more than 50 characters.")]
        public string CityName { get; set; }

        public bool HasForecasts
        {
            get { return Forecasts != null && Forecasts.Any(); }
        }

        public IEnumerable<Forecast> Forecasts { get; set; }

        public IEnumerable<Location> Locations { get; set; }

        //public string Name { get; set; }
    }
}