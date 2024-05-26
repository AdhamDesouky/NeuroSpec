using NeuroSpecCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Views.ChatBot.Templates
{
    internal class MessageDataTemplateSelector:DataTemplateSelector
    {
        public DataTemplate SenderMessageTemplate { get; set; }
        public DataTemplate ReceiverMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = (MessageModel)item;

            if (!message.IsSender)
                return ReceiverMessageTemplate;

            return SenderMessageTemplate;
        }
    }
}
