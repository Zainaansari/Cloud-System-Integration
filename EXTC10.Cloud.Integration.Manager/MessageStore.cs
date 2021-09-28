using EXTC10.Cloud.Integration.DAO.SQL;
using EXTC10.Cloud.Integration.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXTC10.Cloud.Integration.Manager
{
    /// <summary>
    /// The message store.
    /// </summary>
    public class MessageStore
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        private readonly MessageStorage_DAO messageStorage_DAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageStore"/> class.
        /// </summary>
        /// <param name="databaseConnectionString">The database connection string.</param>
        public MessageStore(string databaseConnectionString)
        {
            DatabaseConnectionString = databaseConnectionString;
            messageStorage_DAO = new MessageStorage_DAO(DatabaseConnectionString);
        }

        /// <summary>
        /// Adds the request message in store async.
        /// </summary>
        /// <param name="MessageStorage">The message storage.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> AddRequestMessageInStoreAsync(MessageStorage MessageStorage)
        {
            return await messageStorage_DAO.AddRequestMessageInStoreAsync(MessageStorage);
        }

        /// <summary>
        /// Gets the request message by id.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <returns>A Task<MessageStorage>.</returns>
        public async Task<MessageStorage> GetRequestMessageById(string requestId)
        {
            return await messageStorage_DAO.GetRequestMessageById(requestId);
        }
    }
}
