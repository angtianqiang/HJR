namespace efTest.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<efTest.TestDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(efTest.TestDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Products.RemoveRange(context.Products.ToArray());

            context.Colors.Add(new Color { ColorDes = "1..NiW" });
            context.Colors.Add(new Color { ColorDes = "2..NiW" });
            context.Colors.Add(new Color { ColorDes = "3..NiW" });
            context.Colors.Add(new Color { ColorDes = "4..NiW" });
            context.Colors.Add(new Color { ColorDes = "5..NiW" });
            context.Colors.Add(new Color { ColorDes = "6..NiW" });

            context.Products.Add(new Product { Des = "HBB3*5-2C", ColorID =1 });
            context.Products.Add(new Product { Des = "HBB3*6-2C", ColorID = 2 });
            context.Products.Add(new Product { Des = "HBB3*7-2C", ColorID = 3 });
            context.Products.Add(new Product { Des = "HBB3*8-2C", ColorID = 4 });
            context.Products.Add(new Product { Des = "HBB3*9-2C", ColorID = 5 });
            context.SaveChanges();
        }
    }
}
