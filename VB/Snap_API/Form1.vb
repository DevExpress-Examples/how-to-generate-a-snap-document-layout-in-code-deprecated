Imports Microsoft.VisualBasic
#Region "#SystemUsings"
Imports System
Imports System.Data.OleDb
Imports System.Windows.Forms
#End Region ' #SystemUsings

#Region "#DevExpressData"
Imports DevExpress.Data
#End Region ' #DevExpressData

#Region "#SnapApi"
Imports DevExpress.Snap.Core.API
#End Region ' #SnapApi

#Region "#RichEditApi"
Imports DevExpress.XtraRichEdit.API.Native
#End Region ' #RichEditApi

Imports Snap_API.nwindDataSetTableAdapters

#Region "#Etc"
' ...
#End Region ' #Etc

Namespace Snap_API
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
			snapControl1.DataSource = GetDataSource()
		End Sub

		Private Function GetDataSource() As BindingSource
			Dim dataSource As New nwindDataSet()
			Dim connection As New OleDbConnection()
			connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\nwind.mdb"

			Dim products As New ProductsTableAdapter()
			products.Connection = connection
			products.Fill(dataSource.Products)

			Dim bindingSource As New BindingSource()
			bindingSource.DataSource = dataSource
			bindingSource.DataMember = "Products"

			Return bindingSource
		End Function

#Region "#GenerateLayout"
Private Sub GenerateLayout(ByVal doc As SnapDocument)
	' Delete the document's content.
	doc.Text = String.Empty
	' Add a Snap list to the document.
	Dim list As SnapList = doc.CreateSnList(doc.Range.End, "List")
	list.BeginUpdate()
	list.EditorRowLimit = 100500

	' Add a header to the Snap list.                                                                   
	Dim listHeader As SnapDocument = list.ListHeader
	Dim listHeaderTable As Table = listHeader.InsertTable(listHeader.Range.End, 1, 3)
	Dim listHeaderCells As TableCellCollection = listHeaderTable.FirstRow.Cells
	listHeader.InsertText(listHeaderCells(0).ContentRange.End, "Product Name")
	listHeader.InsertText(listHeaderCells(1).ContentRange.End, "Units in Stock")
	listHeader.InsertText(listHeaderCells(2).ContentRange.End, "Unit Price")

	' Customize the row template.
	Dim listRow As SnapDocument = list.RowTemplate
	Dim listRowTable As Table = listRow.InsertTable(listRow.Range.End, 1, 3)
	Dim listRowCells As TableCellCollection = listRowTable.FirstRow.Cells
	listRow.CreateSnText(listRowCells(0).ContentRange.End, "ProductName")
	listRow.CreateSnText(listRowCells(1).ContentRange.End, "UnitsInStock")
	listRow.CreateSnText(listRowCells(2).ContentRange.End, "UnitPrice \$ $0.00")

	' Apply formatting, filtering and sorting to the Snap list. 
	FormatList(list)
	FilterList(list)
	SortList(list)
	GroupList(list)

	list.EndUpdate()
	list.Field.Update()
End Sub
#End Region ' #GenerateLayout

