using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrdersDelivery.Models;
using OrdersDelivery.Services.ServiceInterfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace OrdersDelivery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrdersService _ordersService;

        public HomeController(ILogger<HomeController> logger, IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        public IActionResult Index() => View(_ordersService.GetOrders());

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            try
            {
                _ordersService.AddOrder(order);
            }
            catch (Exception ex) // I decided only to write Exception into log because all fields in Order object are required.
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SearchOrders(string filter) => View("Index", _ordersService.SearchOrders(filter));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
