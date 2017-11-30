using DomainVsContextualValidation.Core;
using System.Threading.Tasks;

namespace DomainVsContextualValidation.Data
{
    public interface IUserRepository
    {
        Task<int> AddAsync(User user);
    }
}
