using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#region CustomNamespace
using Microsoft.SqlServer.Dts.Runtime;
#endregion


namespace MySsisApplicationC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Instantiate SSIS application object
            Microsoft.SqlServer.Dts.Runtime.Application myApplication = new Microsoft.SqlServer.Dts.Runtime.Application();

            // Load package from file system (use LoadFromSqlServer for SQL Server based packages)
            lblStatus.Text = "Loading package from file system.";
            Package myPackage = myApplication.LoadPackage(@"C:\Users\jvanrossum\OneDrive\Documenten\Extending SSIS with .Net\02_Code\2012\Extending SSIS with .NET\Chapter 21\Package.dtsx", null);

            // Optional set the value from one of the SSIS package variables
            myPackage.Variables["User::myVar"].Value = "test123";

            // Execute package
            lblStatus.Text = "Executing package";
            DTSExecResult myResult = myPackage.Execute();

            // Show the execution result
            lblStatus.Text = "Package result: " + myResult.ToString();

            //  https://msdn.microsoft.com/en-us/library/ms136090(v=sql.120).aspx


            //////////////////////////////////////////
            // Code for showing warnings and errors //
            //////////////////////////////////////////

            // Create a temporary table to store warnings and errors
            DataTable myLogTable = new DataTable("myLogTable");
            myLogTable.Columns.Add("LogTime", typeof(DateTime));
            myLogTable.Columns.Add("Source", typeof(string));
            myLogTable.Columns.Add("Message", typeof(string));

            // Loop through all warnings and add them to the table
            foreach (DtsWarning packageWarning in myPackage.Warnings)
            {
                myLogTable.Rows.Add(Convert.ToDateTime(packageWarning.TimeStamp), packageWarning.Source, packageWarning.Description);
            }

            // Loop through all errors and add them to the table
            foreach (DtsError packageError in myPackage.Errors)
            {
                myLogTable.Rows.Add(Convert.ToDateTime(packageError.TimeStamp), packageError.Source, packageError.Description);
            }

            // Create a sorted view and then make a new datatable with it
            myLogTable.DefaultView.Sort = "LogTime";
            DataTable myLogTableSorted = myLogTable.DefaultView.ToTable();

            // Cleanup resource
            myLogTable.Dispose();

            // Loop through the new sorted dataset and add rows to the listbox
            foreach (DataRow row in myLogTableSorted.Rows)
            {
                lbLog.Items.Add(row.Field<DateTime>(0).ToLongTimeString() + " - " + row.Field<string>(1) + " - " + row.Field<string>(2));
            }

            // Cleanup resource
            myLogTableSorted.Dispose();
        }
    }
}
