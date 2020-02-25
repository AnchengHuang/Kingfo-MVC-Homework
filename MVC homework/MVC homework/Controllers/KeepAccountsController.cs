using MVC_homework.Models;
using MVC_homework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_homework.Controllers
{
    public class KeepAccountsController : Controller
    {
        private static Model1 _model1 = new Model1();

        // GET: KeepAccounts
        public ActionResult Index(Guid? id)
        {
            ViewBag.Category = KeepAccountsAPI.GetKeepAccountType();

            var model = id.HasValue ? _model1.AccountBooks.Find(id) : null;

            if (model != null)
                _model1.Entry(model).Reload();//要加這行才不會讀到快取的舊資料

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult List()
        {
            return PartialView(KeepAccountsAPI.GetData());
        }

        public ActionResult Save(FormCollection form, Guid? id)
        {
            ViewBag.Category = KeepAccountsAPI.GetKeepAccountType();

            Dictionary<string, string> errorMsg = KeepAccountsAPI.DataCheck(form);
            ViewBag.ErrorDic = errorMsg;

            if (errorMsg.Any())
                return View("Index");

            if (id.HasValue)
            {
                if (!KeepAccountsAPI.Edit(form, id))
                {
                    errorMsg.Add("EditFail", "編輯失敗!");
                    return View("Index");
                }
            }
            else
            {
                if (!KeepAccountsAPI.Create(form))
                {
                    errorMsg.Add("CreateFail", "新增失敗!");
                    return View("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>(); ;
            ViewBag.ErrorDic = errorMsg;

            if (!KeepAccountsAPI.Delete(id))
            {
                errorMsg.Add("DeleteFail", "刪除失敗!");
                return View("Index");
            }

            return RedirectToAction("Index");
        }
    }
}