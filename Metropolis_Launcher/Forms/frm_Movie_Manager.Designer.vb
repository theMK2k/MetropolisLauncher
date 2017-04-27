<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Movie_Manager
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
		Me.components = New System.ComponentModel.Container()
		Me.txb_IMDBSearchText = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_IMDBSearchText = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.DS_IMDB = New Metropolis_Launcher.DS_IMDB()
		Me.Ctl_MKDataGridView1 = New MKNetLib.ctl_MKDataGridView(Me.components)
		Me.IdMoviesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.IdCategoriesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.IMDBidDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.TitleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.YearDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.RatingDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.RatingUsersDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.MetaScoreDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.LengthDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.BS_Movies = New System.Windows.Forms.BindingSource(Me.components)
		CType(Me.txb_IMDBSearchText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_IMDB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Ctl_MKDataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Movies, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'txb_IMDBSearchText
		'
		Me.txb_IMDBSearchText.Location = New System.Drawing.Point(63, 66)
		Me.txb_IMDBSearchText.MKBoundLabel = Nothing
		Me.txb_IMDBSearchText.MKEditValue_Compare = Nothing
		Me.txb_IMDBSearchText.Name = "txb_IMDBSearchText"
		Me.txb_IMDBSearchText.Size = New System.Drawing.Size(414, 20)
		Me.txb_IMDBSearchText.TabIndex = 0
		'
		'btn_IMDBSearchText
		'
		Me.btn_IMDBSearchText.Location = New System.Drawing.Point(537, 63)
		Me.btn_IMDBSearchText.Name = "btn_IMDBSearchText"
		Me.btn_IMDBSearchText.Size = New System.Drawing.Size(97, 23)
		Me.btn_IMDBSearchText.TabIndex = 1
		Me.btn_IMDBSearchText.Text = "Search"
		'
		'DS_IMDB
		'
		Me.DS_IMDB.DataSetName = "DS_IMDB"
		Me.DS_IMDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'Ctl_MKDataGridView1
		'
		Me.Ctl_MKDataGridView1.AutoGenerateColumns = False
		Me.Ctl_MKDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Ctl_MKDataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdMoviesDataGridViewTextBoxColumn, Me.IdCategoriesDataGridViewTextBoxColumn, Me.IMDBidDataGridViewTextBoxColumn, Me.TitleDataGridViewTextBoxColumn, Me.YearDataGridViewTextBoxColumn, Me.RatingDataGridViewTextBoxColumn, Me.RatingUsersDataGridViewTextBoxColumn, Me.MetaScoreDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.LengthDataGridViewTextBoxColumn})
		Me.Ctl_MKDataGridView1.DataSource = Me.BS_Movies
		Me.Ctl_MKDataGridView1.Location = New System.Drawing.Point(63, 101)
		Me.Ctl_MKDataGridView1.Name = "Ctl_MKDataGridView1"
		Me.Ctl_MKDataGridView1.Size = New System.Drawing.Size(615, 208)
		Me.Ctl_MKDataGridView1.TabIndex = 2
		'
		'IdMoviesDataGridViewTextBoxColumn
		'
		Me.IdMoviesDataGridViewTextBoxColumn.DataPropertyName = "id_Movies"
		Me.IdMoviesDataGridViewTextBoxColumn.HeaderText = "id_Movies"
		Me.IdMoviesDataGridViewTextBoxColumn.Name = "IdMoviesDataGridViewTextBoxColumn"
		'
		'IdCategoriesDataGridViewTextBoxColumn
		'
		Me.IdCategoriesDataGridViewTextBoxColumn.DataPropertyName = "id_Categories"
		Me.IdCategoriesDataGridViewTextBoxColumn.HeaderText = "id_Categories"
		Me.IdCategoriesDataGridViewTextBoxColumn.Name = "IdCategoriesDataGridViewTextBoxColumn"
		'
		'IMDBidDataGridViewTextBoxColumn
		'
		Me.IMDBidDataGridViewTextBoxColumn.DataPropertyName = "IMDBid"
		Me.IMDBidDataGridViewTextBoxColumn.HeaderText = "IMDBid"
		Me.IMDBidDataGridViewTextBoxColumn.Name = "IMDBidDataGridViewTextBoxColumn"
		'
		'TitleDataGridViewTextBoxColumn
		'
		Me.TitleDataGridViewTextBoxColumn.DataPropertyName = "Title"
		Me.TitleDataGridViewTextBoxColumn.HeaderText = "Title"
		Me.TitleDataGridViewTextBoxColumn.Name = "TitleDataGridViewTextBoxColumn"
		'
		'YearDataGridViewTextBoxColumn
		'
		Me.YearDataGridViewTextBoxColumn.DataPropertyName = "Year"
		Me.YearDataGridViewTextBoxColumn.HeaderText = "Year"
		Me.YearDataGridViewTextBoxColumn.Name = "YearDataGridViewTextBoxColumn"
		'
		'RatingDataGridViewTextBoxColumn
		'
		Me.RatingDataGridViewTextBoxColumn.DataPropertyName = "Rating"
		Me.RatingDataGridViewTextBoxColumn.HeaderText = "Rating"
		Me.RatingDataGridViewTextBoxColumn.Name = "RatingDataGridViewTextBoxColumn"
		'
		'RatingUsersDataGridViewTextBoxColumn
		'
		Me.RatingUsersDataGridViewTextBoxColumn.DataPropertyName = "RatingUsers"
		Me.RatingUsersDataGridViewTextBoxColumn.HeaderText = "RatingUsers"
		Me.RatingUsersDataGridViewTextBoxColumn.Name = "RatingUsersDataGridViewTextBoxColumn"
		'
		'MetaScoreDataGridViewTextBoxColumn
		'
		Me.MetaScoreDataGridViewTextBoxColumn.DataPropertyName = "MetaScore"
		Me.MetaScoreDataGridViewTextBoxColumn.HeaderText = "MetaScore"
		Me.MetaScoreDataGridViewTextBoxColumn.Name = "MetaScoreDataGridViewTextBoxColumn"
		'
		'DescriptionDataGridViewTextBoxColumn
		'
		Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
		Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
		Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
		'
		'LengthDataGridViewTextBoxColumn
		'
		Me.LengthDataGridViewTextBoxColumn.DataPropertyName = "Length"
		Me.LengthDataGridViewTextBoxColumn.HeaderText = "Length"
		Me.LengthDataGridViewTextBoxColumn.Name = "LengthDataGridViewTextBoxColumn"
		'
		'BS_Movies
		'
		Me.BS_Movies.DataMember = "tbl_Movies"
		Me.BS_Movies.DataSource = Me.DS_IMDB
		'
		'frm_Movie_Manager
		'
		Me.ClientSize = New System.Drawing.Size(788, 355)
		Me.Controls.Add(Me.Ctl_MKDataGridView1)
		Me.Controls.Add(Me.btn_IMDBSearchText)
		Me.Controls.Add(Me.txb_IMDBSearchText)
		Me.Name = "frm_Movie_Manager"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.txb_IMDBSearchText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_IMDB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Ctl_MKDataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Movies, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents txb_IMDBSearchText As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_IMDBSearchText As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DS_IMDB As Metropolis_Launcher.DS_IMDB
	Friend WithEvents Ctl_MKDataGridView1 As MKNetLib.ctl_MKDataGridView
	Friend WithEvents IdMoviesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents IdCategoriesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents IMDBidDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents TitleDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents YearDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents RatingDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents RatingUsersDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents MetaScoreDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents LengthDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents BS_Movies As System.Windows.Forms.BindingSource

End Class
