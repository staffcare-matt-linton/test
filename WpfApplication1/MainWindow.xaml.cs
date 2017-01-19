using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            button.Click += Button_Click;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int limit = Convert.ToInt32(textBox.Text);
            //int result = (from n in Enumerable.Range(2, limit)
            //              where Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)
            //              select n).Count();

            //int result = CalculatePrimes(limit);
            int result = await CalculatePrimesAsync(limit);
            sw.Stop();
            label.Content = $"{result} prime numbers. Calculated in { Math.Round(sw.Elapsed.TotalSeconds, 2)} seconds";
        }

        public int CalculatePrimes(int limit)
        {
            return (from n in Enumerable.Range(2, limit)
                    where Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)
                    select n).Count();

        }

        public async Task<int> CalculatePrimesAsync(int limit)
        {
            Func<int> func = () => (from n in Enumerable.Range(2, limit).AsParallel()
                                    where Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)
                                    select n).Count();
            int result = await Task.Run(func); //returns Task<int>
            return result;
        }
    }
}
