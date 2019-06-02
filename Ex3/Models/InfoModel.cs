using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }

        public int time { get; set; }
  
        private double lastLat = 0;
        private double lastLon = 0;
        private double lat, lon;
        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lastLat = lat;
               
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
               
                lon = value;
            }
        }
        public double Hight { get; set; }
        public double Throttel { get; set; }
        public double Rudder { get; set; }
        public string FileName { get; set; }
        public string ToWrite { get; set; }
        public List<string> ReadFile { get; set; }
        public int Index { get; set; }
      

        
        public void AppendXML(string s)
        {
            string createText = s + Environment.NewLine;


           // var roamingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
          //  var filePath = Path.Combine(roamingDirectory, FileName);
            String file = System.Web.Hosting.HostingEnvironment.MapPath(@"/App_Data/" + FileName);
            File.WriteAllText(file, createText);

         

        }

        public void ToXml(XmlWriter writer)
        {
           
         
            writer.WriteElementString("lastLat", this.lastLat.ToString());
            writer.WriteElementString("lastLon", this.lastLon.ToString());
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteElementString("Lon", (this.Lon).ToString());
            writer.WriteElementString("Height", this.Hight.ToString());
            writer.WriteElementString("Throttel", this.Throttel.ToString());
            writer.WriteElementString("Rudder", this.Rudder.ToString());

         
      

        }

        

    }

}