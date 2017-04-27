using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Messages
{
    public class NoeOfferResponse : INoeMessage
    {
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public Boolean NoeOfferAccepted { get; set; }
        public DateTime Timestamp { get; set; }
        public NoeOfferResponse() { }
        public NoeOfferResponse(INoeMessage i)
        {
            SagaId = i.SagaId;
            NoeId = i.NoeId;
            NoeOfferAccepted = false;
            Timestamp = i.Timestamp;
        }
    }
}
