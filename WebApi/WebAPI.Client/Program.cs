using System.Net.Http.Headers;
using System.Text.Json;
using WebApi.DTO;

var client = new HttpClient();

var newProduct = new ProductDto
{
    CategoryId = 5,
    Discontinued = true,
    ProductName = "name",
    QuantityPerUnit = "qw",
    ReorderLevel = 1,
    SupplierId = 1,
    UnitPrice = 12,
    UnitsInStock = 2,
    UnitsOnOrder = 3,
};

var updatedProduct = new ProductDto
{
    ProductId = 81,
    CategoryId = 1,
    Discontinued = true,
    ProductName = Guid.NewGuid().ToString(),
    QuantityPerUnit = "qw",
    ReorderLevel = 1,
    SupplierId = 1,
    UnitPrice = 12,
    UnitsInStock = 2,
    UnitsOnOrder = 3,
};

var getCategories = "https://localhost:7154/api/Categories";
var getProducts = "https://localhost:7154/api/Products?page=1&size=10";
var getProduct = "https://localhost:7154/api/Products/81";
var postProduct = "https://localhost:7154/api/Products";
var putProduct = "https://localhost:7154/api/Products";

var response = await client.GetAsync(getCategories);
Console.WriteLine(response.RequestMessage?.Method + ": " + response.RequestMessage?.RequestUri);
Console.WriteLine(response);
Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.WriteLine();

response = await client.GetAsync(getProducts);
Console.WriteLine(response.RequestMessage?.Method + ": " + response.RequestMessage?.RequestUri);
Console.WriteLine(response);
Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.WriteLine();

response = await client.GetAsync(getProduct);
Console.WriteLine(response.RequestMessage?.Method + ": " + response.RequestMessage?.RequestUri);
Console.WriteLine(response);
Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.WriteLine();

var content = JsonSerializer.Serialize(newProduct);
var buffer = System.Text.Encoding.UTF8.GetBytes(content);
var byteContent = new ByteArrayContent(buffer);
byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

response = await client.PostAsync(postProduct, byteContent);
Console.WriteLine(response.RequestMessage?.Method + ": " + response.RequestMessage?.RequestUri);
Console.WriteLine(response);
Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.WriteLine();

content = JsonSerializer.Serialize(updatedProduct);
buffer = System.Text.Encoding.UTF8.GetBytes(content);
byteContent = new ByteArrayContent(buffer);
byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

response = await client.PutAsync(putProduct, byteContent);
Console.WriteLine(response.RequestMessage?.Method + ": " + response.RequestMessage?.RequestUri);
Console.WriteLine(response);
Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.WriteLine();

response = await client.GetAsync(getProduct);
Console.WriteLine(response.RequestMessage?.Method + ": " + response.RequestMessage?.RequestUri);
Console.WriteLine(response);
Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.WriteLine();