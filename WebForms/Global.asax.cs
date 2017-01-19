using Core.DataAccess.EntityFramework;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            Order order = new Order();
            EfAccountModel accountModel = new EfAccountModel();
            Core.Entity.Account account = accountModel["acc1"];
            order.Account = account;
            Session["order"] = order;

        }
    }
}