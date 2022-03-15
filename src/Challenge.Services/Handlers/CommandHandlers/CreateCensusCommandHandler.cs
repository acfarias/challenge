using Challenge.Domain.Core.Entities;
using Challenge.Domain.Interfaces.Repository;
using Challenge.Services.Dtos.Commands;
using MediatR;
using System;
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

            await _censusRepository.Create(new CensusCollection());

            return true;
        }
    }
}
