using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

//Get all

//Get module by name
//Get module by lecturuer
//Get module description by module name
//Get module by timeslot

//Get timeslots by day
//Get timeslots by day/time
//Get timeslots by roomNumber
//Get timeslots by duration

namespace TimetableAPI
{
    public class TimetableContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:timetable-serv.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=X00131736;Password=Lfclfc08;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<Module> Modules { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
    }

    class TimetableRepository
    {

        // print modules + their timeslots
        public List<Module> getAllModules()
        {
            using (TimetableContext db = new TimetableContext())
            {
                List<Module> moduleObjects = new List<Module>();
                var modules = db.Modules.OrderBy(l => l.ID).Select(l => new { ID = l.ID, ModuleName = l.ModuleName, LecturerName = l.LecturerName, Description = l.Description, Timeslots = l.Timeslots });
                foreach (var module in modules)
                {
                    Module m = new Module() { ID = module.ID, ModuleName = module.ModuleName, LecturerName = module.LecturerName, Description = module.Description, Timeslots = module.Timeslots };
                    moduleObjects.Add(m);
                }
                return moduleObjects;
            }
        }

        public Module getModuleByModuleName(string moduleNameIn)
        {
            using (TimetableContext db = new TimetableContext())
            {
                var module = db.Modules.Where(mod => mod.ModuleName.ToLower() == moduleNameIn.ToLower()).Select(l => new Module { ID = l.ID, ModuleName = l.ModuleName, LecturerName = l.LecturerName, Description = l.Description, Timeslots = l.Timeslots }).FirstOrDefault();
                if (module == null)
                {
                    return module;
                }
                else
                {

                    return module;
                }
            }
        }

        public Module getModuleByLecturerName(string lecturerNameIn)
        {
            using (TimetableContext db = new TimetableContext())
            {
                var module = db.Modules.Where(mod => mod.LecturerName.ToLower() == lecturerNameIn.ToLower()).Select(l => new Module { ID = l.ID, ModuleName = l.ModuleName, LecturerName = l.LecturerName, Description = l.Description, Timeslots = l.Timeslots }).FirstOrDefault();
                if (module == null)
                {
                    return module;
                }
                else
                {

                    return module;
                }
            }
        }

        public Object getModuleDescriptionByModuleName(string moduleNameIn)
        {
            using (TimetableContext db = new TimetableContext())
            {
                var module = db.Modules.Where(mod => mod.ModuleName.ToLower() == moduleNameIn.ToLower()).Select(l => new { ModuleName = l.ModuleName, Description = l.Description }).FirstOrDefault();
                if (module == null)
                {
                    return null;
                }
                else
                {

                    return module;
                }
            }
        }


        public Object getModuleByTimeslot(string day, string time, string amPm)
        {
            using (TimetableContext db = new TimetableContext())
            {
                string timeString = time + amPm;
                var timeSlot = db.TimeSlots.Where(ts => ts.Day.ToLower() == day.ToLower() && ts.Time.ToLower() == timeString.ToLower()).FirstOrDefault();
                if (timeSlot == null)
                {
                    return null;
                }
                else
                {
                    var module = db.Modules.Where(mod => mod.ID == timeSlot.ModuleId).Select(l => new { ID = l.ID, ModuleName = l.ModuleName, LecturerName = l.LecturerName}).FirstOrDefault();
                    return  new { module.ModuleName, module.LecturerName, timeSlot.Day, timeSlot.Time };
                }
            }
        }

        public List<Object> getTimeslotsByDay(string day)
        {
            using (TimetableContext db = new TimetableContext())
            {
                List<Object> timeSlotsForDay = new List<Object>();
                var timeSlots = db.TimeSlots.Where(ts => ts.Day.ToLower() == day.ToLower()).Select(ts => new{ ts.ModuleId, ts.Day, ts.Time, ts.RoomNumber, ts.DurationHours });
                if (timeSlots == null)
                {
                    return null;
                }
                else
                {
                    foreach (var timeSlot in timeSlots)
                    {
                       var module = db.Modules.Where(mod => mod.ID == timeSlot.ModuleId).FirstOrDefault();
                       timeSlotsForDay.Add( new { timeSlot.Day, timeSlot.Time, timeSlot.DurationHours, module.ModuleName, module.LecturerName, module.Description, timeSlot.RoomNumber });
                    }
                    return timeSlotsForDay;
                }
            }
        }

        public List<Object> getTimeslotsByRoomNumber(int roomNumber)
        {
            using (TimetableContext db = new TimetableContext())
            {
                List<Object> timeSlotsForRoomNumber = new List<Object>();
                var timeSlots = db.TimeSlots.Where(ts => ts.RoomNumber == roomNumber).Select(ts => new { ts.ModuleId, ts.Day, ts.Time, ts.RoomNumber, ts.DurationHours });
                if (timeSlots == null)
                {
                    return null;
                }
                else
                {
                    foreach (var timeSlot in timeSlots)
                    {
                        var module = db.Modules.Where(mod => mod.ID == timeSlot.ModuleId).FirstOrDefault();
                        var timeSlotAndModule = new { timeSlot.Day, timeSlot.Time, timeSlot.DurationHours, timeSlot.RoomNumber, module.ModuleName, module.LecturerName };
                        timeSlotsForRoomNumber.Add(timeSlotAndModule);
                    }
                    return timeSlotsForRoomNumber;
                }
            }
        }

        public List<Object> getTimeslotsByDuration(double duration)
        {
            using (TimetableContext db = new TimetableContext())
            {
                List<Object> timeSlotsByDuration = new List<Object>();
                var timeSlots = db.TimeSlots.Where(ts => ts.DurationHours == duration).Select(ts => new { ts.ModuleId, ts.Day, ts.Time, ts.RoomNumber });
                if (timeSlots == null)
                {
                    return null;
                }
                else
                {
                    foreach (var timeSlot in timeSlots)
                    {
                        var module = db.Modules.Where(mod => mod.ID == timeSlot.ModuleId).FirstOrDefault();
                        timeSlotsByDuration.Add( new { timeSlot.Day, timeSlot.Time, module.ModuleName, timeSlot.RoomNumber });
                    }
                    return timeSlotsByDuration;
                }
            }
        }

    }
}