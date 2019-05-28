using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult display(string ip, int port, int time)
        {
            InfoModel.Instance.startServer(ip, port);
            InfoModel.Instance.readServer(time);
            InfoModel.Instance.time = time;
            ViewBag.Lat = FlightModel.Instance.Lat;
            ViewBag.Lon = FlightModel.Instance.Lon;

             Session["Lat"] = 200;
            Session["Lon"] = 200;
            Session["time"] = time;

            return View();
        }

        [HttpPost]
        public int GetFlightData()
        {
            Session["Lat"] = FlightModel.Instance.Lat;
            Session["Lon"] = FlightModel.Instance.Lon;

            return 0;

        }
        //  private string ToXml()
        //  {
        //     //Initiate XML stuff
        //     StringBuilder sb = new StringBuilder();
        //     XmlWriterSettings settings = new XmlWriterSettings();
        //     XmlWriter writer = XmlWriter.Create(sb, settings);

        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("Employess");

        //    writer.WriteEndElement();
        //    writer.WriteEndDocument();
        //    writer.Flush();
        //    return sb.ToString();
        //}
    }
}