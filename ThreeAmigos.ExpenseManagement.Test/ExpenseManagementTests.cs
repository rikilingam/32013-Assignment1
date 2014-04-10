using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.BusinessObject;
using ThreeAmigos.ExpenseManagement.DataAccess;
using System.IO;
using System.Configuration;


namespace ThreeAmigos.ExpenseManagement.Test
{
    [TestClass]
    public class ExpenseManagementTests
    {
        //TestContext { get; set; }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            string path = "C:\\Users\\rikil\\Source\\Repos\\32013-Assignment1";

            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //|DataDirectory|\32013-Assignment1\App_Data\
        }

        [TestMethod]
        public void AppConfig_VerifyAppDomainHasConnectionString()
        {
            string value = ConfigurationManager.ConnectionStrings["localDatabase"].ConnectionString;
            Assert.IsFalse(String.IsNullOrEmpty(value), "No App.Config found.");
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
    }
}
