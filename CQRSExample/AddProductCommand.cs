using MediatR;

namespace CQRSExample;

public class AddProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}