using ADO.Models;
using ADO.Repositories;

namespace ADO.Test;

public class Tests
{
    private static readonly string _connectionString = "Server=EPBYMINW096D;Database=Ado;Trusted_Connection=True;";
    OrdersRepository _ordersRepository = new OrdersRepository(_connectionString);
    ProductsRepository _productsRepository = new ProductsRepository(_connectionString);

    [Fact]
    public async Task CreatingProducts()
    {
        for (var i = 1; i < 10; i++)
        {
            await _productsRepository.Create(new Product
            {
                Id = Guid.NewGuid(),
                Description = $"product {i}",
                Height = i * 11,
                Length = i * 11,
                Name = $"name {i}",
                Weight = i * 11,
                Width = i * 11
            });
        }
    }

    [Fact]
    public async Task CreatingOrders()
    {
        var allProducts = await _productsRepository.Fetch();

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-123),
            Status = "NotStarted",
            UpdatedDate = DateTime.Now.AddDays(-117),
            ProductId = allProducts[2].Id
        });

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-123),
            Status = "Loading",
            UpdatedDate = DateTime.Now.AddDays(-122),
            ProductId = allProducts[3].Id
        });

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-133),
            Status = "InProgress",
            UpdatedDate = DateTime.Now.AddDays(-133),
            ProductId = allProducts[4].Id
        });

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-73),
            Status = "Arrived",
            UpdatedDate = DateTime.Now.AddDays(-72),
            ProductId = allProducts[4].Id
        });

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-31),
            Status = "Unloading",
            UpdatedDate = DateTime.Now.AddDays(-29),
            ProductId = allProducts[5].Id
        });

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-431),
            Status = "Cancelled",
            UpdatedDate = DateTime.Now.AddDays(-429),
            ProductId = allProducts[7].Id
        });

        await _ordersRepository.Create(new Order
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now.AddDays(-531),
            Status = "Done",
            UpdatedDate = DateTime.Now.AddDays(-529),
            ProductId = allProducts[9].Id
        });
    }

    [Fact]
    public async Task DeletingProduct()
    {
        var allProducts = await _productsRepository.Fetch();
        var product = allProducts[0];
        await _productsRepository.Delete(product);
    }

    [Fact]
    public async Task CreatingOrder()
    {
        var allOrders = await _ordersRepository.Fetch();
        var order = allOrders[0];
        await _ordersRepository.Delete(order);
    }

    [Fact]
    public async Task UpdateProduct()
    {
        var allProducts = await _productsRepository.Fetch();
        var product = allProducts[^1];
        product.Description = "Test description";
        await _productsRepository.Update(product);
    }

    [Fact]
    public async Task UpdateOrders()
    {
        var allOrders = await _ordersRepository.Fetch(new OrderInfo
        {
            Month = 3, 
            Year = 2022
        });

        foreach (var order in allOrders)
        {
            order.UpdatedDate = DateTime.Now;
            await _ordersRepository.Update(order);
        }
    }

    [Fact]
    public async Task BulkDeleteOrders()
    {
        await _ordersRepository.BulkDelete(new OrderInfo
        {
            Status = "InProgress"
        });
    }
}