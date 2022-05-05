using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class RegistrationModel
    {
        public string UserName { get; set; }

        public string UserLogin { get; set; }

        public string Password { get; set; }

        public string Password2 { get; set; }
    }
}