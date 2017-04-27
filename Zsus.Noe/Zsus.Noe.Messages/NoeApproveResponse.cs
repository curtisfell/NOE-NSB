using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Messages
{
    public class NoeApproveResponse : INoeMessage
    {
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public Boolean NoeApproved { get; set; }
        public DateTime Timestamp { get; set; }

        public NoeApproveResponse() { }
        public NoeApproveResponse(INoeMessage i)
        {
            SagaId = i.SagaId;
            NoeId = i.NoeId;
            NoeApproved = false;
            Timestamp = i.Timestamp;
        }
    }
}
