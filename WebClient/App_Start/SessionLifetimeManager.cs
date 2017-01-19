using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace WebClient.App_Start
{
    public class SessionLifetimeManager : LifetimeManager
    {
        private string key = Guid.NewGuid().ToString();

        public override object GetValue()
        {
            return HttpContext.Current.Session[key];
        }

        public override void SetValue(object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}