using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Messages
{
    public class NoeResponse : INoeMessage
    {
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }
        public NoeResponse() { }
        public NoeResponse(INoeMessage i)
        {
            SagaId = i.SagaId;
            NoeId = i.NoeId;
            Status = -1;
            Timestamp = i.Timestamp;
        }
    }
}
