using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Zsus.Noe.Commands
{
    public class NoeSubmitted : ICommand
    {
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
