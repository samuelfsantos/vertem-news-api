using FluentValidation;
using Vertem.News.Domain.Interfaces;

namespace Vertem.News.Application.Commands.Validators
{
    public class BaseDeepValidator<T> : AbstractValidator<T>
    {
        private readonly INoticiaRepository _noticiaRepository;

        public BaseDeepValidator(
            INoticiaRepository noticiaRepository)
        {
            _noticiaRepository = noticiaRepository;
        }

        protected bool TituloDaNoticiaJaExistente(string titulo)
        {
            var noticias = _noticiaRepository.Select(titulo: titulo).Result;

            return noticias.Any();
        }
    }
}
