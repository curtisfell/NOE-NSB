using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Messages
{
    public interface INoeMessage
    {
        Guid SagaId { get; set; }
        int NoeId { get; set; }
        DateTime Timestamp { get; set; }
    }
}
