using MediatR;

namespace CQRSExample;

public class AddProductCommandHandler(AppDbContext db) : 
    IRequestHandler<AddProductCommand, Product>
{
    public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Category = request.Category,
            Name = request.Name,
            Price = request.Price
        };
        
        await db.Products.AddAsync(product, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        
        return product;
    }
}