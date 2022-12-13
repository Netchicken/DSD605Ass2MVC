using DSD605Ass2MVC.Data;
using DSD605Ass2MVC.DTO;
using DSD605Ass2MVC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace DSD605Ass2MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {



            //======================================================


            return View();
        }

        public IActionResult Privacy()
        {  //which staff member sold the most stock?

            var StaffSoldMostStock = _context.Order.GroupBy(o => o.Staff.Name).Select(n => new MostSoldDTO
            {
                ProductName = n.Key,
                Count = n.Count()
            }).OrderByDescending(n => n.Count).Take(1);

            ViewData["StaffWhoSoldMostStock"] = StaffSoldMostStock;

            ////what is the most popular stock?

            //var MostPopularStock = _context.Order.OrderByDescending(o => o.Stock.Price).Select(s => s.Staff.Name).Take(1).FirstOrDefault();





            //get all the sales and take the top 15 most expensive
            //var allSalesTop15 = _context.Order.Include(o => o.Stock).OrderByDescending(o => o.Stock.Price).Take(15);


            //List<TopSalesDTO> top15Stock = new List<TopSalesDTO>();

            //foreach (var item in allSalesTop15)
            //{
            //    TopSalesDTO sales = new TopSalesDTO();
            //    sales.ProductName = item.Stock.ProductName;
            //    sales.ProductDescription = item.Stock.ProductDescription;
            //    sales.ProductType = item.Stock.ProductType;
            //    sales.Price = item.Stock.Price;
            //    top15Stock.Add(sales);
            //}

            //========================================


            //List<MostSoldDTO> mostStockSoldTop15 = new List<MostSoldDTO>();

            //mostStockSoldTop15.AddRange((IEnumerable<MostSoldDTO>)top15Stock.GroupBy(n => n.ProductName)
            //                         .Select(n => new MostSoldDTO
            //                         {
            //                             ProductName = n.Key,
            //                             Count = n.Count()
            //                         })
            //                         .OrderByDescending(n => n.Count)
            //                         );

            //ViewData["mostStockSoldTop15"] = mostStockSoldTop15.Take(5);



            //11.	Create a list of stock items names that have NOT been sold.

            //Get the names of all the stock sold
            var StockSold = _context.Order.Include(o => o.Stock).Select(s => s.Stock.ProductName).ToList();
            //Get the names of all the stock
            var AllStock = _context.Stock.Select(s => s.ProductName).ToList();

            List<string> StockNotSold = new List<string>();

            //For each of the Stock we have, if its not in the StockSold, then add it to StockNotSold
            foreach (var s in AllStock)
            {
                if (!StockSold.Contains(s))
                {
                    StockNotSold.Add(s);
                }
            }

            ViewData["StockNotSold"] = StockNotSold.ToList();


            //12.	What is the name of the staff member, who has sold the least $ value of stock?

            //Get the names of all the stock sold
            var StaffSoldCheapestStock = _context.Order.OrderBy(o => o.Stock.Price).Select(s => s.Staff.Name).Take(1).FirstOrDefault();


            ViewData["StaffWhoSoldCheapestStock"] = StaffSoldCheapestStock;






            //   13.Create a list of customers names and the products they have purchased, grouped by Customer.
            List<CustomersAndPurchasesDTO> CustomersAndStock = new List<CustomersAndPurchasesDTO>();

            var customersAndStock = _context.Order.OrderBy(c => c.CustomerId).Select(s => new CustomersAndPurchasesDTO
            {
                Name = s.Customer.Name,
                ProductName = s.Stock.ProductName
            }).ToList();

            CustomersAndStock.AddRange(customersAndStock);
            ViewData["CustomersAndPurchases"] = CustomersAndStock;

            //14.	What is the name of the customer who has bought the greatest quantity of products?
            var CustomerBoughtMostItems = _context.Order.GroupBy(c => c.Customer.Name).Select(n => new MostSoldDTO
            {
                ProductName = n.Key,
                Count = n.Count()
            }).OrderByDescending(n => n.Count).Take(1);

            ViewData["CustomerBoughtMostItems"] = CustomerBoughtMostItems;



            //15.	What is the name of the customer who has spent the most money at the Bike Shop?
            var CustomerWhoSpentMostMoney = _context.Order.GroupBy(c => c.Customer.Name).Select(n => new CustomerWhoSpentMostDTO
            {
                Name = n.Key,
                Cost = n.Sum(x => x.Stock.Price)
            }).OrderByDescending(c => c.Cost).Take(1);

            ViewData["CustomerWhoSpentMost"] = CustomerWhoSpentMostMoney;


            //16.Create a list of the 5 dearest stock items with their names and prices, from most expensive to least expensive.
            var allSalesTop5 = _context.Order.Select(n => new TopSalesDTO
            {
                ProductName = n.Stock.ProductName,
                ProductDescription = n.Stock.ProductDescription,
                ProductType = n.Stock.ProductType,
                Price = n.Stock.Price
            }).Distinct().OrderByDescending(o => o.Price).Take(5);

            ViewData["AllSalesTop5"] = allSalesTop5;



            // 17.List all the stock that have Saddle in the Name or Description
            var SaddleStock = _context.Stock.Where(s => s.ProductName.Contains("saddle") || s.ProductDescription.Contains("saddle"));

            ViewData["SaddleStock"] = SaddleStock;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}