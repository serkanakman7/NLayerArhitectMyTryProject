using ClosedXML.Excel;
using Core.Business.Response;
using Core.Entites.Dtos.Common;
using Core.Services.RabbitMQ;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data;
using System.Text.Json;

namespace Core.BackgroundServices
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IRabbitMQClientService _rabbitMQClientService;

        private IModel _channel;

        public RabbitMQConsumer(IRabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.ConnectSender();

            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var objectData = JsonSerializer.Deserialize<GetListResponse<GetListDto>>(@event.Body.ToArray());

            using var ms = new MemoryStream();

            var wb = new XLWorkbook();

            var ds = new DataSet();
            ds.Tables.Add(GetTable(nameof(GetListDto), objectData));

            wb.Worksheets.Add(ds);

            var path = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/files/", Guid.NewGuid().ToString().Substring(1, 10) + ".xlsx");

            FileStream fileStream = new FileStream(path, FileMode.Create);

            wb.SaveAs(fileStream);

            _channel.BasicAck(@event.DeliveryTag, false);
        }

        private DataTable GetTable(string tableName, GetListResponse<GetListDto> getListResponseDto)
        {
            List<GetListDto> data;

            data = (List<GetListDto>)getListResponseDto.Items;

            DataTable dataTable = new() { TableName = tableName };

            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("CratedDate", typeof(DateTime));
            dataTable.Columns.Add("DeletedDate", typeof(DateTime));

            data.ForEach(x =>
            {
                dataTable.Rows.Add(x.Name, x.CreatedDate, x.DeletedDate);
            });

            return dataTable;
        }
    }
}
