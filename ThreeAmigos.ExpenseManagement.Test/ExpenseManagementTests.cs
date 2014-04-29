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
           //  string path = "C:\\Users\\riki\\Source\\Repos\\32013-Assignment1";
            string path = "C:\\Users\\Bikrem\\Documents\\Visual Studio 2012\\Projects\\.Net Assignment-1\\32013-Assignment1";
            //string path = "C:\\Users\\JohnLe\\Source\\Repos\\32013-Assignment1";

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
            Assert.IsNotNull(employee.Dept.DepartmentName, "Employee DepartmentName is null");
            Assert.IsNotNull(employee.FirstName, "Employee FirstName is null");
            Assert.IsNotNull(employee.Surname, "Employee Surname is null");
            Assert.IsNotNull(employee.Role, "Employee Role is null");
        }

        [TestMethod]
        public void ExpenseReport_CheckExpenseReportItemsIsNotNull_IsNotNull()
        {
            ExpenseReport expenseReport = new ExpenseReport();

            Assert.IsNotNull(expenseReport.ExpenseItems, "ExpenseReport ExpenseItems is null");
        }

        [TestMethod]
        public void ExpenseItem_CheckExpenseItemsIsNotNull_IsNotNull()
        {

        }

        [TestMethod]
        public void EmployeeDAL_IsGetEmployeeEqualToTestEmployee_IsEqual()
        {
            Guid id = new Guid("78560dd3-f95e-4011-b40d-a7b56ed17f24");
            Employee employee = new Employee();
            EmployeeDAL employeeDAL = new EmployeeDAL();
            employee = employeeDAL.GetEmployee(id);
            bool IsEqual = TestEmployeeComparer(employee);
            Assert.IsTrue(IsEqual, "Employee from database is not equal to test employee");
        }

        [TestMethod]
        public void ConvertCurrency_CheckConfigurationForRates_RatesAreValid()
        {
            bool isValid = false;
            decimal rate = 0;
            
            isValid = decimal.TryParse(ConfigurationManager.AppSettings["CNY"], out rate);
            Assert.IsTrue(isValid, "CNY is not valid");

            isValid = decimal.TryParse(ConfigurationManager.AppSettings["EUR"], out rate);
            Assert.IsTrue(isValid, "EUR is not valid");
        }

        [TestMethod]
        public void ConvertCurrency_ConvertCNYToAUD_AreEqual()
        {
            decimal testAUDAmount = Convert.ToDecimal( 100 * 0.17430);

            Assert.IsNotNull(ConfigurationManager.AppSettings["CNY"], "Currency CNY is NULL");
            Assert.AreEqual(testAUDAmount, CurrencyConverter.ConvertToAUD("CNY", 100), "Conversion to CNY Failed");
        }

        [TestMethod]
        public void ConvertCurrency_ConvertEURToAUD_AreEqual()
        {
            decimal testAUDAmount = Convert.ToDecimal( 100 * 1.49146);

            Assert.IsNotNull(ConfigurationManager.AppSettings["EUR"], "Currency EUR is NULL in AppConfig");
            Assert.AreEqual(testAUDAmount, CurrencyConverter.ConvertToAUD("EUR", 100), "Conversion to EUR Failed");
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportByConsultant_StatusIsSubmitted_IsTrue()
        {
            using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                List<ExpenseReport> reports = new List<ExpenseReport>();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                Guid userId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                string status = ReportStatus.Submitted.ToString();

                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.ExpenseToDept.DepartmentId = 1;
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.Submitted;
                item.ExpenseDate = DateTime.Now;
                item.Location = "Sydney";
                item.Description = "AirTicket";
                item.Amount = 5000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                expenseReportDAL.ProcessExpense(expenseReport);
                Assert.IsTrue(CheckDatabaseForExpenseId(expenseReport.ExpenseId), "Expense Id was not found in database");
                reports = expenseReportDAL.GetExpenseReportsByConsultant(userId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                testTransaction.Dispose();
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_GetExpenseItemByExpenseId_IsNotEmpty()
        {

        }

        [TestMethod]
        public void ExpenseReportDAL_ProcessExpense_InsertSuccess()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();

            ExpenseReport expenseReport = new ExpenseReport();
            ExpenseItem item = new ExpenseItem();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy.UserId = new Guid("78560DD3-F95E-4011-B40D-A7B56ED17F24");
            expenseReport.CreatedBy.Dept.DepartmentId = 2;
            expenseReport.Status = ReportStatus.Submitted;

            item.ExpenseDate = DateTime.Now;
            item.Location = "Sydney";
            item.Description = "AirTicket";
            item.Amount = 2000;
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

        [TestMethod]
        public void ExpenseReportDAL_GetExpenseItemsByExpenseId_IsNotEmpty()
        { }

        private bool TestEmployeeComparer(Employee employee)
        {
            Employee testEmployee = new Employee();
            testEmployee.UserId = new Guid("78560dd3-f95e-4011-b40d-a7b56ed17f24");
            testEmployee.FirstName = "Vikki";
            testEmployee.Surname = "Car";
            testEmployee.Dept.DepartmentId = 1;
            testEmployee.Dept.DepartmentName = "State Services";
            testEmployee.Role = "Consultant";

            if (testEmployee.UserId == employee.UserId && testEmployee.FirstName == employee.FirstName
                && testEmployee.Surname == employee.Surname && testEmployee.Dept.DepartmentId == employee.Dept.DepartmentId
                && testEmployee.Dept.DepartmentName == employee.Dept.DepartmentName && testEmployee.Role == employee.Role)
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

        // Below are Tests For Supervisors Functions
        [TestMethod]
        public void ExpenseReportDAL_GetReportBySupervisor_StatusIsSubmitted_IsTrue()
        {
           using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                List<ExpenseReport> reports = new List<ExpenseReport>();
                int deptId = 1;
                string status = ReportStatus.Submitted.ToString();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.ExpenseToDept.DepartmentId = 1;
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.Submitted;
                item.ExpenseDate = DateTime.Now;
                item.Location = "Sydney";
                item.Description = "AirTicket";
                item.Amount = 5000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                expenseReportDAL.ProcessExpense(expenseReport);
                Assert.IsTrue(CheckDatabaseForExpenseId(expenseReport.ExpenseId), "Expense Id was not found in database");
                reports = expenseReportDAL.GetReportsByDepartment(deptId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                testTransaction.Dispose();
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportBySupervisor_StatusIsApprovedBySupervisor_IsTrue()
        {
           using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                List<ExpenseReport> reports = new List<ExpenseReport>();
                int deptId = 1;
                string status = ReportStatus.ApprovedBySupervisor.ToString();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.ExpenseToDept.DepartmentId = 1;
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.ApprovedBySupervisor;
                item.ExpenseDate = DateTime.Now;
                item.Location = "Sydney";
                item.Description = "AirTicket";
                item.Amount = 5000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                expenseReportDAL.ProcessExpense(expenseReport);
                Assert.IsTrue(CheckDatabaseForExpenseId(expenseReport.ExpenseId), "Expense Id was not found in database");
                reports = expenseReportDAL.GetReportsByDepartment(deptId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                testTransaction.Dispose();
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportBySupervisor_StatusIsRejectedBySupervisor_IsTrue()
        {           
            using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                List<ExpenseReport> reports = new List<ExpenseReport>();
                int deptId = 1;
                string status = ReportStatus.RejectedBySupervisor.ToString();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.ExpenseToDept.DepartmentId = 1;
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.RejectedBySupervisor;
                item.ExpenseDate = DateTime.Now;
                item.Location = "Sydney";
                item.Description = "AirTicket";
                item.Amount = 5000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                expenseReportDAL.ProcessExpense(expenseReport);
                reports = expenseReportDAL.GetReportsByDepartment(deptId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                foreach (ExpenseReport report in reports)
                {
                    Assert.IsTrue(report.ExpenseItems.Count > 0, "No item in expense report");
                }
                testTransaction.Dispose();
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportBySupervisor_StatusIsRejectedByAccounts_IsTrue()
        {         
            using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                List<ExpenseReport> reports = new List<ExpenseReport>();
                int deptId = 1;
                string status = ReportStatus.RejectedByAccounts.ToString();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.ExpenseToDept.DepartmentId = 1;
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.RejectedByAccounts;
                item.ExpenseDate = DateTime.Now;
                item.Location = "Sydney";
                item.Description = "AirTicket";
                item.Amount = 5000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                expenseReportDAL.ProcessExpense(expenseReport);
                reports = expenseReportDAL.GetReportsByDepartment(deptId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                testTransaction.Dispose();
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportBySupervisor_StatusIsApprovedByAccounts_IsTrue()
        {
           using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                List<ExpenseReport> reports = new List<ExpenseReport>();
                int deptId = 1;
                string status = ReportStatus.ApprovedByAccounts.ToString();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("78560dd3-f95e-4011-b40d-a7b56ed17f24");
                expenseReport.ExpenseToDept.DepartmentId = 1;
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.ApprovedByAccounts;
                item.ExpenseDate = DateTime.Now;
                item.Location = "Sydney";
                item.Description = "AirTicket";
                item.Amount = 5000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                expenseReportDAL.ProcessExpense(expenseReport);
                reports = expenseReportDAL.GetReportsByDepartment(deptId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                testTransaction.Dispose();
            }
         }
        
        [TestMethod]
        public void ExpenseReportDAL_SupervisorActionOnExpenseReport_Approve_ActionSuccess()
        {          
            using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.Submitted;
                item.ExpenseDate = DateTime.Now;
                item.Location = "London";
                item.Description = "AirTicket";
                item.Amount = 2000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                string status = "ApprovedBySupervisor";
                Guid approvedBy = new Guid("832e2e47-e82c-40be-9c39-8587274468fe");
                expenseReportDAL.ProcessExpense(expenseReport);
                Assert.IsTrue(CheckDatabaseForExpenseId(expenseReport.ExpenseId), "Expense Id was not found in database");
                expenseReportDAL.SupervisorActionOnExpenseReport(expenseReport.ExpenseId, approvedBy, status);
                testTransaction.Dispose();
            }
        }

        [TestMethod]
        public void ExpenseReportDAL_SupervisorActionOnExpenseReport_Reject_ActionSuccess()
        {
            
            using (TransactionScope testTransaction = new TransactionScope())
            {
                ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
                ExpenseReport expenseReport = new ExpenseReport();
                ExpenseItem item = new ExpenseItem();
                expenseReport.CreateDate = DateTime.Now;
                expenseReport.CreatedBy.UserId = new Guid("18b783f1-cb97-4e03-bb54-d3c41637b69c");
                expenseReport.CreatedBy.Dept.DepartmentId = 1;
                expenseReport.Status = ReportStatus.Submitted;
                item.ExpenseDate = DateTime.Now;
                item.Location = "London";
                item.Description = "AirTicket";
                item.Amount = 2000;
                item.Currency = "AUD";
                item.AudAmount = item.Amount;
                expenseReport.ExpenseItems.Add(item);
                string status = "RejectedBySupervisor";
                Guid approvedBy = new Guid("832e2e47-e82c-40be-9c39-8587274468fe");
                expenseReportDAL.ProcessExpense(expenseReport);
                Assert.IsTrue(CheckDatabaseForExpenseId(expenseReport.ExpenseId), "Expense Id was not found in database");
                expenseReportDAL.SupervisorActionOnExpenseReport(expenseReport.ExpenseId, approvedBy, status);
                testTransaction.Dispose();
             }
        }

        private bool CheckDatabaseForReportStatus(int id)
        {
            bool updated;

            DataAccessFunctions daFunctions = new DataAccessFunctions();
            string query = String.Format("SELECT status  from ExpenseHeader WHERE ExpenseId={0}", id);
            daFunctions.Command.CommandText = query;

            try
            {
                daFunctions.Connection.Open();

                string status = daFunctions.Command.ExecuteScalar().ToString();

                daFunctions.Connection.Close();

                if (status == "ApprovedBySupervisor")
                {
                    updated = true;
                }
                else if (status == "RejectedBySupervisor")
                {
                    updated = true;
                }
                else
                {
                    updated = false;
                }

                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem running method CheckDatabaseForReportStatus: " + ex.Message);
            }
        }


        //Tests related to BudgetTracker
        [TestMethod]
        public void BudgetTracker_IsBudgetExceeded_ReturnTrue()
        {
            BudgetTracker budget = new BudgetTracker();
            budget.DepartmentBudget(10000, 1);            
            bool result= budget.IsBudgetExceeded(10001);
            Assert.IsTrue(result);           
        }

        [TestMethod]
        public void BudgetTracker_IsBudgetExceeded_ReturnFalse()
        {
            BudgetTracker budget = new BudgetTracker();
            budget.DepartmentBudget(10000, 1);
            bool result = budget.IsBudgetExceeded(2000);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BudgetTracker_CompanyBudget_IsValid()
        {
            BudgetTracker budget = new BudgetTracker();
            budget.CompanyBudget();
            Assert.IsTrue(budget.BudgetAmount==30000);
        }

        //Tests related to SpendTrackerDAL
        [TestMethod]
        public void SpendTrackerDAL_TotalExpenseAmountByDept_IsTrue()
        {
            SpendTrackerDAL tracker = new SpendTrackerDAL();
            decimal ReturnAmount=tracker.TotalExpenseAmountByDept(2,DateTime.Now.Month);
            Assert.IsTrue(ReturnAmount >= 0);
        }

        [TestMethod]
        public void SpendTrackerDAL_TotalExpenseAmountByDeptProcessed_IsTrue()
        {
            SpendTrackerDAL tracker = new SpendTrackerDAL();
            decimal ReturnAmount = tracker.TotalExpenseAmountByDeptProcessed(3, DateTime.Now.Month);
            Assert.IsTrue(ReturnAmount>=0);
        }

        [TestMethod]
        public void SpendTrackerDAL_TotalExpenseAmountByCompany_IsTrue()
        {
            SpendTrackerDAL tracker = new SpendTrackerDAL();
            decimal ReturnAmount = tracker.TotalExpenseAmountByCompany(DateTime.Now.Month);
            Assert.IsTrue(ReturnAmount>=0);
        }
 
    }
}
    

