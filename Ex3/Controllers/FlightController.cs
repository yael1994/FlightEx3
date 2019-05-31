using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Ex3.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult save(string ip, int port, int time,int during, string fileName)
        {
            Client.getInstance().Connect(ip, port);
            InfoModel.Instance.time = time;
            InfoModel.Instance.FileName = fileName;
            InfoModel.Instance.Lon= Client.getInstance().Write("get /position/longitude-deg\r\n");
            InfoModel.Instance.Lat = Client.getInstance().Write("get /position/latitude-deg\r\n");
            InfoModel.Instance.Speed= Client.getInstance().Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
            InfoModel.Instance.Direction = Client.getInstance().Write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;
            Session["time"] = time;
            Session["during"] = during;

            return View();
        }
        [HttpPost]
        public string GetFlightDataToFile()
        {
            InfoModel.Instance.Lon = Client.getInstance().Write("get /position/longitude-deg\r\n");
            InfoModel.Instance.Lat = Client.getInstance().Write("get /position/latitude-deg\r\n");
            InfoModel.Instance.Speed = Client.getInstance().Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
            InfoModel.Instance.Direction = Client.getInstance().Write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
            var flight = InfoModel.Instance;
            string s = ToXml(flight);
            s += '\n';
            InfoModel.Instance.ToWrite += s;
           
            return s;

        }
        [HttpPost]
        public string SaveToXML()
        {
            InfoModel.Instance.AppendXML(InfoModel.Instance.ToWrite);
            return "saved";
        }


        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            //Client.getInstance().Connect(ip, port);
            //InfoModel.Instance.Lon = Client.getInstance().Write("get /position/longitude-deg\r\n");
            //InfoModel.Instance.Lat = Client.getInstance().Write("get /position/latitude-deg\r\n");
            InfoModel.Instance.Lat = 30;
            InfoModel.Instance.Lon = 30;
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;

            return View();
        }
        [HttpGet]
        public ActionResult displayTime(string ip, int port, int time)
        {
            Client.getInstance().Connect(ip, port);
            InfoModel.Instance.time = time;
            InfoModel.Instance.Lon = Client.getInstance().Write("get /position/longitude-deg\r\n");
            InfoModel.Instance.Lat = Client.getInstance().Write("get /position/latitude-deg\r\n");
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;
            Session["time"] = time;

            return View();
        }

        [HttpPost]
        public string GetFlightData()
        {
           InfoModel.Instance.Lon = Client.getInstance().Write("get /position/longitude-deg\r\n");
            InfoModel.Instance.Lat = Client.getInstance().Write("get /position/latitude-deg\r\n");
            var flight = InfoModel.Instance;
            return ToXml(flight);

        }
        private string ToXml(InfoModel flight)
        {
            //Initiate XML stuff
            XmlDocument doc = new XmlDocument();
            StringBuilder sb = new StringBuilder();
             XmlWriterSettings settings = new XmlWriterSettings();
             XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Flight");
            flight.ToXml(writer);

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                settings.Indent = true;

                return sb.ToString();
         }

    
        }
    }