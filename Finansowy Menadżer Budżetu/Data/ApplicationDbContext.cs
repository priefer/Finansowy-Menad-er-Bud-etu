using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Finansowy_Menadżer_Budżetu.Models;

namespace Finansowy_Menadżer_Budżetu.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Finansowy_Menadżer_Budżetu.Models.Transactions>? Transactions { get; set; }
        public DbSet<Finansowy_Menadżer_Budżetu.Models.Group>? Group { get; set; }
        public DbSet<Finansowy_Menadżer_Budżetu.Models.Category>? Category { get; set; }
    }
}