using FluentValidation;
using Models.Helpers;
using Models.Requests.Shortener;
using UrlShortener.Configuration.Interafces;

namespace UrlShortener.Validators.Shortener
{
    public class AddShortUrlRequestValidator : AbstractValidator<AddShortUrlRequest>
    {
        public AddShortUrlRequestValidator(IAppConfig appConfig)
        {
            RuleFor(x => x.LongUrl)
                .NotEmpty()
                .NotNull()
                .Must(x => UrlValidatorHelper.UrlStringIsValid(x))
                .Must(x => !x.StartsWith(appConfig.SelfBaseUrl)).WithMessage($"Url is not correct");
        }
    }
}
