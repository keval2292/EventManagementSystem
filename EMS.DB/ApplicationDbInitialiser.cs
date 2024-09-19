namespace EMS.DB
{
    using System;
    using System.Linq;
    using EMS.DB.Models;
    using Microsoft.AspNetCore.Identity;

    public static class ApplicationDbInitialiser
    {

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            AddRoleIfNotExists(roleManager, "Admin");
            AddRoleIfNotExists(roleManager, "Staff");
            AddRoleIfNotExists(roleManager, "Operator");
        }
        public static void SeedUsers(UserManager<User> userManager)
        {
            (string name, string password, string role,string fullName,string number,string address)[] demoUsers = new[]
            {
                (name: "sadmin@yopmail.com", password: "Test@123", role: "Admin",fullName:"sadmin",number:"9874563210",address:"this is demo."),
            };

            foreach ((string name, string password, string role,string fullName,string number,string address) user in demoUsers)
            {
                AddUserIfNotExists(userManager, user);
            }

        }

        private static void AddUserIfNotExists(UserManager<User> userManager, (string name, string password, string role,string fullName,string number, string address ) demoUser)
        {
            User user = userManager.FindByNameAsync(demoUser.name).Result;
            if (user == default)
            {
                var newAppUser = new User
                {
                    UserName = demoUser.name,
                    Password = demoUser.password,
                    FullName = demoUser.fullName,
                    Useraddress = demoUser.address,
                    ContactNo = demoUser.number,
                    Email = demoUser.name,
                    Userrole = demoUser.role,
                    EmailConfirmed = true,
                    IsActive = true
                };
                _ = userManager.CreateAsync(newAppUser, demoUser.password).Result;
                if (!string.IsNullOrWhiteSpace(demoUser.role))
                {
                    var roles = demoUser.role.Split(',').Select(a => a.Trim()).ToArray();
                    Console.WriteLine($"{roles.Length}");
                    foreach (var role in roles)
                    {
                        _ = userManager.AddToRoleAsync(newAppUser, role).Result;

                    }
                }

            }
        }

        private static void AddRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (roleManager.FindByNameAsync(roleName).Result == default)
            {
                roleManager.CreateAsync(new IdentityRole { Name = roleName }).Wait();
            }
        }
    }
}
