<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Snap_API/Form1.cs) (VB: [Form1.vb](./VB/Snap_API/Form1.vb))
<!-- default file list end -->
# How to generate a document layout in code via the Snap application programming interface (API)


<p>This example illustrates the <a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument14525"><u>Snap API</u></a> that is used to generate a document from scratch and connect it to data completely in code.<br />
The following code generates a tabular report layout. For a sample code that creates a mail-merge report (in the context of <a href="https://documentation.devexpress.com/#DocumentServer/clsDevExpressSnapSnapDocumentServertopic"><u>SnapDocumentServer</u></a>), refer to the following example: <a href="https://www.devexpress.com/Support/Center/CodeCentral/ViewExample.aspx?exampleId=E5078"><u>How to automatically create mail-merge documents using the Snap Document Server</u></a>.</p><p><a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressSnapSnapControltopic"><u>SnapControl</u></a> extends the <a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraRichEditRichEditControltopic"><u>RichEditControl</u></a>'s API and introduces the <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressSnapCoreAPISnapListtopic"><u>SnapList</u></a> class that is used to insert dynamic data elements into a document.</p><p>To generate a data-aware Snap document, do the following:<br />
- Create a Snap application and <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument16043"><u>connect it to a data source</u></a>.<br />
- Add a <strong>SnapList</strong> to the <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressSnapCoreAPISnapDocumenttopic"><u>SnapDocument</u></a>.<br />
- To generate the <strong>SnapList</strong> layout, define the <strong>ListHeader</strong>, <strong>ListFooter</strong>, and <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressSnapCoreAPISnapList_RowTemplatetopic"><u>RowTemplate</u></a>. <br />
- Inject <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument15559"><u>data fields</u></a> into the document (e.g., by using <strong>SnapText</strong> to display text data).<br />
- Format the document content (see the <strong>FormatList</strong> method implementation in the <strong>Form1.cs</strong> file of the sample solution).<br />
- If required, apply grouping, sorting, and/or filtering to the <strong>SnapList</strong> content.</p><p>A <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressSnapCoreAPISnapEntitytopic"><u>SnapEntity</u></a> is open to customization only after calling its <strong>BeginUpdate</strong> method and not after the <strong>EndUpdate</strong> method is called to apply the new settings (see the <strong>GenerateLayout</strong> method in the <strong>Form1.cs </strong>file of the sample solution).</p>

<br/>


