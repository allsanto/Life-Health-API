using FluentValidation;
using FluentValidation.Validators;
using Life_Healthy_API.Domain.Models.Request;

namespace Life_Healthy_API.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            #region :: Validar UserLogin ::

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe o email.")
                .NotEqual("string").WithMessage("Informe o Email.")
                .MinimumLength(8).WithMessage("O email deve ter no minimo 8 caracteres.")
                .MaximumLength(150).WithMessage("O email deve ter no maximo 150 caracteres.")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("E-mail com formato invalido.")
                .DependentRules(() =>
                {
                RuleFor(x => x.Senha)
                    .NotNull().WithMessage("Informe uma senha")
                    .MinimumLength(3).WithMessage("A senha deve ter no minimo 6 caracteres")
                    .MaximumLength(18).WithMessage("A senha deve ter no maximo 18 caracteres")
                    .NotEqual("string").WithMessage("Informe a senha.");
                });
            #endregion
        }
    }
}
