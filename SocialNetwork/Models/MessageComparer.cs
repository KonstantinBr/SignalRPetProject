using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    class MessageComparer : IComparer<MessageUserModel>
    {
        public int Compare(MessageUserModel p1, MessageUserModel p2)
        {
            if (p1.Message.CreatedDate > p2.Message.CreatedDate)
                return 1;
            else if (p1.Message.CreatedDate < p2.Message.CreatedDate)
                return -1;
            else
                return 0;
        }
    }
}