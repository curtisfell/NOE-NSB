﻿using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Zsus.Noe.Messages;
using Zsus.Noe.Repository;
using Zsus.Noe.Repository.Domain.Entities;
using Zsus.Noe.Utilities;

namespace Zsus.Noe.Approve.Request
{
    public class Handler :
        IHandleMessages<NoeApproveRequest>
    {
        static ILog log = LogManager.GetLogger<Handler>();

        public async Task Handle(NoeApproveRequest request, IMessageHandlerContext context)
        {
            int rc;
            String rm = String.Empty;

            try
            {
                NoeDoc doc = await MongoLegacyDriverFactory
                    .GetMongoLegacyDriver()
                    .GetNoeDoc(request.NoeId, request.SagaId.ToString());

                doc.needsapproval = Constants.ZSUS_NOE_TRUE;

                await MongoLegacyDriverFactory.GetMongoLegacyDriver().UpdateNoeDoc(doc);

                EmailNotifcationFactory
                    .GetEmailNotifcation()
                    .SendNeedsApproval(new EmailParameters(request.NoeId, request.SagaId));

                rc = Constants.ZSUS_NOE_SUCCESS;
                rm = Constants.ZSUS_NOE_SUCCESS_MSG;
                log.InfoFormat(
                    "Needs approval email submitted; sagaid {0}, noeid {1}",
                    request.SagaId, request.NoeId);
            }
            catch (Exception e)
            {
                rc = Constants.ZSUS_NOE_FAIL;
                rm = e.Message;
                log.ErrorFormat(
                    "Needs approval email not submitted; {0}, {1}, {2}, {3}",
                    request.SagaId, request.NoeId, e.Message, e.StackTrace);
            }

            var response = new NoeApproveRequestComplete(request);
            response.ResponseCode = rc;
            response.ResponseMessage = rm;
            await context.Reply(response).ConfigureAwait(false);
        }
    }
}
