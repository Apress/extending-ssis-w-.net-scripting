<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.lblStatus = New System.Windows.Forms.Label()
    Me.btnStart = New System.Windows.Forms.Button()
    Me.lbLog = New System.Windows.Forms.ListBox()
    Me.SuspendLayout()
    '
    'lblStatus
    '
    Me.lblStatus.AutoSize = True
    Me.lblStatus.Location = New System.Drawing.Point(20, 68)
    Me.lblStatus.Name = "lblStatus"
    Me.lblStatus.Size = New System.Drawing.Size(243, 17)
    Me.lblStatus.TabIndex = 5
    Me.lblStatus.Text = "StatusLabel                                        "
    '
    'btnStart
    '
    Me.btnStart.Location = New System.Drawing.Point(23, 23)
    Me.btnStart.Name = "btnStart"
    Me.btnStart.Size = New System.Drawing.Size(75, 23)
    Me.btnStart.TabIndex = 4
    Me.btnStart.Text = "Start"
    Me.btnStart.UseVisualStyleBackColor = True
    '
    'lbLog
    '
    Me.lbLog.FormattingEnabled = True
    Me.lbLog.ItemHeight = 16
    Me.lbLog.Location = New System.Drawing.Point(23, 98)
    Me.lbLog.Name = "lbLog"
    Me.lbLog.Size = New System.Drawing.Size(247, 132)
    Me.lbLog.TabIndex = 6
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(282, 253)
    Me.Controls.Add(Me.lbLog)
    Me.Controls.Add(Me.lblStatus)
    Me.Controls.Add(Me.btnStart)
    Me.Name = "Form1"
    Me.Text = "Form1"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Private WithEvents lblStatus As System.Windows.Forms.Label
  Private WithEvents btnStart As System.Windows.Forms.Button
  Friend WithEvents lbLog As System.Windows.Forms.ListBox

End Class
