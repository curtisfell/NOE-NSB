using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Zsus.Noe.Messages
{
    public class NoeNegotiationRequest : IMessage, INoeMessage
    {
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public DateTime Timestamp { get; set; }
        public NoeNegotiationRequest() { }
        public NoeNegotiationRequest(INoeMessage i)
        {
            SagaId = i.SagaId;
            NoeId = i.NoeId;
            Timestamp = i.Timestamp;
        }
    }
}
