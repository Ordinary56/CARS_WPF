using CARS_WPF.Model;
using CARS_WPF.Repositories;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Új_mappa;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    ObservableCollection<object> _products, _countries, _orders;

    public ObservableCollection<object> Products => _products;
    public ObservableCollection <object> Countries => _countries;
    public ObservableCollection <object> Orders => _orders;
    public MainWindow()
    {
        _products = [];
        _countries = [];
        _orders = [];
        Loaded += async (o,e) => await LoadCollections();
        InitializeComponent();
        DataContext = this;
    }
    public async Task LoadCollections()
    {
        ProductRepo product = new();
        CountryRepo country = new();
        OrderRepo order = new();    
        await foreach(var item in product.GetProductsAsync())
        {
            _products.Add(item);
        }
        await foreach(var item in country.GetCountriesAsync())
        {
            _countries.Add(item);
        }
        await foreach(var item in order.GetOrdersAsync())
        {
            _orders.Add(item);  
        }
    }

    private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(sender is ListView view && view.SelectedItem is Product product)
        {
            ProductCountRepo count = new();
            int res = await count.GetProductCount(product);
            await Dispatcher.BeginInvoke(() =>
            {
                lbl_countResult.Content = $"Termékek száma: {res}";
            });
        }
    }
    private async void SelectCountry(object sender, SelectionChangedEventArgs e)
    {
        if(sender is ListView view && view.SelectedItem is Country country)
        {
            CustomerRepo repo = new();
            await Dispatcher.InvokeAsync(async () =>
            {
                dg_customer.Items.Clear();
                dg_customer.Columns.Clear();
                dg_customer.Columns.Add(new DataGridTextColumn() { Header = "Name", Binding = new Binding("CustomerName") });
                dg_customer.Columns.Add(new DataGridTextColumn() { Header = "Phone", Binding = new Binding("Phone")});
                dg_customer.Columns.Add(new DataGridTextColumn() { Header = "City", Binding = new Binding("City")});

                await foreach(var customer in repo.GetCustomersByCountry(country.Name))
                {
                    dg_customer.Items.Add(customer);
                }
                dg_customer.Items.Refresh();
            });
        }
    }

    private async void SelectOrder(object sender, SelectionChangedEventArgs args)
    {
        if(sender is DataGrid dg && dg.SelectedItem is Order order)
        {
            ProductRepo repo = new();
            await Dispatcher.InvokeAsync(async () =>
            {
                lb_Products.Items.Clear();  
                await foreach(var product in repo.GetProductsByOrderAsync(order))
                {
                    Debug.WriteLine(product);
                    lb_Products.Items.Add(product);
                }
                lb_Products.Items.Refresh();
            });

            
        } 
    }
}