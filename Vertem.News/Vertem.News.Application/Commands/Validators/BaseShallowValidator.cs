using FluentValidation;

namespace Vertem.News.Application.Commands.Validators
{
    public class BaseShallowValidator<T> : AbstractValidator<T>
    {
        protected bool DataEhPresenteOuPassado(DateTime campo)
        {
            return campo <= DateTime.Now;
        }

        protected bool TamanhoMaximoOuNulo350(string? campo)
        {
            return String.IsNullOrWhiteSpace(campo) || campo.Length <= 350;
        }

        protected bool TamanhoMaximoOuNulo200(string? campo)
        {
            return String.IsNullOrWhiteSpace(campo) || campo.Length <= 200;
        }
    }
}
