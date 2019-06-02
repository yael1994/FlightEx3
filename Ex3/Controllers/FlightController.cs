using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using System.IO;

namespace Ex3.Controllers
{
    public class FlightController : Controller
    {
       private int lineNum = 0;
        // GET: Flight

        public ActionResult Index()
        {
            return View();
        }


        private void updateValues()
        {
            InfoModel.Instance.Lon = Client.getInstance().Write("get /position/longitude-deg\r\n");
            InfoModel.Instance.Lat = Client.getInstance().Write("get /position/latitude-deg\r\n");
            InfoModel.Instance.Speed = Client.getInstance().Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
            InfoModel.Instance.Direction = Client.getInstance().Write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
        }
        public ActionResult save(string ip, int port, int time,int during, string fileName)
        {
            Client.getInstance().Connect(ip, port);
            InfoModel.Instance.time = time;
            InfoModel.Instance.FileName = fileName;
            updateValues();
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;
            Session["time"] = time;
            Session["during"] = during;

            return View();
        }
        [HttpPost]
        public string GetFlightDataToFile()
        {
            updateValues();
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


        public bool ValidateIPv4(string ipString)
        {
            if (ipString.Count(c => c == '.') != 3) return false;
            IPAddress address;
            return IPAddress.TryParse(ipString, out address);
        }

        [HttpPost]
        public string GetLine()
        {
            if (InfoModel.Instance.Index < InfoModel.Instance.ReadFile.Count) {
                string line = InfoModel.Instance.ReadFile[InfoModel.Instance.Index];
                InfoModel.Instance.Index++;
                return line;
            }
            return "END";
        }

        [HttpGet]
        public ActionResult upload(string fileName, int time)
        {
            String path=@"C:\Users\yael4\source\repos\Ex3\Ex3\";
            //string path = @"C:\Users\Danielle\source\repos\FlightEx3\Ex3\";
            var logFile = System.IO.File.ReadAllLines(path + fileName);
            InfoModel.Instance.ReadFile = new List<string>(logFile);
            InfoModel.Instance.Index = 0;
            Session["time"] = time;

            return View(@"~\Views\Flight\upload.cshtml");
        }

        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            
            if (!ValidateIPv4(ip))
            {
              return upload(ip,port);
            }
           // Client.getInstance().Connect(ip, port);
        //    updateValues();
            InfoModel.Instance.Lat = 30;
            InfoModel.Instance.Lon = 30;
            //   Session["Lat"] = InfoModel.Instance.Lat;
            //   Session["Lon"] = InfoModel.Instance.Lon;
            Session["Lat"] = 200;
            Session["Lon"] = 200;
            return View();
        }
        [HttpGet]
        public ActionResult displayTime(string ip, int port, int time)
        {
            Client.getInstance().Connect(ip, port);
            InfoModel.Instance.time = time;
            updateValues();
            Session["Lat"] = InfoModel.Instance.Lat;
            Session["Lon"] = InfoModel.Instance.Lon;
            Session["time"] = time;

            return View();
        }
        [HttpPost]
        public string GetFlightData()
        {
            updateValues();
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