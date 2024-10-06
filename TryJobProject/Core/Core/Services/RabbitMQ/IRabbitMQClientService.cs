using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.RabbitMQ
{
    public interface IRabbitMQClientService
    {
        IModel ConnectSender();
    }
}
