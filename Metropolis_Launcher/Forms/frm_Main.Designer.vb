<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Main
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim TileItemElement1 As DevExpress.XtraEditors.TileItemElement = New DevExpress.XtraEditors.TileItemElement()
		Dim TileItemElement2 As DevExpress.XtraEditors.TileItemElement = New DevExpress.XtraEditors.TileItemElement()
		Dim TileItemElement3 As DevExpress.XtraEditors.TileItemElement = New DevExpress.XtraEditors.TileItemElement()
		Dim TileItemElement4 As DevExpress.XtraEditors.TileItemElement = New DevExpress.XtraEditors.TileItemElement()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Main))
		Me.DocMngr = New MKNetDXLib.cmp_MKDXDocumentManager()
		Me.MetroUIView = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.WindowsUIView(Me.components)
		Me.tilecontainer_Main = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.TileContainer(Me.components)
		Me.tile_Apps = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile(Me.components)
		Me.doc_Apps = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document(Me.components)
		Me.tile_Emulation = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile(Me.components)
		Me.doc_Emulation = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document(Me.components)
		Me.tile_Cinema = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile(Me.components)
		Me.doc_Cinema = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document(Me.components)
		Me.tile_Settings = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile(Me.components)
		Me.doc_Settings = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document(Me.components)
		Me.DS_MLApps = New Metropolis_Launcher.DS_MLApps()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.Bar3 = New DevExpress.XtraBars.Bar()
		Me.bbi_DownloadManager = New DevExpress.XtraBars.BarButtonItem()
		Me.bsi_DownloadManager = New DevExpress.XtraBars.BarStaticItem()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.BarHeaderItem1 = New DevExpress.XtraBars.BarHeaderItem()
		CType(Me.DocMngr, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.MetroUIView, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tilecontainer_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tile_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.doc_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tile_Emulation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.doc_Emulation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tile_Cinema, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.doc_Cinema, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tile_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.doc_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MLApps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'DocMngr
		'
		Me.DocMngr.ContainerControl = Me
		Me.DocMngr.ShowThumbnailsInTaskBar = DevExpress.Utils.DefaultBoolean.[False]
		Me.DocMngr.View = Me.MetroUIView
		Me.DocMngr.ViewCollection.AddRange(New DevExpress.XtraBars.Docking2010.Views.BaseView() {Me.MetroUIView})
		'
		'MetroUIView
		'
		Me.MetroUIView.Caption = "Metropolis Launcher"
		Me.MetroUIView.ContentContainers.AddRange(New DevExpress.XtraBars.Docking2010.Views.WindowsUI.IContentContainer() {Me.tilecontainer_Main})
		Me.MetroUIView.Documents.AddRange(New DevExpress.XtraBars.Docking2010.Views.BaseDocument() {Me.doc_Settings, Me.doc_Apps, Me.doc_Emulation, Me.doc_Cinema})
		Me.MetroUIView.SearchPanelProperties.Enabled = False
		Me.MetroUIView.Tiles.AddRange(New DevExpress.XtraBars.Docking2010.Views.WindowsUI.BaseTile() {Me.tile_Settings, Me.tile_Apps, Me.tile_Emulation, Me.tile_Cinema})
		Me.MetroUIView.UseSplashScreen = DevExpress.Utils.DefaultBoolean.[False]
		'
		'tilecontainer_Main
		'
		Me.tilecontainer_Main.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.tilecontainer_Main.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.tilecontainer_Main.AppearanceItem.Normal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.tilecontainer_Main.AppearanceItem.Normal.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tilecontainer_Main.AppearanceItem.Normal.Options.UseBackColor = True
		Me.tilecontainer_Main.AppearanceItem.Normal.Options.UseBorderColor = True
		Me.tilecontainer_Main.AppearanceItem.Normal.Options.UseFont = True
		Me.tilecontainer_Main.Items.AddRange(New DevExpress.XtraBars.Docking2010.Views.WindowsUI.BaseTile() {Me.tile_Apps, Me.tile_Emulation, Me.tile_Cinema, Me.tile_Settings})
		Me.tilecontainer_Main.Name = "tilecontainer_Main"
		Me.tilecontainer_Main.Properties.ItemSize = 100
		Me.tilecontainer_Main.Subtitle = "initializing..."
		'
		'tile_Apps
		'
		Me.tile_Apps.Appearances.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.tile_Apps.Appearances.Normal.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.tile_Apps.Appearances.Normal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.tile_Apps.Appearances.Normal.Options.UseBackColor = True
		Me.tile_Apps.Appearances.Normal.Options.UseBorderColor = True
		Me.tile_Apps.Document = Me.doc_Apps
		TileItemElement1.Text = "Apps"
		Me.tile_Apps.Elements.Add(TileItemElement1)
		Me.tile_Apps.Enabled = False
		Me.tile_Apps.Name = "tile_Apps"
		'
		'doc_Apps
		'
		Me.doc_Apps.Caption = "Applications"
		Me.doc_Apps.ControlName = ""
		'
		'tile_Emulation
		'
		Me.tile_Emulation.Appearances.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(145, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.tile_Emulation.Appearances.Normal.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(145, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.tile_Emulation.Appearances.Normal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(145, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.tile_Emulation.Appearances.Normal.Options.UseBackColor = True
		Me.tile_Emulation.Appearances.Normal.Options.UseBorderColor = True
		Me.tile_Emulation.Document = Me.doc_Emulation
		TileItemElement2.Text = "Games & Emulation"
		Me.tile_Emulation.Elements.Add(TileItemElement2)
		Me.tile_Emulation.Enabled = False
		Me.tilecontainer_Main.SetID(Me.tile_Emulation, 1)
		Me.tile_Emulation.Name = "tile_Emulation"
		'
		'doc_Emulation
		'
		Me.doc_Emulation.Caption = "Games & Emulation"
		Me.doc_Emulation.ControlName = ""
		'
		'tile_Cinema
		'
		Me.tile_Cinema.Appearances.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.tile_Cinema.Appearances.Normal.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.tile_Cinema.Appearances.Normal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.tile_Cinema.Appearances.Normal.Options.UseBackColor = True
		Me.tile_Cinema.Appearances.Normal.Options.UseBorderColor = True
		Me.tile_Cinema.Document = Me.doc_Cinema
		TileItemElement3.Text = "Cinema"
		Me.tile_Cinema.Elements.Add(TileItemElement3)
		Me.tilecontainer_Main.SetID(Me.tile_Cinema, 2)
		Me.tile_Cinema.Name = "tile_Cinema"
		Me.tile_Cinema.Visible = False
		'
		'doc_Cinema
		'
		Me.doc_Cinema.Caption = "Cinema & TV"
		Me.doc_Cinema.ControlName = ""
		'
		'tile_Settings
		'
		Me.tile_Settings.Appearances.Normal.BackColor = System.Drawing.Color.DimGray
		Me.tile_Settings.Appearances.Normal.BackColor2 = System.Drawing.Color.DimGray
		Me.tile_Settings.Appearances.Normal.BorderColor = System.Drawing.Color.DimGray
		Me.tile_Settings.Appearances.Normal.Options.UseBackColor = True
		Me.tile_Settings.Appearances.Normal.Options.UseBorderColor = True
		Me.tile_Settings.Document = Me.doc_Settings
		TileItemElement4.Text = "Settings"
		Me.tile_Settings.Elements.Add(TileItemElement4)
		Me.tile_Settings.Enabled = False
		Me.tilecontainer_Main.SetID(Me.tile_Settings, 3)
		Me.tile_Settings.Name = "tile_Settings"
		'
		'doc_Settings
		'
		Me.doc_Settings.Caption = "Settings"
		Me.doc_Settings.ControlName = ""
		'
		'DS_MLApps
		'
		Me.DS_MLApps.DataSetName = "DS_MLApps"
		Me.DS_MLApps.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'barmng
		'
		Me.barmng.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar3})
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarHeaderItem1, Me.bsi_DownloadManager, Me.bbi_DownloadManager})
		Me.barmng.MaxItemId = 3
		Me.barmng.StatusBar = Me.Bar3
		'
		'Bar3
		'
		Me.Bar3.BarName = "Statusleiste"
		Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
		Me.Bar3.DockCol = 0
		Me.Bar3.DockRow = 0
		Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
		Me.Bar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_DownloadManager, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.bsi_DownloadManager, DevExpress.XtraBars.BarItemPaintStyle.Standard)})
		Me.Bar3.OptionsBar.AllowQuickCustomization = False
		Me.Bar3.OptionsBar.DrawDragBorder = False
		Me.Bar3.OptionsBar.UseWholeRow = True
		Me.Bar3.Text = "Statusleiste"
		Me.Bar3.Visible = False
		'
		'bbi_DownloadManager
		'
		Me.bbi_DownloadManager.Caption = "Download Manager:"
		Me.bbi_DownloadManager.Id = 2
		Me.bbi_DownloadManager.Name = "bbi_DownloadManager"
		'
		'bsi_DownloadManager
		'
		Me.bsi_DownloadManager.Caption = "idle"
		Me.bsi_DownloadManager.Id = 1
		Me.bsi_DownloadManager.Name = "bsi_DownloadManager"
		Me.bsi_DownloadManager.TextAlignment = System.Drawing.StringAlignment.Near
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(784, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 537)
		Me.barDockControlBottom.Size = New System.Drawing.Size(784, 25)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 537)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(784, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 537)
		'
		'BarHeaderItem1
		'
		Me.BarHeaderItem1.Caption = "idle..."
		Me.BarHeaderItem1.Id = 0
		Me.BarHeaderItem1.Name = "BarHeaderItem1"
		'
		'frm_Main
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(784, 562)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "frm_Main"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Metropolis Launcher"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		CType(Me.DocMngr, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.MetroUIView, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tilecontainer_Main, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tile_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.doc_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tile_Emulation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.doc_Emulation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tile_Cinema, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.doc_Cinema, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tile_Settings, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.doc_Settings, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MLApps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents DocMngr As MKNetDXLib.cmp_MKDXDocumentManager
	Friend WithEvents MetroUIView As DevExpress.XtraBars.Docking2010.Views.WindowsUI.WindowsUIView
	Friend WithEvents tilecontainer_Main As DevExpress.XtraBars.Docking2010.Views.WindowsUI.TileContainer
	Friend WithEvents tile_Apps As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile
	Friend WithEvents doc_Apps As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document
	Friend WithEvents tile_Emulation As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile
	Friend WithEvents doc_Emulation As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document
	Friend WithEvents tile_Settings As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile
	Friend WithEvents doc_Settings As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document
	Friend WithEvents tile_Cinema As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Tile
	Friend WithEvents doc_Cinema As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document
	Friend WithEvents DS_MLApps As Metropolis_Launcher.DS_MLApps
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
	Friend WithEvents BarHeaderItem1 As DevExpress.XtraBars.BarHeaderItem
	Friend WithEvents bsi_DownloadManager As DevExpress.XtraBars.BarStaticItem
	Friend WithEvents bbi_DownloadManager As DevExpress.XtraBars.BarButtonItem
End Class
