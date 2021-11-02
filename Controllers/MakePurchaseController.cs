using EcommerceAPI.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakePurchaseController : ControllerBase
    {
        private IMediator mediator;
        public MakePurchaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPut("renew-licence")]
        public async Task<IActionResult> Update(int userId, int productId, int quantity, MakePurchaseCommand command)
        {
            command.UserId = userId;
            command.ProductId = productId;
            command.Quantity = quantity;
            
            return Ok(await mediator.Send(command));
        }
    };

}