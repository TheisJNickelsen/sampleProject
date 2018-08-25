using System;
using SampleSolution.Common.Domain.Commands;

namespace SampleSolution.Domain.Commands.Commands
{
    public class ShareContactCommand : ICommand
    {
        public ShareContactCommand(Guid contactId, Guid recipientUserId, string fromUserId)
        {
            ContactId = contactId;
            RecipientUserId = recipientUserId;
            FromUserId = fromUserId;
        }

        public Guid ContactId { get; }
        public Guid RecipientUserId { get; }
        public string FromUserId { get; }
    }
}
