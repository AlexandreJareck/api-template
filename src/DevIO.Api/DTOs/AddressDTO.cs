using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.DTOs
{
    public class AddressDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 1)]
        public string Number { get; set; }

        public string Complement { get; set; }
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracateres", MinimumLength = 2)]
        public string State { get; set; }

        public Guid ProviderId { get; set; }
    }
}
