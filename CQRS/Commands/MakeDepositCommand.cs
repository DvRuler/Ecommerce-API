using EcommerceAPI.Models;
using MediatR;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace EcommerceAPI.CQRS.Commands
{
    public class MakeDepositCommand : IRequest<decimal>
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
    }

    public class MakeDepositCommandHandler : IRequestHandler<MakeDepositCommand, decimal>
    {
        private EcommerceDbContext context;
        public MakeDepositCommandHandler(EcommerceDbContext context)
        {
            this.context = context;
        }
        public async Task<decimal> Handle(MakeDepositCommand command, CancellationToken cancellationToken)
        {
            var account = context.Accounts.Where(a => a.Id == command.Id).FirstOrDefault();

            //Checks if the user account can be found, if not it instructs the user to re-enter their account Id. If the account is found, it increments the balance to the amount passed in
            if (account == null)
            {
                throw new ArgumentException("Please enter a valid account");
            }
            else
            {       
                account.Balance += command.Balance;
                   
                await context.SaveChangesAsync();
                return account.Balance;
            }

        }
    }
}
