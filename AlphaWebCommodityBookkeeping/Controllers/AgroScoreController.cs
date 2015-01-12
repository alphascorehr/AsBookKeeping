using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlphaWebCommodityBookkeeping.Controllers
{
    public class AgroScoreKontrolerController : Controller
    {
        [AllowAnonymous]
        public ActionResult IzborSortimenata()
        {
            alphascore_bisl4Entities db = new alphascore_bisl4Entities();
            var items = db.IzborSortimenata;

            JsonResult result = new JsonResult();
            result.Data = items;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [AllowAnonymous]
        public ActionResult Pesticidi()
        {
            alphascore_bisl4Entities db = new alphascore_bisl4Entities();
            var items = db.Pesticidi;

            JsonResult result = new JsonResult();
            result.Data = items;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

    }
}
