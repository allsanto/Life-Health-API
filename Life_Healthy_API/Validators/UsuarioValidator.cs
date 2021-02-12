using FluentValidation;
using FluentValidation.Validators;
using Life_Healthy_API.Domain.Models.Request;

namespace Life_Healthy_API.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequest>
    {
        public UsuarioValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            #region Validar Usuario
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o Nome.")
                .MinimumLength(5).WithMessage("O nome deve ter no minimo 3 caracteres") // Não deixa cadastrar com alunos com menos de 5 letras.
                .MaximumLength(150).WithMessage("O nome deve ter no maximo 150 caracteres")
                .NotEqual("string").WithMessage("Informe o Nome.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.DataNascimento).NotNull().WithMessage("Informe sua data de nascimento")
                    .DependentRules(() =>
                    {
                        RuleFor(x => x.Altura)
                            .NotEmpty().WithMessage("Infome a Altura")
                            .GreaterThan(0).WithMessage("A altura deve ser maior que 0.")
                            .DependentRules(() => 
                            {
                                RuleFor(x => x.Genero)
                                    .NotEmpty().WithMessage("Informe o genero")
                                    .NotEqual("string").WithMessage("Informe o genero.")
                                    .MaximumLength(1).WithMessage("O genero deve ter no maximo 1 caracteres")
                                    .DependentRules(() => 
                                    {
                                        RuleFor(x => x.Email)
                                            .NotEmpty().WithMessage("Informe o email")
                                            .NotEqual("string").WithMessage("Informe o Email.")
                                            .MinimumLength(8).WithMessage("O email deve ter no minimo 8 caracteres")
                                            .MaximumLength(150).WithMessage("O email deve ter no maximo 150 caracteres")
                                            .EmailAddress(EmailValidationMode.Net4xRegex)
                                            .DependentRules(() => 
                                            {
                                                RuleFor(x => x.Senha)
                                                    .NotNull().WithMessage("Informe uma senha")
                                                    .MinimumLength(3).WithMessage("A senha deve ter no minimo 6 caracteres")
                                                    .MaximumLength(18).WithMessage("A senha deve ter no maximo 18 caracteres")
                                                    .NotEqual("string").WithMessage("Informe a senha.")
                                                    .Equal(x => x.ConfiSenha).WithMessage("As senhas não confere")
                                                    .DependentRules(() => 
                                                    {
                                                        RuleFor(x => x.ConfiSenha)
                                                            .NotNull().WithMessage("Informe a segunda senha")
                                                            .MinimumLength(3).WithMessage("A senha deve ter no minimo 6 caracteres")
                                                            .MaximumLength(18).WithMessage("A senha deve ter no maximo 18 caracteres")
                                                            .NotEqual("string").WithMessage("Informe a senha.")
                                                            .Equal(x => x.Senha).WithMessage("As senhas não confere");
                                                    });
                                            });
                                    });
                            });
                    });
                });

            #endregion
        }
    }
}
