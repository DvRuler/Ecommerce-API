using EcommerceAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace EcommerceAPI.CQRS.Queries
{
    public class GetPurchaseHistory : IRequest<string>
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePurchased { get; set; }
        public int AccountId { get; set; }
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }

        public class GetPurchaseHistoryQueryHandler : IRequestHandler<GetPurchaseHistory, string>
        {
            private EcommerceDbContext context;
            public GetPurchaseHistoryQueryHandler(EcommerceDbContext context)
            {
                this.context = context;
            }
            public async Task<string> Handle(GetPurchaseHistory query, CancellationToken cancellationToken)
            {
                //startIndex sets how many pages worth of search results will be skipped
                var startIndex = (query.PageNumber - 1) * query.ResultsPerPage;
                //Skip and Take methods are used to paginate the search results. Users can set how many results should display per page, as well as the page that they would like to see
                var productList = await context.PurchaseHistories.Where(pl =>
                pl.Brand == query.Brand ||
                pl.Name == query.Name ||
                pl.DatePurchased == query.DatePurchased ||
                pl.AccountId == query.AccountId)
                    .Skip(startIndex).Take(query.ResultsPerPage).ToListAsync();


                //Nested if-statements check if the information passed in can be found, if not an error message prompts the user to re-enter their search query. If found it returns a serialized list of all vehicles matching the query
                if (query.Brand == null)
                {
                    if (query.Name == null)
                    {
                        if (query.AccountId == 0)
                        {
                            throw new ArgumentException("Please search again using a different search term. Make sure to enter the correct details");
                        }
                        else
                        {
                            var options = new JsonSerializerOptions { WriteIndented = true };
                            string productListJson = JsonSerializer.Serialize(productList, options);
                            return productListJson;
                        }
                    }
                    else
                    {
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        string productListJson = JsonSerializer.Serialize(productList, options);
                        return productListJson;
                    }
                }
                else
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string productListJson = JsonSerializer.Serialize(productList, options);
                    return productListJson;
                }
            }
        }
    }
} 