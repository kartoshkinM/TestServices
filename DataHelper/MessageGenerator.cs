using System;

namespace DataHelper
{
    public class MessageGenerator
    {
        public static MESSAGE GenerateNewMessage(string messageText = null, Guid? lastMessageId = null, int num = 0)
        {
            return new MESSAGE
            {
                ID = Guid.NewGuid(),
                LAST_MESSAGE_ID = lastMessageId,
                MESSAGE_TEXT = messageText ?? $"Новое сообщение создано в {DateTime.Now}",
                NUM = num
            };
        }
    }
}