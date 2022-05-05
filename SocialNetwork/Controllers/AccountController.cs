using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Filters;
using System.IO;

namespace SocialNetwork.Controllers
{
    [AuthorizeFilter]
    public class AccountController : Controller
    {
        readonly AccountService AccountService = new AccountService();
        int authorizedUserId;
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            var hasIdCookie = HttpContext.Request.Cookies.AllKeys.Contains("id");
            if (hasIdCookie)
            {
                authorizedUserId = Convert.ToInt32(HttpContext.Request.Cookies.Get("id").Value);
            }
        }
        [HttpGet, InAnonymous]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost, InAnonymous]
        public ActionResult Registration(RegistrationModel RegistrInfo)
        {
            ViewBag.Message = "";
            if (RegistrInfo != null)
            {
                if (RegistrInfo.Password == RegistrInfo.Password2)
                {
                    Users newUser = AccountService.CreateUser(RegistrInfo.UserName, RegistrInfo.UserLogin, RegistrInfo.Password);
                    try
                    { 
                        authorizedUserId = newUser.Id;
                        HttpContext.Response.Cookies.Add(new HttpCookie("id", authorizedUserId.ToString()) { Expires = DateTime.Now.AddDays(1) });
                        return RedirectToAction("Index", "Account");
                    }
                    catch (InvalidDataException)
                    {
                        ViewBag.Message = "пользователь с такими данными уже существует";
                    }
                    catch
                    {
                        ViewBag.Message = "Ошибка создания пользователя";
                    }
                }
                else
                    ViewBag.Message = "Введенные пароли не совпадают";
            }
            return View();
        }
        [InAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, InAnonymous]
        public ActionResult Login(LoginModel loginInfo)
        {
            if (loginInfo != null)
            {
                Users userIn = AccountService.SerchUser(loginInfo.UserLogin, loginInfo.Password);
                if (userIn != null)
                {
                    authorizedUserId = userIn.Id;
                    HttpContext.Response.Cookies.Add(new HttpCookie("id", authorizedUserId.ToString()) { Expires = DateTime.Now.AddDays(1) });
                    return RedirectToAction("Index", "Account");
                }
                else
                    ViewBag.Message = "Неверное имя пользователя или пароль";
            }
            return View();
        }
        [InAnonymous]
        public ActionResult HelloPage()
        {
            authorizedUserId = 0;
            Response.Cookies.Add(new HttpCookie("id", "0"));
            return View();
        }
        public ActionResult Index()
        {
            return View(AccountService.GetUserById(authorizedUserId));
        }

        public ActionResult FriendPage(int userId)
        {
            return View("Index", AccountService.GetUserById(userId));
        }

        [HttpGet]
        public ActionResult Settings()
        {
            var user = AccountService.GetUserById(authorizedUserId);
            return View(user);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadAvatar)
        {
            if (uploadAvatar != null)
            {
                string fileName = System.IO.Path.GetFileName(uploadAvatar.FileName);
                string path = Routing.PathToAvatars + fileName;
                AccountService.ChengePathAvatar(authorizedUserId, path);
                uploadAvatar.SaveAs(Server.MapPath("~" + path));
            }
            return RedirectToAction("Settings", "Account");
        }
        public ActionResult DeletePage()
        {
            AccountService.RemoveUser(authorizedUserId);
            return RedirectToAction("HelloPage", "Account");
        }
        [HttpPost]
        public ActionResult ChangeFirstName(Users user)
        {
            AccountService.ChangeFirstNames(user, authorizedUserId);
            return new JsonResult { Data = user.UserName };
        }
        [HttpPost]
        public ActionResult ChangeSurname(Users user)
        {
            AccountService.ChangeSurnames(user, authorizedUserId);
            return new JsonResult { Data = user.UserSurname };
        }
        [HttpPost]
        public ActionResult ChangeDayOfBirth(Users user)
        {
            AccountService.ChangeDayOfBirths(user, authorizedUserId);
            return new JsonResult { Data = user.DayOfBirth.Value.ToString("dd.MM.yyyy")};
        }
        [HttpPost]
        public ActionResult ChangeLogin(Users user)
        {
            AccountService.ChangeLogins(user, authorizedUserId);
            return new JsonResult { Data = user.UserLogin };
        }
        [HttpPost]
        public ActionResult ChangePassword(Users user)
        {
            AccountService.ChangePasswords(user, authorizedUserId);
            return new JsonResult { Data = user.UserPassword };
        }

    }
}