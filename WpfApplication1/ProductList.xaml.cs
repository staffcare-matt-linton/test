using Core.DataAccess.WebApi;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        private WebApiProductModel model = new WebApiProductModel();
        public ProductList()
        {
            InitializeComponent();
            SearchBox_TextChangedAsync(null, null);
            SearchBox.Focus();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollection<Product> products = model.SelectByName(SearchBox.Text);
            DataContext = products;
        }

        private async void SearchBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            var products  = await model.SelectByNameAsync(SearchBox.Text);
            DataContext = products;
        }
    }
}
