namespace BlazorCourse.Components.Pages.Shop;

public class ShopProduct
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }

    public Rating Rating { get; set; }
}

public class Rating
{
    public decimal Rate { get; set; }
    public int Count { get; set; }
}