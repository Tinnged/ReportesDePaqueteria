using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportesDePaqueteria.MVVM.Models
{
    public class TrackingInfo
    {
        public string ShipmentCode { get; set; }
        public List<TrackingEvent> Events { get; set; }
    }
}
