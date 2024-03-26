namespace BlazorCourse.Components.Pages.Shop;

public class ShopProduct
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string Category { get; set; } = null!;
    public string Image { get; set; } = null!;

    public Rating Rating { get; set; } = null!;
}

public class Rating
{
    public decimal Rate { get; set; }
    public int Count { get; set; }
}