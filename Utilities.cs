internal class Color
{
    public static void Red()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    internal static void Reset() {
        Console.ResetColor();
    }
}
