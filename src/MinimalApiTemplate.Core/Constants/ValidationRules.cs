using FluentValidation;

namespace MinimalApiTemplate.Core.Constants
{
    public static class ValidationRules
    {
        public static IRuleBuilderOptions<T, string?> IsRequired<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Is required.");
        }
    }
}
