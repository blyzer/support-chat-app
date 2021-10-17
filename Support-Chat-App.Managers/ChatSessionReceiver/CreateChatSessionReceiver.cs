using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.Helpers;
using Support_Chat_App.Managers.IManagers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace upport_Chat_App.Managers.ChatSessionReceiver
{
    public class CreateChatSessionReceiver : BackgroundService
    {
        private IModel _listnerChannel;
        private IConnection _listnerConnection;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private IChatManager _chatManager; 

        public CreateChatSessionReceiver(IOptions<RabbitMqConfiguration> rabbitMqConfigurations,
            IChatManager chatManager)
        {
            _hostname = rabbitMqConfigurations.Value.Hostname;
            _queueName = rabbitMqConfigurations.Value.QueueName;
            _username = rabbitMqConfigurations.Value.UserName;
            _password = rabbitMqConfigurations.Value.Password;
            _chatManager = chatManager;

            InitializeListener();
        }

        /// <summary>
        /// Initialize rabbit mq listner
        /// </summary>
        private void InitializeListener()
        {
            var connectionfactory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            //create connection
            _listnerConnection = connectionfactory.CreateConnection();
            _listnerConnection.ConnectionShutdown += RabbitMQConnectionShutdown;
            _listnerChannel = _listnerConnection.CreateModel();
            _listnerChannel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        /// <summary>
        /// Subscribing to the receive event
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var eventingBasicConsumer = new EventingBasicConsumer(_listnerChannel);
            eventingBasicConsumer.Received += (ch, ea) =>
            {
                var encodedContent = Encoding.UTF8.GetString(ea.Body.ToArray());
                var chatSession = JsonConvert.DeserializeObject<ChatSessionCreateDto>(encodedContent);

                CreateChatSession(chatSession);

                _listnerChannel.BasicAck(ea.DeliveryTag, false);
            };

            _listnerChannel.BasicConsume(_queueName, false, eventingBasicConsumer);

            return Task.CompletedTask;
        }

        private void CreateChatSession(ChatSessionCreateDto chatSession)
        {
            _chatManager.CreateChatSession(chatSession);
        }

        private void RabbitMQConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        /// <summary>
        /// Dispose connection
        /// </summary>
        public override void Dispose()
        {
            _listnerChannel.Close();
            _listnerConnection.Close();
            base.Dispose();
        }
    }
}
