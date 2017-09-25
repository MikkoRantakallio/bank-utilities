using System;
using System.Numerics;
using Ekoodi.Utilities.Bank;

namespace int_reference_number_app
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------
            // Get long value from user
            // -------------------
            string getBigIntValue(string msg, string exitStr, int checkFromIndex, out bool success)
            {
                bool validNumber;
                BigInteger converted;
                string inputStr;
                success = true;

                // Loop until valid number given or input is equal to exit value
                do
                {
                    Console.Write("{0} ", msg);
                    inputStr = Console.ReadLine();

                    if (inputStr.ToUpper() == exitStr)
                    {
                        success = false;
                        return "";
                    }
                    validNumber = BigInteger.TryParse(inputStr.Substring(checkFromIndex), out converted);

                } while (!validNumber);
                return inputStr;
            }

            // --------
            // Show "menu", ask user input, loop until "X" given
            // --------

            bool exitMain = false;
            bool valid;

            do
            {
                Console.WriteLine("1 = Check international reference number");
                Console.WriteLine("2 = Create international reference number(s)");
                Console.WriteLine("X = Exit");
                Console.Write("Select: ");

                string menuChoice = Console.ReadLine();

                if (menuChoice.ToUpper() == "X")
                    exitMain = true;

                if (!exitMain)
                {
                    switch (menuChoice)
                    {
                        case "1":
                            string intRefNumberInput = getBigIntValue("Enter international reference number (X=Back): ", "X", 2, out valid);
                            Console.WriteLine();
                            if (valid)
                            {
                                // Try to construct an international reference number object with given value
                                try
                                {
                                    IntReferenceNumber refNumInt = new IntReferenceNumber(intRefNumberInput);
                                    Console.WriteLine("{0} - OK", refNumInt.RefNumberINT.ToUpper());
                                }
                                catch (InvalidRefNumberException e)
                                {
                                    Console.WriteLine("{0} - {1}", intRefNumberInput.ToUpper(), e.Message);
                                }
                            }
                            break;
                        case "2":
                            string basePartFI = getBigIntValue("Enter finnish reference number (X=Back): ", "X", 0, out valid);
                            Console.WriteLine();
                            if (valid)
                            {
                                IntReferenceNumber refNumInt = new IntReferenceNumber();
                                try
                                {
                                    refNumInt.CheckFinnishPart(basePartFI);
                                    string intRefNumStr = refNumInt.GenerateIntRefNumber();
                                    Console.WriteLine("{0}", intRefNumStr);
                                }
                                catch (InvalidRefNumberException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine();
                }
            } while (!exitMain);
        }
    }
}
