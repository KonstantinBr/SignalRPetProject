using System;
using System.Linq;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Filters;

namespace SocialNetwork.Controllers
{
    [AuthorizeFilter]
    public class DialogsController : Controller
    {
        int authorizedUserId;
        DialogsService dialogsService;

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            var hasIdCookie = Request.Cookies.AllKeys.Contains("id");
            if (hasIdCookie)
            {
                authorizedUserId = int.Parse(Request.Cookies.Get("id").Value);
            }
            dialogsService = new DialogsService();
        }
        public ActionResult DialogList()
        {
            return View(dialogsService.GetDialogListByUser(authorizedUserId));
        }
        public ActionResult RemoveDialog(int id)
        {
            dialogsService.RemoveDialogs(id);
            return RedirectToAction("DialogList", "Dialogs");

        }
        public ActionResult Messages(int diadogId)
        {
            return View(dialogsService.GetMessageListByDialog(diadogId,authorizedUserId));
        }
        public ActionResult CreateDialog()
        {
            return View(dialogsService.GetUsersList(authorizedUserId));
        }


        public ActionResult AddDialogUser(int diadogId)
        {
            return View(dialogsService.GetUsersWithoutDialog(diadogId, authorizedUserId));
        }
        public ActionResult DeliteDialogUser(int diadogId, int userId)
        {
            dialogsService.RemoveUserDialog(diadogId, userId);
            return RedirectToAction("Messages", "Dialogs", new { diadogId });
        }
        public ActionResult AddDialog(string idArray, string dialogName)
        {
            SocialEntitiesContext db = new SocialEntitiesContext();
            if (dialogName == "")
                dialogName = "  ";
            Dialog newDialog = new Dialog();
            newDialog.DialogName = dialogName;
            db.Dialog.Add(newDialog);
            db.SaveChanges();
            int dialogId = newDialog.Id;
            foreach (string id in idArray.Split(','))
            {
                User_Dialog userDialog = new User_Dialog();
                userDialog.DialogId = dialogId;
                userDialog.UserId = int.Parse(id);
                db.User_Dialog.Add(userDialog);
            }
            db.SaveChanges();
            return null;
        }
    }
}