Public Class frm_Screenshot_Edit
	Private _img As Image

	Private _Backgrounds As System.Drawing.Color() = {System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Pink}
	Private _Current_Background As Integer = 0

	Private _btn_Template_Plus As DevExpress.XtraEditors.Controls.EditorButton = Nothing
	Private _btn_Template_Minus As DevExpress.XtraEditors.Controls.EditorButton = Nothing
	Private _btn_Template_Edit As DevExpress.XtraEditors.Controls.EditorButton = Nothing

	Public ReadOnly Property CropImage As Image
		Get
			Return pic_Game.Image
		End Get
	End Property

	Public Sub New(ByRef img As Image)
		Me.InitializeComponent()

		Me._img = img
		Me.pic_Game.Image = _img

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_ImageEditorSettings(tran, Me.DS_ML.tbl_ImageEditorTemplates)
			tran.Commit()
		End Using

		For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Templates.Properties.Buttons
			Select Case btn.Kind
				Case DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
					_btn_Template_Plus = btn
				Case DevExpress.XtraEditors.Controls.ButtonPredefines.Minus
					_btn_Template_Minus = btn
				Case DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis
					_btn_Template_Edit = btn
			End Select
		Next

		If _btn_Template_Plus IsNot Nothing Then _btn_Template_Plus.Enabled = False
		If _btn_Template_Minus IsNot Nothing Then _btn_Template_Minus.Enabled = False
		If _btn_Template_Edit IsNot Nothing Then _btn_Template_Edit.Enabled = False
	End Sub

	Private Sub Handle_Spinedits_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spn_Right.EditValueChanged, spn_Left.EditValueChanged, spn_Bottom.EditValueChanged, spn_Top.EditValueChanged
		Dim rect_Crop As New Rectangle(spn_Left.Value, spn_Top.Value, _img.Width - spn_Left.Value - spn_Right.Value, _img.Height - spn_Top.Value - spn_Bottom.Value)
		Dim img_Crop As New Bitmap(rect_Crop.Width, rect_Crop.Height)
		Using gfx As System.Drawing.Graphics = Graphics.FromImage(img_Crop)
			gfx.DrawImage(_img, New Rectangle(0, 0, rect_Crop.Width, rect_Crop.Height), rect_Crop, GraphicsUnit.Pixel)
			Me.pic_Game.Image = img_Crop
		End Using

		If _btn_Template_Plus IsNot Nothing Then _btn_Template_Plus.Enabled = True

		If spn_Top.Value = 0 And spn_Bottom.Value = 0 And spn_Left.Value = 0 And spn_Right.Value = 0 Then
			If _btn_Template_Plus IsNot Nothing Then _btn_Template_Plus.Enabled = False

		ElseIf TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM tbl_ImageEditorTemplates WHERE Top = " & TC.getSQLFormat(spn_Top.Value) & " AND Bottom = " & TC.getSQLFormat(spn_Bottom.Value) & " AND Left = " & TC.getSQLFormat(spn_Left.Value) & " AND Right = " & TC.getSQLFormat(spn_Right.Value)), 0) > 0 Then
			If _btn_Template_Plus IsNot Nothing Then _btn_Template_Plus.Enabled = False
		End If
	End Sub

	Private Sub Handle_SpinEdits_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles spn_Top.EditValueChanging, spn_Right.EditValueChanging, spn_Left.EditValueChanging, spn_Bottom.EditValueChanging
		Select Case sender.name
			Case "spn_Top"
				If _img.Height - e.NewValue - spn_Bottom.Value <= 0 Then
					e.Cancel = True
					Return
				End If
			Case "spn_Bottom"
				If _img.Height - spn_Top.Value - e.NewValue <= 0 Then
					e.Cancel = True
					Return
				End If
			Case "spn_Left"
				If _img.Width - e.NewValue - spn_Right.Value <= 0 Then
					e.Cancel = True
					Return
				End If
			Case "spn_Right"
				If _img.Width - spn_Left.Value - e.NewValue <= 0 Then
					e.Cancel = True
					Return
				End If
		End Select
	End Sub

	Private Sub pic_Game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pic_Game.Click
		_Current_Background += 1
		If _Current_Background > _Backgrounds.Length - 1 Then _Current_Background = 0
		Me.pic_Game.BackColor = Me._Backgrounds(_Current_Background)
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub

	Private Sub cmb_Templates_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles cmb_Templates.EditValueChanging
		Dim rows() As DataRow = Me.DS_ML.tbl_ImageEditorTemplates.Select("id_ImageEditorTemplates = " & TC.getSQLFormat(e.NewValue))
		If rows.Length > 0 Then
			If _img.Width - rows(0)("Left") - rows(0)("Right") < 0 Then
				MKDXHelper.MessageBox("The template cannot be used because the horizontal cropping is too big", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				e.Cancel = True
				Return
			End If
			If _img.Width - rows(0)("Top") - rows(0)("Bottom") < 0 Then
				MKDXHelper.MessageBox("The tempate cannot be used because the vertical cropping is too big", "Cannot use template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				e.Cancel = True
				Return
			End If
		End If
	End Sub

	Private Sub BS_ImageEditorTemplates_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_ImageEditorTemplates.CurrentChanged
		If BS_ImageEditorTemplates.Current Is Nothing Then
			If _btn_Template_Edit IsNot Nothing Then _btn_Template_Edit.Enabled = False
			If _btn_Template_Minus IsNot Nothing Then _btn_Template_Minus.Enabled = False
			Return
		End If

		Me.spn_Top.Value = BS_ImageEditorTemplates.Current("Top")
		Me.spn_Bottom.Value = BS_ImageEditorTemplates.Current("Bottom")
		Me.spn_Left.Value = BS_ImageEditorTemplates.Current("Left")
		Me.spn_Right.Value = BS_ImageEditorTemplates.Current("Right")

		If TC.NZ(BS_ImageEditorTemplates.Current("id_ImageEditorTemplates"), 0) > 0 Then
			If _btn_Template_Edit IsNot Nothing Then _btn_Template_Edit.Enabled = True
			If _btn_Template_Minus IsNot Nothing Then _btn_Template_Minus.Enabled = True
		Else
			If _btn_Template_Edit IsNot Nothing Then _btn_Template_Edit.Enabled = False
			If _btn_Template_Minus IsNot Nothing Then _btn_Template_Minus.Enabled = False
		End If
	End Sub

	Private Sub cmb_Templates_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Templates.ButtonClick
		Select Case e.Button.Kind
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
				'Add Template
				Using frm As New MKNetDXLib.frm_TextBoxEdit("Template Title:", "Please input a template title", "<new template>", False)
					If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
						Dim Title As String = frm.Input
						Dim id_ImageEditorTemplates As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "INSERT INTO tbl_ImageEditorTemplates (Title, Top, Bottom, Left, Right) VALUES (" & TC.getSQLParameter(Title, spn_Top.Value, spn_Bottom.Value, spn_Left.Value, spn_Right.Value) & "); SELECT last_insert_rowid()")
						Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
							DS_ML.Fill_tbl_ImageEditorSettings(tran, Me.DS_ML.tbl_ImageEditorTemplates)
							tran.Commit()
						End Using
						If TC.NZ(id_ImageEditorTemplates, 0) > 0 Then
							'BS_ImageEditorTemplates.Position = BS_ImageEditorTemplates.Find("id_ImageEditorTemplates", CLng(id_ImageEditorTemplates))
							cmb_Templates.EditValue = CLng(id_ImageEditorTemplates)
						End If
					End If
				End Using
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Minus
				'Delete Template
				If MKDXHelper.MessageBox("Do you really want to delete the selected template?", "Delete template", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
					DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_ImageEditorTemplates WHERE id_ImageEditorTemplates = " & TC.getSQLFormat(BS_ImageEditorTemplates.Current("id_ImageEditorTemplates")))
					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						DS_ML.Fill_tbl_ImageEditorSettings(tran, Me.DS_ML.tbl_ImageEditorTemplates)
						tran.Commit()
					End Using
					'BS_ImageEditorTemplates.Position = BS_ImageEditorTemplates.Find("id_ImageEditorTemplates", CLng(0))
					cmb_Templates.EditValue = CLng(0)
				End If
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis
				'Edit Template
				Using frm As New MKNetDXLib.frm_TextBoxEdit("Edit Template Title:", "Please input a new template title", "<new template title>", False)
					If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
						Dim Title As String = frm.Input
						Dim id_ImageEditorTemplates As Object = BS_ImageEditorTemplates.Current("id_ImageEditorTemplates")
						DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_ImageEditorTemplates SET Title = " & TC.getSQLFormat(Title) & " WHERE id_ImageEditorTemplates = " & TC.getSQLFormat(id_ImageEditorTemplates))
						Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
							DS_ML.Fill_tbl_ImageEditorSettings(tran, Me.DS_ML.tbl_ImageEditorTemplates)
							tran.Commit()
						End Using
						If TC.NZ(id_ImageEditorTemplates, 0) > 0 Then
							'BS_ImageEditorTemplates.Position = BS_ImageEditorTemplates.Find("id_ImageEditorTemplates", CLng(id_ImageEditorTemplates))
							cmb_Templates.EditValue = CLng(id_ImageEditorTemplates)
						End If
					End If
				End Using
		End Select
	End Sub
End Class