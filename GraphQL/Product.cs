

namespace GraphQL
{
    public record Product(int Id, string Name, int Quantity);

    public class ProductType: ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(x=> x.Id);
            Field(x => x.Name);
            Field(x => x.Quantity);
        }
    }

    //Responsible for returning the data
    public class ProductQuery: ObjectGraphType
    {
        public ProductQuery(IProductProvider productProvider)
        {
            Field<ListGraphType<ProductType>>(Name = "products", resolve: x => productProvider.GetProducts());
            Field<ProductType>(Name = "product",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: x => productProvider.GetProducts().FirstOrDefault(p => p.Id == x.GetArgument<int>("id")));            
        }

    }

    //Documentation - schema
    public class ProductSchema: Schema
    {
        public ProductSchema(IServiceProvider serviceProvider): base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<ProductQuery>();
        }
    }

    public interface IProductProvider
    {
        Product[] GetProducts();
    }

    public class ProductProvider : IProductProvider
    {
        public Product[] GetProducts() => new[]
        {
            new Product(1, "Fan",10),
            new Product(2, "TV",5),
            new Product(3, "AC",2),
            new Product(4, "Bike",1),
            new Product(5, "Car",2),
        };
    }
}
