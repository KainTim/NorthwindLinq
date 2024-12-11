Console.WriteLine("LINQ with Northwind-Data".PadLeft(80, '-'));
Console.WriteLine("Alle Tests ausführen mit: Menü Test --> Run --> All Tests");
new NorthwindLinq.Queries().CheckAll();
Console.ReadKey();
