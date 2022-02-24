using Microsoft.EntityFrameworkCore;
using Tele2API.Models;

namespace Tele2API.Data
{
    public class CitizenContext : DbContext
    {
        public CitizenContext(DbContextOptions<CitizenContext> opt) : base(opt)
        {
        }
        public DbSet<Citizen> Citizens { get; set; }
    }
}