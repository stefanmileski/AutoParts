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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PartCompatibility>()
                .HasOne(pc => pc.Part)
                .WithMany()
                .HasForeignKey(pc => pc.PartId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PartCompatibility>()
                .HasOne(pc => pc.CarModel)
                .WithMany()
                .HasForeignKey(pc => pc.CarModelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PartCompatibility>()
                .HasOne(pc => pc.PartCategory)
                .WithMany(c => c.PartCompatibilities)
                .HasForeignKey(pc => pc.PartCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Manufacturers
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { Id = 1, Name = "BMW" },
                new Manufacturer { Id = 2, Name = "Volkswagen" },
                new Manufacturer { Id = 3, Name = "Toyota" },
                new Manufacturer { Id = 4, Name = "Ford" },
                new Manufacturer { Id = 5, Name = "Audi" }
            );

            // Car Models
            modelBuilder.Entity<CarModel>().HasData(
                new CarModel { Id = 1, Name = "E46 320i", ManufacturerId = 1, YearInProductionSince = 1998, YearInProductionTill = 2005 },
                new CarModel { Id = 2, Name = "E90 320d", ManufacturerId = 1, YearInProductionSince = 2005, YearInProductionTill = 2012 },
                new CarModel { Id = 3, Name = "Golf Mk5", ManufacturerId = 2, YearInProductionSince = 2003, YearInProductionTill = 2009 },
                new CarModel { Id = 4, Name = "Passat B6", ManufacturerId = 2, YearInProductionSince = 2005, YearInProductionTill = 2010 },
                new CarModel { Id = 5, Name = "Corolla E120", ManufacturerId = 3, YearInProductionSince = 2001, YearInProductionTill = 2007 },
                new CarModel { Id = 6, Name = "Camry XV50", ManufacturerId = 3, YearInProductionSince = 2011, YearInProductionTill = 2017 },
                new CarModel { Id = 7, Name = "Focus Mk2", ManufacturerId = 4, YearInProductionSince = 2004, YearInProductionTill = 2011 },
                new CarModel { Id = 8, Name = "Mondeo Mk4", ManufacturerId = 4, YearInProductionSince = 2007, YearInProductionTill = 2014 },
                new CarModel { Id = 9, Name = "A4 B7", ManufacturerId = 5, YearInProductionSince = 2004, YearInProductionTill = 2008 },
                new CarModel { Id = 10, Name = "A6 C6", ManufacturerId = 5, YearInProductionSince = 2004, YearInProductionTill = 2011 }
            );

            // Part Categories
            modelBuilder.Entity<PartCategory>().HasData(
                new PartCategory { Id = 1, Name = "Brakes" },
                new PartCategory { Id = 2, Name = "Engine" },
                new PartCategory { Id = 3, Name = "Filters" },
                new PartCategory { Id = 4, Name = "Suspension" },
                new PartCategory { Id = 5, Name = "Electrical" },
                new PartCategory { Id = 6, Name = "Exhaust" },
                new PartCategory { Id = 7, Name = "Transmission" },
                new PartCategory { Id = 8, Name = "Cooling" }
            );

            // Parts
            modelBuilder.Entity<Part>().HasData(
                // Brakes
                new Part { Id = 1, Name = "Front Brake Pads", OemNumber = "34116761244", Price = 28.90m, StockQuantity = 15, PartCategoryId = 1, ManufacturerId = 1, Description = "OEM front brake pads for BMW E46" },
                new Part { Id = 2, Name = "Rear Brake Pads", OemNumber = "34216761245", Price = 24.50m, StockQuantity = 12, PartCategoryId = 1, ManufacturerId = 1, Description = "OEM rear brake pads for BMW E46" },
                new Part { Id = 3, Name = "Front Brake Disc", OemNumber = "34116864056", Price = 45.00m, StockQuantity = 8, PartCategoryId = 1, ManufacturerId = 1, Description = "Front brake disc 300mm" },
                new Part { Id = 4, Name = "Brake Caliper Front Left", OemNumber = "34116750418", Price = 89.99m, StockQuantity = 4, PartCategoryId = 1, ManufacturerId = 1, Description = "Front left brake caliper" },
                new Part { Id = 5, Name = "Front Brake Pads", OemNumber = "1K0698151B", Price = 22.50m, StockQuantity = 20, PartCategoryId = 1, ManufacturerId = 2, Description = "Front brake pads for VW Golf Mk5" },
                new Part { Id = 6, Name = "Rear Brake Disc", OemNumber = "1K0615601N", Price = 38.00m, StockQuantity = 10, PartCategoryId = 1, ManufacturerId = 2, Description = "Rear brake disc for VW Golf Mk5" },
                new Part { Id = 7, Name = "Front Brake Pads", OemNumber = "04465-02200", Price = 19.90m, StockQuantity = 25, PartCategoryId = 1, ManufacturerId = 3, Description = "Front brake pads for Toyota Corolla" },
                new Part { Id = 8, Name = "Front Brake Pads", OemNumber = "1229912", Price = 21.00m, StockQuantity = 18, PartCategoryId = 1, ManufacturerId = 4, Description = "Front brake pads for Ford Focus Mk2" },

                // Engine
                new Part { Id = 9, Name = "Valve Cover Gasket", OemNumber = "11127512839", Price = 18.50m, StockQuantity = 9, PartCategoryId = 2, ManufacturerId = 1, Description = "Valve cover gasket for BMW E46 M54" },
                new Part { Id = 10, Name = "Timing Chain Kit", OemNumber = "11311748051", Price = 120.00m, StockQuantity = 5, PartCategoryId = 2, ManufacturerId = 1, Description = "Complete timing chain kit" },
                new Part { Id = 11, Name = "Intake Manifold Gasket", OemNumber = "058129717G", Price = 14.00m, StockQuantity = 11, PartCategoryId = 2, ManufacturerId = 2, Description = "Intake manifold gasket for VW 1.8T" },
                new Part { Id = 12, Name = "Crankshaft Seal", OemNumber = "04465-33040", Price = 9.90m, StockQuantity = 14, PartCategoryId = 2, ManufacturerId = 3, Description = "Rear crankshaft seal for Toyota" },

                // Filters
                new Part { Id = 13, Name = "Oil Filter", OemNumber = "11427512300", Price = 8.50m, StockQuantity = 30, PartCategoryId = 3, ManufacturerId = 1, Description = "Engine oil filter for BMW E46" },
                new Part { Id = 14, Name = "Air Filter", OemNumber = "13721730449", Price = 12.00m, StockQuantity = 22, PartCategoryId = 3, ManufacturerId = 1, Description = "Air filter for BMW E46 320i" },
                new Part { Id = 15, Name = "Cabin Air Filter", OemNumber = "64316823725", Price = 15.00m, StockQuantity = 17, PartCategoryId = 3, ManufacturerId = 1, Description = "Cabin pollen filter for BMW E46" },
                new Part { Id = 16, Name = "Oil Filter", OemNumber = "1K0115562", Price = 7.20m, StockQuantity = 35, PartCategoryId = 3, ManufacturerId = 2, Description = "Oil filter for VW Golf Mk5" },
                new Part { Id = 17, Name = "Air Filter", OemNumber = "1K0129620", Price = 11.00m, StockQuantity = 28, PartCategoryId = 3, ManufacturerId = 2, Description = "Air filter for VW Golf Mk5" },
                new Part { Id = 18, Name = "Oil Filter", OemNumber = "04152-YZZA6", Price = 6.90m, StockQuantity = 40, PartCategoryId = 3, ManufacturerId = 3, Description = "Oil filter for Toyota Corolla" },
                new Part { Id = 19, Name = "Fuel Filter", OemNumber = "1229894", Price = 13.50m, StockQuantity = 16, PartCategoryId = 3, ManufacturerId = 4, Description = "Fuel filter for Ford Focus Mk2" },

                // Suspension
                new Part { Id = 20, Name = "Front Shock Absorber", OemNumber = "31316750417", Price = 75.00m, StockQuantity = 6, PartCategoryId = 4, ManufacturerId = 1, Description = "Front shock absorber for BMW E46" },
                new Part { Id = 21, Name = "Rear Shock Absorber", OemNumber = "33521091867", Price = 65.00m, StockQuantity = 7, PartCategoryId = 4, ManufacturerId = 1, Description = "Rear shock absorber for BMW E46" },
                new Part { Id = 22, Name = "Front Control Arm", OemNumber = "31126758510", Price = 55.00m, StockQuantity = 8, PartCategoryId = 4, ManufacturerId = 1, Description = "Front lower control arm" },
                new Part { Id = 23, Name = "Front Shock Absorber", OemNumber = "1K0413031BE", Price = 68.00m, StockQuantity = 9, PartCategoryId = 4, ManufacturerId = 2, Description = "Front shock absorber for VW Golf Mk5" },
                new Part { Id = 24, Name = "Stabilizer Link", OemNumber = "48820-02030", Price = 18.00m, StockQuantity = 13, PartCategoryId = 4, ManufacturerId = 3, Description = "Front stabilizer link for Toyota" },

                // Electrical
                new Part { Id = 25, Name = "Alternator", OemNumber = "12317501886", Price = 145.00m, StockQuantity = 4, PartCategoryId = 5, ManufacturerId = 1, Description = "Alternator 140A for BMW E46" },
                new Part { Id = 26, Name = "Starter Motor", OemNumber = "12417527229", Price = 110.00m, StockQuantity = 3, PartCategoryId = 5, ManufacturerId = 1, Description = "Starter motor for BMW E46" },
                new Part { Id = 27, Name = "Ignition Coil", OemNumber = "12131703228", Price = 32.00m, StockQuantity = 20, PartCategoryId = 5, ManufacturerId = 1, Description = "Ignition coil for BMW E46 320i" },
                new Part { Id = 28, Name = "Ignition Coil", OemNumber = "07K905715F", Price = 28.00m, StockQuantity = 22, PartCategoryId = 5, ManufacturerId = 2, Description = "Ignition coil for VW Golf Mk5" },

                // Exhaust
                new Part { Id = 29, Name = "Catalytic Converter", OemNumber = "18307503816", Price = 220.00m, StockQuantity = 3, PartCategoryId = 6, ManufacturerId = 1, Description = "Catalytic converter for BMW E46" },
                new Part { Id = 30, Name = "Exhaust Manifold Gasket", OemNumber = "11621437567", Price = 16.00m, StockQuantity = 10, PartCategoryId = 6, ManufacturerId = 1, Description = "Exhaust manifold gasket" },
                new Part { Id = 31, Name = "Rear Silencer", OemNumber = "1K0253609AK", Price = 95.00m, StockQuantity = 5, PartCategoryId = 6, ManufacturerId = 2, Description = "Rear silencer for VW Golf Mk5" },

                // Transmission
                new Part { Id = 32, Name = "Clutch Kit", OemNumber = "21207526519", Price = 185.00m, StockQuantity = 4, PartCategoryId = 7, ManufacturerId = 1, Description = "Complete clutch kit for BMW E46" },
                new Part { Id = 33, Name = "Gearbox Oil", OemNumber = "83222282583", Price = 22.00m, StockQuantity = 25, PartCategoryId = 7, ManufacturerId = 1, Description = "MTF-LT-3 gearbox oil 1L" },
                new Part { Id = 34, Name = "Clutch Kit", OemNumber = "02T141015D", Price = 160.00m, StockQuantity = 5, PartCategoryId = 7, ManufacturerId = 2, Description = "Clutch kit for VW Golf Mk5" },

                // Cooling
                new Part { Id = 35, Name = "Radiator", OemNumber = "17117561757", Price = 130.00m, StockQuantity = 4, PartCategoryId = 8, ManufacturerId = 1, Description = "Coolant radiator for BMW E46" },
                new Part { Id = 36, Name = "Water Pump", OemNumber = "11517527799", Price = 55.00m, StockQuantity = 7, PartCategoryId = 8, ManufacturerId = 1, Description = "Water pump for BMW E46 M54" },
                new Part { Id = 37, Name = "Thermostat", OemNumber = "11531437040", Price = 25.00m, StockQuantity = 10, PartCategoryId = 8, ManufacturerId = 1, Description = "Coolant thermostat for BMW E46" },
                new Part { Id = 38, Name = "Radiator", OemNumber = "1K0121251AT", Price = 110.00m, StockQuantity = 5, PartCategoryId = 8, ManufacturerId = 2, Description = "Radiator for VW Golf Mk5" }
            );

            // Part Compatibilities
            modelBuilder.Entity<PartCompatibility>().HasData(
                // BMW E46 320i (CarModelId = 1)
                new PartCompatibility { Id = 1, PartId = 1, CarModelId = 1, PartCategoryId = 1 },
                new PartCompatibility { Id = 2, PartId = 2, CarModelId = 1, PartCategoryId = 1 },
                new PartCompatibility { Id = 3, PartId = 3, CarModelId = 1, PartCategoryId = 1 },
                new PartCompatibility { Id = 4, PartId = 4, CarModelId = 1, PartCategoryId = 1 },
                new PartCompatibility { Id = 5, PartId = 9, CarModelId = 1, PartCategoryId = 2 },
                new PartCompatibility { Id = 6, PartId = 10, CarModelId = 1, PartCategoryId = 2 },
                new PartCompatibility { Id = 7, PartId = 13, CarModelId = 1, PartCategoryId = 3 },
                new PartCompatibility { Id = 8, PartId = 14, CarModelId = 1, PartCategoryId = 3 },
                new PartCompatibility { Id = 9, PartId = 15, CarModelId = 1, PartCategoryId = 3 },
                new PartCompatibility { Id = 10, PartId = 20, CarModelId = 1, PartCategoryId = 4 },
                new PartCompatibility { Id = 11, PartId = 21, CarModelId = 1, PartCategoryId = 4 },
                new PartCompatibility { Id = 12, PartId = 22, CarModelId = 1, PartCategoryId = 4 },
                new PartCompatibility { Id = 13, PartId = 25, CarModelId = 1, PartCategoryId = 5 },
                new PartCompatibility { Id = 14, PartId = 26, CarModelId = 1, PartCategoryId = 5 },
                new PartCompatibility { Id = 15, PartId = 27, CarModelId = 1, PartCategoryId = 5 },
                new PartCompatibility { Id = 16, PartId = 29, CarModelId = 1, PartCategoryId = 6 },
                new PartCompatibility { Id = 17, PartId = 30, CarModelId = 1, PartCategoryId = 6 },
                new PartCompatibility { Id = 18, PartId = 32, CarModelId = 1, PartCategoryId = 7 },
                new PartCompatibility { Id = 19, PartId = 33, CarModelId = 1, PartCategoryId = 7 },
                new PartCompatibility { Id = 20, PartId = 35, CarModelId = 1, PartCategoryId = 8 },
                new PartCompatibility { Id = 21, PartId = 36, CarModelId = 1, PartCategoryId = 8 },
                new PartCompatibility { Id = 22, PartId = 37, CarModelId = 1, PartCategoryId = 8 },

                // BMW E90 320d (CarModelId = 2)
                new PartCompatibility { Id = 23, PartId = 1, CarModelId = 2, PartCategoryId = 1 },
                new PartCompatibility { Id = 24, PartId = 2, CarModelId = 2, PartCategoryId = 1 },
                new PartCompatibility { Id = 25, PartId = 3, CarModelId = 2, PartCategoryId = 1 },
                new PartCompatibility { Id = 26, PartId = 13, CarModelId = 2, PartCategoryId = 3 },
                new PartCompatibility { Id = 27, PartId = 14, CarModelId = 2, PartCategoryId = 3 },
                new PartCompatibility { Id = 28, PartId = 20, CarModelId = 2, PartCategoryId = 4 },
                new PartCompatibility { Id = 29, PartId = 21, CarModelId = 2, PartCategoryId = 4 },
                new PartCompatibility { Id = 30, PartId = 35, CarModelId = 2, PartCategoryId = 8 },
                new PartCompatibility { Id = 31, PartId = 36, CarModelId = 2, PartCategoryId = 8 },

                // VW Golf Mk5 (CarModelId = 3)
                new PartCompatibility { Id = 32, PartId = 5, CarModelId = 3, PartCategoryId = 1 },
                new PartCompatibility { Id = 33, PartId = 6, CarModelId = 3, PartCategoryId = 1 },
                new PartCompatibility { Id = 34, PartId = 11, CarModelId = 3, PartCategoryId = 2 },
                new PartCompatibility { Id = 35, PartId = 16, CarModelId = 3, PartCategoryId = 3 },
                new PartCompatibility { Id = 36, PartId = 17, CarModelId = 3, PartCategoryId = 3 },
                new PartCompatibility { Id = 37, PartId = 23, CarModelId = 3, PartCategoryId = 4 },
                new PartCompatibility { Id = 38, PartId = 28, CarModelId = 3, PartCategoryId = 5 },
                new PartCompatibility { Id = 39, PartId = 31, CarModelId = 3, PartCategoryId = 6 },
                new PartCompatibility { Id = 40, PartId = 34, CarModelId = 3, PartCategoryId = 7 },
                new PartCompatibility { Id = 41, PartId = 38, CarModelId = 3, PartCategoryId = 8 },

                // VW Passat B6 (CarModelId = 4)
                new PartCompatibility { Id = 42, PartId = 5, CarModelId = 4, PartCategoryId = 1 },
                new PartCompatibility { Id = 43, PartId = 6, CarModelId = 4, PartCategoryId = 1 },
                new PartCompatibility { Id = 44, PartId = 11, CarModelId = 4, PartCategoryId = 2 },
                new PartCompatibility { Id = 45, PartId = 16, CarModelId = 4, PartCategoryId = 3 },
                new PartCompatibility { Id = 46, PartId = 17, CarModelId = 4, PartCategoryId = 3 },
                new PartCompatibility { Id = 47, PartId = 23, CarModelId = 4, PartCategoryId = 4 },
                new PartCompatibility { Id = 48, PartId = 38, CarModelId = 4, PartCategoryId = 8 },

                // Toyota Corolla E120 (CarModelId = 5)
                new PartCompatibility { Id = 49, PartId = 7, CarModelId = 5, PartCategoryId = 1 },
                new PartCompatibility { Id = 50, PartId = 12, CarModelId = 5, PartCategoryId = 2 },
                new PartCompatibility { Id = 51, PartId = 18, CarModelId = 5, PartCategoryId = 3 },
                new PartCompatibility { Id = 52, PartId = 24, CarModelId = 5, PartCategoryId = 4 },

                // Toyota Camry XV50 (CarModelId = 6)
                new PartCompatibility { Id = 53, PartId = 7, CarModelId = 6, PartCategoryId = 1 },
                new PartCompatibility { Id = 54, PartId = 12, CarModelId = 6, PartCategoryId = 2 },
                new PartCompatibility { Id = 55, PartId = 18, CarModelId = 6, PartCategoryId = 3 },
                new PartCompatibility { Id = 56, PartId = 24, CarModelId = 6, PartCategoryId = 4 },

                // Ford Focus Mk2 (CarModelId = 7)
                new PartCompatibility { Id = 57, PartId = 8, CarModelId = 7, PartCategoryId = 1 },
                new PartCompatibility { Id = 58, PartId = 19, CarModelId = 7, PartCategoryId = 3 },

                // Ford Mondeo Mk4 (CarModelId = 8)
                new PartCompatibility { Id = 59, PartId = 8, CarModelId = 8, PartCategoryId = 1 },
                new PartCompatibility { Id = 60, PartId = 19, CarModelId = 8, PartCategoryId = 3 },

                // Audi A4 B7 (CarModelId = 9)
                new PartCompatibility { Id = 61, PartId = 1, CarModelId = 9, PartCategoryId = 1 },
                new PartCompatibility { Id = 62, PartId = 2, CarModelId = 9, PartCategoryId = 1 },
                new PartCompatibility { Id = 63, PartId = 11, CarModelId = 9, PartCategoryId = 2 },
                new PartCompatibility { Id = 64, PartId = 13, CarModelId = 9, PartCategoryId = 3 },
                new PartCompatibility { Id = 65, PartId = 23, CarModelId = 9, PartCategoryId = 4 },
                new PartCompatibility { Id = 66, PartId = 28, CarModelId = 9, PartCategoryId = 5 },

                // Audi A6 C6 (CarModelId = 10)
                new PartCompatibility { Id = 67, PartId = 1, CarModelId = 10, PartCategoryId = 1 },
                new PartCompatibility { Id = 68, PartId = 2, CarModelId = 10, PartCategoryId = 1 },
                new PartCompatibility { Id = 69, PartId = 11, CarModelId = 10, PartCategoryId = 2 },
                new PartCompatibility { Id = 70, PartId = 13, CarModelId = 10, PartCategoryId = 3 },
                new PartCompatibility { Id = 71, PartId = 23, CarModelId = 10, PartCategoryId = 4 },
                new PartCompatibility { Id = 72, PartId = 38, CarModelId = 10, PartCategoryId = 8 }
            );
        }
    }
}