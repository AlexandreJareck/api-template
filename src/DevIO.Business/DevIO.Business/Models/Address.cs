using FluentValidation;

namespace DevIO.Business.Models;

public class Address : Entity
{
    public Guid ProviderId { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Zipcode { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    /* EF Relation */
    public Provider Provider { get; set; }
}


public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(c => c.Street)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Neighborhood)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Zipcode)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(8).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("A campo {PropertyName} precisa ser fornecida")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.State)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Number)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}