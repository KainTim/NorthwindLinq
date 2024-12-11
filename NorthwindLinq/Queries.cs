using Microsoft.EntityFrameworkCore;

namespace NorthwindLinq;

public class Queries
{
  private NorthwindContext db;
  //private NorthwindContext _db = null!;
  public Queries() => InitializeDatabase();

  private void InitializeDatabase()
  {
    db = new NorthwindContext();
  }

  public void CheckAll()
  {
    First5ProductsWithCategories().Show("First5ProductsWithCategories");
    OrderedProductsOfBOTTM().Show("OrderedProductsOfBOTTM");
    NrOfEmployeesWhoSoldToCustomersInGivenCity("Nantes").Show("NrOfEmployeesWhoSoldToCustomersInGivenCity Nantes");
    NrOfEmployeesWhoSoldToCustomersInGivenCity("London").Show("NrOfEmployeesWhoSoldToCustomersInGivenCity London");
    NrOfEmployeesWhoSoldToCustomersInGivenCity("Buenos Aires").Show("NrOfEmployeesWhoSoldToCustomersInGivenCity Buenos Aires");
    CustomersWithUnshippedOrders().Show("CustomersWithUnshippedOrders");
    TotalQuantityOfShipper("Speedy Express").Show("TotalQuantityOfShipper Speedy Express");
    TotalQuantityOfShipper("United Package").Show("TotalQuantityOfShipper United Package");
    TotalQuantityOfShipper("Federal Shipping").Show("TotalQuantityOfShipper Federal Shipping");
    AveragePriceOfSuppliersOfCity("Tokyo").Show("AveragePriceOfSuppliersOfCity Tokyo");
    AveragePriceOfSuppliersOfCity("Paris").Show("AveragePriceOfSuppliersOfCity Paris");
    AveragePriceOfSuppliersOfCity("Berlin").Show("AveragePriceOfSuppliersOfCity Berlin");
    CategoriesWithProductsInStockMoreThan(400).Show("CategoriesWithProductsInStockMoreThan 400");
    CategoriesWithProductsInStockMoreThan(600).Show("CategoriesWithProductsInStockMoreThan 600");
    CategoriesWithProductsInStockMoreThan(200).Show("CategoriesWithProductsInStockMoreThan 200");
  }

  #region Q01
  public List<string> First5ProductsWithCategories()
  {
    return db.Products
      .OrderBy(p => p.ProductName)
      .Take(5)
      .Select(x => $"{x.ProductName} - {x.Category.CategoryName}")
      .ToList();
  }
  #endregion

  #region Q02
  public List<string> OrderedProductsOfBOTTM()
  {
    return db.OrderDetails.
      Where(x => x.Order.Customer.CustomerId == "BOTTM").
      Where(x => x.Product.UnitPrice > 30).
      Select(x => x.Product.ProductName).
      Distinct().
      Order().
      ToList();
  }
  #endregion

  #region Q03
  public int NrOfEmployeesWhoSoldToCustomersInGivenCity(string city)
  {
    return db.Orders.
      Where(x => x.Customer.City == city).
      ToList().
      DistinctBy(x => x.EmployeeId).
      Count();
  }
  #endregion

  #region Q04
  public List<string> CustomersWithUnshippedOrders()
  {
    return db.Orders.
      Where(x => x.ShipCountry == "Argentina" || x.ShipCountry == "Venezuela").
      Where(x => x.ShippedDate == null).
      OrderBy(x => x.Customer.CompanyName)
      .ThenBy(x => x.Employee.LastName).
      Select(x => $"{x.Customer.CompanyName} - {x.ShipCity}/{x.ShipCountry} - {x.Employee.FirstName} {x.Employee.LastName}")
      .ToList();
      
  }
  #endregion

  #region Q05
  public int TotalQuantityOfShipper(string shipperCompany)
  {
    return db.OrderDetails
        .Where(od => od.Order.ShipViaNavigation.CompanyName == shipperCompany && od.Order.ShippedDate != null)
        .Sum(od => od.Quantity);

  }
  #endregion

  #region Q06
  public double AveragePriceOfSuppliersOfCity(string city)
  {
    return db.Products.
      Where(x => x.Supplier.City == city).
      Average(x => x.UnitPrice) ?? 0;
  }
  #endregion

  #region Q07
  public List<string> CategoriesWithProductsInStockMoreThan(int totalStock)
  {
    return db.Products
        .GroupBy(p => p.Category.CategoryName)
        .Where(g => g.Sum(p => p.UnitsInStock) > totalStock)
        .Select(g => g.Key)
        .OrderBy(name => name)
        .ToList();
  }
  #endregion
}
