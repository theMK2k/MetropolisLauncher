<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucr_Todo
	Inherits MKNetDXLib.ctl_MKDXUserControl

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer

	'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
	'Sie kann mit dem Windows Form-Designer geändert werden.  
	'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.SuspendLayout()
		'
		'ucr_Todo
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Name = "ucr_Todo"
		Me.Size = New System.Drawing.Size(800, 600)
		Me.ResumeLayout(False)

	End Sub

End Class
