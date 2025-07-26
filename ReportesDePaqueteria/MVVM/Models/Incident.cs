using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportesDePaqueteria.MVVM.Models
{
    public class Incident
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string ShipmentCode { get; set; }
        public string Description { get; set; }
    }
}
