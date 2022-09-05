using DevIO.Business.Models.Validations;
using FluentValidation;

namespace DevIO.Business.Models;

public class Provider : Entity
{
    public string Name { get; set; }
    public string Document { get; set; }
    public bool Active { get; set; }

    public TypeProvider TypeProvider { get; set; }
    public Address? Address { get; set; }

    public IEnumerable<Product>? Products { get; set; }
}

public class ProviderValidation : AbstractValidator<Provider>
{
    public ProviderValidation()
    {
        RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        When(f => f.TypeProvider == TypeProvider.PhysicalPerson, () =>
        {
            RuleFor(f => f.Document.Length).Equal(CpfValidator.DocumentLength)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => CpfValidator.Validation(f.Document)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        });

        When(f => f.TypeProvider == TypeProvider.LegalPerson, () =>
        {
            RuleFor(f => f.Document.Length).Equal(CnpjValidator.Length)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => CnpjValidator.Validation(f.Document)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        });
    }
}
