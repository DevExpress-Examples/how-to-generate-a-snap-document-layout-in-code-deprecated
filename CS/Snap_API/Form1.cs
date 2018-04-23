#region #SystemUsings
using System;
using System.Data.OleDb;
using System.Windows.Forms;
#endregion #SystemUsings

#region #DevExpressData
using DevExpress.Data;
#endregion #DevExpressData

#region #SnapApi
using DevExpress.Snap.Core.API;
#endregion #SnapApi

#region #RichEditApi
using DevExpress.XtraRichEdit.API.Native;
#endregion #RichEditApi

using Snap_API.nwindDataSetTableAdapters;

#region #Etc
// ...
#endregion #Etc

namespace Snap_API {
    public partial class Form1 : Form {        
        public Form1() {
            InitializeComponent();
            snapControl1.DataSource = GetDataSource();
        }

        private BindingSource GetDataSource() {           
            nwindDataSet dataSource = new nwindDataSet();
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\nwind.mdb";

            ProductsTableAdapter products = new ProductsTableAdapter();
            products.Connection = connection;
            products.Fill(dataSource.Products);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataSource;
            bindingSource.DataMember = "Products";

            return bindingSource;
        }

#region #GenerateLayout
private void GenerateLayout(SnapDocument doc) {
    // Delete the document's content.
    doc.Text = String.Empty;
    // Add a Snap list to the document.
    SnapList list = doc.CreateSnList(doc.Range.End, @"List");
    list.BeginUpdate();
    list.EditorRowLimit = 100500;                                                                                 
                                                                                                             
    // Add a header to the Snap list.                                                                   
    SnapDocument listHeader = list.ListHeader;                                        
    Table listHeaderTable = listHeader.Tables.Create(listHeader.Range.End, 1, 3);                                         
    TableCellCollection listHeaderCells = listHeaderTable.FirstRow.Cells;                                   
    listHeader.InsertText(listHeaderCells[0].ContentRange.End, "Product Name");                             
    listHeader.InsertText(listHeaderCells[1].ContentRange.End, "Units in Stock");
    listHeader.InsertText(listHeaderCells[2].ContentRange.End, "Unit Price");

    // Customize the row template.
    SnapDocument listRow = list.RowTemplate;
    Table listRowTable = listRow.Tables.Create(listRow.Range.End, 1, 3);
    TableCellCollection listRowCells = listRowTable.FirstRow.Cells;
    listRow.CreateSnText(listRowCells[0].ContentRange.End, @"ProductName");
    listRow.CreateSnText(listRowCells[1].ContentRange.End, @"UnitsInStock");
    listRow.CreateSnText(listRowCells[2].ContentRange.End, @"UnitPrice \$ $0.00");
    
    // Apply formatting, filtering and sorting to the Snap list. 
    FormatList(list);
    FilterList(list);
    SortList(list);
    GroupList(list);

    list.EndUpdate();
    list.Field.Update();
}
#endregion #GenerateLayout

#region #FormatText
private void FormatList(SnapList list) {                     
    // Customize the list header.
    SnapDocument header = list.ListHeader;
    Table headerTable = header.Tables[0];
    headerTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent);
            
    foreach(TableRow row in headerTable.Rows) {
        foreach(TableCell cell in row.Cells) {
            // Apply cell formatting.
            cell.Borders.Left.LineColor = System.Drawing.Color.White;
            cell.Borders.Right.LineColor = System.Drawing.Color.White;
            cell.Borders.Top.LineColor = System.Drawing.Color.White;
            cell.Borders.Bottom.LineColor = System.Drawing.Color.White;
            cell.BackgroundColor = System.Drawing.Color.SteelBlue;
                    
            // Apply text formatting.
            CharacterProperties formatting = header.BeginUpdateCharacters(cell.ContentRange);
            formatting.Bold = true;
            formatting.ForeColor = System.Drawing.Color.White;
            header.EndUpdateCharacters(formatting);
        }
    }
                        
    // Customize the row template.
    SnapDocument rowTemplate = list.RowTemplate;
    Table rowTable = rowTemplate.Tables[0];
    rowTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent);
    foreach(TableRow row in rowTable.Rows) {
        foreach(TableCell cell in row.Cells) {
            cell.Borders.Left.LineColor = System.Drawing.Color.Transparent;
            cell.Borders.Right.LineColor = System.Drawing.Color.Transparent;
            cell.Borders.Top.LineColor = System.Drawing.Color.Transparent;
            cell.Borders.Bottom.LineColor = System.Drawing.Color.LightGray;
        }
    }
}
#endregion #FormatText

#region #FilterList
private void FilterList(SnapList list) {
    const string filter = "[Discontinued] = False";
    if(!list.Filters.Contains(filter)) {
        list.Filters.Add(filter);
    }          
}
#endregion #FilterList

#region #SortList
private void SortList(SnapList list) {
    list.Sorting.Add(new SnapListGroupParam("UnitPrice", ColumnSortOrder.Descending));
}
#endregion #SortList

#region #GroupList
private void GroupList(SnapList list) {
    // Add grouping to the Snap list.
    SnapListGroupInfo group = list.Groups.CreateSnapListGroupInfo(
        new SnapListGroupParam("CategoryID", ColumnSortOrder.Ascending));
    list.Groups.Add(group);

    // Add a group header.
    SnapDocument groupHeader = group.CreateHeader();
    Table headerTable = groupHeader.Tables.Create(groupHeader.Range.End, 1, 1);
    headerTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent);
    TableCellCollection groupHeaderCells = headerTable.FirstRow.Cells;
    groupHeader.InsertText(groupHeaderCells[0].ContentRange.End, "Category ID: ");
    groupHeader.CreateSnText(groupHeaderCells[0].ContentRange.End, "CategoryID");

    // Customize the group header formatting.
    groupHeaderCells[0].BackgroundColor = System.Drawing.Color.LightGray;
    groupHeaderCells[0].Borders.Bottom.LineColor = System.Drawing.Color.White;
    groupHeaderCells[0].Borders.Left.LineColor = System.Drawing.Color.White;
    groupHeaderCells[0].Borders.Right.LineColor = System.Drawing.Color.White;
    groupHeaderCells[0].Borders.Top.LineColor = System.Drawing.Color.White;

    // Add a group footer.
    SnapDocument groupFooter = group.CreateFooter();
    Table footerTable = groupFooter.Tables.Create(groupFooter.Range.End, 1, 1);
    footerTable.SetPreferredWidth(50 * 100, WidthType.FiftiethsOfPercent);
    TableCellCollection groupFooterCells = footerTable.FirstRow.Cells;
    groupFooter.InsertText(groupFooterCells[0].ContentRange.End, "Count = ");
    groupFooter.CreateSnText(groupFooterCells[0].ContentRange.End, 
        @"CategoryID \sr Group \sf Count");

    // Customize the group footer formatting.
    groupFooterCells[0].BackgroundColor = System.Drawing.Color.LightGray;
    groupFooterCells[0].Borders.Bottom.LineColor = System.Drawing.Color.White;
    groupFooterCells[0].Borders.Left.LineColor = System.Drawing.Color.White;
    groupFooterCells[0].Borders.Right.LineColor = System.Drawing.Color.White;
    groupFooterCells[0].Borders.Top.LineColor = System.Drawing.Color.White;            
}
#endregion #GroupList
        
        private void Form1_Load(object sender, EventArgs e) {
            GenerateLayout(snapControl1.Document);            
        }
    }
}
