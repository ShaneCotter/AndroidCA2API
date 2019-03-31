using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimetableAPI
{
    public class Module
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public string LecturerName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TimeSlot> Timeslots { get; set; }
    }
}