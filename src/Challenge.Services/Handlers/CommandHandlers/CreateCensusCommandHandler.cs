using Challenge.Services.Dtos.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Services.Handlers.CommandHandlers
{
    public class CreateCensusCommandHandler : IRequestHandler<CreateCensusCommand, bool>
    {

        public async Task<bool> Handle(CreateCensusCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return false;


            throw new NotImplementedException();
        }
    }
}
