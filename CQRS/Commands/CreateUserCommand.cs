using EcommerceAPI.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace EcommerceAPI.CQRS.Commands
{
    public class CreateUserCommand : IRequest<(int, int)>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (int, int)>
    {
        private EcommerceDbContext context;
        public CreateUserCommandHandler(EcommerceDbContext context)
        {
            this.context = context;
        }
        public async Task<(int, int)> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            //Sets the user info equal to the information filled in by the user
            var user = new User();
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Username = command.Username;
            user.Password = command.Password;
            user.Email = command.Email;
            context.Users.Add(user);
            await context.SaveChangesAsync();

            user.Account = new Account();
            user.Account.Balance = command.Balance;
            context.Accounts.Add(user.Account);
            await context.SaveChangesAsync();

            return (user.Id, user.Account.Id);
        }
    }
}

