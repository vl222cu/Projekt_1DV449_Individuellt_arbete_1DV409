using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Domain;
using Weather.MVC.ViewModels;


namespace Weather.MVC.Controllers
{
    public class WeatherController : Controller
    {
        #region Fields

        private IWeatherService _service;

        #endregion

        #region Constructors

        public WeatherController()
            : this(new WeatherService())
        {
            // Empty!
        }

        public WeatherController(IWeatherService service)
        {
            _service = service;
        }

        #endregion

        #region ActionResults

        // GET: /Weather/
        public ActionResult Index()
        {
            return View();
        }

        // POST: /
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CityName")] WeatherIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Locations = _service.GetLocation(model.CityName);

                    if (model.Locations.Count() > 1)
                    {
                        return View("LocationList", model);
                    }

                    return RedirectToAction("Forecast", model);
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Index", model);
        }

        // Actionmetod som visar den valda platsens väder
        public ActionResult Forecast(Location location, WeatherIndexViewModel model)
        {
            try
            {
                ViewBag.dayNow = (int)System.DateTime.Now.DayOfWeek;
                model.Location = location;
                model.Forecasts = _service.GetForecast(location);
                return View("Forecast", model);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View();
        }



        #endregion

        #region IDisposible

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}