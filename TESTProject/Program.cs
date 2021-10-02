using EXTC10.Cloud.Integration.Entities;
using Newtonsoft.Json;
using System;

namespace TESTProject
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueMessage queueMessage = new QueueMessage();
            queueMessage.CorrelationId = System.Guid.NewGuid().ToString();
            queueMessage.MessageContent = "abcd";
            queueMessage.RequestedAction = "Create Invoive";
            queueMessage.RequestedDate = DateTime.UtcNow;
            queueMessage.RequestId = string.Empty;
            queueMessage.RouteName = "A2B";
            queueMessage.SourceSystem = "A";
            queueMessage.TargetSystem = "B";

            string queuJsonData = JsonConvert.SerializeObject(queueMessage);
            Console.WriteLine(queuJsonData);

            QueueMessage queueMessage1 = JsonConvert.DeserializeObject<QueueMessage>(queuJsonData);

            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}
