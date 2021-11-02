using EcommerceAPI.Models;
using MediatR;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace EcommerceAPI.CQRS.Commands
{
    public class MakePurchaseCommand : IRequest<(decimal, int)>
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public decimal Balance { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public List<Product> PurchaseHistories { get; set; }
        public List<PurchaseHistory> Products { get; set; }
    }
    public class MakePurchaseCommandHandler : IRequestHandler<MakePurchaseCommand, (decimal, int)>
    {
        private EcommerceDbContext context;
        public MakePurchaseCommandHandler(EcommerceDbContext context)
        {
            this.context = context;
        }
        public async Task<(decimal, int)> Handle(MakePurchaseCommand command, CancellationToken cancellationToken)
        {
            //Sets the account and vehicle equal to the information passed in by the user
            var account = context.Accounts.Where(a => a.UserId == command.UserId).FirstOrDefault();
            var product = context.Products.Where(p => p.Id == command.Id ).FirstOrDefault();
            var purchaseHistory = context.PurchaseHistories.Where(ph => ph.AccountId == command.AccountId).FirstOrDefault();

            //Checks if the account can be found. If not, an error message asks the user to re-enter their account Id
            if (account == null)
            {
                throw new ArgumentException("Please enter a valid account");
            }
            else if (command.Quantity > product.Stock)
            {
                throw new ArgumentException("Insufficient stock to complete transaction. Reduce the quantity or contact us to enquire when more stock will be available");
            }
            //Checks if the user's balance is enough to complete the transaction
            else if (account.Balance - (command.Balance * command.Quantity) < 0)
            {
                throw new ArgumentException("Insufficient funds to make purchase. Please deposit money into your account.");
            }
            else
            {
                account.Balance -= command.Balance;
                await context.SaveChangesAsync();

                product.Stock -= command.Quantity;
                await context.SaveChangesAsync();

                return (account.Balance, product.Stock);
            }
        }
    }
} 
