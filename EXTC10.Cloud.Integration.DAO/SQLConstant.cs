using System;
using System.Collections.Generic;
using System.Text;

namespace EXTC10.Cloud.Integration.DAO
{
   internal static class SQLConstant
    {
        public const string GETALLCONFIGURATIONKEYSANDVALUES = "dbo.GetAllConfigurationKeysAndValues";
        public const string GETCONFIGURATIONVALUEBYCONFIGKEY = "dbo.GetConfigurationValueByConfigKey";
        public const string ADDNEWKEYVALUEINCONFIGURATIONS = "dbo.AddNewKeyValueInConfigurations";
        public const string UPDATEVALUEINCONFIGURATIONBYCONFIGKEY = "dbo.UpdateValueInConfigurationByConfigKey";
    }
}
