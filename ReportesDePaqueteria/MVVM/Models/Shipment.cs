using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ReportesDePaqueteria.MVVM.Models;

namespace ReportesDePaqueteria.MVVM.Models
{
    public class Shipment
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Route { get; set; }
        public string Driver { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string PackageType { get; set; }
        public double Weight { get; set; }
        public Address Origin { get; set; }
        public Address Destination { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }

    }
}
