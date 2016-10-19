using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#region CustomNamespace
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.IntegrationServices;
using System.Collections.ObjectModel;
#endregion

namespace MySsisApplication2C
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Connecting to the SQL Server instance where the catalog is located
            using (SqlConnection ssisConnection = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=SSPI;"))
            {
                try
                {
                    // SSIS server object with connection
                    IntegrationServices ssisServer = new IntegrationServices(ssisConnection);

                    // The reference to the package which you want to execute
                    lblStatus.Text = "Loading package from catalog.";
                    Form.ActiveForm.Refresh();
                    PackageInfo ssisPackage = ssisServer.Catalogs["SSISDB"].Folders["Extending SSIS"].Projects["Chapter 21"].Packages["myPackage.dtsx"];

                    // Setting parameters
                    Collection<PackageInfo.ExecutionValueParameterSet> executionParameter = new Collection<PackageInfo.ExecutionValueParameterSet>();

                    // Add execution parameter for an asynchronized (value=0, default) or synchronized (value=1) execution
                    executionParameter.Add(new PackageInfo.ExecutionValueParameterSet {ObjectType = 50, ParameterName = "SYNCHRONIZED", ParameterValue = 0});

                    // Add execution parameter (value) to override the default logging level (0=None, 1=Basic, 2=Performance, 3=Verbose)
                    executionParameter.Add(new PackageInfo.ExecutionValueParameterSet {ObjectType = 50, ParameterName = "LOGGING_LEVEL", ParameterValue = 3});

                    // Add a project parameter (value) to fill a project parameter
                    //executionParameter.Add(new PackageInfo.ExecutionValueParameterSet { ObjectType = 20, ParameterName = "MyProjectParameter", ParameterValue = "some value" });

                    // Add a package parameter (value) to fill a package parameter
                    //executionParameter.Add(new PackageInfo.ExecutionValueParameterSet { ObjectType = 30, ParameterName = "MyPackageParameter", ParameterValue = "some value" });

                    // Execute package and return the ServerExecutionId
                    long executionIdentifier = ssisPackage.Execute(false, null, executionParameter);

                    // Get execution details with the ServerExecutionId from the previous step
                    lblStatus.Text = "Executing package";
                    Form.ActiveForm.Refresh();
                    ExecutionOperation executionOperation = ssisServer.Catalogs["SSISDB"].Executions[executionIdentifier];

                    // Workaround for 30 second timeout:
                    // Loop while the execution is not completed
                    while (!(executionOperation.Completed))
                    {
                        // Refresh execution info
                        executionOperation.Refresh();

                        // Wait 5 seconds before refreshing (we don't want to stress the server)
                        System.Threading.Thread.Sleep(5000);
                    }
                    // Showing the ServerExecutionId
                    lblStatus.Text = "Execution " + executionOperation.Id.ToString() + " finished: " + executionOperation.Status.ToString();

                    // Clear listbox before adding log rows to it
                    lbLog.Items.Clear();

                    // Loop through the log and add the messages to the listbox
                    foreach (OperationMessage message in ssisServer.Catalogs["SSISDB"].Executions[executionIdentifier].Messages)
                    {
                        lbLog.Items.Add(message.MessageType.ToString() + ": " + message.Message);
                    }
                }
                catch (Exception ex)
                {
                    // Log code for exceptions
                    lblStatus.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}
