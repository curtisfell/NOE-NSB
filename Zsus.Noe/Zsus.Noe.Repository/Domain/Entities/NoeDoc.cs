using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Repository.Domain.Entities
{
    public class NoeDoc : IEntity
    {
        public String Id { get; set; }
        public int noeid { get; set; }
        public String sagaid { get; set; }
        public String description { get; set; }
        public DateTime timestamp { get; set; }
        public int needsapproval { get; set; }
        public int approved { get; set; }
        public int needsnegotiation { get; set; }
        public int negotiated { get; set; }
        public int needsoffering { get; set; }
        public int offered { get; set; }
        public int status { get; set; }
    }
}
