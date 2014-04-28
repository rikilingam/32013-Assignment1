using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    //enum Currency { AUD, CNY, EUR }

    public static class CurrencyConverter
    {
        public static decimal ConvertToAUD(string currency, decimal amount)
        {
            if (currency == "AUD")
            {
                return amount;
            }
            else
            {
                return amount * GetExchangeRate(currency);
            }
        }

        /// <summary>
        /// Get the exchange rate from the webconfig
        /// </summary>
        /// <param name="currency">Currency to get</param>
        /// <returns>The exchange rate</returns>
        private static decimal GetExchangeRate(string currency)
        {
            decimal rate = 0;

            if (decimal.TryParse(ConfigurationManager.AppSettings[currency], out rate) && rate > 0)
            {
                return rate;
            }
            else
            {
                return 0;
            }
        }
    }
}
