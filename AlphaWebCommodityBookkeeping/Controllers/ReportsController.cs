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
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        [ValidateInput(false)]
        public ActionResult Index()
        {
            ViewData["reportType"] = System.Web.HttpContext.Current.Session["reportType"];
            ViewData["source"] = System.Web.HttpContext.Current.Session["source"];


           

            
           

            ViewData["Cache"] = HttpRuntime.Cache["Pero"];
            return View();
        }

        public ActionResult Print(FormCollection collection)
        {
            //if(string.IsNullOrEmpty(collection["check"]))
            //    return PartialView("ReportForMainPage");

            // prvi parametar je tip koji se prosljeduje u storu, a drugi je 0 za izlazne fakture, 1 za ulazne fakture, 2 za nefakturiano, 3 za dospjeca
            
            // @Source = 0 za izlazne fakture
            // @Source = 1 za ulazne fakture
            // @Source = 2 za nefakturiano
            // @Source = 3 za dospjeca

            // @Tip = 0 - Ukupna potrazivanja
            // @Tip = 1 - Dospjela potrazivanja
            // @Tip = 2 - Nedospjela potrazivanja
            // @Tip = 30 - Od = 0  do = 30 potrazivanja
            // @Tip = 31 - Od = 30 do = 60 potrazivanja
            // @Tip = 32 - Od = 60 do = 90 potrazivanja
            // @Tip = 4 - preko 90 potrazivanja

            //All- @VrstaDokumenta = 0 
            //Offer - @VrstaDokumenta = 2 
            //Quote - @VrstaDokumenta = 5
            //WorkOrder - @VrstaDokumenta = 7

            // @Source = 3 za dospjeca
            //@Tip = 20 - IRA Danas
            //@Tip = 21 - IRA 5 dana
            //@Tip = 22 - IRA 10 dana
            //@Tip = 23 - IRA 15 dana
            //@Tip = 24 - URA Danas
            //@Tip = 25 - URA 5 dana
            //@Tip = 26 - URA 10 dana
            //@Tip = 27 - URA 15 dana

            int _vrstadokumenta = Convert.ToInt32(System.Web.HttpContext.Current.Session["vrstadokumenta"]);
            
            int tip = Convert.ToInt32(System.Web.HttpContext.Current.Session["tip"]);
            int _tip = 0;

            if (tip >= 30)
                _tip = 3;
            else
                _tip = tip;

            int _od = 0, _do = 0;
           string  _podnaslov = string.Empty;
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
                    _period = "Dospjelo " + _od.ToString() + "-" + _do.ToString()+" dana";
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
            //int? clientSP = null;
            //if (client != 0)
            //    clientSP = client;
            
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
                            var item = data.uspInvoicesClaimsDetailsFilter(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId,client,datumOd,datumDo,valutaOd,valutaOd);
                            report.bindingSourceMainPage.DataSource = item;
                            _podnaslov = "Otvorena potraživanja";
                            report.LabelPodnaslov.Text = "";
                        }
                        //int count = item.Count();
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
                            var item = data.uspIncomingInvoicesClaimsDetailsFilter(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId,client,datumOd,datumDo,valutaOd,valutaOd);
                            report.bindingSourceMainPage.DataSource = item;
                            _podnaslov = "Otvorena dugovanja";
                            report.LabelPodnaslov.Text = "";
                        }
                    }
                    break;
                //case 2: // nefakturirano
                //    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                //    {
                //        var item = data.uspUnbilledDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _vrstadokumenta);
                //        xrUnbilledDetails report_unbilled = new xrUnbilledDetails();
                //        report_unbilled.bindingSourceMainPage.DataSource = item;
                //        _podnaslov = "Nefakturirano";
                //        report_unbilled.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
                //        report_unbilled.LabelPodnaslov.Text = _podnaslov;
                //        report_unbilled.LabelPeriod.Text = _period;
                //        report_unbilled.CreateDocument();

                //        return PartialView("ReportForMainPageUnbilled", report_unbilled);
                //    }
                //case 3: // dospjeca
                //    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                //    {
                //        var item = data.uspPaymentStateDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip);
                //        xrFutureClaimsDetails report_future = new xrFutureClaimsDetails();
                //        report_future.bindingSourceMainPage.DataSource = item;
                //        _podnaslov = "Buduće obaveze";

                //        report_future.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
                //        report_future.LabelPodnaslov.Text = _podnaslov;
                //        report_future.LabelPeriod.Text = _period;
                //        report_future.CreateDocument();

                //        return PartialView("ReportForMainPageFuture", report_future);
                //    }
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

        public ActionResult PrintUnbilled(FormCollection collection)
        {
            //if(string.IsNullOrEmpty(collection["check"]))
            //    return PartialView("ReportForMainPage");

            // prvi parametar je tip koji se prosljeduje u storu, a drugi je 0 za izlazne fakture, 1 za ulazne fakture, 2 za nefakturiano, 3 za dospjeca

            // @Source = 0 za izlazne fakture
            // @Source = 1 za ulazne fakture
            // @Source = 2 za nefakturiano
            // @Source = 3 za dospjeca

            // @Tip = 0 - Ukupna potrazivanja
            // @Tip = 1 - Dospjela potrazivanja
            // @Tip = 2 - Nedospjela potrazivanja
            // @Tip = 30 - Od = 0  do = 30 potrazivanja
            // @Tip = 31 - Od = 30 do = 60 potrazivanja
            // @Tip = 32 - Od = 60 do = 90 potrazivanja
            // @Tip = 4 - preko 90 potrazivanja

            //All- @VrstaDokumenta = 0 
            //Offer - @VrstaDokumenta = 2 
            //Quote - @VrstaDokumenta = 5
            //WorkOrder - @VrstaDokumenta = 7

            // @Source = 3 za dospjeca
            //@Tip = 20 - IRA Danas
            //@Tip = 21 - IRA 5 dana
            //@Tip = 22 - IRA 10 dana
            //@Tip = 23 - IRA 15 dana
            //@Tip = 24 - URA Danas
            //@Tip = 25 - URA 5 dana
            //@Tip = 26 - URA 10 dana
            //@Tip = 27 - URA 15 dana

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
                default:
                    break;
            }

            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);

            xrUnbilledDetails report = new xrUnbilledDetails();
            switch (source)
            {
                case 2: // nefakturirano
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        var item = data.uspUnbilledDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _vrstadokumenta);
                        report.bindingSourceMainPage.DataSource = item;
                        _podnaslov = "Nefakturirani dokumenti";

                    }
                    break;
                default:
                    break;
            }
            report.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
            report.LabelPodnaslov.Text = _podnaslov;
            report.LabelPeriod.Text = _period;
            report.CreateDocument();
            return PartialView("ReportForMainPageUnbilled", report);
        }

        public ActionResult PrintFuture(FormCollection collection)
        {
            //if(string.IsNullOrEmpty(collection["check"]))
            //    return PartialView("ReportForMainPage");

            // prvi parametar je tip koji se prosljeduje u storu, a drugi je 0 za izlazne fakture, 1 za ulazne fakture, 2 za nefakturiano, 3 za dospjeca

            // @Source = 0 za izlazne fakture
            // @Source = 1 za ulazne fakture
            // @Source = 2 za nefakturiano
            // @Source = 3 za dospjeca

            // @Tip = 0 - Ukupna potrazivanja
            // @Tip = 1 - Dospjela potrazivanja
            // @Tip = 2 - Nedospjela potrazivanja
            // @Tip = 30 - Od = 0  do = 30 potrazivanja
            // @Tip = 31 - Od = 30 do = 60 potrazivanja
            // @Tip = 32 - Od = 60 do = 90 potrazivanja
            // @Tip = 4 - preko 90 potrazivanja

            //All- @VrstaDokumenta = 0 
            //Offer - @VrstaDokumenta = 2 
            //Quote - @VrstaDokumenta = 5
            //WorkOrder - @VrstaDokumenta = 7

            // @Source = 3 za dospjeca
            //@Tip = 20 - IRA Danas
            //@Tip = 21 - IRA 5 dana
            //@Tip = 22 - IRA 10 dana
            //@Tip = 23 - IRA 15 dana
            //@Tip = 24 - URA Danas
            //@Tip = 25 - URA 5 dana
            //@Tip = 26 - URA 10 dana
            //@Tip = 27 - URA 15 dana

            int _vrstadokumenta = Convert.ToInt32(System.Web.HttpContext.Current.Session["vrstadokumenta"]);

            int tip = Convert.ToInt32(System.Web.HttpContext.Current.Session["tip"]);
            int _tip = 0;

            string _podnaslov = string.Empty;
            string _period = string.Empty;

            switch (tip)
            {
                case 20:
                    _period = "Danas";
                    _podnaslov = "Buduća potraživanja";
                    break;
                case 21:
                    _period = "U slijedećih 5 dana";
                    _podnaslov = "Buduća potraživanja";
                    break;
                case 22:
                    _period = "U slijedećih 10 dana";
                    _podnaslov = "Buduća potraživanja";
                    break;
                case 23:
                    _period = "U slijedećih 15 dana";
                    _podnaslov = "Buduća potraživanja";
                    break;
                case 24:
                    _period = "Danas";
                    _podnaslov = "Buduće obaveze";
                    break;
                case 25:
                    _period = "U slijedećih 5 dana";
                    _podnaslov = "Buduće obaveze";
                    break;
                case 26:
                    _period = "U slijedećih 10 dana";
                    _podnaslov = "Buduće obaveze";
                    break;
                case 27:
                    _period = "U slijedećih 15 dana";
                    _podnaslov = "Buduće obaveze";
                    break;
                default:
                    break;
            }

            xrFutureClaimsDetails report = new xrFutureClaimsDetails();
            if (tip != 20 && tip != 21 && tip != 22 && tip != 23 && tip != 24 && tip != 25 && tip != 27 && tip != 27)
                return PartialView("ReportForMainPageFuture", report);
            else
            {
                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                {
                    var item = data.uspPaymentStateDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, tip);
                    report.bindingSourceMainPage.DataSource = item;
                }

                report.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
                report.LabelPodnaslov.Text = _podnaslov;
                report.LabelPeriod.Text = _period;
                report.CreateDocument();
                return PartialView("ReportForMainPageFuture", report);
            }
        }

        public ActionResult IndexPost(FormCollection collection)
        {
            int tip = Convert.ToInt32(collection["tip"]);
            int source = Convert.ToInt32(collection["source"]);
            int report = Convert.ToInt32(collection["reportType"]);

            System.Web.HttpContext.Current.Session["tip"] = tip;
            System.Web.HttpContext.Current.Session["source"] = source;
            System.Web.HttpContext.Current.Session["vrstadokumenta"] = -1;
            System.Web.HttpContext.Current.Session["reportType"] = report;
            //RedirectToAction("Index");
            return View("Index");
        }

        public ActionResult IndexPostVrstaDockumenta(FormCollection collection)
        {
            int vrstadokumenta = Convert.ToInt32(collection["vrstadokumenta"]);
            int report = Convert.ToInt32(collection["reportType"]);
            System.Web.HttpContext.Current.Session["vrstadokumenta"] = vrstadokumenta;
            System.Web.HttpContext.Current.Session["tip"] = -1;
            System.Web.HttpContext.Current.Session["source"] = 2;
            System.Web.HttpContext.Current.Session["reportType"] = report;
            return View("Index");
        }

        public ActionResult IndexPostNaDan(FormCollection collection)
        {
            int tip = Convert.ToInt32(collection["tip"]);
            int report = Convert.ToInt32(collection["reportType"]);
            System.Web.HttpContext.Current.Session["vrstadokumenta"] = -1;
            System.Web.HttpContext.Current.Session["tip"] = tip;
            System.Web.HttpContext.Current.Session["source"] = 3;
            System.Web.HttpContext.Current.Session["reportType"] = report;
            return View("Index");
        }

        public ActionResult ReportExportToUnbilled()
        {
            //int tip = Convert.ToInt32(System.Web.HttpContext.Current.Session["tip"]);
            //int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);
            //int _vrstadokumenta = Convert.ToInt32(System.Web.HttpContext.Current.Session["vrstadokumenta"]);
            //xrUnbilledDetails report = new xrUnbilledDetails();
            //using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            //{
            //    var item = data.uspUnbilledDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _vrstadokumenta);
            //    report.bindingSourceMainPage.DataSource = item;

            //}
            //report.CreateDocument();

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
                default:
                    break;
            }

            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);

            xrUnbilledDetails report = new xrUnbilledDetails();
            switch (source)
            {
                case 2: // nefakturirano
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        var item = data.uspUnbilledDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _vrstadokumenta);
                        report.bindingSourceMainPage.DataSource = item;
                        _podnaslov = "Nefakturirano";

                    }
                    break;
                default:
                    break;
            }
            report.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
            report.LabelPodnaslov.Text = _podnaslov;
            report.LabelPeriod.Text = _period;
            report.CreateDocument();

            return ReportViewerExtension.ExportTo(report);
        }

        public ActionResult ReportExportTo()
        {
            //int tip = Convert.ToInt32(System.Web.HttpContext.Current.Session["tip"]);
            //int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);
            //xrInvocesClaimsDetails report = new xrInvocesClaimsDetails();
            //using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            //{
            //    var item = data.uspInvoicesClaimsDetails(0, 30, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, tip, null);
            //    report.bindingSourceMainPage.DataSource = item;
            //    //int count = item.Count();
            //}
            //if(string.IsNullOrEmpty(collection["check"]))
            //    return PartialView("ReportForMainPage");

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
            int client = Convert.ToInt32(System.Web.HttpContext.Current.Session["client"]);
            int? clientSP = null;
            if (client != 0)
                clientSP = client;
            System.Web.HttpContext.Current.Session["client"] = null;
            xrInvocesClaimsDetails report = new xrInvocesClaimsDetails();
            switch (source)
            {
                case 0: // IRA
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        var item = data.uspInvoicesClaimsDetails(_od, _do, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip, clientSP);
                        report.bindingSourceMainPage.DataSource = item;
                        _podnaslov = "Otvorena potraživanja";
                        report.LabelPodnaslov.Text = _podnaslov;
                        //int count = item.Count();
                    }
                    break;
                case 1: // URA
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        var item = data.uspIncomingInvoicesClaimsDetails(_od, _do, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip, clientSP);
                        report.bindingSourceMainPage.DataSource = item;

                        _podnaslov = "Otvorena dugovanja";
                        report.LabelPodnaslov.Text = _podnaslov;
                        //int count = item.Count();
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
            return ReportViewerExtension.ExportTo(report);
        }

        public ActionResult ReportExportToFuture()
        {
            //int _tip = Convert.ToInt32(System.Web.HttpContext.Current.Session["tip"]);
            //int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);
            //xrFutureClaimsDetails report = new xrFutureClaimsDetails();
            //using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            //{
            //    var item = data.uspPaymentStateDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip);
            //    report.bindingSourceMainPage.DataSource = item;
            //}
            //report.CreateDocument();
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
                default:
                    break;
            }

            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["source"]);

            xrFutureClaimsDetails report = new xrFutureClaimsDetails();
            switch (source)
            {
                case 3: // dospjeca
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        var item = data.uspPaymentStateDetails(((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, _tip);
                        report.bindingSourceMainPage.DataSource = item;
                        _podnaslov = "Buduće obaveze";
                    }
                    break;
                default:
                    break;
            }
            report.LabelDatumIspisa.Text = DateTime.Now.ToShortDateString();
            report.LabelPodnaslov.Text = _podnaslov;
            report.LabelPeriod.Text = _period;
            report.CreateDocument();
            return ReportViewerExtension.ExportTo(report);
        }

        public ActionResult Filter(FormCollection collection)
        {
            int report = Convert.ToInt32(collection["reportType"]);
            int? client = null;
            DateTime? datumOd = null;
            DateTime? datumDo = null;
            DateTime? valutaOd = null;
            DateTime? valutaDo = null;
            bool? filtered = null;

            if (collection["client"] != "null")
                client = Convert.ToInt32(collection["client"]);

            if (collection["datumOd"] != "null" && collection["datumOd"] != "")
                datumOd = Convert.ToDateTime(collection["datumOd"]);
            else
                datumOd = new DateTime(1900,1,1);

            if (collection["datumDo"] != "null" && collection["datumDo"] != "")
                datumDo = Convert.ToDateTime(collection["datumDo"]);
            else
                datumDo = new DateTime(1900,1,1);

            if (collection["valutaOd"] != "null" && collection["valutaOd"] != "")
                valutaOd = Convert.ToDateTime(collection["valutaOd"]);
            else
                valutaOd = new DateTime(1900, 1, 1);

            if (collection["valutaDo"] != "null" && collection["valutaDo"] != "")
                valutaDo = Convert.ToDateTime(collection["valutaDo"]);
            else
                valutaDo = new DateTime(1900, 1, 1);

            if (collection["filtered"] != "null")
                filtered = Convert.ToBoolean(collection["filtered"]);

            System.Web.HttpContext.Current.Session["reportType"] = report;
            System.Web.HttpContext.Current.Session["client"] = client;

            System.Web.HttpContext.Current.Session["datumOd"] = datumOd;
            System.Web.HttpContext.Current.Session["datumDo"] = datumDo;

            System.Web.HttpContext.Current.Session["valutaOd"] = valutaOd;
            System.Web.HttpContext.Current.Session["valutaDo"] = valutaDo;

            System.Web.HttpContext.Current.Session["filtered"] = filtered;

            JsonResult res = new JsonResult();
            res.Data = true;
            return res;
        }
    }
}
