using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Features;
using NServiceBus.Persistence.MongoDB;
using Zsus.Noe.Utilities;
using Zsus.Noe.Commands;
using Zsus.Noe.Messages;

namespace Zsus.Noe.Approve
{
    public class MessageHelperFactory
    {
        private static MessageHelper helper = null;

        public static MessageHelper GetMessageHelper()
        {
            if (helper != null)
            {
                return helper;
            }
            else
            {
                helper = new MessageHelper();
                return helper;
            }
        }
    }

    public class MessageHelper : IMessageHelper
    {
        private static ILog log = LogManager.GetLogger<MessageHelper>();
        private EndpointConfiguration configuration;
        private IEndpointInstance endpointInstance;

        public MessageHelper()
        {
            configuration = new EndpointConfiguration(Constants.ZSUS_NOE_APPROVE_WCF_ENDPOINT);
            configuration.UsePersistence<MongoDbPersistence>()
                .SetConnectionString(Constants.ZSUS_NOE_DB_ESB_CONNECTION);
            configuration.EnableFeature<Sagas>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.UseTransport<MsmqTransport>();
            configuration.EnableInstallers();
            configuration.EnableDurableMessages();
            configuration.Conventions()
                .DefiningMessagesAs(t => t.Namespace == Constants.ZSUS_NOE_MESSAGES);
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace == Constants.ZSUS_NOE_COMMANDS);
            // NServiceBus will move messages that fail repeatedly to a separate "error" queue. We recommend
            // that you start with a shared error queue for all your endpoints for easy integration with ServiceControl.
            configuration.SendFailedMessagesTo("error");
            // NServiceBus will store a copy of each successfully process message in a separate "audit" queue. We recommend
            // that you start with a shared audit queue for all your endpoints for easy integration with ServiceControl.
            configuration.AuditProcessedMessagesTo("audit");
            StartupAsync();
        }
        
        public async void StartupAsync()
        {
            var startableEndpoint = 
                await Endpoint
                .Create(configuration)
                .ConfigureAwait(false);

            endpointInstance = 
                await startableEndpoint
                .Start()
                .ConfigureAwait(false);

        }
        
        public async Task SubmitNoeAsync(int noeId, string sagaid)
        {
            log.InfoFormat("SubmitNoe - noeId {0}", noeId);

            var cmd = new NoeSubmitted()
            {
                NoeId = noeId,
                SagaId = Guid.Parse(sagaid),
                Timestamp = DateTime.Now
            };

            //StartupAsync();
            var options = new SendOptions();
            options.SetDestination(Constants.ZSUS_NOE_SAGA_ENDPOINT);
            await endpointInstance.Send(cmd, options).ConfigureAwait(false);
            //ShutdownAsync();

        }

        public async Task NoeApprovedAsync(int noeId, Guid sagaId, int flag)
        {
            log.InfoFormat("NoeApproved - noeId {0}, flag {1}", noeId, flag);

            var message = new NoeApproveResponse()
            {
                NoeId = noeId,
                SagaId = sagaId,
                NoeApproved = (flag == 0 ? false : true)
            };

            //StartupAsync();
            var options = new SendOptions();
            options.SetDestination(Constants.ZSUS_NOE_SAGA_ENDPOINT);
            await endpointInstance.Send(message, options).ConfigureAwait(false);
            //ShutdownAsync();
        }

        public async Task NoeNegotiatedAsync(int noeId, Guid sagaId, int flag)
        {
            log.InfoFormat("NoeNegotiated - noeId {0}, flag {1}", noeId, flag);

            var message = new NoeNegotiationResponse()
            {
                NoeId = noeId,
                SagaId = sagaId,
                NoeNegotiated = (flag == 0 ? false : true)
            };

            //StartupAsync();
            var options = new SendOptions();
            options.SetDestination(Constants.ZSUS_NOE_SAGA_ENDPOINT);
            await endpointInstance.Send(message, options).ConfigureAwait(false);
            //ShutdownAsync();
        }

        public async Task NoeOfferedAsync(int noeId, Guid sagaId, int flag)
        {
            log.InfoFormat("NoeOffered - noeId {0}, flag {1}", noeId, flag);

            var message = new NoeOfferResponse()
            {
                NoeId = noeId,
                SagaId = sagaId,
                NoeOfferAccepted = (flag == 0 ? false : true)
            };

            //StartupAsync();
            var options = new SendOptions();
            options.SetDestination(Constants.ZSUS_NOE_SAGA_ENDPOINT);
            await endpointInstance.Send(message, options).ConfigureAwait(false);
            //ShutdownAsync();
        }

        public void ShutdownAsync()
        {
            endpointInstance
                .Stop()
                .GetAwaiter()
                .GetResult();
        }


    }
}
