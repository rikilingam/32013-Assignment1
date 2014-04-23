using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
    enum Currency { AUD, CNY, EUR }

    public static class CurrencyConverter
    {
        public static decimal ConvertToAUD(string currency, decimal amount)
        {
            decimal rate = 0;
            bool isRateValid = false;

            if (currency == Currency.AUD.ToString())
            {
                return amount;
            }
            else if (currency == Currency.CNY.ToString())
            {
                isRateValid = decimal.TryParse(ConfigurationManager.AppSettings["CNY"], out rate);
                
                if (isRateValid = true && rate > 0)
                {
                    return amount * rate;
                }
                else return rate;
            }
            else if (currency == Currency.EUR.ToString())
            {
                isRateValid = decimal.TryParse(ConfigurationManager.AppSettings["EUR"], out rate);

                if (isRateValid = true && rate > 0)
                {
                    return amount * rate;
                }
                else return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
