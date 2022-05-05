using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SocialNetwork.Models;

namespace SocialNetwork.Hubs
{
    public class ChatHub : Hub
    {
        SocialEntitiesContext db = new SocialEntitiesContext();

        public void Send(string id,  string message)
        {
            AccountService accountService = new AccountService();

            var newMessage = new MessageList();
            newMessage.CreatedDate = DateTime.Now;
            newMessage.DialogId = Convert.ToInt32(id.Split(' ')[1]);
            newMessage.MessageText = message;
            newMessage.UserId = Convert.ToInt32(id.Split(' ')[0]);
            db.MessageList.Add(newMessage);
            db.SaveChanges();
            Clients.Caller.addMessageToMe(accountService.GetUserById(newMessage.UserId).UserName, message);
            Clients.Others.addMessage(accountService.GetUserById(newMessage.UserId).UserName, message, accountService.GetUserById(newMessage.UserId).PhotoURL);
        }


    }
}