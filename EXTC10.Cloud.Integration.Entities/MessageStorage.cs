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
}
