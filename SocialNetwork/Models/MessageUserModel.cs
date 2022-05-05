using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class MessageUserModel
    {
        public Users User { get; set; }
        public MessageList Message { get; set; }
    }
}