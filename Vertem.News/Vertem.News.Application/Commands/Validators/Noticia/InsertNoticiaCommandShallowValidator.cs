using FluentValidation;
using Vertem.News.Infra.PipelineBehavior;

namespace Vertem.News.Application.Commands.Validators
{
    public class InsertNoticiaCommandShallowValidator : BaseShallowValidator<InsertNoticiaCommand>, IShallowValidator<InsertNoticiaCommand>
    {
        public InsertNoticiaCommandShallowValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("O título deve ser informado").WithErrorCode("InvalidTitulo");
            RuleFor(x => x.Titulo).MaximumLength(250).WithMessage("O título deve ter no máximo 250 caractéres").WithErrorCode("InvalidTitulo");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição deve ser informada").WithErrorCode("InvalidDescricao");
            RuleFor(x => x.Descricao).MaximumLength(800).WithMessage("A descrição deve ter no máximo 800 caractéres").WithErrorCode("InvalidDescricao");

            RuleFor(x => x.Conteudo).NotEmpty().WithMessage("O conteúdo deve ser informado").WithErrorCode("InvalidConteudo");

            RuleFor(x => x.Categoria).NotEmpty().WithMessage("A categoria deve ser informada").WithErrorCode("InvalidCategoria");
            RuleFor(x => x.Categoria).MaximumLength(150).WithMessage("A categoria deve ter no máximo 150 caractéres").WithErrorCode("InvalidCategoria");

            RuleFor(x => x.Fonte).NotEmpty().WithMessage("A fonte deve ser informada").WithErrorCode("InvalidFonte");
            RuleFor(x => x.Fonte).MaximumLength(180).WithMessage("A fonte deve ter no máximo 180 caractéres").WithErrorCode("InvalidFonte");

            RuleFor(x => x.DataPublicacao)
                .Must(DataEhPresenteOuPassado)
                .WithMessage("A data de publicação deve ser presente ou passado")
                .WithErrorCode("InvalidDataPublicacao");

            RuleFor(x => x.ImgUrl)
                .Must(TamanhoMaximoOuNulo350)
                .WithMessage("A url da imagem deve ter no máximo 350 caractéres")
                .WithErrorCode("InvalidImgUrl");

            RuleFor(x => x.Autor)
                .Must(TamanhoMaximoOuNulo200)
                .WithMessage("O autor deve ter no máximo 200 caractéres")
                .WithErrorCode("InvalidAutor");
        }
    }
    }
}
