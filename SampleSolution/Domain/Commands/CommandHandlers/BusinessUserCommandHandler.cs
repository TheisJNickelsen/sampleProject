using SampleSolution.Common.Domain.Commands;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleSolution.Domain.Commands.CommandHandlers
{
    public class BusinessUserCommandHandler :
        ICommandHandler<CreateBusinessUserCommand>
    {
        private readonly IBusinessUserRepositoy _businessUserRepositoy;

        public BusinessUserCommandHandler(IBusinessUserRepositoy businessUserRepositoy)
        {
            _businessUserRepositoy = businessUserRepositoy ?? throw new ArgumentNullException(nameof(businessUserRepositoy));
        }

        public Task<Unit> Handle(CreateBusinessUserCommand request, CancellationToken cancellationToken)
        {
            _businessUserRepositoy.Create(request);

            return Unit.Task;
        }
    }
}
