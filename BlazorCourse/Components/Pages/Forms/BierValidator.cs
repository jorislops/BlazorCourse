using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using FluentValidation;

namespace BlazorCourse.Components.Pages.Forms;

public class BierValidator : AbstractValidator<Beer>
{
    public BierValidator()
    {
        RuleFor(b => b.Name).NotEmpty()
            .WithMessage("Error message From BierValidator.cs")
            .MinimumLength(2).WithMessage("omvang te klein")
            .MaximumLength(50).WithMessage("omvang te groot");
        RuleFor(b => b.Type).NotEmpty()
            .WithMessage("Error message From BierValidator.cs").MinimumLength(2).MaximumLength(30);
        RuleFor(b => b.Style).NotEmpty().MaximumLength(20);
        RuleFor(b => b.Alcohol).NotEmpty().InclusiveBetween(0, 16);
        RuleFor(b => b.BrewerId).NotEmpty();
    }
}