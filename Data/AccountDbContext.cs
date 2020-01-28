using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class AccountDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
           : base(options)
        {
        }
      
    
    }
}
