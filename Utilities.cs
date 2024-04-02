
internal static class Utils
{
  internal static int GetDirection() {
    switch (Console.ReadKey(true).Key)
    {
      case ConsoleKey.W:
        return 1;
      case ConsoleKey.A:
        return 2;
      case ConsoleKey.S:
        return 3;
      case ConsoleKey.D:
        return 4;
      default:
        return 0;
    }
  }

  /// <summary>
  /// Function to get number input from console
  /// </summary>
  /// <param name="max">The highest number you want to accept</param>
  /// <remarks>
  /// Has a minimum of 1, maximum of 9.
  /// </remarks>
  internal static int GetNumber(int max)
  {
  // This goto statement could be a "while true" loop, although this is
  // a cleaner solution. It could be a recursive call, but that would
  // be very computationally demanding in comparison.
  s:
    switch (Console.ReadKey(true).Key)
    {
      case ConsoleKey.D1 when max >= 1:
        return 1;
      case ConsoleKey.D2 when max >= 2:
        return 2;
      case ConsoleKey.D3 when max >= 3:
        return 3;
      case ConsoleKey.D4 when max >= 4:
        return 4;
      case ConsoleKey.D5 when max >= 5:
        return 5;
      case ConsoleKey.D6 when max >= 6:
        return 6;
      case ConsoleKey.D7 when max >= 7:
        return 7;
      case ConsoleKey.D8 when max >= 8:
        return 8;
      case ConsoleKey.D9 when max >= 9:
        return 9;
      default:
        goto s;
    }
  }

  internal static void SetCenterVertical(int offset = 0)
  {
    int h = Console.WindowHeight;
    int center = (h / 2) - offset;
    SetPos(0, center);
  }

  // NOTE: These are set to 0 due to not knowing a runtime variable at
  // compile-time, they are changed to runtime values in the function
  internal static void SetPos(int left = 0, int top = 0)
  {
    if (left == 0)
    {
      left = Console.CursorLeft;
    }
    if (top == 0)
    {
      top = Console.CursorLeft;
    }
    Console.SetCursorPosition(left, top);
  }

  // PERF: This is a horrible solution, but Microsoft doesn't offer an event
  // listener to hook into:
  // https://learn.microsoft.com/en-us/dotnet/api/system.console?view=net-8.0#events
  internal static void StartWindowResizeListener()
  {
    int h = Console.WindowHeight;
    int w = Console.WindowWidth;
    new Thread(() =>
    {
      for (; ; )
      {
        if (h != Console.WindowHeight || w != Console.WindowWidth)
        {
          h = Console.WindowHeight;
          w = Console.WindowWidth;
          if (h < 20 || w < 40) // arbitrary values
          {
            Console.Clear();
            Console.WriteLine("Console too small");
            Console.WriteLine("Please fix it and restart");
            Console.WriteLine("Press Ctrl+C to exit");
          }
        }
        Thread.Sleep(500);
      }
    }).Start();
  }
  private static void StartWindowResizeListenerWork()
  {
    Console.WriteLine("Started");
  }

  internal static void WriteCenter(string input = "")
  {
    WritePos(input.Length);
    Console.WriteLine(input);

  }

  internal static void WriteCenterLines(string[] lines, bool sameCenter = true)
  {
    // PERF: While `lines.Aggregate()` would be faster ( O(N) ), it is a lot
    // more unreadable, and performance isn't a focus in this application.
    // Reference: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-8.0
    int longest = lines.OrderByDescending(s => s.Length).First().Length;
    foreach (string line in lines)
    {
      if (sameCenter)
      {
        WritePos(longest);
      }
      else
      {
        WritePos(line.Length);
      }
      Console.WriteLine(line);
    }
  }

  private static void WritePos(int lineLength)
  {
    SetPos((Console.WindowWidth - lineLength) / (int)2, Console.CursorTop);
  }
}
