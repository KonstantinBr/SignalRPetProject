using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SocialNetwork.Models;

namespace SocialNetwork.Hubs
{
    public class AddDialogUserHub : Hub
    {
        SocialEntitiesContext db = new SocialEntitiesContext();

        public void Send(int[] idUser, int idDialog)
        {
            foreach(int item in idUser)
            {
                User_Dialog newUserDialog = new User_Dialog();
                newUserDialog.DialogId = idDialog;
                newUserDialog.UserId = item;
                db.User_Dialog.Add(newUserDialog);
            }
            db.SaveChanges();
        }
    }
}