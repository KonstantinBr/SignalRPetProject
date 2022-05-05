using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class UsersAndMessagesModel
    {
        public List<Users> Users { get; set; }
        public List<MessageList> Messages { get; set; }

        public List<MessageUserModel> MessagesAndUsers
        {
            get
            {
                List<MessageUserModel> model = new List<MessageUserModel>();
                for (int i = 0; i < Users.Count; i++)
                {
                    for (int j = 0; j < Messages.Count; j++)
                    {
                        if (Users[i].Id == Messages[j].UserId)
                        {
                            var item = new MessageUserModel();
                            item.User = Users[i];
                            item.Message = Messages[j];
                            model.Add(item);
                        }
                    }
                }
                model.Sort(new MessageComparer());
                return model;
            }
        }
        public Users AuthorizedUser { get; set; }
        public Dialog CurrentDialog { get; set; }
    }
}