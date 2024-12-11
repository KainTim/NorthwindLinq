using FluentAssertions;

using NorthwindLinq;

using Xunit;

namespace NorthwindLinqTests;

/*
Install-Package xunit -ProjectName NorthwindLinqTests
Install-Package xunit.runner.console -ProjectName NorthwindLinqTests
Install-Package xunit.runner.visualstudio -ProjectName NorthwindLinqTests
Install-Package FluentAssertions -ProjectName NorthwindLinqTests
Install-Package Microsoft.NET.Test.Sdk -ProjectName NorthwindLinqTests
*/

public class QueriesTests()
{
  private readonly Queries _queries = new();

  [Fact]
  public void T01_SelectFirst5ProductsWithCategoriesTest()
  {
    List<string> expected =
    [
      "Alice Mutton - Meat/Poultry",
      "Aniseed Syrup - Condiments",
      "Boston Crab Meat - Seafood",
      "Camembert Pierrot - Dairy Products",
      "Carnarvon Tigers - Seafood",
    ];
    _queries.First5ProductsWithCategories().Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void T02_OrderedProductsOfBOTTMTest()
  {
    List<string> expected =
    [
      "Alice Mutton",
      "Camembert Pierrot",
      "Chai",
      "Gnocchi di nonna Alice",
      "Ikura",
      "Ipoh Coffee",
      "Manjimup Dried Apples",
      "Mozzarella di Giovanni",
      "Northwoods Cranberry Sauce",
      "Raclette Courdavault",
      "Tarte au sucre"
    ];
    _queries.OrderedProductsOfBOTTM().Should().BeEquivalentTo(expected);
  }

  [Theory]
  [InlineData(4, "Nantes")]
  [InlineData(9, "London")]
  [InlineData(8, "Buenos Aires")]
  public void T03_NrOfEmployeesWhoSoldToCustomersInGivenCity(int expectedNr, string city)
    => _queries.NrOfEmployeesWhoSoldToCustomersInGivenCity(city).Should().Be(expectedNr, $"for city {city}");

  [Fact]
  public void T04_CustomersWithUnshippedOrdersTest()
  {
    List<string> expected =
    [
      "Cactus Comidas para llevar - Buenos Aires/Argentina - Laura Callahan",
      "LILA-Supermercado - Barquisimeto/Venezuela - Laura Callahan",
      "LILA-Supermercado - Barquisimeto/Venezuela - Nancy Davolio",
      "LINO-Delicateses - I. de Margarita/Venezuela - Nancy Davolio",
      "Rancho grande - Buenos Aires/Argentina - Michael Suyama",
    ];
    _queries.CustomersWithUnshippedOrders().Should().BeEquivalentTo(expected);
  }

  [Theory]
  [InlineData(15730, "Speedy Express")]
  [InlineData(19195, "United Package")]
  [InlineData(15195, "Federal Shipping")]
  public void T05_TotalQuantityOfShipperTest(int expectedQuantity, string shipperName)
    => _queries.TotalQuantityOfShipper(shipperName).Should().Be(expectedQuantity, $"for shipperName {shipperName}");

  [Theory]
  [InlineData(46, "Tokyo")]
  [InlineData(140.75, "Paris")]
  [InlineData(29.71, "Berlin")]
  public void T06_AveragePriceOfSuppliersOfCityTest(double expectedPrice, string city)
    => _queries.AveragePriceOfSuppliersOfCity(city).Should().BeApproximately(expectedPrice, 0.01, $"for city {city}");

  [Theory]
  [InlineData(400, "Beverages", "Condiments", "Seafood")]
  [InlineData(600, "Seafood")]
  [InlineData(200, "Beverages", "Condiments", "Confections", "Dairy Products", "Grains/Cereals", "Seafood")]
  public void T07_CategoriesWithProductsInStockMoreThanTest(int limit, params string[] expected)
    => _queries.CategoriesWithProductsInStockMoreThan(limit).Should().BeEquivalentTo(expected, $"for limit {limit}");
}
