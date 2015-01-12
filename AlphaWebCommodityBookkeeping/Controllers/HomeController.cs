using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AlphaWebCommodityBookkeeping.Areas.Documents.Reports;

namespace AlphaWebCommodityBookkeeping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                int message = -1;

                    if (!Csla.ApplicationContext.User.IsInRole("SuperAdmin"))
                    {
                        DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();
                        var itemSubject = data.Auth_Company.FirstOrDefault(p => p.Id == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                        var noDays = itemSubject.NumbnerOfDays;
                        var acDate = itemSubject.ActivationDate;
                        if (noDays != null && acDate != null)
                        {
                            int NumberOfdays = Convert.ToInt32(noDays);
                            string sub = "N/A";

                            DateTime dt = DateTime.Now;
                            DateTime Expirationdate = Convert.ToDateTime(acDate).AddDays(NumberOfdays);
                            TimeSpan ts = Expirationdate.Subtract(dt);
                            int DaysRemaining = Convert.ToInt32(ts.Days);
                            if (DaysRemaining <= 20)
                            {
                                message = DaysRemaining;
                            }
                        }
                    }
                ViewData["Message"] = message;
                return View();
        }

        [HttpPost]
        public ActionResult Index(BusinessObjects.MDSubjects.cMDSubjects_Enums_Bank obj)
        {
            return View();
        }

        public ActionResult PaymentClosureGridPartial()
        {
            return PartialView("PaymentClosureGridPartial");
        }
        public ActionResult IncomingPaymentClosureGridPartial()
        {
            return PartialView("IncomingPaymentClosureGridPartial");
        }
        public ActionResult PaymentClosureChartPartial()
        {
            return PartialView("PaymentClosureChartPartial");
        }

        public ActionResult Knockout()
        {
            return View();
        }

        public ActionResult CollectionDataToClient()
        {
            JsonResult result = new JsonResult();
            List<Tuple<string, decimal>> lst = new List<Tuple<string, decimal>>();

            for (int i = 0; i <= 5; i++)
            {
                Tuple<string, decimal> l = new Tuple<string, decimal>("pero peric", 12.00M);
                lst.Add(l);
            }
            result.Data = lst;
            return result;
        }
        

        public ActionResult KnockoutPost(FormCollection collection)
        {
            string test = collection["ko_unique_1"];
            return View("Knockout");
        }
    }
}
