using FluentValidation;
using UrlShortener.Requests.Shortener;

namespace UrlShortener.Validators.Shortener
{
    public class GetUrlsStatisticRequestValidator : AbstractValidator<GetUrlsStatisticRequest>
    {
        public GetUrlsStatisticRequestValidator()
        {
            RuleFor(x => x.Take).GreaterThan(0);
            RuleFor(x => x.LastId).NotNull();
            RuleFor(x => x.LastId).NotEmpty();
        }
    }
}
