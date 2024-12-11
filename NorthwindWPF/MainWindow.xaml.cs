using Microsoft.EntityFrameworkCore;

using NorthwindLinq;

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

namespace NorthwindWPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private NorthwindContext _db = new NorthwindContext();
  public MainWindow() => InitializeComponent();

  private void Window_Loaded(object sender, RoutedEventArgs e)
  {
    foreach (var order in _db.Orders)
    {
      cbxOrders.Items.Add(order);
    }
  }
  private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    FillListBox();
    FillDataGrid();
  }

  private void FillListBox()
  {
    if (cbxOrders.SelectedIndex < 0 || cbxOrders.SelectedIndex >= _db.Orders.ToList().Count) return;
    Order? selectedOrder = _db.Orders
      .Include(x => x.OrderDetails)
      .ThenInclude(x => x.Product)
      .ThenInclude(x => x.Category)
      .Include(x => x.Employee)
      .Include(x => x.ShipViaNavigation)
      .ToList()[cbxOrders.SelectedIndex] as Order;
    if (selectedOrder == null) return;
    lsbList1.ItemsSource = selectedOrder.
      OrderDetails
      .Select(x =>
      {
        return new OrderDetail(
                EmployeeName: selectedOrder.Employee?.FirstName ?? "Unknown",
                Quantity: x.Quantity,
                ProductName: x.Product?.ProductName ?? "Unknown",
                CategoryName: x.Product?.Category?.CategoryName ?? "Unknown",
                SupplierCompanyName: selectedOrder.ShipViaNavigation?.CompanyName ?? "Unknown"
              );
      });
  }
  private void FillDataGrid()
  {
    if (cbxOrders.SelectedIndex < 0 || cbxOrders.SelectedIndex >= _db.Orders.ToList().Count) return;
    Order? selectedOrder = _db.Orders
      .Include(x => x.OrderDetails)
      .ThenInclude(x => x.Product)
      .ThenInclude(x => x.Category)
      .Include(x => x.Customer)
      .ToList()[cbxOrders.SelectedIndex] as Order;
    if (selectedOrder == null) return;
    dgGrid1.ItemsSource = selectedOrder.
      OrderDetails
      .Select(x =>
      {
        return new OrderDetailGridElement(
                OrderDate: selectedOrder.OrderDate??DateTime.MinValue,
                CustomerName: selectedOrder.Customer?.ContactName ?? "Unknown",
                Quantity: x.Quantity,
                ProductName: x.Product?.ProductName ?? "Unknown",
                CategoryName: x.Product?.Category?.CategoryName ?? "Unknown"
              );
      });
  }

  public record OrderDetail(string EmployeeName, int Quantity, string ProductName, string CategoryName, string SupplierCompanyName)
  {
    public override string ToString() => $"{EmployeeName}: {Quantity} x {ProductName} ({CategoryName}, {SupplierCompanyName})";
  }
  public record OrderDetailGridElement(DateTime OrderDate, string CustomerName, int Quantity, string ProductName, string CategoryName);
}
