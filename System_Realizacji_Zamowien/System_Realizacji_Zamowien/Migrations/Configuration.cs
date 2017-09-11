namespace System_Realizacji_Zamowien.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System_Realizacji_Zamowien.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<System_Realizacji_Zamowien.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "System_Realizacji_Zamowien.Models.Context";
        }

        protected override void Seed(System_Realizacji_Zamowien.Models.Context context)
        {
            //CopyCategory(context);

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
        }
        //private void CopyCategory(System_Realizacji_Zamowien.Models.Context context) 
        //{
        //    var oldCategory = context.PrimaryCategory.Include("SubCategory").ToList();

        //    var newCategory = oldCategory.Select(m => new Category
        //    {
        //        Name = m.Name,
        //        SubCategories = m.SubCategory != null && m.SubCategory.Any() 
        //        ? m.SubCategory.Select(n => new Category
        //            {
        //                Name = n.Name
        //            }).ToList()
        //        : null
        //    });

        //    context.Categories.AddRange(newCategory);
        //    context.SaveChanges();
        //}
    }


}
