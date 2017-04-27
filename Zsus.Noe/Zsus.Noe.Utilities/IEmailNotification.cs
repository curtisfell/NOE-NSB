using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Utilities
{
    public interface IEmailNotification<T>
    {
        void SendNeedsApproval(T parameters);
        void SendNeedsNegotiation(T parameters);
        void SendNeedsOffering(T parameters);
    }
}
