using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomeSell.Models;
using Microsoft.Reporting.WebForms;

namespace DomeSell.Reports
{
    public partial class Web_Order : System.Web.UI.Page
    {
        private readonly SellOTOP5929Entities db = new SellOTOP5929Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<View_BayOr> ViewR = null;

                ViewR = db.View_BayOr.OrderBy(a => a.O_User).ToList();
                ViewR.Where(a => a.O_Status == 4);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Order.rdlc");
                //ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", ViewR);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();


            }
        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            var name = TextBox1.Text;
            var data = db.View_BayOr.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.O_User.Contains(name)).ToList();
            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Order.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button2_OnClick(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Product/Index");

        }
    }
}