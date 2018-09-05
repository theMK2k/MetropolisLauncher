<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Emulators_Pre_Post_Launch_Command_Edit
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer

	'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
	'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
	'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Emulators_Pre_Post_Launch_Command_Edit))
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip4 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem4 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip5 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem5 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip6 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem6 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem6 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip7 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem7 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem7 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.lbl_StartupParameter = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Executable = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Directory = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Directory = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Executable = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_EmulatorFileOpen = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.txb_StartupParameter = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_WaitForExit = New MKNetDXLib.ctl_MKDXLabel()
		Me.chb_WaitForExit = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.chb_Minimized = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.lbl_Minimized = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		CType(Me.txb_Directory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Executable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_StartupParameter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_WaitForExit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_Minimized.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_StartupParameter
		'
		Me.lbl_StartupParameter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_StartupParameter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_StartupParameter.Location = New System.Drawing.Point(2, 51)
		Me.lbl_StartupParameter.MKBoundControl1 = Nothing
		Me.lbl_StartupParameter.MKBoundControl2 = Nothing
		Me.lbl_StartupParameter.MKBoundControl3 = Nothing
		Me.lbl_StartupParameter.MKBoundControl4 = Nothing
		Me.lbl_StartupParameter.MKBoundControl5 = Nothing
		Me.lbl_StartupParameter.Name = "lbl_StartupParameter"
		Me.lbl_StartupParameter.Size = New System.Drawing.Size(112, 20)
		ToolTipTitleItem1.Text = "Startup Parameter"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = resources.GetString("ToolTipItem1.Text")
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.lbl_StartupParameter.SuperTip = SuperToolTip1
		Me.lbl_StartupParameter.TabIndex = 12
		Me.lbl_StartupParameter.Text = "Startup Parameter:"
		'
		'lbl_Executable
		'
		Me.lbl_Executable.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Executable.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Executable.Location = New System.Drawing.Point(2, 27)
		Me.lbl_Executable.MKBoundControl1 = Nothing
		Me.lbl_Executable.MKBoundControl2 = Nothing
		Me.lbl_Executable.MKBoundControl3 = Nothing
		Me.lbl_Executable.MKBoundControl4 = Nothing
		Me.lbl_Executable.MKBoundControl5 = Nothing
		Me.lbl_Executable.Name = "lbl_Executable"
		Me.lbl_Executable.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Executable.TabIndex = 13
		Me.lbl_Executable.Text = "Executable:"
		'
		'txb_Directory
		'
		Me.txb_Directory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Directory.Location = New System.Drawing.Point(116, 3)
		Me.txb_Directory.MKBoundLabel = Nothing
		Me.txb_Directory.MKEditValue_Compare = Nothing
		Me.txb_Directory.Name = "txb_Directory"
		Me.txb_Directory.Size = New System.Drawing.Size(283, 20)
		Me.txb_Directory.TabIndex = 8
		'
		'lbl_Directory
		'
		Me.lbl_Directory.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Directory.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Directory.Location = New System.Drawing.Point(3, 4)
		Me.lbl_Directory.MKBoundControl1 = Nothing
		Me.lbl_Directory.MKBoundControl2 = Nothing
		Me.lbl_Directory.MKBoundControl3 = Nothing
		Me.lbl_Directory.MKBoundControl4 = Nothing
		Me.lbl_Directory.MKBoundControl5 = Nothing
		Me.lbl_Directory.Name = "lbl_Directory"
		Me.lbl_Directory.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Directory.TabIndex = 14
		Me.lbl_Directory.Text = "Directory:"
		'
		'txb_Executable
		'
		Me.txb_Executable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Executable.Location = New System.Drawing.Point(116, 26)
		Me.txb_Executable.MKBoundLabel = Nothing
		Me.txb_Executable.MKEditValue_Compare = Nothing
		Me.txb_Executable.Name = "txb_Executable"
		Me.txb_Executable.Size = New System.Drawing.Size(283, 20)
		Me.txb_Executable.TabIndex = 10
		'
		'btn_EmulatorFileOpen
		'
		Me.btn_EmulatorFileOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_EmulatorFileOpen.Location = New System.Drawing.Point(402, 3)
		Me.btn_EmulatorFileOpen.Name = "btn_EmulatorFileOpen"
		Me.btn_EmulatorFileOpen.Size = New System.Drawing.Size(32, 20)
		ToolTipTitleItem2.Text = "Browse"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = "Browse an executable incl. its path"
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		Me.btn_EmulatorFileOpen.SuperTip = SuperToolTip2
		Me.btn_EmulatorFileOpen.TabIndex = 9
		Me.btn_EmulatorFileOpen.Text = "..."
		'
		'txb_StartupParameter
		'
		Me.txb_StartupParameter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_StartupParameter.Location = New System.Drawing.Point(116, 49)
		Me.txb_StartupParameter.MKBoundLabel = Nothing
		Me.txb_StartupParameter.MKEditValue_Compare = Nothing
		Me.txb_StartupParameter.Name = "txb_StartupParameter"
		Me.txb_StartupParameter.Size = New System.Drawing.Size(283, 20)
		ToolTipTitleItem3.Text = "Startup Parameter"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = resources.GetString("ToolTipItem3.Text")
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem3)
		Me.txb_StartupParameter.SuperTip = SuperToolTip3
		Me.txb_StartupParameter.TabIndex = 15
		'
		'lbl_WaitForExit
		'
		Me.lbl_WaitForExit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_WaitForExit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_WaitForExit.Location = New System.Drawing.Point(3, 74)
		Me.lbl_WaitForExit.MKBoundControl1 = Nothing
		Me.lbl_WaitForExit.MKBoundControl2 = Nothing
		Me.lbl_WaitForExit.MKBoundControl3 = Nothing
		Me.lbl_WaitForExit.MKBoundControl4 = Nothing
		Me.lbl_WaitForExit.MKBoundControl5 = Nothing
		Me.lbl_WaitForExit.Name = "lbl_WaitForExit"
		Me.lbl_WaitForExit.Size = New System.Drawing.Size(112, 20)
		ToolTipTitleItem4.Text = "Wait for Exit"
		ToolTipItem4.LeftIndent = 6
		ToolTipItem4.Text = "Wait until the command finished."
		SuperToolTip4.Items.Add(ToolTipTitleItem4)
		SuperToolTip4.Items.Add(ToolTipItem4)
		Me.lbl_WaitForExit.SuperTip = SuperToolTip4
		Me.lbl_WaitForExit.TabIndex = 16
		Me.lbl_WaitForExit.Text = "Wait for Exit:"
		'
		'chb_WaitForExit
		'
		Me.chb_WaitForExit.Location = New System.Drawing.Point(116, 74)
		Me.chb_WaitForExit.MKBoundLabel = Nothing
		Me.chb_WaitForExit.MKEditValue_Compare = Nothing
		Me.chb_WaitForExit.Name = "chb_WaitForExit"
		Me.chb_WaitForExit.Properties.Caption = ""
		Me.chb_WaitForExit.Size = New System.Drawing.Size(75, 19)
		ToolTipTitleItem5.Text = "Wait for Exit"
		ToolTipItem5.LeftIndent = 6
		ToolTipItem5.Text = "Wait until the command finished."
		SuperToolTip5.Items.Add(ToolTipTitleItem5)
		SuperToolTip5.Items.Add(ToolTipItem5)
		Me.chb_WaitForExit.SuperTip = SuperToolTip5
		Me.chb_WaitForExit.TabIndex = 17
		'
		'chb_Minimized
		'
		Me.chb_Minimized.Location = New System.Drawing.Point(116, 98)
		Me.chb_Minimized.MKBoundLabel = Nothing
		Me.chb_Minimized.MKEditValue_Compare = Nothing
		Me.chb_Minimized.Name = "chb_Minimized"
		Me.chb_Minimized.Properties.Caption = ""
		Me.chb_Minimized.Size = New System.Drawing.Size(75, 19)
		ToolTipTitleItem6.Text = "Run Minimized"
		ToolTipItem6.LeftIndent = 6
		ToolTipItem6.Text = "The command will be executed in a minimized mode."
		SuperToolTip6.Items.Add(ToolTipTitleItem6)
		SuperToolTip6.Items.Add(ToolTipItem6)
		Me.chb_Minimized.SuperTip = SuperToolTip6
		Me.chb_Minimized.TabIndex = 19
		'
		'lbl_Minimized
		'
		Me.lbl_Minimized.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Minimized.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Minimized.Location = New System.Drawing.Point(3, 97)
		Me.lbl_Minimized.MKBoundControl1 = Nothing
		Me.lbl_Minimized.MKBoundControl2 = Nothing
		Me.lbl_Minimized.MKBoundControl3 = Nothing
		Me.lbl_Minimized.MKBoundControl4 = Nothing
		Me.lbl_Minimized.MKBoundControl5 = Nothing
		Me.lbl_Minimized.Name = "lbl_Minimized"
		Me.lbl_Minimized.Size = New System.Drawing.Size(112, 20)
		ToolTipTitleItem7.Text = "Run Minimized"
		ToolTipItem7.LeftIndent = 6
		ToolTipItem7.Text = "The command will be executed in a minimized mode."
		SuperToolTip7.Items.Add(ToolTipTitleItem7)
		SuperToolTip7.Items.Add(ToolTipItem7)
		Me.lbl_Minimized.SuperTip = SuperToolTip7
		Me.lbl_Minimized.TabIndex = 18
		Me.lbl_Minimized.Text = "Run Minimized:"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(348, 115)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(86, 20)
		Me.btn_Cancel.TabIndex = 21
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(259, 115)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(86, 20)
		Me.btn_OK.TabIndex = 20
		Me.btn_OK.Text = "&OK"
		'
		'frm_Emulators_Pre_Post_Launch_Command_Edit
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(437, 138)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.chb_Minimized)
		Me.Controls.Add(Me.lbl_Minimized)
		Me.Controls.Add(Me.chb_WaitForExit)
		Me.Controls.Add(Me.lbl_WaitForExit)
		Me.Controls.Add(Me.lbl_StartupParameter)
		Me.Controls.Add(Me.lbl_Executable)
		Me.Controls.Add(Me.txb_Directory)
		Me.Controls.Add(Me.lbl_Directory)
		Me.Controls.Add(Me.txb_Executable)
		Me.Controls.Add(Me.btn_EmulatorFileOpen)
		Me.Controls.Add(Me.txb_StartupParameter)
		Me.Name = "frm_Emulators_Pre_Post_Launch_Command_Edit"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit Pre/Post Launch Command"
		CType(Me.txb_Directory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Executable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_StartupParameter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_WaitForExit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_Minimized.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents lbl_StartupParameter As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Executable As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Directory As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Directory As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Executable As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_EmulatorFileOpen As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents txb_StartupParameter As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_WaitForExit As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents chb_WaitForExit As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents chb_Minimized As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents lbl_Minimized As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
End Class
