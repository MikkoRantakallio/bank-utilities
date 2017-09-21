using System;
using System.Collections.Generic;
using System.Text;

namespace Ekoodi.Utilities.Bank
{
    class BicCode
    {
        string[,] bicCodeArray = new string[,]
        {
            {"1", "NDEAFIHH" },
            {"2", "NDEAFIHH" },
            {"31", "HANDFIHH" },
            {"33", "ESSEFIHX" },
            {"34", "DABAFIHX" },
            {"36", "SBANFIHH" },
            {"37", "DNBAFIHX" },
            {"38", "SWEDFIHH" },
            {"39", "SBANFIHH" },
            {"400", "ITELFIHH" },
            {"402", "ITELFIHH" },
            {"403", "ITELFIHH" },
            {"405", "HELSFIHH" },
            {"406", "ITELFIHH" },
            {"407", "ITELFIHH" },
            {"408", "ITELFIHH" },
            {"410", "ITELFIHH" },
            {"411", "ITELFIHH" },
            {"412", "ITELFIHH" },
            {"414", "ITELFIHH" },
            {"415", "ITELFIHH" },
            {"416", "ITELFIHH" },
            {"417", "ITELFIHH" },
            {"418", "ITELFIHH" },
            {"419", "ITELFIHH" },
            {"420", "ITELFIHH" },
            {"421", "ITELFIHH" },
            {"423", "ITELFIHH" },
            {"424", "ITELFIHH" },
            {"425", "ITELFIHH" },
            {"426", "ITELFIHH" },
            {"427", "ITELFIHH" },
            {"428", "ITELFIHH" },
            {"429", "ITELFIHH" },
            {"430", "ITELFIHH" },
            {"431", "ITELFIHH" },
            {"432", "ITELFIHH" },
            {"435", "ITELFIHH" },
            {"436", "ITELFIHH" },
            {"437", "ITELFIHH" },
            {"438", "ITELFIHH" },
            {"439", "ITELFIHH" },
            {"440", "ITELFIHH" },
            {"441", "ITELFIHH" },
            {"442", "ITELFIHH" },
            {"443", "ITELFIHH" },
            {"444", "ITELFIHH" },
            {"445", "ITELFIHH" },
            {"446", "ITELFIHH" },
            {"447", "ITELFIHH" },
            {"448", "ITELFIHH" },
            {"449", "ITELFIHH" },
            {"450", "ITELFIHH" },
            {"451", "ITELFIHH" },
            {"452", "ITELFIHH" },
            {"454", "ITELFIHH" },
            {"455", "ITELFIHH" },
            {"456", "ITELFIHH" },
            {"457", "ITELFIHH" },
            {"458", "ITELFIHH" },
            {"459", "ITELFIHH" },
            {"460", "ITELFIHH" },
            {"461", "ITELFIHH" },
            {"462", "ITELFIHH" },
            {"463", "ITELFIHH" },
            {"464", "ITELFIHH" },
            {"470", "POPFFI22" },
            {"471", "POPFFI22" },
            {"472", "POPFFI22" },
            {"473", "POPFFI22" },
            {"474", "POPFFI22" },
            {"475", "POPFFI22" },
            {"476", "POPFFI22" },
            {"477", "POPFFI22" },
            {"478", "POPFFI22" },
            {"479", "POPFFI22" },
            {"483", "ITELFIHH" },
            {"484", "ITELFIHH" },
            {"485", "ITELFIHH" },
            {"486", "ITELFIHH" },
            {"487", "ITELFIHH" },
            {"488", "ITELFIHH" },
            {"489", "ITELFIHH" },
            {"490", "ITELFIHH" },
            {"491", "ITELFIHH" },
            {"492", "ITELFIHH" },
            {"493", "ITELFIHH" },
            {"495", "ITELFIHH" },
            {"496", "ITELFIHH" },
            {"497", "HELSFIHH" },
            {"5", "OKOYFIHH" },
            {"6", "AABAFI22" },
            {"713", "CITIFIHX" },
            {"715", "ITELFIHH" },
            {"717", "BIGKFIH1" },
            {"799", "HOLVFIHH" },
            {"8", "DABAFIHH" },

        };

        //Constructor
        public BicCode()
        {
        }

        public string GetBicCode(string accNumber)
        {
            int arrLength = bicCodeArray.GetLength(0);

            // Remove "FI" and check number if needed
            if (accNumber.Substring(0,2)=="FI")
            {
                accNumber = accNumber.Substring(4).Trim();
            }

            // Loop thru array and try to find the account number start (1, 2 or 3 chars)
            for (int i=0; i<arrLength; i++)
            {
                if (accNumber.Substring(0,1)== bicCodeArray[i,0] || accNumber.Substring(0, 2) == bicCodeArray[i, 0]
                    || accNumber.Substring(0, 3) == bicCodeArray[i, 0])
                {
                    return bicCodeArray[i, 1];
                }
            }
            return "Not Found";
        }
    }
}
