using FluentValidation;
using MinimalApiTemplate.Core.Constants;

namespace MinimalApiTemplate.Core.Models.Config
{
    public class LoggerSettings
    {
        public string? FilePath { get; set; }
    }

    public class LoggerSettingsValidator : AbstractValidator<LoggerSettings>
    {
        public LoggerSettingsValidator()
        {
            RuleFor(model => model.FilePath)
                .IsRequired();
        }
    }
}
