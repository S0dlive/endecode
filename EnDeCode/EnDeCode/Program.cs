using System;

public class Program
{
    private static int inputBase = 10;  
    private static int outputBase = 10; 

    public static void Main()
    {
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            switch (input.Trim().ToLower())
            {
                case "exit":
                    return;

                case "input":
                    Console.Write("> Entrez la base d'entrée (2-36) : ");
                    if (!int.TryParse(Console.ReadLine(), out inputBase) || inputBase < 2 || inputBase > 36)
                    {
                        Console.Error.WriteLine("Base d'entrée invalide (doit être entre 2 et 36).");
                        inputBase = 10;
                    }
                    continue;

                case "output":
                    Console.Write("> Entrez la base de sortie (2-36) :");
                    if (!int.TryParse(Console.ReadLine(), out outputBase) || outputBase < 2 || outputBase > 36)
                    {
                        Console.Error.WriteLine("Base de sortie invalide (doit être entre 2 et 36).");
                        outputBase = 10;
                    }
                    continue;

                default:
                    ConvertAndDisplay(input);
                    continue;
            }
        }
    }

    private static void ConvertAndDisplay(string input)
    {
        const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string cleanInput = input.Trim().ToUpper();

        long valueInBase10 = 0;

        foreach (char c in cleanInput)
        {
            int digitValue = digits.IndexOf(c);

            if (digitValue == -1 || digitValue >= inputBase)
            {
                Console.Error.WriteLine($"Erreur : '{c}' n'est pas un chiffre valide en base {inputBase}.");
                return;
            }

            try
            {checked { valueInBase10 = valueInBase10 * inputBase + digitValue; }}
            catch (OverflowException)
            {
                Console.Error.WriteLine("Erreur : Le nombre dépasse la capacité (long).");
                return;
            }
        }

        string convertedResult = ConvertFromBase10(valueInBase10, outputBase, digits);

        Console.WriteLine($"Résultat ({inputBase} -> {outputBase}) : {convertedResult}");
    }

    private static string ConvertFromBase10(long value, int targetBase, string digits)
    {
        if (value == 0) return "0";

        string result = "";
        long current = value;

        while (current > 0)
        {
            int remainder = (int)(current % targetBase);
            result = digits[remainder] + result;
            current /= targetBase;
        }

        return result;
    }
}