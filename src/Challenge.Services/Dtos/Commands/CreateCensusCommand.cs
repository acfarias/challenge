using Challenge.Domain.Core.Enums;
using Challenge.Domain.ValidationsMessages;
using FluentValidation;
using MediatR;
using System.Collections.Generic;

namespace Challenge.Services.Dtos.Commands
{
    public record CreateCensusCommand(string FirstName, string LastName, string SkinColor, ParentsCommand Parents, List<SonCommand> Sons, string Schooling, Regions Region) : IRequest<bool> { }

    public record ParentsCommand(string FatherName, string MotherName) { }
    public record SonCommand(string Name, int Age) { }

    public class CreateCensusCommandValidations : AbstractValidator<CreateCensusCommand>
    {
        public CreateCensusCommandValidations()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage(string.Format(CensusMessages.RequiredField, "FirstName"));

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage(string.Format(CensusMessages.RequiredField, "LastName"));

            RuleFor(c => c.SkinColor)
                .NotEmpty()
                .WithMessage(string.Format(CensusMessages.RequiredField, "SkinColor"));

            RuleFor(c => c.Schooling)
                .NotEmpty()
                .WithMessage(string.Format(CensusMessages.RequiredField, "Schooling"));

            RuleFor(c => c.Parents)
                .Must(ValidateParents)
                .WithMessage(CensusMessages.ParentsRequired);

            RuleFor(c => c.Region)
                .IsInEnum()
                .WithMessage(string.Format(CensusMessages.RequiredField, "Region"));
        }

        private bool ValidateParents(ParentsCommand parents)
        {
            if (string.IsNullOrWhiteSpace(parents.FatherName) || string.IsNullOrWhiteSpace(parents.MotherName))
                return false;

            return true;
        }
    }
}
