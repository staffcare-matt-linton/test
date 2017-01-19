using Core.DataAccess.WebApi;
using Microsoft.Practices.Unity;
using System.Windows;

using WinClient.Products.View;

namespace WinClient
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
            //view.Show();
            //the above code is brittle; it will cease working if any changes 
            //are made to the constructors

            //Bootstrap();
        }

        private static void Bootstrap()
        {
            IUnityContainer container = new UnityContainer();
            //ProductsViewModel constructor takes interface argument
            //so need to register association between IProductModel and
            //implementing class
            //container.RegisterType<IProductModelAsync, WebApiProductModel>();
            container.Resolve<ProductsView>().Show();
        }
    }
}
