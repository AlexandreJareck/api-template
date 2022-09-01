using FluentValidation;

namespace DevIO.Business.Models;

public class Product : Entity
{
    public Guid ProviderId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Value { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool Active { get; set; }

    public Provider Provider { get; set; }
}


public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 1000).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Value)
            .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
    }
}