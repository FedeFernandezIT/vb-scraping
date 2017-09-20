<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.panBrowser = New System.Windows.Forms.Panel()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnInit = New System.Windows.Forms.Button()
        Me.dgwDataCard = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.lblStatus = New System.Windows.Forms.Label()
        CType(Me.dgwDataCard,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'panBrowser
        '
        Me.panBrowser.Location = New System.Drawing.Point(254, 12)
        Me.panBrowser.Name = "panBrowser"
        Me.panBrowser.Size = New System.Drawing.Size(1117, 557)
        Me.panBrowser.TabIndex = 0
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(13, 24)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(100, 23)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Abrir"
        Me.btnOpen.UseVisualStyleBackColor = true
        '
        'btnInit
        '
        Me.btnInit.Location = New System.Drawing.Point(119, 24)
        Me.btnInit.Name = "btnInit"
        Me.btnInit.Size = New System.Drawing.Size(100, 23)
        Me.btnInit.TabIndex = 2
        Me.btnInit.Text = "Iniciar"
        Me.btnInit.UseVisualStyleBackColor = true
        '
        'dgwDataCard
        '
        Me.dgwDataCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwDataCard.Location = New System.Drawing.Point(13, 83)
        Me.dgwDataCard.Name = "dgwDataCard"
        Me.dgwDataCard.Size = New System.Drawing.Size(235, 486)
        Me.dgwDataCard.TabIndex = 3
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = true
        Me.lblStatus.Location = New System.Drawing.Point(16, 55)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblStatus.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1382, 581)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.dgwDataCard)
        Me.Controls.Add(Me.btnInit)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.panBrowser)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dgwDataCard,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents panBrowser As Panel
    Friend WithEvents btnOpen As Button
    Friend WithEvents btnInit As Button
    Friend WithEvents dgwDataCard As DataGridView
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents lblStatus As Label
End Class
