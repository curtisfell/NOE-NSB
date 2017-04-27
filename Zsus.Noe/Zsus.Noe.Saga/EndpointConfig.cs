
namespace Zsus.Noe.Saga
{
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Persistence.MongoDB;
    using Zsus.Noe.Utilities;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<MongoDbPersistence>()
                .SetConnectionString(Constants.ZSUS_NOE_DB_ESB_CONNECTION);
            endpointConfiguration.EnableFeature<Sagas>();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableDurableMessages();
            endpointConfiguration.Conventions()
                .DefiningMessagesAs(t => t.Namespace == Constants.ZSUS_NOE_MESSAGES);
            endpointConfiguration.Conventions()
                .DefiningCommandsAs(t => t.Namespace == Constants.ZSUS_NOE_COMMANDS);
            // NServiceBus will move messages that fail repeatedly to a separate "error" queue. We recommend
            // that you start with a shared error queue for all your endpoints for easy integration with ServiceControl.
            endpointConfiguration.SendFailedMessagesTo("error");
            // NServiceBus will store a copy of each successfully process message in a separate "audit" queue. We recommend
            // that you start with a shared audit queue for all your endpoints for easy integration with ServiceControl.
            endpointConfiguration.AuditProcessedMessagesTo("audit");
        }
    }
}
