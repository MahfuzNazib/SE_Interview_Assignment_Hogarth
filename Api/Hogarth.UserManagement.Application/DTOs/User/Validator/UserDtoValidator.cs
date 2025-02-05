using FluentValidation;

namespace Hogarth.UserManagement.Application.DTOs.User.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(150).WithMessage("First name cannot exceed 150 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(150).WithMessage("Last name cannot exceed 150 characters");

            RuleFor(x => x.Contact.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .MaximumLength(11).WithMessage("Phone cannot exceed 11 digit");

            RuleFor(x => x.Contact.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.Contact.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.Contact.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role is required");

            RuleFor(x => x.Sex)
                .NotEmpty().WithMessage("Sex is required");

        }
    }
}
