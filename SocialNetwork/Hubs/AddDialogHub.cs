using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SocialNetwork.Models;

namespace SocialNetwork.Hubs
{
    public class AddDialogHub : Hub
    {
        SocialEntitiesContext db = new SocialEntitiesContext();

        public void Send(int[] idArray, string dialogName)
        {
            if (dialogName == "")
                dialogName = "  ";
            Dialog newDialog = new Dialog();
            newDialog.DialogName = dialogName;
            db.Dialog.Add(newDialog);
            db.SaveChanges();
            int dialogId = newDialog.Id;
            foreach (int id in idArray)
            {
                User_Dialog userDialog = new User_Dialog();
                userDialog.DialogId = dialogId;
                userDialog.UserId = id;
                db.User_Dialog.Add(userDialog);
            }
            db.SaveChanges();
        }
    }
}