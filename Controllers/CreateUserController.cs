using EcommerceAPI.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private IMediator mediator;
        public CreateUserController (IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("add-User")]
        public async Task<IActionResult> Create(string firstName, string lastName, string password, string username, string email, decimal balance)
        {
            return Ok(await mediator.Send(new CreateUserCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Username = username,
                Email = email,
                Balance = balance
            }));
        }
    }
}
