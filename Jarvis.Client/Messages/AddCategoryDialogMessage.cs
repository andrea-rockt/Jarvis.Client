using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jarvis.Client.Model;

namespace Jarvis.Client.Messages
{
    public class AddCategoryDialogMessage:GalaSoft.MvvmLight.Messaging.GenericMessage<LocationCategory>
    {
        public AddCategoryDialogMessage(LocationCategory content) : base(content)
        {
        }

        public AddCategoryDialogMessage(object sender, LocationCategory content) : base(sender, content)
        {
        }

        public AddCategoryDialogMessage(object sender, object target, LocationCategory content) : base(sender, target, content)
        {
        }
    }
}
