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
            //  string path = "C:\\Users\\rikil\\Source\\Repos\\32013-Assignment1";
            string path = "C:\\Users\\Bikrem\\Documents\\Visual Studio 2012\\Projects\\.Net Assignment-1\\32013-Assignment1";

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
            Guid id = new Guid("78560DD3-F95E-4011-B40D-A7B56ED17F24");
            Employee employee = new Employee();

            EmployeeDAL employeeDAL = new EmployeeDAL();

            employee = employeeDAL.GetEmployee(id);

            bool IsEqual = TestEmployeeComparer(employee);

            Assert.IsTrue(IsEqual, "Employee from database is not equal to test employee");
        }

        [TestMethod]
        public void ConvertCurrency_ConvertCNYToAUD_AreEqual()
        {
            double testAUDAmount = 100 * 0.17430;

            Assert.IsNotNull(ConfigurationManager.AppSettings["CNY"], "Currency CNY is NULL");
            Assert.AreEqual(testAUDAmount, CurrencyConverter.ConvertToAUD("CNY", 100), "Conversion to CNY Failed");

        }

        [TestMethod]
        public void ConvertCurrency_ConvertEURToAUD_AreEqual()
        {
            double testAUDAmount = 100 * 1.49146;

            Assert.IsNotNull(ConfigurationManager.AppSettings["EUR"], "Currency EUR is NULL in AppConfig");
            Assert.AreEqual(testAUDAmount, CurrencyConverter.ConvertToAUD("EUR", 100), "Conversion to EUR Failed");
        }

        [TestMethod]
        public void ExpenseReportDAL_GetReportSummaryByConsultant_IsTrue()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();

            List<ExpenseReport> reports = new List<ExpenseReport>();
            Guid id = new Guid("2ABC120C-F985-4FEF-87D1-74B6F697B140");
            string status = "%";

            reports = expenseReportDAL.GetExpenseReportsByConsultant(id, status);

            Assert.IsTrue(reports.Count > 0, "No data in list of reports");

            foreach (ExpenseReport report in reports)
            {
                Assert.IsTrue(report.ExpenseItems.Count > 0, "No Data in list of expenseitems");
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
            item.Location = "Brisbane";
            item.Description = "Mouse and Keyboard";
            item.Amount = 10.50M;
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
            testEmployee.Dept.DepartmentId = 2;
            testEmployee.Dept.DepartmentName = "Logistics Services";
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
        public void ExpenseReportDAL_GetReportBySupervisor_IsTrue()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            List<ExpenseReport> reports = new List<ExpenseReport>();
            int deptId = 1;

                string status = ReportStatus.ApprovedBySupervisor.ToString();
           //   string status = ReportStatus.RejectedByAccountant.ToString();
           //   string status = ReportStatus.RejectedBySupervisor.ToString();

                reports = expenseReportDAL.GetReportsByDepartment(deptId, status);
                Assert.IsTrue(reports.Count > 0, "No data in expense report");
                foreach (ExpenseReport report in reports)
                {
                    Assert.IsTrue(report.ExpenseItems.Count > 0, "No item in expense report");
                }
        
        }

        [TestMethod]
        public void ExpenseReportDAL_SumOfExpenseApproved_AreEqual()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            int deptId = 1;
            decimal actual= expenseReportDAL.SumOfExpenseApproved(deptId);
            decimal expected = 133;        
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseReportDAL_SupervisorActionOnExpenseReport_ActionSuccess()
        {
            ExpenseReportDAL expenseReportDAL = new ExpenseReportDAL();
            ExpenseReport expenseReport = new ExpenseReport();
            ExpenseItem expenseItem = new ExpenseItem();

            int expenseId = 31;
            Guid approvedBy = new Guid("651c58ff-c87a-4721-9f13-33caef1a12fe");
            DateTime approvedDate = DateTime.Now;
           // string status = "ApprovedBySupervisor";
           string status = "RejectedBySupervisor";

            using (TransactionScope testTransaction = new TransactionScope())
            {
                expenseReportDAL.SupervisorActionOnExpenseReport(expenseId, approvedBy, status);
                Assert.IsTrue(CheckDatabaseForReportStatus(expenseId));
                testTransaction.Dispose();
            }
        }

        //[TestMethod]
        //public void ExpenseReportBuilder_CalcualateRemainingBudget_AreEqaul()
        //{
        //    ExpenseReportBuilder expReportBuilder=new ExpenseReportBuilder(); 
        //    double expected = 44;
        //    double actual = expReportBuilder.CalculateRemainingBudget(100, 56);
        //    Assert.AreEqual(expected, actual);
        //}

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
    }
}
    

