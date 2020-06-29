using System.Threading.Tasks;
using Core.Filters.Operation;

namespace Core.Services
{
    public interface IOperationService
    {
        Task<decimal> CalculateInterest(OperationFilter filter);
    }
}