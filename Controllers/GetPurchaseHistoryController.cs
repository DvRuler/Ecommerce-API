using EcommerceAPI.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPurchaseHistoryController : ControllerBase
    {
        private IMediator mediator;
        public GetPurchaseHistoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("get-Purchase-History")]
        public async Task<IActionResult> Get(string brand, string name, decimal price, int quantity, DateTime datePurchased, int accountId, int pageNumber, int resultsPerPage)
        {           
            return Ok(await mediator.Send(new GetPurchaseHistory{ 
            Brand = brand,
            Name = name,
            Price = price,
            Quantity = quantity,
            DatePurchased = datePurchased,
            AccountId = accountId,
            PageNumber = pageNumber,
            ResultsPerPage = resultsPerPage
            })); 
        }
    };
}