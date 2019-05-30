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
      
        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            Client.getInstance().Connect(ip, port);
            Client.getInstance().Write("get /position/longitude-deg\r\n");
            Client.getInstance().Write("get /position/latitude-deg\r\n");
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;

            return View();
        }
        [HttpGet]
        public ActionResult displayTime(string ip, int port, int time)
        {
            Client.getInstance().Connect(ip, port);
            InfoModel.Instance.time = time;
            Client.getInstance().Write("get /position/longitude-deg\r\n");
            Client.getInstance().Write("get /position/latitude-deg\r\n");
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;
            Session["time"] = time;

            return View();
        }

        [HttpPost]
        public String GetFlightData()
        {
            Client.getInstance().Write("get /position/longitude-deg\r\n");
            Client.getInstance().Write("get /position/latitude-deg\r\n");
            var flight = InfoModel.Instance;
            return ToXml(flight);

        }
        private string ToXml(InfoModel flight)
        {
             //Initiate XML stuff
             StringBuilder sb = new StringBuilder();
             XmlWriterSettings settings = new XmlWriterSettings();
             XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Flight");
            flight.ToXml(writer);

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                return sb.ToString();
         }
        }
    }