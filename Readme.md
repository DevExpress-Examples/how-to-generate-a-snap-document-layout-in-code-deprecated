<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128608659/16.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4781)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Snap_API/Form1.cs) (VB: [Form1.vb](./VB/Snap_API/Form1.vb))
* [Program.cs](./CS/Snap_API/Program.cs) (VB: [Program.vb](./VB/Snap_API/Program.vb))
<!-- default file list end -->
# How to generate a document layout in code via the Snap application programming interface (API)

> **Note**
>
> As you may already know, the [WinForms Snap control](https://docs.devexpress.com/WindowsForms/11373/controls-and-libraries/snap) and [Snap Report API](https://docs.devexpress.com/OfficeFileAPI/15188/snap-report-api) are now in maintenance support mode. No new features or capabilities are incorporated into these products. We recommend that you use [DevExpress Reporting](https://docs.devexpress.com/XtraReports/2162/reporting) tool to generate, edit, print, and export your business reports/documents.

This example illustrates the [Snap API](https://docs.devexpress.com/WindowsForms/11373/controls-and-libraries/snap?v=21.2) that is used to generate a document from scratch and connect it to data completely in code. The following code generates a tabular report layout. For a sample code that creates a mail-merge report (in the context of [SnapDocumentServer](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Snap.SnapDocumentServer?v=21.2)), refer to the following example: [How to automatically create mail-merge documents using the Snap Document Server](https://github.com/DevExpress-Examples/how-to-automatically-create-mail-merge-documents-using-the-snap-report-api-e5078). [SnapControl](https://docs.devexpress.com/WindowsForms/DevExpress.Snap.SnapControl?v=21.2) extends the [RichEditControl](https://docs.devexpress.com/WindowsForms/DevExpress.XtraRichEdit.RichEditControl?v=21.2)'s API and introduces the [SnapList](https://docs.devexpress.com/WindowsForms/DevExpress.Snap.Core.API.SnapList?v=21.2) class that is used to insert dynamic data elements into a document. To generate a data-aware Snap document, do the following: 

- Create a Snap application and [connect it to a data source](https://docs.devexpress.com/WindowsForms/16043/controls-and-libraries/snap/data-acquisition?v=21.2). 
- Add a **SnapList** to the [SnapDocument](https://docs.devexpress.com/WindowsForms/DevExpress.Snap.Core.API.SnapDocument?v=21.2). 
- To generate the **SnapList** layout, define the **ListHeader**, **ListFooter**, and [RowTemplate](https://docs.devexpress.com/WindowsForms/DevExpress.Snap.Core.API.SnapList.RowTemplate?v=21.2). 
- Inject [data fields](https://docs.devexpress.com/WindowsForms/7874/winforms-controls?v=21.2) into the document (e.g., by using **SnapText** to display text data). - Format the document content (see the **FormatList** method implementation in the **Form1.cs** file of the sample solution). 
- If required, apply grouping, sorting, and/or filtering to the **SnapList** content. A [SnapEntity](https://docs.devexpress.com/WindowsForms/DevExpress.Snap.Core.API.SnapEntity?v=21.2) is open to customization only after calling its **BeginUpdate** method and not after the **EndUpdate** method is called to apply the new settings (see the **GenerateLayout** method in the **Form1.cs** file of the sample solution).
