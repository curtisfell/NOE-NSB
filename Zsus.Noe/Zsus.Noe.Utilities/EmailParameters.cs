using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Utilities
{
    public class EmailParameters : IParameters
    {
        public int NoeId {set; get; }
        public Guid SagaId { set; get; }
        public EmailParameters() { }
        public EmailParameters(int _noeid, Guid _sagaid)
        {
            this.NoeId = _noeid;
            this.SagaId = _sagaid;
        }
    }
}
