using System;
using SampleSolution.Common.Domain.Commands;

namespace SampleSolution.Domain.Commands.Commands
{
    public class DeleteSomeDataCommand : ICommand
    {
        public DeleteSomeDataCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
