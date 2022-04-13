using Challenge.Domain.Core.Entities;
using Challenge.Domain.Factories;
using Challenge.Domain.Interfaces.Repository;
using Challenge.Services.Dtos.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Services.Handlers.CommandHandlers
{
    public class CreateCensusCommandHandler : IRequestHandler<CreateCensusCommand, bool>
    {
        private readonly ICensusRepository _censusRepository;

        public CreateCensusCommandHandler(ICensusRepository censusRepository)
        {
            _censusRepository = censusRepository;
        }

        public async Task<bool> Handle(CreateCensusCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return false;

            var censusFactory = CensusFactory.NewCensus(request.FirstName, request.LastName, request.SkinColor, request.Schooling, (int)request.Region, new Parents
            {
                FatherName = request.Parents.FatherName,
                MotherName = request.Parents.MotherName
            }, new List<Son>(request.Sons.Select(s => new Son { Age = s.Age, FullName = s.Name })));

            await _censusRepository.Create(censusFactory);

            return true;
        }
    }
}