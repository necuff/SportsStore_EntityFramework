using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Controllers
{
    public class OrdersController : Controller
    {
        private IRepository productRepository;
        private IOrdersRepository ordersReporitory;

        public OrdersController(IRepository productRepo, IOrdersRepository ordersRepo)
        {
            this.productRepository = productRepo;
            this.ordersReporitory = ordersRepo;
        }

        public IActionResult Index() => View(ordersReporitory.Orders);

        public IActionResult EditOrder(long id)
        {
            var products = productRepository.Products;

            Order order = id == 0 ? new Order() : ordersReporitory.GetOrder(id);
            
            IDictionary<long, OrderLine> linesMap = order.Lines?.ToDictionary(l => l.ProductId)
                ?? new Dictionary<long, OrderLine>();
            
            ViewBag.Lines = products.Select(p => linesMap.ContainsKey(p.Id)
                ? linesMap[p.Id]
                : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });
            
            return View(order);
        }

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            order.Lines = order.Lines.Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();
            if(order.Id == 0)
            {
                ordersReporitory.AddOrder(order);
            } else
            {
                ordersReporitory.UpdateOrder(order);
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            ordersReporitory.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
