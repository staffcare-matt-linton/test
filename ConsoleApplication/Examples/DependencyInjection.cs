using Core.DataAccess;
using Core.DataAccess.Collection;
using Core.DataAccess.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Examples
{
    public class DependencyInjection
    {
        public static void Example()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IProductModel, CollectionProductModel>();
            container.RegisterType<IProductSerializer, JsonProductSerializer>();
            IProductModel productModel = container.Resolve<IProductModel>();
            Console.ReadKey();
        }
    }

}
