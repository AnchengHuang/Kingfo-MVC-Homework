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
            return View();
        }

        [ChildActionOnly]
        public ActionResult List()
        {
            return PartialView(KeepAccountsAPI.GetData());
        }
    }
}