using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Filters;
using System.IO;

namespace SocialNetwork
{
    public class DialogsService
    {
        ISocialEntitiesContext db;
        public DialogsService()
        {
            db = new SocialEntitiesContext();
        }
        public DialogsService(ISocialEntitiesContext dbContext)
        {
            db = dbContext;
        }
        public IEnumerable<Dialog> GetDialogListByUser(int authorizedUserId)
        {
            try
            {
                return db.Dialog.Join(db.User_Dialog.Where(p => p.UserId == authorizedUserId), p => p.Id, t => t.DialogId, (p, t) => p);
            }
            catch
            {
                return null;
            }
        }
        public bool RemoveDialogs(int id)
        {
            try
            {
                db.Dialog.Remove(db.Dialog.Where(p => p.Id == id).FirstOrDefault());
                db.User_Dialog.RemoveRange(db.User_Dialog.Where(p => p.DialogId == id));
                db.MessageList.RemoveRange(db.MessageList.Where(p => p.DialogId == id));
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public UsersAndMessagesModel GetMessageListByDialog(int diadogId, int authorizedUserId)
        {
            AccountService accountService = new AccountService(db);
            UsersAndMessagesModel model = new UsersAndMessagesModel();
            model.Messages = db.MessageList.Where(t => t.DialogId == diadogId).ToList();
            model.Users = db.Users.Join(db.User_Dialog.Where(p => p.DialogId == diadogId), p => p.Id, t => t.UserId, (p, t) => p).ToList();
            model.AuthorizedUser = accountService.GetUserById(authorizedUserId);
            model.CurrentDialog = db.Dialog.Where(p => p.Id == diadogId).FirstOrDefault();
            return model;
        }
        public UsersAndMessagesModel GetUsersList(int authorizedUserId)
        {
            AccountService accountService = new AccountService(db);
            UsersAndMessagesModel model = new UsersAndMessagesModel();
            model.Users = db.Users.ToList();
            model.AuthorizedUser = accountService.GetUserById(authorizedUserId);
            return model;
        }
        public UsersAndMessagesModel GetUsersWithoutDialog(int diadogId, int authorizedUserId)
        {
            AccountService accountService = new AccountService(db);
            UsersAndMessagesModel model = new UsersAndMessagesModel();
            var usersByDialog = db.Users.Join(db.User_Dialog.Where(p => p.DialogId == diadogId), p => p.Id, t => t.UserId, (p, t) => p);
            model.Users = db.Users.Except(usersByDialog).ToList();
            model.CurrentDialog = db.Dialog.Where(p => p.Id == diadogId).FirstOrDefault();
            model.AuthorizedUser = accountService.GetUserById(authorizedUserId);
            return model;
        }
        public bool RemoveUserDialog(int diadogId, int userId)
        {
            try
            {
                var removedItem = db.User_Dialog.Where(p => p.DialogId == diadogId && p.UserId == userId).FirstOrDefault();
                db.User_Dialog.Remove(removedItem);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}