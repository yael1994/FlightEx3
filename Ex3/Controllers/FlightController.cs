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
            InfoModel.Instance.startServer(ip, port);
            InfoModel.Instance.readServer();
           
            Session["Lat"] = InfoModel.Instance.Flight.Lat;
            Session["Lon"] = InfoModel.Instance.Flight.Lon;
            return View();
        }
        [HttpGet]
        public ActionResult displayTime(string ip, int port, int time)
        {
            InfoModel.Instance.startServer(ip, port);
            InfoModel.Instance.readServerTime();
            InfoModel.Instance.time = time;

            Session["Lat"] = InfoModel.Instance.Flight.Lat;
            Session["Lon"] = InfoModel.Instance.Flight.Lon;
            Session["time"] = time;

            return View();
        }

        [HttpPost]
        public String GetFlightData()
        {

            var flight = InfoModel.Instance.Flight;
            String latS =
            ViewBag.Lat = "";
            ViewBag.Lon = "";
        //    flight.Lat=
        //    flight.Lon=
            return ToXml(flight);

        }
        private string FromXml(FlightModel flight)
        {

        }


        private string ToXml(FlightModel flight)
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