using System;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using DataHelper;
using NATS.Client;
using NATS.Client.Rx;
using NATS.Client.Rx.Ops;

namespace SubscriberService
{
    public partial class SubscriberService : ServiceBase
    {
        private IConnection _connection;
        private INATSObservable<MESSAGE> _messagesCollection;
        private TestDbContext _testDbContext;

        public SubscriberService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _testDbContext = new TestDbContext();
            _connection = new ConnectionFactory().CreateConnection();
            SubscribeMessages();
        }

        protected override void OnStop()
        {
            _messagesCollection.Dispose();
            _connection.Dispose();
            _testDbContext.Dispose();
        }

        public void SubscribeMessages()
        {
            _messagesCollection = _connection.Observe("messages")
                .Where(m => m.Data?.Any() == true)
                .Select(m => Serialization.DeserializeMessage(Encoding.UTF8.GetString(m.Data)));

            _messagesCollection.Subscribe(message =>
            {
                message.RECEIPT_TIME = DateTime.Now;
                _testDbContext.MESSAGES.Add(message);
                _testDbContext.SaveChanges();
            });
        }
    }
}