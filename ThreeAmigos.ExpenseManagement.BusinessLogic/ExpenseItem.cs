﻿using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos.ExpenseManagement.DataAccess;


namespace ThreeAmigos.ExpenseManagement.BusinessLogic
{
   public class ExpenseItem
    {
       public int ExpenseHeaderId { get; set; }
       public int ItemId { get; set; }
       public DateTime ExpenseDate { get; set; }
       public string Location { get; set; }
       public string Description { get; set; }
       public double Amount { get; set; }
       public string Currency { get; set; }
       public double AudAmount { get; set; }
       public string ReceiptFileName { get; set; }

       /// <summary>
       /// Submits each individual expense item
       /// </summary>
       /// <param name="expenseId">expense id from the expense header</param>
       public void SubmitExpenseItem(int expenseId)
       {
           ExpenseHeaderId = expenseId;

           ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
           expenseReportDAL.InsertExpenseItem(ExpenseHeaderId, ExpenseDate, Location, Description, Amount, Currency, AudAmount, ReceiptFileName);

       }

       //public void AddExpenseItem(DateTime expDate, string location, string description, double amount, string currency, double audAmount, string receiptFileName, int expHeaderId)
       //{
       //    ExpenseItemDAL item = new ExpenseItemDAL();
       //    item.AddExpenseItem(expDate, location, description, amount, currency, audAmount, receiptFileName, expHeaderId);
       //}
    }
}
