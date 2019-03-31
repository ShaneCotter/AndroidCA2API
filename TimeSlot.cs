using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableAPI
{
    public class TimeSlot
    {
        public int ID { get; set; }
        public int? ModuleId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int RoomNumber { get; set; }
        public double DurationHours { get; set; }
    }
}