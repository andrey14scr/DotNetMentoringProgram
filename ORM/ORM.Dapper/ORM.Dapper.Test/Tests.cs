using ORM.Dapper.DAL.Models;
using ORM.Dapper.DAL.Repositories;

namespace ORM.Dapper.Test;

public class Tests
{
    private readonly string connectionString = "Server=EPBYMINW096D;Database=Ado;Trusted_Connection=True;";

    [Fact]
    public async Task CreateProducts()
    {
        var pr = new ProductRepository(connectionString);

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 1",
            Description = "Desc",
            Width = 1,
            Height = 1,
            Length = 1,
            Weight = 1
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 2",
            Description = "Desc",
            Width = 2,
            Height = 2,
            Length = 2,
            Weight = 2
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 3",
            Description = "Desc",
            Width = 1,
            Height = 2,
            Length = 3,
            Weight = 4
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 4",
            Description = "Desc",
            Width = 4,
            Height = 3,
            Length = 2,
            Weight = 1
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 5",
            Description = "Desc",
            Width = 6,
            Height = 2,
            Length = 5,
            Weight = 3
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 6",
            Description = "Desc",
            Width = 9,
            Height = 2,
            Length = 8,
            Weight = 2
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 7",
            Description = "Desc",
            Width = 3,
            Height = 5,
            Length = 4,
            Weight = 2
        });

        await pr.Create(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product 8",
            Description = "Desc",
            Width = 4,
            Height = 7,
            Length = 7,
            Weight = 4
        });
    }

    [Fact]
    public async Task CreateOrders()
    {
        var or = new OrderRepository(connectionString);
        var pr = new ProductRepository(connectionString);
        var products = await pr.Fetch();

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(22),
            UpdatedDate = DateTime.Now.AddDays(22),
            Status = "Not Started",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(400),
            UpdatedDate = DateTime.Now.AddDays(400),
            Status = "Loading",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(333),
            UpdatedDate = DateTime.Now.AddDays(333),
            Status = "InProgress",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(11),
            UpdatedDate = DateTime.Now.AddDays(11),
            Status = "Arrived",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(6),
            UpdatedDate = DateTime.Now.AddDays(6),
            Status = "Unloading",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(245),
            UpdatedDate = DateTime.Now.AddDays(245),
            Status = "Cancelled",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });

        await or.Create(new Order()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(189),
            UpdatedDate = DateTime.Now.AddDays(189),
            Status = "Done",
            ProductId = products[new Random().Next(products.Count - 1)].Id
        });
    }

    [Fact]
    public async Task UpdateProduct()
    {
        var or = new OrderRepository(connectionString);
        var pr = new ProductRepository(connectionString);
        var products = await pr.Fetch();

        var productToUpdate = await pr.Select(products[new Random().Next(products.Count - 1)].Id);
        productToUpdate.Description = "New updated description";
        await pr.Update(productToUpdate);
    }

    [Fact]
    public async Task UpdateOrder()
    {
        var or = new OrderRepository(connectionString);
        var pr = new ProductRepository(connectionString);
        var products = await pr.Fetch();
        var orders = await or.Fetch();

        var orderToUpdate = await or.Select(orders[new Random().Next(products.Count - 1)].Id);
        orderToUpdate.CreatedDate = new DateTime(2000, 1, 1);
        await or.Update(orderToUpdate);
    }

    [Fact]
    public async Task DeleteOrders()
    {
        var or = new OrderRepository(connectionString);

        await or.BulkDelete(new OrderInfo()
        {
            Year = 2023
        });
    }
}