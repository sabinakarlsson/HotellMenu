using HotellMenu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Contexts
{
    public class DataInitializer
    {
        public static ApplicationDbContext Run()
        {
            //var dbContext = new ApplicationDbContext();

            //Skapar en Configuration Builder som kan hämta enskilda värden från appsettings.json.
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //Hämtar vår connection string inuti appsettings.json med ConfigurationBuilder objektet
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Med vår connection string skapar vi en DbContextOption, alltså en inställning för vår databas.
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseSqlServer(connectionString)
             .Options;

            // Skapar ett objekt av ApplicationDbContext genom att skicka in våra inställningar som innehåller connection stringen.
            var dbContext = new ApplicationDbContext(contextOptions);

            dbContext.Database.Migrate();

            return dbContext;
        }

        public static void InitializeData(ApplicationDbContext dbContext)
        {
            if (!IfAnyDataExists(dbContext)) //om detta är false, så kommer nedanstående kod börja generera data
            {
                GenerateHotelRooms(dbContext);
                GenerateCustomers(dbContext);
            }


        }

        public static bool IfAnyDataExists(ApplicationDbContext dbContext)
        {
            return dbContext.Bookings.Any() || dbContext.Customers.Any() || dbContext.HotelRooms.Any();
        }


        //genererar hotellrum

        public static void GenerateHotelRooms(ApplicationDbContext dbContext)
        {

            dbContext.HotelRooms.Add(new HotelRooms
            {
                RoomNumber = 101,
                IsDouble = false,
                RoomAvailability = true,
                RoomSize = 20,
                NbrExtraBeds = 0
            });

            dbContext.HotelRooms.Add(new HotelRooms
            {
                RoomNumber = 102,
                IsDouble = true,
                RoomAvailability = true,
                RoomSize = 30,
                NbrExtraBeds = 1
            });

            dbContext.HotelRooms.Add(new HotelRooms
            {
                RoomNumber = 201,
                IsDouble = true,
                RoomAvailability = true,
                RoomSize = 40,
                NbrExtraBeds = 2
            });

            dbContext.HotelRooms.Add(new HotelRooms
            {
                RoomNumber = 202,
                IsDouble = false,
                RoomAvailability = true,
                RoomSize = 20,
                NbrExtraBeds = 0
            });

            dbContext.SaveChanges();
        }

        
        //generar kunder
        public static void GenerateCustomers(ApplicationDbContext dbContext)
        {

            dbContext.Customers.Add(new Customers
            {
                CustomerName = "Anna Svensson",
                Email = "anna@svensson.se",
            });

            dbContext.Customers.Add(new Customers
            {
                CustomerName = "Petra Karlsson",
                Email = "petra@karlsson.se",
            });

            dbContext.Customers.Add(new Customers
            {
                CustomerName = "Kalle Persson",
                Email = "kalle@persson.se",
            });

            dbContext.Customers.Add(new Customers
            {
                CustomerName = "Sven Gustafsson",
                Email = "sven@gustafsson.se",
            });

            dbContext.SaveChanges();
        }

    }
}
