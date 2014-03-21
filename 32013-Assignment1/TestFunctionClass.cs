using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _32013_Assignment1
{
    public class TestFunctionClass
    {
        const double cny = 0.178;
        const double euro = 1.52;
        public string convertCurrency(string text, double value)
        {
            if (text == "AUD")
            {
                return value.ToString();
            }
            else if (text == "CNY")
            {
                return (value * cny).ToString();
            }
            else if (text == "Euro")
            {
                return (value * euro).ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}