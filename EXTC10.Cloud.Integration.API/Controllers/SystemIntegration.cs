using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EXTC10.Cloud.Integration.Entities;
using Newtonsoft.Json;
using EXTC10.Cloud.Integration.Manager;
using EXTC10.Cloud.Integration.DAO.SQL;

namespace EXTC10.Cloud.Integration.API.Controllers
{
    /// <summary>
    /// The system integration API.
    /// </summary>
    [Route("api/SystemIntegration")]
    [ApiController]
    public class SystemIntegration : ControllerBase
    {

        [HttpPost]
        [Route("AddRequestInQueue")]
        public async Task<string> AddRequestInQueue(QueueMessage requestMessage)
        {
            //QueueMessage queueMessage = JsonConvert.DeserializeObject<QueueMessage>(requestMessage);
            string connectionString = "Server=DESKTOP-LRDICV5;Database=Request_Acceptance_Service;User Id=sa;Password=Password123;";

            RequestQueueManager requestQueueManager = new RequestQueueManager(connectionString);
            string integrationQueueResponse = await requestQueueManager.AddIntegrationRequestInQueue(requestMessage);
            return integrationQueueResponse;
        }

        //[HttpPost]
        //[Route("SendRequestInQueue")]
        public async Task<string> SendRequestInQueue(QueueMessage requestMessage)
        {
            //QueueMessage queueMessage = JsonConvert.DeserializeObject<QueueMessage>(requestMessage);
            string connectionString = "Server=DESKTOP-LRDICV5;Database=Request_Acceptance_Service;User Id=sa;Password=Password123;";

            RequestQueueManager requestQueueManager = new RequestQueueManager(connectionString);
            string integrationQueueResponse = await requestQueueManager.AddIntegrationRequestInQueue(requestMessage);
            return integrationQueueResponse;
        }

    }

   
    

}


