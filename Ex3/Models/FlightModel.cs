using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class FlightModel
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Hight { get; set; }
        public double Direction { get; set; }
        public double Speed { get; set; }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Flight");
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Hight", this.Hight.ToString());
            writer.WriteElementString("Direction", this.Direction.ToString());
            writer.WriteElementString("Speed", this.Speed.ToString());
            writer.WriteEndElement();
        }

      
    }
}