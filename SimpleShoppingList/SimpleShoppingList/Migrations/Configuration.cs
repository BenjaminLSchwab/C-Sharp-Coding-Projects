namespace SimpleShoppingList.Migrations
{
    using SimpleShoppingList.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleShoppingList.Models.SimpleShoppingListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleShoppingList.Models.SimpleShoppingListContext context)
        {
            context.ShoppingLists.AddOrUpdate(
                new ShoppingList
                {
                    Name = "Groceries",
                    Items =
                    {
                        new Item{Name = "Milk" },
                        new Item{Name = "Stuff" }
                    }
                },
                new ShoppingList
                {
                    Name = "Other Crap",
                    Items =
                    {
                        new Item{Name = "Banjo" },
                        new Item{Name = "Pop-Socket" }
                    }
                }
                );
        }
    }
}
