using System;
using API.Models;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]s")]
    public class CartController : Controller
    {
        private string connectionString;
        private readonly CartService cartService;
        
        public CartController (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnectionString");
            cartService = new CartService(new CartRepository(connectionString));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cart = cartService.Get(id);

            if (cart != null)
            {
                return Ok(cart);
            }

            return NotFound();
        }
        
        public class Data
        {
            public int productId {get; set;}
        }
        
        [Route("{cartId}/products")]
        [HttpPost]
        public IActionResult AddProduct(int cartId, [FromBody] dynamic data )
        {
            cartService.AddProduct(cartId, (int)data.productId);
            return Ok(data.productId);

        }
    }
}