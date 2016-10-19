#Region "CustomNamespace"
Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Management.IntegrationServices
Imports System.Collections.ObjectModel
#End Region

Public Class Form1

  Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
    ' Connecting to the SQL Server instance where the catalog is located
    Using ssisConnection As New SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=SSPI;")
      Try
        ' SSIS server object with connection
        Dim ssisServer As New IntegrationServices(ssisConnection)

        ' The reference to the package which you want to execute
        lblStatus.Text = "Loading package from catalog."
        Form.ActiveForm.Refresh()
        Dim ssisPackage As PackageInfo = ssisServer.Catalogs("SSISDB").Folders("Extending SSIS").Projects("Chapter 21").Packages("myPackage.dtsx")

        ' Setting parameters
        Dim executionParameter As New Collection(Of PackageInfo.ExecutionValueParameterSet)()

        ' Add execution parameter for an asynchronized (value=0, default) or synchronized (value=1) execution
        executionParameter.Add(New PackageInfo.ExecutionValueParameterSet() With { _
          .ObjectType = 50, _
          .ParameterName = "SYNCHRONIZED", _
          .ParameterValue = 0 _
        })

        ' Add execution parameter (value) to override the default logging level (0=None, 1=Basic, 2=Performance, 3=Verbose)
        executionParameter.Add(New PackageInfo.ExecutionValueParameterSet() With { _
          .ObjectType = 50, _
          .ParameterName = "LOGGING_LEVEL", _
          .ParameterValue = 3 _
        })

        ' Add a project parameter (value) to fill a project parameter
        executionParameter.Add(New PackageInfo.ExecutionValueParameterSet() With { _
          .ObjectType = 20, _
          .ParameterName = "MyProjectParameter", _
          .ParameterValue = "some value" _
        })

        ' Add a package parameter (value) to fill a package parameter
        executionParameter.Add(New PackageInfo.ExecutionValueParameterSet() With { _
          .ObjectType = 30, _
          .ParameterName = "MyPackageParameter", _
          .ParameterValue = "some value" _
        })

        ' Execute package and return the ServerExecutionId
        Dim executionIdentifier As Long = ssisPackage.Execute(False, Nothing, executionParameter)

        ' Get execution details with the ServerExecutionId from the previous step
        lblStatus.Text = "Executing package"
        Form.ActiveForm.Refresh()
        Dim executionOperation As ExecutionOperation = ssisServer.Catalogs("SSISDB").Executions(executionIdentifier)

        ' Workaround for 30 second timeout:
        ' Loop while the execution is not completed
        While Not (executionOperation.Completed)
          ' Refresh execution info
          executionOperation.Refresh()

          ' Wait 5 seconds before refreshing (we don't want to stress the server)
          System.Threading.Thread.Sleep(5000)
        End While
        ' Showing the ServerExecutionId
        lblStatus.Text = "Execution " + executionOperation.Id.ToString() + " finished: " + executionOperation.Status.ToString()

        ' Clear listbox before adding log rows to it
        lbLog.Items.Clear()

        ' Loop through the log and add the messages to the listbox
        For Each message As OperationMessage In ssisServer.Catalogs("SSISDB").Executions(executionIdentifier).Messages
          lbLog.Items.Add(message.MessageType.ToString() + ": " + message.Message)
        Next
      Catch ex As Exception
        ' Log code for exceptions
        lblStatus.Text = "Error: " + ex.Message
      End Try
    End Using
  End Sub
End Class
