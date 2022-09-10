using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.DTOs;

public class ProviderDTO
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 2)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 11)]
    public string Document { get; set; }
    public int TypeProvider { get; set; }
    public AddressDTO? Address { get; set; }
    public bool Active { get; set; }

    public IEnumerable<ProductDTO>? Products { get; set; }
}
