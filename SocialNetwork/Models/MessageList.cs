using System;
using System.Collections.Generic;

namespace SocialNetwork.Models
{
    
    public partial class MessageList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DialogId { get; set; }
        public string MessageText { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> DateOfChange { get; set; }
    
        public virtual Dialog Dialog { get; set; }
    }
}
