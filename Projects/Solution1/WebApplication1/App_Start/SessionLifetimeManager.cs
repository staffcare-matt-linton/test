using System;
using Microsoft.Practices.Unity;
using System.Web;

namespace WebApplication1.App_Start
{
    public class SessionLifetimeManager : LifetimeManager
    {

        private string key = Guid.NewGuid().ToString();

        
        public override object GetValue()
        {
            return HttpContext.Current.Session[key];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Session[key] = newValue;
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
    }
