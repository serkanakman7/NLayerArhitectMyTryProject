using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.RabbitMQ
{
    public class RabbitMQClientService : IRabbitMQClientService ,IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public static string ExchangeName = "ExcelDirectExchange";
        public static string RoutingExcel = "queue-excel-file";
        public static string QueueName = "excel-route-file";

        public RabbitMQClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel ConnectReceiver()
        {
            _connection = _connectionFactory.CreateConnection();

            if(_channel is { IsOpen:true } )
            {
                return _channel;
            }

            _channel = _connection.CreateModel();

            return _channel;
        }

        public IModel ConnectSender()
        {
            _connection = _connectionFactory.CreateConnection();

            if(_channel is { IsOpen:true } ) return _channel; 

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueName, true, false, false);
            _channel.QueueBind(QueueName, ExchangeName, RoutingExcel);

            return _channel;
        }

        public void Dispose()
        {
            _channel.Close();
            _channel.Dispose();

            _connection.Close();
            _connection.Dispose();
        }
    }
}
