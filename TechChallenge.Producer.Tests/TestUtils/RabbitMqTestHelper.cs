using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

public class RabbitMqTestHelper : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqTestHelper()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void DeclareQueue(string queueName)
    {
        _channel.QueueDeclare(queue: queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void PublishMessage(string queueName, NovoContato contato)
    {
        var json = JsonConvert.SerializeObject(contato);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(exchange: "",
                              routingKey: queueName,
                              basicProperties: null,
                              body: body);
    }

    public NovoContato ConsumeMessage(string queueName)
    {
        var result = _channel.BasicGet(queueName, autoAck: true);
        if (result != null)
        {
            var json = Encoding.UTF8.GetString(result.Body.ToArray());
            return JsonConvert.DeserializeObject<NovoContato>(json);
        }
        return null;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
