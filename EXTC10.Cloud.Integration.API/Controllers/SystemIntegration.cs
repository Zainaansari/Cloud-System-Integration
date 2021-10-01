using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EXTC10.Cloud.Integration.Entities;
using Newtonsoft.Json;
using EXTC10.Cloud.Integration.Manager;

namespace EXTC10.Cloud.Integration.API.Controllers
{
    /// <summary>
    /// The system integration API.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SystemIntegration : ControllerBase
    {
       public async Task<string> AddRequestInQueue(string requestMessage)
        {
            QueueMessage queueMessage = (QueueMessage)JsonConvert.DeserializeObject(requestMessage);

            RequestQueueManager requestQueueManager = new RequestQueueManager("Database connection");
            await requestQueueManager.AddIntegrationRequestInQueue(queueMessage);

            return string.Empty;
        }
    }
}
