﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Messages
{
    public class NoeOfferRequestComplete : INoeMessage
    {
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public NoeOfferRequestComplete() { }
        public NoeOfferRequestComplete(INoeMessage i)
        {
            SagaId = i.SagaId;
            NoeId = i.NoeId;
            Timestamp = i.Timestamp;
        }
    }
}
