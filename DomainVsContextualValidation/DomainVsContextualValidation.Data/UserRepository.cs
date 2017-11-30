using DomainVsContextualValidation.Core;
using System.Threading.Tasks;

namespace DomainVsContextualValidation.Data
{
    public class UserRepository : IUserRepository
    {
        public async Task<int> AddAsync(User user)
        {
            return 0;
        }
    }
}
