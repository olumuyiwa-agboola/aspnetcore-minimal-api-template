using FluentValidation;
using MinimalApiTemplate.Core.Constants;

namespace MinimalApiTemplate.Core.Models.Config
{
    public class FileLoggerSettings
    {
        public string FilePath { get; set; } = string.Empty;
    }

    public class FileLoggerSettingsValidator : AbstractValidator<FileLoggerSettings>
    {
        public FileLoggerSettingsValidator()
        {
            RuleFor(model => model.FilePath)
                .IsRequired();
        }
    }
}
