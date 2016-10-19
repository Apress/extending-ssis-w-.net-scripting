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
    Me.lbLog = New System.Windows.Forms.ListBox()
    Me.lblStatus = New System.Windows.Forms.Label()
    Me.btnStart = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'lbLog
    '
    Me.lbLog.FormattingEnabled = True
    Me.lbLog.ItemHeight = 16
    Me.lbLog.Location = New System.Drawing.Point(13, 85)
    Me.lbLog.Name = "lbLog"
    Me.lbLog.Size = New System.Drawing.Size(802, 148)
    Me.lbLog.TabIndex = 7
    '
    'lblStatus
    '
    Me.lblStatus.AutoSize = True
    Me.lblStatus.Location = New System.Drawing.Point(14, 56)
    Me.lblStatus.Name = "lblStatus"
    Me.lblStatus.Size = New System.Drawing.Size(83, 17)
    Me.lblStatus.TabIndex = 6
    Me.lblStatus.Text = "StatusLabel"
    '
    'btnStart
    '
    Me.btnStart.Location = New System.Drawing.Point(17, 19)
    Me.btnStart.Name = "btnStart"
    Me.btnStart.Size = New System.Drawing.Size(75, 23)
    Me.btnStart.TabIndex = 5
    Me.btnStart.Text = "Start"
    Me.btnStart.UseVisualStyleBackColor = True
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(827, 253)
    Me.Controls.Add(Me.lbLog)
    Me.Controls.Add(Me.lblStatus)
    Me.Controls.Add(Me.btnStart)
    Me.Name = "Form1"
    Me.Text = "Form1"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Private WithEvents lbLog As System.Windows.Forms.ListBox
  Private WithEvents lblStatus As System.Windows.Forms.Label
  Private WithEvents btnStart As System.Windows.Forms.Button

End Class
