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
        // GET: KeepAccounts
        public ActionResult Index()
        {
            ViewBag.Category = KeepAccountsAPI.GetKeepAccountType();
            return View();
        }

        [ChildActionOnly]
        public ActionResult List()
        {
            return PartialView(KeepAccountsAPI.GetData());
        }

        public ActionResult Create(FormCollection form)
        {
            ViewBag.Category = KeepAccountsAPI.GetKeepAccountType();

            Dictionary<string, string> errorMsg = KeepAccountsAPI.DataCheck(form);
            ViewBag.ErrorDic = errorMsg;

            if (errorMsg.Any())
                return View("Index");

            if (!KeepAccountsAPI.Create(form))
                errorMsg.Add("CreateFail", "新增失敗!");

            return View("Index");
        }        
    }
}