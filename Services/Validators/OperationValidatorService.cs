using Core.Filters.Operation;
using FluentValidation;

namespace Service.Validators
{
    public class OperationValidatorService : AbstractValidator<OperationFilter>
    {
        public OperationValidatorService()
        {
            RuleFor(operation => operation.Tempo)
                .NotNull();

           RuleFor(operation => operation.ValorInicial)
                .NotNull();

        }    
    }
}
