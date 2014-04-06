using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
  public static class CurrencyConverter
    {
      public static double ConvertCurrency(string text, double value,double cny,double eur)
        {
            if (text == "AUD")
            {
                return value;
            }
            else if (text == "CNY")
            {
                return (value *cny);
            }
            else if (text == "EUR")
            {
                return (value * eur);
            }
            else
            {
                return 0;
            }            
        }
    }
}
