using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Zsus.Noe.Utilities;
using Zsus.Noe.Commands;
using Zsus.Noe.Messages;
using NServiceBus.Sagas;
using NServiceBus.Persistence.MongoDB;

namespace Zsus.Noe.Saga
{
    public class NoeSaga : ContainSagaData

    {
        //[Unique]
        public Guid SagaId { get; set; }
        public int NoeId { get; set; }
        public Boolean NoeApproved { get; set; }
        public Boolean NoeOfferAccepted { get; set; }
        public Boolean NoeNegotiated { get; set; }
        public DateTime Timestamp { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int ServiceResponseCode { get; set; }
        public string ServiceResponseMessage { get; set; }

        [DocumentVersion]
        public int Version { get; set; }
    }
    public class Handler : Saga<NoeSaga>,
        IAmStartedByMessages<NoeSubmitted>,
        IHandleMessages<NoeApproveRequestComplete>,
        IHandleMessages<NoeApproveResponse>,
        IHandleMessages<NoeOfferRequestComplete>,
        IHandleMessages<NoeOfferResponse>,
        IHandleMessages<NoeNegotiationRequestComplete>,
        IHandleMessages<NoeNegotiationResponse>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Handler));

        public Handler()
        {

        }
        
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<NoeSaga> mapper)
        {
            mapper.ConfigureMapping<NoeSubmitted>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<NoeApproveRequestComplete>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<NoeOfferRequestComplete>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<NoeNegotiationRequestComplete>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<NoeApproveResponse>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<NoeOfferResponse>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<NoeNegotiationResponse>(m => m.SagaId).ToSaga(s => s.SagaId);
        }

        public async Task Handle(NoeSubmitted cmd, IMessageHandlerContext context)
        {
            log.InfoFormat("Recieved NoeSubmitted command to start saga for {0} at {1}, sending NoeApproveRequest",
                cmd.NoeId, cmd.Timestamp);

            // can't do this anymore, have to set guid where command is built.
            //Guid sid = Guid.NewGuid();
            //Data.SagaId = sid;
            // 03202017 - SagaId for now built by MessageHelper
            Data.SagaId = cmd.SagaId;
            Data.NoeId = cmd.NoeId;
            Data.NoeApproved = false;
            Data.NoeNegotiated = false;
            Data.NoeOfferAccepted = false;
            Data.Timestamp = cmd.Timestamp;
            Data.ResponseCode = Constants.ZSUS_NOE_UNSET;
            Data.ResponseMessage = string.Empty;

            var request = new NoeApproveRequest()
            {
                SagaId = cmd.SagaId,
                NoeId = cmd.NoeId,
                Timestamp = cmd.Timestamp
            };

            var options = new SendOptions();
            options.SetDestination(Constants.ZSUS_NOE_APPROVE_REQUEST_ENDPOINT);
            await context.Send(request, options).ConfigureAwait(false);

        }

        public Task Handle(NoeApproveRequestComplete message, IMessageHandlerContext context)
        {
            log.InfoFormat("Recieved NoeApproveRequestComplete for {0} ({1}), {2}, {3}", 
                message.NoeId, message.SagaId, message.ResponseCode, message.ResponseMessage);
            return Task.CompletedTask;
        }

        public async Task Handle(NoeApproveResponse message, IMessageHandlerContext context)
        {
            if (message.NoeApproved)
            {
                log.InfoFormat("Recieved NoeApproveResponse, NoeApproved flag TRUE, sending NoeNegotiationRequest for {0} ({1})",
                message.NoeId, message.SagaId);
                Data.NoeApproved = message.NoeApproved;
                var request = new NoeNegotiationRequest(message);
                var options = new SendOptions();
                options.SetDestination(Constants.ZSUS_NOE_NEGOTIATION_REQUEST_ENDPOINT);
                await context.Send(request, options).ConfigureAwait(false);
            }
            else
            {
                log.InfoFormat("Recieved NoeApproveResponse, NoeApproved flag FALSE for {0} ({1}), marking saga complete",
                message.NoeId, message.SagaId);
                MarkAsComplete();
            }
        }

        public Task Handle(NoeNegotiationRequestComplete message, IMessageHandlerContext context)
        {
            log.InfoFormat("Recieved NoeNegotiationRequestComplete for {0} ({1}), {2}, {3}",
                message.NoeId, message.SagaId, message.ResponseCode, message.ResponseMessage);
            return Task.CompletedTask;
        }

        public async Task Handle(NoeNegotiationResponse message, IMessageHandlerContext context)
        {
            if (message.NoeNegotiated)
            {
                log.InfoFormat("Recieved NoeNegotiationResponse, NoeNegotiated flag TRUE, sending NoeOfferRequest for {0} ({1})",
                message.NoeId, message.SagaId);
                Data.NoeNegotiated = message.NoeNegotiated;
                var request = new NoeOfferRequest(message);
                var options = new SendOptions();
                options.SetDestination(Constants.ZSUS_NOE_OFFER_REQUEST_ENDPOINT);
                await context.Send(request, options).ConfigureAwait(false);
            }
            else
            {
                log.InfoFormat("Recieved NoeNegotiationResponse, NoeNegotiated flag FALSE for {0} ({1}), marking saga complete",
                message.NoeId, message.SagaId);
                MarkAsComplete();
            }
        }


        public Task Handle(NoeOfferRequestComplete message, IMessageHandlerContext context)
        {
            log.InfoFormat("Recieved NoeOfferRequestComplete for {0} ({1}), {2}, {3}",
                message.NoeId, message.SagaId, message.ResponseCode, message.ResponseMessage);
            return Task.CompletedTask;
        }
        

        public Task Handle(NoeOfferResponse message, IMessageHandlerContext context)
        {
            if (message.NoeOfferAccepted)
            {
                log.InfoFormat("Recieved NoeOfferResponse, NoeOfferAccepted flag TRUE, marking saga complete for {0} ({1})",
                message.NoeId, message.SagaId);
                Data.NoeOfferAccepted = message.NoeOfferAccepted;
            }
            else
            {
                log.InfoFormat("Recieved NoeOfferResponse, NoeOfferAccepted flag FALSE for {0} ({1}), marking saga complete",
                message.NoeId, message.SagaId);
            }
            MarkAsComplete();
            return Task.CompletedTask;
        }

    }
}
