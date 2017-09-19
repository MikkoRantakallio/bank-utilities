using System;
using Ekoodi.Utilities.BankAccount;
using Ekoodi.Utilities.Checker;

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

                        Console.WriteLine("Finnish format: \t{0}", myAccount.FinnishFormat);
                        Console.WriteLine("Long format: \t\t{0}", myAccount.LongFormat);
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
