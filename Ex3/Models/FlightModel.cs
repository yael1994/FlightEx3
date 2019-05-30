using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class FlightModel
    {
        private double lastLat = 0;
        private double lastLon = 0;
        private double lat, lon;
        public double Lat {
            get
            {
                return lat;
            }
            set
            {
                lastLat = lat;
                if (lat - value < 1)
                {
                    value = value + 5;
                }
                lat = value;
            }
        }
        public double Lon
        {
            get
            {
                return lon;
            }
            set
            {
                lastLon = lon;
                if(lon - value  < 1)
                {
                    value = value + 5;
                }
                lon = value;
            }
        }
        public double Hight { get; set; }
        public double Direction { get; set; }
        public double Speed { get; set; }

        public void ToXml(XmlWriter writer)
        {

            writer.WriteElementString("lastLat", this.lastLat.ToString());
            writer.WriteElementString("lastLon", this.lastLon.ToString());
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