#Region "#FormatText"
Private Sub FormatList(ByVal list As SnapList)
	' Customize the list header.
	Dim header As SnapDocument = list.ListHeader
	Dim headerTable As Table = header.Tables(0)
	headerTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent)

	For Each row As TableRow In headerTable.Rows
		For Each cell As TableCell In row.Cells
			' Apply cell formatting.
			cell.Borders.Left.LineColor = System.Drawing.Color.White
			cell.Borders.Right.LineColor = System.Drawing.Color.White
			cell.Borders.Top.LineColor = System.Drawing.Color.White
			cell.Borders.Bottom.LineColor = System.Drawing.Color.White
			cell.BackgroundColor = System.Drawing.Color.SteelBlue

			' Apply text formatting.
			Dim formatting As CharacterProperties = header.BeginUpdateCharacters(cell.ContentRange)
			formatting.Bold = True
			formatting.ForeColor = System.Drawing.Color.White
			header.EndUpdateCharacters(formatting)
		Next cell
	Next row

	' Customize the row template.
	Dim rowTemplate As SnapDocument = list.RowTemplate
	Dim rowTable As Table = rowTemplate.Tables(0)
	rowTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent)
	For Each row As TableRow In rowTable.Rows
		For Each cell As TableCell In row.Cells
			cell.Borders.Left.LineColor = System.Drawing.Color.Transparent
			cell.Borders.Right.LineColor = System.Drawing.Color.Transparent
			cell.Borders.Top.LineColor = System.Drawing.Color.Transparent
			cell.Borders.Bottom.LineColor = System.Drawing.Color.LightGray
		Next cell
	Next row
End Sub
#End Region ' #FormatText

#Region "#FilterList"
Private Sub FilterList(ByVal list As SnapList)
	Const filter As String = "[Discontinued] = False"
	If (Not list.Filters.Contains(filter)) Then
		list.Filters.Add(filter)
	End If
End Sub
#End Region ' #FilterList

#Region "#SortList"
Private Sub SortList(ByVal list As SnapList)
	list.Sorting.Add(New SnapListGroupParam("UnitPrice", ColumnSortOrder.Descending))
End Sub
#End Region ' #SortList

#Region "#GroupList"
Private Sub GroupList(ByVal list As SnapList)
	' Add grouping to the Snap list.
	Dim group As SnapListGroupInfo = list.Groups.CreateSnapListGroupInfo(New SnapListGroupParam("CategoryID", ColumnSortOrder.Ascending))
	list.Groups.Add(group)

	' Add a group header.
	Dim groupHeader As SnapDocument = group.CreateHeader()
	Dim headerTable As Table = groupHeader.InsertTable(groupHeader.Range.End, 1, 1)
	headerTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent)
	Dim groupHeaderCells As TableCellCollection = headerTable.FirstRow.Cells
	groupHeader.InsertText(groupHeaderCells(0).ContentRange.End, "Category ID: ")
	groupHeader.CreateSnText(groupHeaderCells(0).ContentRange.End, "CategoryID")

	' Customize the group header formatting.
	groupHeaderCells(0).BackgroundColor = System.Drawing.Color.LightGray
	groupHeaderCells(0).Borders.Bottom.LineColor = System.Drawing.Color.White
	groupHeaderCells(0).Borders.Left.LineColor = System.Drawing.Color.White
	groupHeaderCells(0).Borders.Right.LineColor = System.Drawing.Color.White
	groupHeaderCells(0).Borders.Top.LineColor = System.Drawing.Color.White

	' Add a group footer.
	Dim groupFooter As SnapDocument = group.CreateFooter()
	Dim footerTable As Table = groupFooter.InsertTable(groupFooter.Range.End, 1, 1)
	footerTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent)
	Dim groupFooterCells As TableCellCollection = footerTable.FirstRow.Cells
	groupFooter.InsertText(groupFooterCells(0).ContentRange.End, "Count = ")
	groupFooter.CreateSnText(groupFooterCells(0).ContentRange.End, "CategoryID \sr Group \sf Count")

	' Customize the group footer formatting.
	groupFooterCells(0).BackgroundColor = System.Drawing.Color.LightGray
	groupFooterCells(0).Borders.Bottom.LineColor = System.Drawing.Color.White
	groupFooterCells(0).Borders.Left.LineColor = System.Drawing.Color.White
	groupFooterCells(0).Borders.Right.LineColor = System.Drawing.Color.White
	groupFooterCells(0).Borders.Top.LineColor = System.Drawing.Color.White
End Sub
#End Region ' #GroupList

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			GenerateLayout(snapControl1.Document)
		End Sub
	End Class
End Namespace
