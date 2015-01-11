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

        #region Index

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
                    model.Location = _service.GetLocation(model.CityName);
                    _service.RefreshForecasts(model.Location);
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

            return View(model);
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