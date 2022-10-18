using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    public class SeedController : Controller
    {
        private DataContext context;

        public SeedController(DataContext ctx) => context = ctx;

        public IActionResult Index()
        {
            ViewBag.Count = context.Products.Count();
                
            return View(context.Products.Include(p => p.Category).OrderBy(p => p.Id).Take(20));
        }

        [HttpPost]
        [System.Obsolete]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if(count > 0)
            {
                context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
                context.Database.ExecuteSqlCommand("DROP PROCEDURE IF EXISTS CreateSeedData");
                context.Database.ExecuteSqlCommand($@"
CREATE PROCEDURE CreateSeedData
@RowCount decimal
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @i INT = 1;
	DECLARE @catId BIGINT;
	DECLARE @CatCount INT = @RowCount / 10;
	DECLARE @pprice DECIMAL(5,2);
	DECLARE @rprice DECIMAL(5,2);
	BEGIN TRANSACTION
		WHILE @i <= @CatCount
			BEGIN
				INSERT INTO Categories (Name, Description)
				VALUES (CONCAT('Category-', @i),'Test Data Category');
				SET @catid = SCOPE_IDENTITY();
				DECLARE @j INT = 1;
				WHILE @j <= 10
					BEGIN
						SET @pprice = RAND() * (500-5+1);
						SET @rprice = (RAND () * @pprice) + @pprice;

						INSERT INTO Products (Name, Categoryid, PurchasePrice, RetailPrice)
						VALUES (CONCAT('Product', @i, '-', @j),@catId, @pprice, @rprice)
						SET @j = @j + 1
					END
				SET @i = @i + 1
			END
		COMMIT
END
");
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand($"EXEC CreateSeedData @RowCount = {count}");
                context.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [System.Obsolete]
        public IActionResult ClearData()
        {
            context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("DELETE FROM Orders");
            context.Database.ExecuteSqlCommand("DELETE FROM Categories");
            context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateProductionData()
        {
            ClearData();
            context.Categories.AddRange(new Category[]
            {
                new Category
                {
                    Name = "Watersports",
                    Description = "Make a splash",
                    Products = new Product[]
                    {
                        new Product
                        {
                            Name = "Kayak",
                            Description = "A boat for one person",
                            PurchasePrice = 200,
                            RetailPrice = 275
                        },
                        new Product
                        {
                            Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            PurchasePrice = 40,
                            RetailPrice = 48.95m
                        }
                    }
                },
                new Category
                {
                    Name = "Soccer",
                    Description = "The world's favorite game",
                    Products = new Product[]
                    {
                        new Product
                        {
                            Name = "Soccer ball",
                            Description = "FIFA-approved size and weight",
                            PurchasePrice = 18,
                            RetailPrice = 19.50m
                        },
                        new Product
                        {
                            Name = "Corner Flags",
                            Description = "Give your playing field a professional touch",
                            PurchasePrice = 32.50m,
                            RetailPrice = 34.95m
                        },
                        new Product
                        {
                            Name = "Stadium",
                            Description = "Flat-packet 35000-seat stadium",
                            PurchasePrice = 75000,
                            RetailPrice = 79500
                        }
                    }
                },
                new Category
                {
                    Name = "Chess",
                    Description = "The thinky game",
                    Products = new Product[]
                    {
                        new Product
                        {
                            Name = "Thinkin cap",
                            Description = "Improve brain efficiency by 75%",
                            PurchasePrice = 10,
                            RetailPrice = 16
                        },
                        new Product
                        {
                            Name = "Unsteady chair",
                            Description = "Secretely give your opponent a disadvantage",
                            PurchasePrice = 28,
                            RetailPrice = 29.95m
                        },
                        new Product
                        {
                            Name = "Human chess board",
                            Description = "A fun game for the family",
                            PurchasePrice = 68.50m,
                            RetailPrice = 75
                        },
                        new Product
                        {
                            Name = "Bling-Bling king",
                            Description = "Gold plated, giamond-studded King",
                            PurchasePrice = 800,
                            RetailPrice = 1200
                        }
                    }
                }
            });
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
