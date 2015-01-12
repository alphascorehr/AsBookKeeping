using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Reports
{
    public partial class xrWorkOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public xrWorkOrder()
        {
            InitializeComponent();

            xrWorkOrderTimeSub wot = new xrWorkOrderTimeSub();
            xrSubreport1.ReportSource = wot;
            
        }

        

    }
}
