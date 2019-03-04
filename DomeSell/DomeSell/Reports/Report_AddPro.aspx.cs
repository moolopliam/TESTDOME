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
    public partial class Report_AddPro : System.Web.UI.Page
    {
        private readonly  SellOTOP5929Entities db = new SellOTOP5929Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<View_User> ViewR = null;

                    ViewR = db.View_User.OrderBy(a => a.T_TilName).ToList();
                    ViewR.Where(a => a.U_Role == 1);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_ADD.rdlc");
                    //ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", ViewR);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();

                
            }
        }

        protected void Button1_OnClick1(object sender, EventArgs e)
        {
            var name = TextBox1.Text;
            var data = db.View_User.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.U_Name.Contains(name)).ToList();
            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_ADD.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);



        }

        protected void Button2_OnClick2(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Product/Index");
        }
    }
}