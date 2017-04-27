using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using NServiceBus;
using NServiceBus.Logging;
using Zsus.Noe.Messages;
using Zsus.Noe.Utilities;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Zsus.Noe.Approve.API
{
    public class NoeController : ApiController
    {
        static ILog log = LogManager.GetLogger<NoeController>();
        private MessageHelper messageHelper;

        public NoeController()
        {
            messageHelper = MessageHelperFactory.GetMessageHelper();
        }
        
        [HttpPost]
        public void PostNoe([FromBody]int id)
        {
            log.InfoFormat("PostNoe - id {0}", id);
            throw new Exception("PostNoe not implemented");
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutNoe(int noeid, string sagaid)
        {
            log.InfoFormat("PutNoe - noeid {0}, sagaid {1}", noeid, sagaid);
            await MessageHelperFactory.GetMessageHelper().SubmitNoeAsync(noeid, sagaid).ConfigureAwait(false);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Approved(int noeid, String sagaid, int flag)
        {
            log.InfoFormat("Approved - noeid {0}, sagaid {1}, flag {2}", noeid, flag, sagaid);
            await messageHelper.NoeApprovedAsync(noeid, new Guid(sagaid), flag).ConfigureAwait(false);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Negotiated(int noeid, String sagaid, int flag)
        {
            log.InfoFormat("Negotiated - noeid {0}, sagaid {1}, flag {2}", noeid, flag, sagaid);
            await messageHelper.NoeNegotiatedAsync(noeid, new Guid(sagaid), flag).ConfigureAwait(false);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Offered(int noeid, String sagaid, int flag)
        {
            log.InfoFormat("Offered - noeid {0}, sagaid {1}, flag {2}", noeid, flag, sagaid);
            await messageHelper.NoeOfferedAsync(noeid, new Guid(sagaid), flag).ConfigureAwait(false);
            return Ok();
        }

    }

    
}
