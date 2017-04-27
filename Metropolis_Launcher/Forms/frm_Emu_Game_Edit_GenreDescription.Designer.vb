<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Emu_Game_Edit_GenreDescription
    Inherits MKNetDXLib.frm_MKDXBaseForm

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
		Me.lbl_Genre = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Description = New MKNetDXLib.ctl_MKDXMemoEdit()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Genre
		'
		Me.lbl_Genre.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Genre.AutoEllipsis = True
		Me.lbl_Genre.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Genre.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Genre.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Genre.MKBoundControl1 = Nothing
		Me.lbl_Genre.MKBoundControl2 = Nothing
		Me.lbl_Genre.MKBoundControl3 = Nothing
		Me.lbl_Genre.MKBoundControl4 = Nothing
		Me.lbl_Genre.MKBoundControl5 = Nothing
		Me.lbl_Genre.Name = "lbl_Genre"
		Me.lbl_Genre.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Genre.Size = New System.Drawing.Size(402, 36)
		Me.lbl_Genre.TabIndex = 1
		'
		'txb_Description
		'
		Me.txb_Description.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txb_Description.Location = New System.Drawing.Point(0, 36)
		Me.txb_Description.MKBoundLabel = Nothing
		Me.txb_Description.MKEditValue_Compare = Nothing
		Me.txb_Description.Name = "txb_Description"
		Me.txb_Description.Properties.ReadOnly = True
		Me.txb_Description.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txb_Description.Size = New System.Drawing.Size(402, 275)
		Me.txb_Description.TabIndex = 2
		'
		'frm_Emu_Game_Edit_GenreDescription
		'
		Me.ClientSize = New System.Drawing.Size(402, 311)
		Me.Controls.Add(Me.txb_Description)
		Me.Controls.Add(Me.lbl_Genre)
		Me.Name = "frm_Emu_Game_Edit_GenreDescription"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_Genre As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Description As MKNetDXLib.ctl_MKDXMemoEdit

End Class
