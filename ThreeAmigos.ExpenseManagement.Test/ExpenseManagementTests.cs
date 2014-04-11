using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;
using System.Configuration;
using System.Transactions;


namespace ThreeAmigos.ExpenseManagement.Test
{
    [TestClass]
    public class ExpenseManagementTests
    {
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            // This path needs to be changed to root path of the Visual Studio solution
            string path = "C:\\Users\\rikil\\Source\\Repos\\32013-Assignment1";

            AppDomain.CurrentDomain.SetData("DataDirectory", path);

        }

        [TestMethod]
        public void AppConfig_VerifyAppDomainHasConnectionString_IsNotEmpty()
        {
            string value = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            Assert.IsFalse(String.IsNullOrEmpty(value), "No App.Config found.");
        }

        [TestMethod]
        public void Employee_CheckEmployeeIsNotNull_IsNotNull()
        {
            Employee employee = new Employee();

            Assert.IsNotNull(employee.UserId, "Employee UserId is null");
            Assert.IsNotNull(employee.DepartmentName, "Employee DepartmentName is null");
            Assert.IsNotNull(employee.FirstName, "Employee FirstName is null");
            Assert.IsNotNull(employee.Surname, "Employee Surname is null");
            Assert.IsNotNull(employee.Role, "Employee Role is null");
        }

        [TestMethod]
        public void EmployeeDAL_IsGetEmployeeEqualToTestEmployee_IsEqual()
        {
            Guid id = new Guid("78560DD3-F95E-4011-B40D-A7B56ED17F24");
            Employee employee = new Employee();

            EmployeeDAL employeeDAL = new EmployeeDAL();

            employee = employeeDAL.GetEmployee(id);

            bool IsEqual = TestEmployeeComparer(employee);

            Assert.IsTrue(IsEqual, "Employee from database is not equal to test employee");
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportSummaryByConsultant_IsTrue()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();

            List<ExpenseReport> reports = new List<ExpenseReport>();
            Guid id = new Guid("2ABC120C-F985-4FEF-87D1-74B6F697B140");

            reports = expenseReportDAL.GetReportSummaryByConsultant(id);

            Assert.IsTrue(reports.Count > 0, "No data in list of reports");

            foreach (ExpenseReport report in reports)
            {
                Assert.IsTrue(report.ExpenseItems.Count > 0, "No Data in list of expenseitems");
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_ProcessExpense_InsertSuccess()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();

            ExpenseReport expenseReport = new ExpenseReport();
            ExpenseItem item = new ExpenseItem();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedById = new Guid("78560DD3-F95E-4011-B40D-A7B56ED17F24");
            expenseReport.DepartmentId = 2;
            expenseReport.Status = ReportStatus.Submitted;

            item.ExpenseDate = DateTime.Now;
            item.Location = "Brisbane";
            item.Description = "Mouse and Keyboard";
            item.Amount = 10.50;
            item.Currency = "AUD";
            item.AudAmount = item.Amount;

            expenseReport.ExpenseItems.Add(item);

            using (TransactionScope testTransaction = new TransactionScope())
            {
                expenseReportDAL.ProcessExpense(expenseReport);

                Assert.IsTrue(CheckDatabaseForExpenseId(expenseReport.ExpenseId), "Expense Id was not found in database");

                testTransaction.Dispose();

            }
        }

        //[TestMethod]
        //public void ExpenseHeader_OnInitialisation_CreateExpenseReport()
        //{

        //}

        ////////////////////////////////////
        // Begin - TestMethod for Consultant
        //[TestMethod]
        //public void Consultant_WhenRequireShowAllReport_ReturnAllReport()
        //{

        //}

        //[TestMethod]
        //public void Consultant_WhenRequireShowApprovedReport_ReturnOnlyApprovedReport()
        //{

        //}

        //[TestMethod]
        //public void Consultant_WhenRequireShowNotYetApprovedReport_ReturnOnlyNotYetApprovedReport()
        //{

        //}
        //End - TestMethod for Consultant
        /////////////////////////////////

        ///////////////////////////////////
        //Begin - TestMethod for Supervisor
        //[TestMethod]
        //public void Supervisor_WhenRequireTotalExpenseApproved_ReturnTotalExpenseApproved()
        //{

        //}

        //[TestMethod]
        //public void Supervisor_WhenRequireBudgetRemain_ReturnBudgetRemain()
        //{

        //}

        //[TestMethod]
        //public void Consultant_WhenApproveExpenseReportOverBudget_ReturnWarningToConfirm()
        //{

        //}

        //[TestMethod]
        //public void Consultant_WhenRequireRejectedReportByAccountStaff_ReturnReportRejectedByAccountStaff()
        //{

        //}
        //End - TestMethod for Supervisor
        /////////////////////////////////

        /////////////////////////////////////////
        //Begin - TestMethod for Accountant Staff
        //[TestMethod]        
        //public void Accounts_WhenRequireShowAllReportsApprovedBySupervisors_ReturnAllReportsApprovedBySupervisors()
        //{

        //}

        //[TestMethod]
        //public void Accounts_WhenExpenseReportOverBudgetOfDepartment_ReturnExpenseReportHighlighted()
        //{

        //}

        //[TestMethod]
        //public void Accounts_WhenRequireTotalExpenseApprovedBySupervisor_ReturnTotalExpenseApprovedBySupervisor()
        //{

        //}

        //[TestMethod]
        //public void Accounts_WhenRequireTotalExpenseApprovedBySupervisor_ReturnTotalExpenseApprovedBySupervisor()
        //{

        //}

        //[TestMethod]
        //public void Accounts_WhenRequireCompanyBudgetRemains_ReturnBudgetRemains()
        //{

        //}
        //End - TestMethod for Accountant Staff
        ///////////////////////////////////////    


        private bool TestEmployeeComparer(Employee employee)
        {
            Employee testEmployee = new Employee();
            testEmployee.UserId = new Guid("78560DD3-F95E-4011-B40D-A7B56ED17F24");
            testEmployee.FirstName = "Vikki";
            testEmployee.Surname = "Car";
            testEmployee.DepartmentId = 2;
            testEmployee.DepartmentName = "Logistics Services";
            testEmployee.Role = "Consultant";

            if (testEmployee.UserId == employee.UserId && testEmployee.FirstName == employee.FirstName
                && testEmployee.Surname == employee.Surname && testEmployee.DepartmentId == employee.DepartmentId
                && testEmployee.DepartmentName == employee.DepartmentName && testEmployee.Role == employee.Role)
            {
                return true;
            }
            else { return false; }
        }

        private bool CheckDatabaseForExpenseId(int id)
        {
            bool exist;

            DataAccessFunctions daFunctions = new DataAccessFunctions();
            string query = String.Format("SELECT ExpenseId from ExpenseHeader WHERE ExpenseId={0}", id);
            daFunctions.Command.CommandText = query;

            try
            {
                daFunctions.Connection.Open();

                int dbExpenseId = (int)daFunctions.Command.ExecuteScalar();

                daFunctions.Connection.Close();

                if (id == dbExpenseId)
                {
                    exist = true;
                }
                else
                {
                    exist = false;
                }

                return exist;


            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem running method CheckDatabaseForExpense: " + ex.Message);
            }
        }
    }
}
