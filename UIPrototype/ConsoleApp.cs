using Kamra.Core.Models;
using Kamra.Core.Services;
using System;

namespace Kamra.UI.ConsoleApp
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            var productService = new ProductService();
            var categoryService = new CategoryService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Kamra Inventory Management");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. List Products");
                Console.WriteLine("3. Add Category");
                Console.WriteLine("4. List Categories");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(productService, categoryService);
                        break;
                    case "2":
                        ListProducts(productService);
                        break;
                    case "3":
                        AddCategory(categoryService);
                        break;
                    case "4":
                        ListCategories(categoryService);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void AddProduct(ProductService productService, CategoryService categoryService)
        {
            Console.Write("Enter product name: ");
            var name = Console.ReadLine();

            Console.Write("Enter product description: ");
            var description = Console.ReadLine();

            Console.Write("Enter product expiration date (yyyy-MM-dd): ");
            DateTime expirationDate;
            while (!DateTime.TryParse(Console.ReadLine(), out expirationDate))
            {
                Console.WriteLine("Invalid date. Please enter a valid date (yyyy-MM-dd):");
            }

            Console.Write("Enter product quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Invalid quantity. Please enter a number:");
            }

            Console.Write("Enter product barcode: ");
            var barcode = Console.ReadLine();

            Console.Write("Enter category name: ");
            var categoryName = Console.ReadLine();
            var category = categoryService.GetCategoryByName(categoryName);
            if (category == null)
            {
                Console.WriteLine("Category not found. Please add the category first.");
                Console.ReadKey();
                return;
            }

            var product = new Product
            {
                Name = name,
                Description = description,
                ExpirationDate = expirationDate,
                Quantity = quantity,
                Barcode = barcode,
                Category = category
            };

            productService.AddProduct(product);
            Console.WriteLine("Product added successfully. Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void ListProducts(ProductService productService)
        {
            var products = productService.GetAllProducts();
            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"- {product.Name} ({product.Category.Name}): {product.Quantity} units");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void AddCategory(CategoryService categoryService)
        {
            Console.Write("Enter category name: ");
            var name = Console.ReadLine();

            var category = new Category { Name = name };
            categoryService.AddCategory(category);
            Console.WriteLine("Category added successfully. Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void ListCategories(CategoryService categoryService)
        {
            var categories = categoryService.GetAllCategories();
            Console.WriteLine("Categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"- {category.Name}");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
    }
}
