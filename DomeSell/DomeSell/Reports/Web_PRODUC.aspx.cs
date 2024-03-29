﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomeSell.Models;
using Microsoft.Reporting.WebForms;

namespace DomeSell.Reports
{
    public partial class Web_PRODUC : System.Web.UI.Page
    {
        private readonly SellOTOP5929Entities db = new SellOTOP5929Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<View_Product> ViewR = null;

                ViewR = db.View_Product.OrderBy(a => a.P_ProName).ToList();

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_PRODUC.rdlc");
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
            var data = db.Table_Product.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.P_ProName.Contains(name)).ToList();
            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_PRODUC.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button2_OnClick(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Product/Index");

        }
    }
}