internal static class Cons
{
    // NOTE: Colors are relative to the colors of the shell, that means if
    // someone has changed their "red" to a blue color, it will show up as
    // blue. This is not an oversight but rather a design decision, the colors
    // are just to make different sections distinct and doesn't inherently
    // have a reason for being the color it is.

    internal static void Clear()
    {
        Console.Clear();
    }

    internal static void Green() {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    internal static void Red()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    internal static void ResetColor()
    {
        Console.ResetColor();
    }
}
