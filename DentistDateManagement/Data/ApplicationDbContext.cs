using DentistDateManagement.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistDateManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Data.Entity deki Appoinment taki class ı DB deki Appoinments ile   eşleştir.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }
    }
}
