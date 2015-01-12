using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.Collections;
using DalEf;
using DevExpress.XtraReports.UI;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Models
{

    public class ReportsModel
    {
        public string ReportID { get; set; }
        public XtraReport Report { get; set; }
        public string ParametersView { get; set; }
    }

    public static class DataSources
    {


    }

}