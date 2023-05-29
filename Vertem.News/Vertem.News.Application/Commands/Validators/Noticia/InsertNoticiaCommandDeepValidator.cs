using FluentValidation;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.PipelineBehavior;

namespace Vertem.News.Application.Commands.Validators
{
    public class InsertNoticiaCommandDeepValidator : BaseDeepValidator<InsertNoticiaCommand>, IDeepValidator<InsertNoticiaCommand>
    {
        public InsertNoticiaCommandDeepValidator(INoticiaRepository noticiaRepository) : base(noticiaRepository)
        {
            RuleFor(x => x.Titulo)
                .Must(TituloDaNoticiaJaExistente)
                .WithMessage("O título informado já está em uso")
                .WithErrorCode("InvalidTituloNoticia");
        }
    }
}
