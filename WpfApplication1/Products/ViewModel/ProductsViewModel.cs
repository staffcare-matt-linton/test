using Core.DataAccess;
using Core.DataAccess.WebApi;
using Core.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using WinClient.MVVM.Utility;

namespace WinClient.Products.ViewModel
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAsyncProductModel productModel;

        private string search;
        private ICollection<Product> products;
        public ProductsViewModel(IAsyncProductModel productModel)
        {
            this.productModel = productModel;
            LoadData();
        }

        private async void LoadData()
        {
            Products = await productModel.SelectByNameAsync(Search);
        }

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                LoadData();
            }
        }

        public ICollection<Product> Products  //Bound to DataGrid ItemsSource
        {
            get { return products; }
            set
            {
                products = value;
                if (PropertyChanged != null) //If there are no subscribers to the event, due to DataContext not being set
                    PropertyChanged(this, new PropertyChangedEventArgs("Products"));
            }
        }    
    }
}

