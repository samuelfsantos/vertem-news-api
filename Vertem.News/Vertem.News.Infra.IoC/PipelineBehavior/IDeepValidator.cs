using FluentValidation;

namespace Vertem.News.Infra.PipelineBehavior
{
    public interface IDeepValidator<T> : IValidator<T>, IEnumerable<IValidationRule>
    {
    }
}
