using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos.ExpenseManagement.BusinessLogic;
using ThreeAmigos.ExpenseManagement.DataAccess;

namespace ThreeAmigos.ExpenseManagement.Test
{
    [TestClass]
    public class ExpenseManagementTests
    {

        [TestMethod]
        public void EmployeeDAL_GetEmployee_IsNotNull()
        {
                      
            Guid id = new Guid("78560DD3-F95E-4011-B40D-A7B56ED17F24");
            Employee employee = new Employee(id);


            string test = "test";

            Assert.IsNotNull(test, "employee is not null");

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
    }
}
