using System;
using System.Collections.Generic;

namespace SocialNetwork.Models
{
    
    public partial class User_Dialog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DialogId { get; set; }
    
        public virtual Dialog Dialog { get; set; }
        public virtual Users Users { get; set; }
    }
}
