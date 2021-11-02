using EcommerceAPI.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private IMediator mediator;
        public DepositController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPut("deposit-amount")]
        public async Task<IActionResult> Update(int Id, decimal amount, MakeDepositCommand command)
        {
            //Checks if the user entered a non-zero, positive amount to be added to their balance
            if (amount < 0)
            {
                throw new ArgumentException("Please enter a valid amount to deposit");
            }
            
            command.Id = Id;
            command.Balance = amount;
            return Ok(await mediator.Send(command));
        }
    };
 
}