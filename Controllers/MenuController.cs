using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class MenuController : Controller
    {
        // GET /tacos
        
        [HttpGet("/tacos")]
        public IActionResult OrderTacos()
        {
            var response = new MenuModel
            {
                Description = "Delicious Tacos",
                Price = 3.99M,
                PriceGoodUntil = DateTime.Now.AddDays(5)
            };
            return Ok(response);
        }
    }

    public class MenuModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PriceGoodUntil  { get; set; }
    }
}
