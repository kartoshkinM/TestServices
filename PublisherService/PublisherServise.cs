using System;
using System.Configuration;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataHelper;
using NATS.Client;

namespace PublisherService
{
    public partial class PublisherServise : ServiceBase
    {
        private Configuration _config;
        private IConnection _connection;
        private MESSAGE _lastMessage;
        private Timer _timer;

        public PublisherServise()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _connection = new ConnectionFactory().CreateConnection();
            _lastMessage = string.IsNullOrEmpty(_config.AppSettings.Settings["lastMessageId"].Value) ? null :
                new MESSAGE
                {
                    ID = new Guid(_config.AppSettings.Settings["lastMessageId"].Value),
                    NUM = int.Parse(_config.AppSettings.Settings["lastMessageNum"].Value)
                };
            _timer = new Timer(PublishMessage, null, 0, 1000);
        }

        protected override void OnStop()
        {
            _timer.Dispose();
            _connection.Dispose();
            _config.AppSettings.Settings["lastMessageId"].Value = _lastMessage.ID.ToString();
            _config.AppSettings.Settings["lastMessageNum"].Value = _lastMessage.NUM.ToString();
            _config.Save(ConfigurationSaveMode.Modified);
        }

        private void PublishMessage(object arg)
        {
            var newMessage = _lastMessage == null
                ? MessageGenerator.GenerateNewMessage()
                : MessageGenerator.GenerateNewMessage(null, _lastMessage.ID, _lastMessage.NUM + 1);

            newMessage.SEND_TIME = DateTime.Now;
            _lastMessage = newMessage;
            _connection.Publish("messages", Encoding.UTF8.GetBytes(Serialization.SerializeMessage(newMessage)));
            Task.Delay(1000);
        }
    }
}