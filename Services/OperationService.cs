using Core.Filters.Operation;
using Core.Services;
using FluentValidation.Results;
using Service.Validators;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class OperationService : IOperationService
    {
        public Task<decimal> CalculateInterest(OperationFilter filter)
        {
            throw new NotImplementedException();
        }

        private async Task<ValidationResult> ValidateFields(OperationFilter filter)
        {
            var validator = new OperationValidatorService();
            var result = await validator.ValidateAsync(filter).ConfigureAwait(true);
            return result;
        }

    }
}
