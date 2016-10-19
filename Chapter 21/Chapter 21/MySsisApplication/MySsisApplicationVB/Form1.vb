#Region "CustomNamespace"
Imports Microsoft.SqlServer.Dts.Runtime
#End Region

Public Class Form1

  Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
    ' Instantiate SSIS application object
    Dim myApplication As Application = New Application()

    ' Load package from file system (use LoadFromSqlServer for SQL Server based packages)
    lblStatus.Text = "Loading package from file system."
    Dim myPackage As Package = myApplication.LoadPackage("C:\Users\jvanrossum\OneDrive\Documenten\Extending SSIS with .Net\02_Code\2012\Extending SSIS with .NET\Chapter 21\Package.dtsx", Nothing)

    ' Optional set the value from one of the SSIS package variables
    myPackage.Variables("User::myVar").Value = "test123"

    ' Execute package
    lblStatus.Text = "Executing package"
    Dim myResult As DTSExecResult = myPackage.Execute()

    ' Show the execution result
    lblStatus.Text = "Package result: " + myResult.ToString()

    ''''''''''''''''''''''''''''''''''''''''
    ' Code for showing warnings and errors '
    ''''''''''''''''''''''''''''''''''''''''

    ' Create a temporary table to store warnings and errors
    Dim myLogTable As DataTable = New DataTable("myLogTable")
    myLogTable.Columns.Add("LogTime", GetType(DateTime))
    myLogTable.Columns.Add("Source", GetType(String))
    myLogTable.Columns.Add("Message", GetType(String))

    ' Loop through all warnings and add them to the table
    For Each packageWarning As DtsWarning In myPackage.Warnings
      myLogTable.Rows.Add(Convert.ToDateTime(packageWarning.TimeStamp), packageWarning.Source, packageWarning.Description)
    Next

    ' Loop through all errors and add them to the table
    For Each packageError As DtsError In myPackage.Errors
      myLogTable.Rows.Add(Convert.ToDateTime(packageError.TimeStamp), packageError.Source, packageError.Description)
    Next

    ' Create a sorted view and then make a new datatable with it
    myLogTable.DefaultView.Sort = "LogTime"
    Dim myLogTableSorted As DataTable = myLogTable.DefaultView.ToTable()

    ' Cleanup resource
    myLogTable.Dispose()

    ' Loop through the new sorted dataset and add rows to the listbox
    For Each row As DataRow In myLogTableSorted.Rows
      lbLog.Items.Add(row.Field(Of DateTime)(0).ToLongTimeString() + " - " + row.Field(Of String)(1) + " - " + row.Field(Of String)(2))
    Next

    ' Cleanup resource
    myLogTableSorted.Dispose()

  End Sub
End Class
