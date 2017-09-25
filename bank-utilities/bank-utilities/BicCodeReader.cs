using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ekoodi.Utilities.Bank
{
    // List class to have bic codes
    class BicCodeReader
    {
        public List<BicCode> BicCodes { get; }

        //Constructor
        public BicCodeReader()
        {
            BicCodes = new List<BicCode>();

            using (var fs = new FileStream("bic-codes.json", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    string json = r.ReadToEnd();
                    BicCodes = JsonConvert.DeserializeObject<List<BicCode>>(json);
                }
            }
        }

        public string GetBicCode(string accNumber)
        {
            // Remove "FI" and check number if needed
            if (accNumber.Substring(0, 2) == "FI")
            {
                accNumber = accNumber.Substring(4).Trim();
            }

            // Loop thru list and try to find the account number start (1, 2 or 3 chars)
            
            foreach ( BicCode bc in BicCodes)
            {
                if (accNumber.Substring(0, 1) == bc.Id || accNumber.Substring(0, 2) == bc.Id
                    || accNumber.Substring(0, 3) == bc.Id)
                {
                    return bc.Name;
                }
            }
            return "Not Found";
        }
    }
}
