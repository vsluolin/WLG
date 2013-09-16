using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WLG.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult CategoryList()
        {
            return View();
        }

        public ActionResult CategoryAdd()
        {
            return View();
        }

    }
}
