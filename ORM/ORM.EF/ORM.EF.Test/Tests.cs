using ORM.EF.DAL;
using ORM.EF.DAL.Models;
using ORM.EF.DAL.Repositories;

namespace ORM.EF.Test;

public class Tests
{
    private readonly Guid orderGuid = Guid.Parse("B2F83DF9-A315-4260-AE1C-48D340D38CE5");
    private readonly Guid productGuid = Guid.Parse("A2F83DF9-A315-4260-AE1C-48D340D38CE5");

    [Fact]
    public async Task CreateProducts()
    {
        using (var context = new ShopContext())
        {
            var pr = new ProductRepository(context);

            await pr.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Desc",
                Name = "Order 1",
                Height = 2,
                Length = 3,
                Weight = 4,
                Width = 5
            });
            await pr.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Desc",
                Name = "Order 2",
                Height = 2,
                Length = 3,
                Weight = 4,
                Width = 5
            });
            await pr.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Desc",
                Name = "Order 3",
                Height = 7,
                Length = 7,
                Weight = 7,
                Width = 7
            });
            await pr.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Desc",
                Name = "Order 4",
                Height = 1,
                Length = 2,
                Weight = 1,
                Width = 2
            });
            await pr.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Desc",
                Name = "Order 5",
                Height = 5,
                Length = 1,
                Weight = 2,
                Width = 7
            });
            await pr.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Desc",
                Name = "Order 6",
                Height = 8,
                Length = 9,
                Weight = 3,
                Width = 5
            });
            await pr.Create(new Product()
            {
                Id = productGuid,
                Description = "Desc",
                Name = "Order 1",
                Height = 2,
                Length = 3,
                Weight = 4,
                Width = 5
            });

            await pr.SaveChanges();
        }
    }

    [Fact]
    public async Task CreateOrders()
    {
        using (var context = new ShopContext())
        {
            var pr = new ProductRepository(context);

            var products = await pr.Fetch();

            var or = new OrderRepository(context);

            await or.Create(new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(5),
                UpdatedDate = DateTime.Now.AddDays(5),
                Status = "Not Started",
                ProductId = products[new Random().Next(products.Count - 1)].Id
            });

            await or.Create(new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(8),
                UpdatedDate = DateTime.Now.AddDays(8),
                Status = "Loading",
                ProductId = products[new Random().Next(products.Count - 1)].Id
            });

            await or.Create(new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(560),
                UpdatedDate = DateTime.Now.AddDays(560),
                Status = "Arrived",
                ProductId = products[new Random().Next(products.Count - 1)].Id
            });

            await or.Create(new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(570),
                UpdatedDate = DateTime.Now.AddDays(570),
                Status = "Unloading",
                ProductId = products[new Random().Next(products.Count - 1)].Id
            });

            await or.Create(new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(120),
                UpdatedDate = DateTime.Now.AddDays(120),
                Status = "Cancelled",
                ProductId = products[new Random().Next(products.Count - 1)].Id
            });

            await or.Create(new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.AddDays(41),
                UpdatedDate = DateTime.Now.AddDays(41),
                Status = "Done",
                ProductId = products[new Random().Next(products.Count - 1)].Id
            });
            await or.Create(new Order()
            {
                Id = orderGuid,
                CreatedDate = DateTime.Now.AddDays(88),
                UpdatedDate = DateTime.Now.AddDays(88),
                Status = "Not Started",
                ProductId = productGuid
            });

            await or.SaveChanges();
        }
    }

    [Fact]
    public async Task UpdateOrder()
    {
        using (var context = new ShopContext())
        {
            var or = new OrderRepository(context);

            var order = await or.Select(orderGuid);
            order.CreatedDate = new DateTime(2000, 1, 1);
            or.Update(order);

            await or.SaveChanges();
        }
    }

    [Fact]
    public async Task UpdateProduct()
    {
        using (var context = new ShopContext())
        {
            var pr = new ProductRepository(context);

            var product = await pr.Select(productGuid);
            product.Description = "new desc";
            pr.Update(product);

            await pr.SaveChanges();
        }
    }

    [Fact]
    public async Task DeleteOrder()
    {
        using (var context = new ShopContext())
        {
            var or = new OrderRepository(context);

            var order = await or.Select(orderGuid);
            or.Delete(order);

            await or.SaveChanges();
        }
    }

    [Fact]
    public async Task DeleteProduct()
    {
        using (var context = new ShopContext())
        {
            var pr = new ProductRepository(context);

            var product = await pr.Select(productGuid);
            pr.Delete(product);

            await pr.SaveChanges();
        }
    }

    [Fact]
    public async Task DeleteOrders()
    {
        using (var context = new ShopContext())
        {
            var or = new OrderRepository(context);

            await or.BulkDelete(new OrderInfo()
            {
                Year = 2023
            });

            await or.SaveChanges();
        }
    }
}