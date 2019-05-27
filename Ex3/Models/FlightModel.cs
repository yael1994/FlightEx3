using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class FlightModel
    {
        private static FlightModel f_instace = null;

        public static FlightModel Instance
        {
            get
            {
                if (f_instace == null)
                {
                    f_instace = new FlightModel();
                }
                return f_instace;
            }
        }


        public double Lat { get; set; }
        public double Lon { get; set; }

        public void write()
        {

        }

        public void readFile()
        {

        }
    }
}