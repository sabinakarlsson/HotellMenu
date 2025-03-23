using HotellMenu.Contexts;
using HotellMenu.Menus;
using HotellMenu.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotellMenu
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = DataInitializer.Run())
            {
                //använd denna för att skapa nya hotellrum och kunder
                DataInitializer.InitializeData(dbContext);
                var menu = new Menu(dbContext);
                menu.Start();
            }
        }
    }
}
