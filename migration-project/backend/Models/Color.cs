namespace Backend.Models;

public class Color
{
    public int ColorId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
