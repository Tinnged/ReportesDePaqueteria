using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportesDePaqueteria.MVVM.Models
{
    internal class TrackingEvent
    {
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string DriverName { get; set; }
        public bool IsCompleted { get; set; }
    }
}
