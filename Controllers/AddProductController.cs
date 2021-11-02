using EcommerceAPI.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        private IMediator mediator;
        public AddProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("add-Product")]
        public async Task<IActionResult> Create(string brand, string name, decimal price, int stock, string supplier)
        {
            return Ok(await mediator.Send(new AddProductCommand
            {
                Brand = brand,
                Name = name,
                Price = price,
                Stock = stock,
                Supplier = supplier
            }));
        }
    }
}