using System;

namespace Ekoodi.Utilities.Bank
{
    public class BankAccount
    {
        // Properties
        public string LongFormatStr { get; }
        public string FinnishFormatStr { get; }
        public string IbanFormatStr { get; }
        public string BicStr { get; }

        // Private fields
        private float _balance;

        // Constructor
        public BankAccount(string accountNumber)
        {
            AccountNumberChecker checker = new AccountNumberChecker();
            BicCodeReader bicList = new BicCodeReader();

            // Check if FI or long format input

            if (accountNumber.Substring(0,2).ToUpper() == "FI")
            {
                accountNumber = accountNumber.Substring(4);
            }

            LongFormatStr = checker.GetLongFormat(accountNumber);
            IbanFormatStr = checker.GetIbanFormat(accountNumber);
            BicStr = bicList.GetBicCode(IbanFormatStr);
            FinnishFormatStr = accountNumber;

            _balance = 0;
        }

        // Deposit
        public void Deposit (float depositAmount)
        {
            _balance += depositAmount;
        }

        // Withdrawal
        public void WithDrawal(float withDrawalAmount)
        {
            _balance -= withDrawalAmount;
        }
    }
}
