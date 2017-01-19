using ConsoleApplication.Examples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleApplication.Tests
{
    public class MessageManagerTest
    {
        [Fact]
        [Trait("ConsoleApplication", "Integration Test")]
        public void MessageManagerShouldStoreAndRetrieveMessages()
        {
            ISerializer serializer = new Serializer();
            File.Delete(@"..\..\messages.json");
            MessageManager messageManager = new MessageManager(serializer);
            Tweet tweet1 = new Tweet("But at my back I always hear time's winged chariot hurrying near");
            Tweet tweet2 = new Tweet("They make a desert and call it peace");
            Tweet tweet3 = new Tweet("Uneasy lies the head that wears a crown");
            Tweet tweet4 = new Tweet("The fool doth think he is wise, but the wise man knows himself to be a fool");
            Tweet tweet5 = new Tweet("What hath God wrought");

            int id1 = messageManager.AddMessage(tweet1);
            int id2 = messageManager.AddMessage(tweet2);
            int id3 = messageManager.AddMessage(tweet3);
            int id4 = messageManager.AddMessage(tweet4);
            int id5 = messageManager.AddMessage(tweet5);

            bool removed = messageManager.RemoveMessage(id2);
            Assert.True(removed);
            ICollection<Message> messages = messageManager.SelectAllMessages();
            Assert.Equal(4, messages.Count);
            messageManager = new MessageManager(new Serializer());
            messages = messageManager.SelectAllMessages();
            Assert.Equal(4, messages.Count);
            Message message1 = messageManager.SelectMessageById(id1);
            Assert.Equal(tweet1.Text, message1.Text);
            Message message2 = messageManager.SelectMessageById(id2);
            Assert.Null(message2);
        }

    }
}
