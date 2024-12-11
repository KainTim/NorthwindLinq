namespace NorthwindLinq;

public static class ExtensionMethodsShow
{
  private const int HEADER_LENGTH = 70;
  public static IEnumerable<KeyValuePair<string, int>> Show(this IEnumerable<KeyValuePair<string, int>> dict, string header)
  {
    ShowWithAction(() => dict.ToList().ForEach(ass => Console.WriteLine($"{ass.Key} --> {ass.Value}")), header);
    return dict;
  }

  public static List<string> Show(this List<string> list, string header)
  {
    ShowWithAction(() => list.ForEach(Console.WriteLine), header);
    return list;
  }

  public static double Show(this double obj, string header)
  {
    ShowWithAction(() => Console.WriteLine(obj.ToString()), header);
    return obj;
  }

  public static int Show(this int obj, string header)
  {
    ShowWithAction(() => Console.WriteLine(obj.ToString()), header);
    return obj;
  }

  private static void ShowWithAction(Action action, string header)
  {
    Console.WriteLine(header.PadLeft(HEADER_LENGTH, '-'));
    action();
    Console.WriteLine("".PadLeft(HEADER_LENGTH, '-'));
  }
}
