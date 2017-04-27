using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Zsus.Noe.Messages;
using Zsus.Noe.Repository;
using Zsus.Noe.Repository.Domain.Entities;
using Zsus.Noe.Utilities;

namespace Zsus.Noe.Response
{
    public class Handler :
        IHandleMessages<NoeResponse>
    {
        static ILog log = LogManager.GetLogger<Handler>();

        public async Task Handle(NoeResponse request, IMessageHandlerContext context)
        {
            int rc;
            String rm = String.Empty;

            try
            {
                NoeDoc doc = await MongoLegacyDriverFactory
                                   .GetMongoLegacyDriver()
                                   .GetNoeDoc(request.NoeId, request.SagaId.ToString());

                doc.status = request.Status;

                await MongoLegacyDriverFactory.GetMongoLegacyDriver().UpdateNoeDoc(doc);

                EmailNotifcationFactory
                    .GetEmailNotifcation()
                    .SendSagaStatusChange(new EmailParameters(request.NoeId, request.SagaId, request.Status));

                rc = Constants.ZSUS_NOE_SUCCESS;
                rm = Constants.ZSUS_NOE_SUCCESS_MSG;
                log.InfoFormat(
                    "Workflow (Saga) status change email submitted; sagaid {0}, noeid {1}, status {2}",
                    request.SagaId, request.NoeId, request.Status);
            }
            catch (Exception e)
            {
                rc = Constants.ZSUS_NOE_FAIL;
                rm = e.Message;
                log.ErrorFormat(
                    "Workflow (Saga) status change email not submitted; {0}, {1}, {2}, {3}",
                    request.SagaId, request.NoeId, e.Message, e.StackTrace);
            }

            var response = new NoeResponseComplete(request);
            response.ResponseCode = rc;
            response.ResponseMessage = rm;
            await context.Reply(response).ConfigureAwait(false);
        }
    }
}
