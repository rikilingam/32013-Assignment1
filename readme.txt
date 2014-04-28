Group Name: Three Amigos

Group Members:
Rikiliesh LINGAM 11332691
Bikrem SING 11477825 
Quan Thanh LE

--------------------------------------------------------------
README - Expense Management Application (Assignment 1)

1. ---Supported Browsers---
	The software has been tested to run on the following browsers only.
	Google Chrome 34.0.1847
	Internet Explorer 10+
	Firefox 28.0


2. ---Test configuration---
a.	For the tests to run successfully, the test project needs to know the path of the database. The path is defined in the TestInitialization section of the ExpenseMangementTests class located within Tests project. 
	Change the "path" variable to the root path of the project i.e."C:\\Users\\riki\\Source\\Repos\\32013-Assignment1";

b.	When running tests if tests fail with error "MSDTC on server <servername> is unavailable then the Distributed Transaction Coordinator service will need to be start on the computer
	running the tests. The services console is available from Start > Control Panel > Administrative Tools > Services


3. ---User Accounts---
	The following test user accounts can be used for testing purposes, the password for all accounts is "$Password"

	Role		User		Username	Department
	------		-----		---------	-----------
	Consultants
			Vikki Car 	vikkic		State Services
			Jack Benny	jackb		Logistics Services
			Tom Jones	tomj		Higher Education Services
			Jane Alexander	janea		Higher Education Services	

	Supervisors
			Woody Allen	woodya		State Services
			Ray Charles 	rayc		State Services
			Jean Arthur	jeana		Higher Education Services
			Tony Bennett	tonyb		Logistics Services

	Accounts
			Richard Burton 	richardb	Accounting
			Minnie Pearl 	minniep		Accounting

	No Role
			Nancy Drew 	nancyd		State Services


4. ---Budget---

a. 	Company Budget
I. 	The company budget is configured in the web.config and is currently set to $30,000. This amount is used to track expenses approved for the current month.
	Therefore, expenses must be approved by the accounts department by the last day of the month otherwise the budget will be reset.

II. 	The expense amount does not reflect against the budget unless the budget has been approved by the accounts department

b. 	Department Budget
I.	Budgets for each department is stored in the database in table Department.
II.	The department budget is the sumt of expenses that have been approved by the accounts department plus expenses approved by the department supervisor/s.


b.	Budget limits
	When a department reaches their monthly limit a pop will be displayed requesting confirmation from the supervisor before updating the status of the expense report. However, when an accounts person approves
	expense reports, reports will have the footer highlighted in red if the expense will result in the department going over their monthly budget.

5. ---Currency Conversion---
	Currency rates are stored in the web.config as the value of the currency in AUD

6. ---Receipts---
	Receipts can be added to each expense item, however, the attachment must be a PDF file. In addition, the file size must be less than 4MB
	which is the default IIS limit, if file greater than 4Mb is uploaded the system will throw an exception, this is because the file size cannot be determined server side unless the
	file has been uploaded in memory.
