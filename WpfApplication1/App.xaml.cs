using Core.DataAccess;
using Core.DataAccess.WebApi;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WinClient.Products.View;
using WinClient.Products.ViewModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //PrimesModel model = new PrimesModel();
            //PrimesViewModel viewModel = new PrimesViewModel(model);
            //PrimesView view = new PrimesView(viewModel);

            //IProductModel model = new WebApiProductModel();
            //ProductsViewModel viewModel = new ProductsViewModel(model);
            //ProductsView view = new ProductsView(viewModel);
            //view.Show();

            IUnityContainer container = new UnityContainer();
            //ProductsViewModel constructor takes interface argument
            //so need to register association between IProductModel and
            //implementing class
            container.RegisterType<IAsyncProductModel, WebApiProductModel>();
            //container.Resolve<ProductsView>().Show();
        }
    }
}
