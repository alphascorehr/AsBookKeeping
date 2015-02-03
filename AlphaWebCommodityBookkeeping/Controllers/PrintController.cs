using AlphaWebCommodityBookkeeping.Areas.Documents.Reports;
using DevExpress.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlphaWebCommodityBookkeeping.Controllers
{
    class PrintController : Controller
    {
        public ActionResult Print(FormCollection collection)
        {

            int _vrstadokumenta = Convert.ToInt32(System.Web.HttpContext.Current.Session["vrstadokumenta"]);

            int tip = Convert.ToInt32(System.Web.HttpContext.Current.Session["tip"]);
            int _tip = 0;

            if (tip >= 30)
                _tip = 3;
            else
                _tip = tip;

            int _od = 0, _do = 0;
            string _podnaslov = string.Empty;
            string _period = string.Empty;

            switch (tip)
            {
                case 0:
                    _period = "Ukupna potraživanja";
                    break;
                case 1:
                    _period = "Dospjela potraživanja";
                    break;
                case 2:
                    _period = "Nedospjela potraživanja";
                    break;
                case 30:
                    _od = 0;
                    _do = 30;
                    _period = "Dospjelo " + _od.ToString() + "-" + _do.ToString() + " dana";
                    break;
                case 31:
                    _od = 31;
                    _do = 60;
                    _period = "Dospjelo " + _od.ToString() + "-" + _do.ToString() + " dana";
                    break;
                case 32:
                    _od = 61;
                    _do = 90;
                    _period = "Dospjelo " + _od.ToString() + "-" + _do.ToString() + " dana";
                    break;
                case 4:
                    _od = 90;
                    _period = "Dospjelo preko " + _od.ToString() + " dana";
                    break;
                default:
                    break;
            }

            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);
            int? client = Convert.ToInt32(System.Web.HttpContext.Current.Session["client"]);
            DateTime? datumOd = Convert.ToDateTime(System.Web.HttpContext.Current.Session["datumOd"]);
            DateTime? datumDo = Convert.ToDateTime(System.Web.HttpContext.Current.Session["datumDo"]);
            DateTime? valutaOd = Convert.ToDateTime(System.Web.HttpContext.Current.Session["valutaOd"]);
            DateTime? valutaDo = Convert.ToDateTime(System.Web.HttpContext.Current.Session["valutaDo"]);

            bool? filtered = Convert.ToBoolean(System.Web.HttpContext.Current.Session["filtered"]);


            System.Web.HttpContext.Current.Session["client"] = null;
            System.Web.HttpContext.Current.Session["filtered"] = false;
            System.Web.HttpContext.Current.Session["datumOd"] = null;
            System.Web.HttpContext.Current.Session["datumDo"] = null;
            System.Web.HttpContext.Current.Session["valutaOd"] = null;
            System.Web.HttpContext.Current.Session["valutaDo"] = null;

            xrInvocesClaimsDetails report = new xrInvocesClaimsDetails();
            switch (source)
            {
                case 0: // IRA
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        if (!filtered ?? false)
                        {
                            var item = data.uspInvoicesClaimsDetails(_od, _do, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip, null);
                            report.bindingSourceMainPage.DataSource = item;
                            _podnaslov = "Otvorena potraživanja";
                            report.LabelPodnaslov.Text = _podnaslov;
                        }
                        else
                        {
                            var item = data.uspInvoicesClaimsDetailsFilter(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, client, datumOd, datumDo, valutaOd, valutaOd);
                            report.bindingSourceMainPage.DataSource = item;
                            _podnaslov = "Otvorena potraživanja";
                            report.LabelPodnaslov.Text = "";
                        }
                    }
                    break;
                case 1: // URA
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        if (!filtered ?? false)
                        {
                            var item = data.uspIncomingInvoicesClaimsDetails(_od, _do, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip, null);
                            report.bindingSourceMainPage.DataSource = item;

                            _podnaslov = "Otvorena dugovanja";
                            report.LabelPodnaslov.Text = _podnaslov;
                        }
                        else
                        {
                            var item = data.uspIncomingInvoicesClaimsDetailsFilter(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, client, datumOd, datumDo, valutaOd, valutaOd);
                            report.bindingSourceMainPage.DataSource = item;
                            _podnaslov = "Otvorena dugovanja";
                            report.LabelPodnaslov.Text = "";
                        }
                    }
                    break;

                default:
                    break;
            }

            using (DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities())
            {
                var item = data.MDSubjects_Subject.FirstOrDefault(a => a.Id == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);

                report.LabelTvrtkaNaziv.Text = item.Name;
                report.LabelTvrtkaAdresa.Text = item.HomeAddress_Street;
                report.LabelTvrtkaOIB.Text = item.OIB;
                //report.LabelTvrtkaTelefon.Text = item;

                report.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
                report.LabelPodnaslov.Text = _podnaslov;
                report.LabelPeriod.Text = _period;
                report.CreateDocument();
            }
            return PartialView("ReportForMainPage", report);
        }
    }
}
