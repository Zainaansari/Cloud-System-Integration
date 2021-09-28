using EXTC10.Cloud.Integration.DAO.SQL;
using EXTC10.Cloud.Integration.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXTC10.Cloud.Integration.Manager
{
    /// <summary>
    /// The request queue.
    /// </summary>
    public class RequestQueue
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        private readonly RequestQueue_DAO requestQueue_DAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageStore"/> class.
        /// </summary>
        /// <param name="databaseConnectionString">The database connection string.</param>
        public RequestQueue(string databaseConnectionString)
        {
            DatabaseConnectionString = databaseConnectionString;
            requestQueue_DAO = new RequestQueue_DAO(DatabaseConnectionString);
        }

        /// <summary>
        /// Adds the request queue entry async.
        /// </summary>
        /// <param name="requestQueue">The request queue.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> AddRequestQueueEntryAsync(Entities.RequestQueue requestQueue)
        {
            return await requestQueue_DAO.AddRequestQueueEntryAsync(requestQueue);
        }

        /// <summary>
        /// Updates the request queue status async.
        /// </summary>
        /// <param name="RequestQueue">The request queue.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> UpdateRequestQueueStatusAsync(Entities.RequestQueue requestQueue)
        {
            return await requestQueue_DAO.UpdateRequestQueueStatusAsync(requestQueue);
        }

        /// <summary>
        /// Gets the request queue by id.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <returns>A Task<Entities.RequestQueue>.</returns>
        public async Task<Entities.RequestQueue> GetRequestQueuebyId(string requestId)
        {
            return await requestQueue_DAO.GetRequestQueuebyId(requestId);
        }

        public async Task<bool> AddIntegrationRequestInQueue(QueueMessage queueMessage)
        {
            queueMessage.RequestId = System.Guid.NewGuid().ToString();



            return true;
        }
    }
}
