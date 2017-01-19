using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class Basket : System.Web.UI.Page
    {
        private Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            order = Session["order"] as Order;
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = order;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EfOrderModel orderModel = new EfOrderModel();
            orderModel.Create(order);
            Server.Transfer("Confirm.aspx");
        }
    }
}