namespace AutoParts.Data
{
    using AutoParts.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCategory> PartCategories { get; set; }
        public DbSet<PartCompatibility> PartCompatibilities { get; set; }
    }
}