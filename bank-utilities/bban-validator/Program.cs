using System;
using Ekoodi.Utilities.Bank;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------
            // Ask user input, loop until "X" given
            // --------
            bool exit = false;

            do
            {
                Console.Write("Give finnish bank account number (X=Exit): ");
                string bankAccountInput = Console.ReadLine();

                if (bankAccountInput.ToUpper() == "X")
                    exit=true;

                if (!exit)
                {
                    // Create and check bank account
                    try {
                        BankAccount myAccount = new BankAccount(bankAccountInput);

                        Console.WriteLine();
                        Console.WriteLine("Finnish format: \t{0}", myAccount.FinnishFormatStr);
                        Console.WriteLine("Long format: \t\t{0}", myAccount.LongFormatStr);
                        Console.WriteLine("IBAN: \t\t\t{0}", myAccount.IbanFormatStr);
                        Console.WriteLine("BIC: \t\t\t{0}", myAccount.BicStr);
                        Console.WriteLine("Check digit OK");
                    }
                    catch (InvalidAccountNumberException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine();
                }
            } while (!exit);
        }
    }
}
