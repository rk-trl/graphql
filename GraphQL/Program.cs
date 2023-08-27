
global using GraphQL.Types;
using GraphQL;
using GraphQL.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IProductProvider, ProductProvider>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<ProductQuery>();
builder.Services.AddSingleton<ISchema, ProductSchema>();

builder.Services.AddGraphQL(opt => opt.EnableMetrics = true).AddSystemTextJson();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseGraphQLAltair();
app.UseGraphQL<ISchema>();

app.MapGet("/api/products", (IProductProvider productProvider) =>
{
    return productProvider.GetProducts();
})
    .WithName("GetProducts");

app.Run();
