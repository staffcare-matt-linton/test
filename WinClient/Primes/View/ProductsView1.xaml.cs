using System.Windows;
using WinClient.MVVM.ViewModel;

namespace WinClient.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView1 : Window
    {
        public ProductsView1(ProductsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            SearchBox.Focus();
        }
    }
}