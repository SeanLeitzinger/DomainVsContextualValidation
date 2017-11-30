using FluentValidation;
using System;
using System.Collections.Generic;

namespace DomainVsContextualValidation.Core
{
    public class User
    {
        public List<CloudApp> CloudApps { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string State { get; set; }

        public string CountryCode { get; set; }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().Must(m => m.Length <= 25);
            RuleFor(m => m.LastName).NotEmpty().Must(m => m.Length <= 50);
        }
    }
}
