using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lift_ChartMaker.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift_ChartMaker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Scores> Scores { get; set; }
    }
}
