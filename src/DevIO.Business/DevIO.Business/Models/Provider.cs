namespace DevIO.Business.Models;

public class Provider : Entity
{
    public string Name { get; set; }
    public string Document { get; set; }
    public bool Active { get; set; }

    public TypeProvider TypeProvider { get; set; }
    public Address Addreess { get; set; }

    public IEnumerable<Product> Products { get; set; }
}
