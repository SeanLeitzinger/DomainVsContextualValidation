using DomainVsContextualValidation.Core;
using DomainVsContextualValidation.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RequestInjector.NetCore;
using System.Threading.Tasks;

namespace DomainVsContextualValidation.Web.Requests
{
    public class AddUserForEveryoneAppRequest : IRequest
    {
        public User User { get; set; }

        IUserRepository userRepository;

        public AddUserForEveryoneAppRequest(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> Handle()
        {
            //Do work to process request

            return new OkObjectResult(await userRepository.AddAsync(User));
        }
    }

    public class AddUserForEveryoneAppRequestValidator : AbstractValidator<AddUserForEveryoneAppRequest>
    {
        public AddUserForEveryoneAppRequestValidator()
        {
            RuleFor(m => m.User).SetValidator(new UserValidator());
        }
    }
}
