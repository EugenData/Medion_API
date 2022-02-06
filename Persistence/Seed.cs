using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> usermanager)
        {
            // bool x = await _roleManager.RoleExistsAsync("Admin");
            // if (!x)
            // {
            //     // first we create Admin rool    
            //     var role = new IdentityRole();
            //     role.Name = "Admin";
            //     await _roleManager.CreateAsync(role);

            //     //Here we create a Admin super user who will maintain the website                   

            //     var user = new AppUser
            //     {
            //         DisplayName = "Avramenko",
            //         UserName = "avramenko",
            //         Email = "avram@test.com"
            //     };

            //     string userPWD = "PA$$w0rd";

            //     IdentityResult chkUser = await usermanager.CreateAsync(user, userPWD);

            //     //Add default User to Role Admin    
            //     if (chkUser.Succeeded)
            //     {
            //         var result1 = await usermanager.AddToRoleAsync(user, "Admin");
            //     }
            // }

            if (usermanager.Users.Count() < 2)
            {

                var ich = new AppUser
                {
                    DisplayName = "Eugene",
                    UserName = "eugene",
                    Email = "eugene@test.com"
                };
                var user = new AppUser
                {
                    DisplayName = "Liliya",
                    UserName = "liliya",
                    Email = "liliya@medion.ua"
                };


                await usermanager.CreateAsync(ich, "PA$$w0rd");
                await usermanager.CreateAsync(user, "MEDion2020");
            }

            if (!context.Patients.Any())
            {
                var meetings = new List<Meeting>{
                    new Meeting{

                        Titel = "Огляд",
                        isDone = true,
                        Date = DateTime.Now.AddMonths(1),
                    },
                    new Meeting{

                        Titel = "Консультація",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(2),
                    },new Meeting{

                        Titel = "Аналізи",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(3),
                    },
                    new Meeting{

                        Titel = "Повторний огляд ",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(4),
                    },
                    new Meeting{

                        Titel = "УЗІ",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(5),
                    },
                    new Meeting{

                        Titel = "Повторний огляд",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(6),
                    },new Meeting{

                        Titel = "Консультація",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(7),
                    },new Meeting{

                        Titel = "Консультація",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(8),
                    },new Meeting{

                        Titel = "Аналізи",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(9),
                    },
                    new Meeting{

                        Titel = "Аналізи",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(8),
                    },new Meeting{

                        Titel = "Повторний огляд",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(6),
                    },new Meeting{

                        Titel = "Повторний огляд",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(7),
                    }
                };
                var patients = new List<Patient>{
                    new Patient
                    {
                        Patient_name = "Galila Petrovna",
                        Date = DateTime.Now,
                        Meetings = meetings
                    },
                     new Patient
                    {
                        Patient_name = "Mar Ivanovna",
                        Date = DateTime.Now,
                        Meetings = new List<Meeting>{
                            new Meeting{

                        Titel = "Огляд",
                        isDone = true,
                        Date = DateTime.Now.AddMonths(1),
                    },
                    new Meeting{

                        Titel = "Консультація",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(2),
                    },new Meeting{

                        Titel = "Аналізи",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(3),
                    },
                    new Meeting{

                        Titel = "Повторний огляд ",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(4),
                    },
                    new Meeting{

                        Titel = "УЗІ",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(5),
                    },
                    new Meeting{

                        Titel = "Повторний огляд",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(6),
                    },new Meeting{

                        Titel = "Консультація",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(7),
                    },new Meeting{

                        Titel = "Консультація",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(8),
                    },new Meeting{

                        Titel = "Аналізи",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(9),
                    },
                    new Meeting{

                        Titel = "Аналізи",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(8),
                    },new Meeting{

                        Titel = "Повторний огляд",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(6),
                    },new Meeting{

                        Titel = "Повторний огляд",
                        isDone = false,
                        Date = DateTime.Now.AddMonths(7),
                    }
                        }
                    },
                     new Patient
                    {
                        Patient_name = "Anastasy Sergeivna",
                        Date = DateTime.Now,
                        Meetings = meetings
                    },
                     new Patient
                    {
                        Patient_name = "Tvoya mamasha",
                        Date = DateTime.Now,
                    }, new Patient
                    {
                        Patient_name = "Donald Trump",
                        Date = DateTime.Now,

                    }
                };
                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}