﻿using System;
using Ekoodi.Utilities.Checker;

namespace Ekoodi.Utilities.BankAccount
{
    public class BankAccount
    {
        // Properties
        public string LongFormat { get; }
        public string FinnishFormat { get; }

        // Private fields
        private long _balance;

        // Constructor
        public BankAccount(string accountNumber)
        {
            AccountNumberChecker checker = new AccountNumberChecker();

            // TODO check FI or long format input

            LongFormat = checker.GetLongFormatFI(accountNumber);
            FinnishFormat = accountNumber;

            _balance = 0;
        }
    }
}
