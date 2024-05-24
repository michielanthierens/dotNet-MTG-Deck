using FluentValidation;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.FluentValidator;

public class CardCustomValidator : AbstractValidator<DeckCard>
{
    public CardCustomValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("The id can't be blank")
            .Length(1, 4);

        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("The name cannot be blank")
            .WithMessage("The name of the card should be between 2 and 50 characters");

        RuleFor(c => c.Amount)
            .NotEmpty()
            .NotNull();
    }
}


// gebruik ook voor CardViewModel in (nodig voor blazor)