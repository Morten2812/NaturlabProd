using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Naturlaboratoriet.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> tbl_NaturLab_Product { get; set; }

        public DbSet<Category> tbl_NaturLab_Category { get; set; }


        public DbSet<User> tbl_NaturLab_Admin { get; set; }

     
    }
}
