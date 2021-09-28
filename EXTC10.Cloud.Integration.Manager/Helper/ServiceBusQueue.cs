using Azure.Messaging.ServiceBus;
using EXTC10.Cloud.Integration.Entities;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXTC10.Cloud.Integration.Manager.Helper
{
    /// <summary>
    /// The service bus queue.
    /// </summary>
    internal class ServiceBusQueue
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusQueue"/> class.
        /// </summary>
        /// <param name="serviceBusConnectionStrings">The service bus connection strings.</param>
        internal ServiceBusQueue(string serviceBusConnectionStrings)
        {
            ServiceBusConnectionStrings = serviceBusConnectionStrings;
        }
        /// <summary>
        /// Gets or sets the service bus connection strings.
        /// </summary>
        public string ServiceBusConnectionStrings { get; set; }



        /// <summary>
        /// Sends the messages to queue async.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="queueMessage">The queue message.</param>
        /// <returns>A Task.</returns>
        public async Task<bool> SendMessagesToQueueAsync(string queueName, QueueMessage queueMessage)
        {
            try
            {
                var serializeQueueMessage = JsonConvert.SerializeObject(queueMessage);
                ServiceBusMessage message = new ServiceBusMessage(serializeQueueMessage)
                {
                    CorrelationId = queueMessage.CorrelationId,
                    MessageId = queueMessage.RequestId,
                    PartitionKey = queueMessage.RouteName
                };

                await CreateServiceBusIfNotExists(queueName).ConfigureAwait(false);
                
                await using var client = new ServiceBusClient(ServiceBusConnectionStrings);
                // create the sender
                ServiceBusSender sender = client.CreateSender(queueName);
                // send the message
                await sender.SendMessageAsync(message);

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Creates the service bus if not exists.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <returns>A Task.</returns>
        private async Task CreateServiceBusIfNotExists(string queueName)
        {
            try
            {
                ManagementClient managementClient = new ManagementClient(ServiceBusConnectionStrings);

                if (!await managementClient.QueueExistsAsync(queueName).ConfigureAwait(false))
                {
                    await managementClient.CreateQueueAsync(new QueueDescription(queueName)
                    {
                        MaxDeliveryCount = int.MaxValue,//it should be come from config service,
                        LockDuration = TimeSpan.FromMinutes(5),//it should be come from config service
                        MaxSizeInMB = 5 * 1024,//it should be come from config service
                        EnableBatchedOperations = true,
                        DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(5),//it should be come from config service
                        DefaultMessageTimeToLive = TimeSpan.FromDays(3),//it should be come from config service
                        EnableDeadLetteringOnMessageExpiration = true,
                        RequiresDuplicateDetection= true,
                    }).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
