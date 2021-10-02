using System;
using System.Collections.Generic;
using System.Text;

namespace EXTC10.Cloud.Integration.Entities
{
    public class RequestQueue
    {
        public string RequestId { get; set; }
        public string SourceSystem { get; set; }
        public string TargetSystem { get; set; }
        public string RequestedAction { get; set; }
        public DateTime? Requesteddate { get; set; }
        public int RequestStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

    public class IntegrationQueueResponse
    {
        public string RequestId { get; set; }

        public string AcknoweldgeResponse { get; set; }

        public int StatusCode { get; set; }

        public string ErrorLog { get; set; }
    }
}
