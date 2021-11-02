using EcommerceAPI.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace EcommerceAPI.CQRS.Commands
{
    public class AddProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Supplier { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private EcommerceDbContext context;
        public AddProductCommandHandler(EcommerceDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            //Sets vehicle details to the information filled in by the user
            var product= new Product();
            

            context.Products.Add(product);

            await context.SaveChangesAsync();
            return product.Id;

        }
    }
}

