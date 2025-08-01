namespace Backend.Models;

public class Model
{
    public int ModelId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}

