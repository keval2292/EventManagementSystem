using EMS.DB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<CategoryService> CategoryServices { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<EventStaffWork> EventStaffWorks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<NotificationMessages> NotificationMessages { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Admin> Admins { get; set; }


    }
}
