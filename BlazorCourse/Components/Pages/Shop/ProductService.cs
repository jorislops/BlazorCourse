using System.Text.Json;

namespace BlazorCourse.Components.Pages.Shop;

public class ProductService
{
    public List<ShopProduct> GetAllProducts()
    {
        HttpClient httpClient = new HttpClient();
        string productAsJson = httpClient.GetStringAsync("https://fakestoreapi.com/products").Result;
        List<ShopProduct> products = JsonSerializer.Deserialize<List<ShopProduct>>(productAsJson, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        })!;

        return products;
    }
}