using System;
using System.Collections.Generic;
using System.Text;

namespace EXTC10.Cloud.Integration.Entities
{
   public class ApplicationConfiguration
    {
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
