using FluentValidation;

namespace Vertem.News.Infra.PipelineBehavior
{
    public interface IShallowValidator<T> : IValidator<T>, IEnumerable<IValidationRule>
    {
    }
}
