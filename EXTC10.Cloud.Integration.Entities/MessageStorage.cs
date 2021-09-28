using System;
using System.Collections.Generic;
using System.Text;

namespace EXTC10.Cloud.Integration.Entities
{
    public class MessageStorage
    {
        public string RequestId { get; set; }
        public string MessageContent { get; set; }
        public DateTime? CreatedDate { get; set; }
       
    }

    /// <summary>
    /// The queue message.
    /// </summary>
    public class QueueMessage
    {
        /// <summary>
        /// Gets or sets the route name.
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Gets or sets the source system.
        /// </summary>
        public string SourceSystem { get; set; }
        /// <summary>
        /// Gets or sets the target system.
        /// </summary>
        public string TargetSystem { get; set; }
        /// <summary>
        /// Gets or sets the requested action.
        /// </summary>
        public string RequestedAction { get; set; }
        /// <summary>
        /// Gets or sets the requested date.
        /// </summary>
        public DateTime RequestedDate { get; set; }
        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the a correlation identifier.
        /// </summary>
        public string CorrelationId { get; set; }
    }
}
