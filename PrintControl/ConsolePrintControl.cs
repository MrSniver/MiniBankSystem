using MiniBankSystem.Entites;

namespace MiniBankSystem.PrintControl;

public static class ConsolePrintControl
{
    public static void PrintHeader(string headerInput)
    {
        Console.WriteLine($"----{headerInput}----");
    }

    public static void PrintMenu(params string[] options)
    {
        Console.WriteLine();
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine();
    }


}