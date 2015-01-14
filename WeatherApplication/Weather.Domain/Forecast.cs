using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weather.Domain
{
    public partial class Forecast
    {
        public Forecast()
        {
            //Empty!
        }

        public string SymbolUrl
        {
            get { return String.Format("http://api.yr.no/weatherapi/weathericon/1.1/?symbol={0};content_type=image/png", SymbolNumber); }
        }

/*       public Forecast(XmlNode node, Location location)
        {
            SymbolNumber = node["symbol"].Attributes["number"].Value;
            Temperature = node["temperature"].Attributes["value"].Value;
            LocationId = location.LocationId;
            Location = location;
        } */
    }
}
