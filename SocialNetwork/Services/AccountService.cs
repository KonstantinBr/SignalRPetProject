using System;
using System.Linq;
using System.Web.Mvc;
using SocialNetwork.Models;
using System.IO;

namespace SocialNetwork
{
    public class AccountService
    {
        readonly ISocialEntitiesContext db;
        public AccountService()
        {
            db = new SocialEntitiesContext();
        }
        public AccountService(ISocialEntitiesContext dbContext)
        {
            db = dbContext;
        }
        public Users CreateUser(string name, string login, string password)
        {
                Users newUser = new Users
                {
                    UserName = name,
                    UserLogin = login,
                    UserPassword = password
                };
                var userExist = db.Users.Any(p => p.UserLogin == login);
                if (userExist)
                {
                   throw new InvalidDataException();
                }
                db.Users.Add(newUser);
                db.SaveChanges();
                return newUser;
        }
        public Users SerchUser(string login, string password)
        {
            return db.Users.Where(p => p.UserLogin == login && p.UserPassword == password).FirstOrDefault();
        }
        public static Users GetUserById(int id, ISocialEntitiesContext dbContext)
        {
            return dbContext.Users.Where(p => p.Id == id).FirstOrDefault();
        }
        public Users GetUserById(int id)
        {
            return GetUserById(id, db);
        }
        public bool ChengePathAvatar(int authorizedUserId, string path)
        {
            try
            {
                var existedUser = AccountService.GetUserById(authorizedUserId, db);
                if (existedUser.PhotoURL != null)
                {
                    var oldPath = AppDomain.CurrentDomain.BaseDirectory + existedUser.PhotoURL.Substring(6);
                    FileInfo fileInf = new FileInfo(oldPath);
                    if (fileInf.Exists)
                        fileInf.Delete();
                }
                existedUser.PhotoURL = Routing.backPathToAvatars + path;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }  
        }
        public bool RemoveUser(int id)
        {
            try
            {
                var existedUser = db.Users.Where(p => p.Id == id).FirstOrDefault();
                if (existedUser != null)
                {
                    db.Users.Remove(existedUser);
                }
                db.SaveChanges();

                var existedMessages = db.MessageList.Where(p => p.UserId == id);
                foreach (MessageList message in existedMessages)
                    db.MessageList.Remove(message);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeFirstNames(Users user, int authorizedUserId )
        {
            try
            {
                GetUserById(authorizedUserId, db).UserName = user.UserName;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeSurnames(Users user, int authorizedUserId)
        {
            try
            {
                GetUserById(authorizedUserId, db).UserSurname = user.UserSurname;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeDayOfBirths(Users user, int authorizedUserId)
        {
            try
            {
                GetUserById(authorizedUserId, db).DayOfBirth = user.DayOfBirth;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangePasswords(Users user, int authorizedUserId)
        {
            try
            {
                GetUserById(authorizedUserId, db).UserPassword = user.UserPassword;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeLogins(Users user, int authorizedUserId)
        {
            try
            {
                GetUserById(authorizedUserId, db).UserLogin = user.UserLogin;
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