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

public class Program
        {
            public static void Main(string[] args)
            {
            CreateWebHostBuilder(args).Build().Run();


            //For testing DB queries locally

            //TimetableRepository repository = new TimetableRepository();
            //Module m = repository.getModuleByModuleName("Project");
            //Console.WriteLine(m.ModuleName + " " + m.Timeslots);
            //Console.ReadLine();
            }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
        }
    }
