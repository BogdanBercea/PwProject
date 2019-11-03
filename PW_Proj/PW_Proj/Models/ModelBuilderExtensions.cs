using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                   new User
                   {
                       Id = 1,
                       UserName = "SimoSim",
                       Email = "SimoCaldarasul@gmail.com",
                       Password = "simospassword",
                       CofirmPassword = "simospassword",
                       Gender = GenderSelector.Male
                   },

                   new User
                   {
                       Id = 2,
                       UserName = "TabitaLove",
                       Email = "TabitaBadGirl69@gmail.com",
                       Password = "tabilove",
                       CofirmPassword = "tabilove",
                       Gender = GenderSelector.Female
                   }
               );
        }
    }
}
