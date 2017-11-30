using DomainVsContextualValidation.Core;
using DomainVsContextualValidation.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RequestInjector.NetCore;
using System.Threading.Tasks;

namespace DomainVsContextualValidation.Web.Requests
{
    public class AddUserForFinancialAppRequest : IRequest
    {
        public User User { get; set; }

        IUserRepository userRepository;

        public AddUserForFinancialAppRequest(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> Handle()
        {
            //Do work to process request

            return new OkObjectResult(await userRepository.AddAsync(User));
        }
    }

    public class AddUserForFinancialAppRequestValidator : AbstractValidator<AddUserForFinancialAppRequest>
    {
        public AddUserForFinancialAppRequestValidator()
        {
            RuleFor(m => m.User).SetValidator(new UserValidator());
            RuleFor(m => m.User.CountryCode.ToUpper()).Equal("US");
        }
    }
}
