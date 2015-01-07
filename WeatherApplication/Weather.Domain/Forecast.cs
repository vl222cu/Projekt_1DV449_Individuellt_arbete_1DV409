using System;
using System.Collections.Generic;
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

        public Forecast(XmlNode node, Location location)
        {
            DateFrom = DateTime.ParseExact((node.Attributes["from"].Value).ToString(),"yyyy-MM-dd HH:MM", CultureInfo.InvariantCulture);
            DateTo = DateTime.ParseExact((node.Attributes["to"].Value).ToString(), "yyyy-MM-dd HH:MM", CultureInfo.InvariantCulture);
            SymbolNumber = SymbolNumber = node["symbol"].Attributes["number"].Value;
            Temperature = node["temperature"].Attributes["value"].Value;
            LocationId = location.LocationId;
            Location = location;
        }
    }
}
