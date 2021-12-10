using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using RichEditDocumentServerAPIExample.CodeUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using RichEditDocumentServerAPIExample.CodeExamples;
using System.ComponentModel;

namespace RichEditDocumentServerAPIExample
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        #region InitializeComponent
        #endregion

        ExampleCodeEditor codeEditor;
        List<CodeExampleGroup> examples;
        RichEditDocumentServer wordProcessor = new RichEditDocumentServer();
        GroupsOfRichEditExamples richEditExamples = new GroupsOfRichEditExamples();


        public Form1()
        {
            InitializeComponent();

            string examplePath = CodeExampleDemoUtils.GetExamplePath("CodeExamples");

            Dictionary<string, FileInfo> examplesCS = CodeExampleDemoUtils.GatherExamplesFromProject(examplePath, ExampleLanguage.Csharp);
            Dictionary<string, FileInfo> examplesVB = CodeExampleDemoUtils.GatherExamplesFromProject(examplePath, ExampleLanguage.VB);
            DisableTabs(examplesCS.Count, examplesVB.Count);
            this.richEditExamples = InitData();
            this.richEditExamples = CodeExampleDemoUtils.FindExamples(examplePath, examplesCS, examplesVB, richEditExamples);
            ShowExamplesInTreeList(treeList1, examples);
            this.codeEditor = new ExampleCodeEditor(richEditControlCS, richEditControlVB);
            CurrentExampleLanguage = CodeExampleDemoUtils.DetectExampleLanguage("RichEditDocumentServerAPIExample");
            
            InitTreeListControl();
            ShowFirstExample();
        }

        void InitTreeListControl()
        {
            DataBinding(richEditExamples);
        }
        public static GroupsOfRichEditExamples InitData()
        {
            GroupsOfRichEditExamples examples = new GroupsOfRichEditExamples();
            #region GroupNodes
            examples.Add(new RichEditNode("Basic Actions"));
            examples.Add(new RichEditNode("Bookmarks and Hyperlinks"));
            examples.Add(new RichEditNode("Comments Actions"));
            examples.Add(new RichEditNode("Custom Xml Actions"));
            examples.Add(new RichEditNode("Document Properties Actions"));
            examples.Add(new RichEditNode("Export Actions"));
            examples.Add(new RichEditNode("Field Actions"));
            examples.Add(new RichEditNode("Formatting Actions"));
            examples.Add(new RichEditNode("Form Fields Actions"));
            examples.Add(new RichEditNode("Header and Footer Actions"));
            examples.Add(new RichEditNode("Import Actions"));
            examples.Add(new RichEditNode("Inline Picture Actions"));
            examples.Add(new RichEditNode("Lists Actions"));
            examples.Add(new RichEditNode("Notes Actions"));
            examples.Add(new RichEditNode("Page Layout Actions"));
            examples.Add(new RichEditNode("Protection Actions"));
            examples.Add(new RichEditNode("Range Actions"));
            examples.Add(new RichEditNode("Shapes Actions"));
            examples.Add(new RichEditNode("Styles Actions"));
            examples.Add(new RichEditNode("Tables Actions"));
            examples.Add(new RichEditNode("Watermark Actions"));

            #endregion

            #region ExampleNodes
            //Add nodes to the "Basic Actions" group of examples.
            examples[0].Groups.Add(new RichEditExample("Create Document", string.Empty, string.Empty, BasicActions.CreateNewDocumentAction, true));
            examples[0].Groups.Add(new RichEditExample("Load Document", string.Empty, string.Empty, BasicActions.LoadDocumentAction, true));
            examples[0].Groups.Add(new RichEditExample("Merge Documents", string.Empty, string.Empty, BasicActions.MergeDocumentsAction, true));
            examples[0].Groups.Add(new RichEditExample("Split Document", string.Empty, string.Empty, BasicActions.SplitDocumentAction, false));
            examples[0].Groups.Add(new RichEditExample("Save Document", string.Empty, string.Empty, BasicActions.SaveDocumentAction, false));
            examples[0].Groups.Add(new RichEditExample("Print Document", string.Empty, string.Empty, BasicActions.PrintDocumentAction, false));

            //Add nodes to the "Bookmarks and Hyperlinks" group of examples.
            examples[1].Groups.Add(new RichEditExample("Insert Bookmark", string.Empty, string.Empty, BookmarksAndHyperlinksActions.InsertBookmarkAction, true));
            examples[1].Groups.Add(new RichEditExample("Insert Hyperlink", string.Empty, string.Empty, BookmarksAndHyperlinksActions.InsertHyperlinkAction, true));

            //Add nodes to the "Comments" group of examples.
            examples[2].Groups.Add(new RichEditExample("Create Comment", string.Empty, string.Empty, CommentsActions.CreateCommentAction, true));
            examples[2].Groups.Add(new RichEditExample("Create Nested Comment", string.Empty, string.Empty, CommentsActions.CreateNestedCommentAction, true));
            examples[2].Groups.Add(new RichEditExample("Delete Comment", string.Empty, string.Empty, CommentsActions.DeleteCommentAction, true));
            examples[2].Groups.Add(new RichEditExample("Edit Comment Properties", string.Empty, string.Empty, CommentsActions.EditCommentPropertiesAction, true));
            examples[2].Groups.Add(new RichEditExample("Edit Comment Content", string.Empty, string.Empty, CommentsActions.EditCommentContentAction, true));

            //Add nodes to the "Custom XML parts" group of examples.
            examples[3].Groups.Add(new RichEditExample("AddCustomXmlPart", string.Empty, string.Empty, CustomXmlActions.AddCustomXmlPartAction, true));
            examples[3].Groups.Add(new RichEditExample("AccessCustomXmlPart", string.Empty, string.Empty, CustomXmlActions.AccessCustomXmlPartAction, true));
            examples[3].Groups.Add(new RichEditExample("RemoveCustomXmlPart", string.Empty, string.Empty, CustomXmlActions.RemoveCustomXmlPartAction, true));


            //Add nodes to the "Document Properties" group of examples.
            examples[4].Groups.Add(new RichEditExample("Standard Document Properties", string.Empty, string.Empty, DocumentPropertiesActions.StandardDocumentPropertiesAction, true));
            examples[4].Groups.Add(new RichEditExample("Custom Document Properties", string.Empty, string.Empty, DocumentPropertiesActions.CustomDocumentPropertiesAction, true));

            //Add nodes to the "Export" group of examples.
            examples[5].Groups.Add(new RichEditExample("Save Image from Range", string.Empty, string.Empty, ExportActions.SaveImageFromRangeAction, false));
            examples[5].Groups.Add(new RichEditExample("Export Range to Html", string.Empty, string.Empty, ExportActions.ExportRangeToHtmlAction, false));
            examples[5].Groups.Add(new RichEditExample("Export Range to Plain Text", string.Empty, string.Empty, ExportActions.ExportRangeToPlainTextAction, false));
            examples[5].Groups.Add(new RichEditExample("Export to PDF", string.Empty, string.Empty, ExportActions.ExportToPDFAction, false));
            examples[5].Groups.Add(new RichEditExample("Convert HTML to PDF", string.Empty, string.Empty, ExportActions.ConvertHTMLtoPDFAction, false));
            examples[5].Groups.Add(new RichEditExample("Convert HTML to DOCX", string.Empty, string.Empty, ExportActions.ConvertHTMLtoDOCXAction, false));
            examples[5].Groups.Add(new RichEditExample("ExportToHTML", string.Empty, string.Empty, ExportActions.ExportToHTMLAction, false));
            examples[5].Groups.Add(new RichEditExample("BeforeExport", string.Empty, string.Empty, ExportActions.BeforeExportAction, false));

            //Add nodes to the "Fields" group of examples.
            examples[6].Groups.Add(new RichEditExample("Insert a Field", string.Empty, string.Empty, FieldActions.InsertFieldAction, true));
            examples[6].Groups.Add(new RichEditExample("Modify Field Code", string.Empty, string.Empty, FieldActions.ModifyFieldCodeAction, true));
            examples[6].Groups.Add(new RichEditExample("Create Field from Range", string.Empty, string.Empty, FieldActions.CreateFieldFromRangeAction, true));
            examples[6].Groups.Add(new RichEditExample("Show Field Codes", string.Empty, string.Empty, FieldActions.ShowFieldCodesAction, true)); //doesn't show field codes

            //Add nodes to the "Formatting" group of examples.
            examples[7].Groups.Add(new RichEditExample("Format Text", string.Empty, string.Empty, FormattingActions.FormatTextAction, true));
            examples[7].Groups.Add(new RichEditExample("Change Spacing", string.Empty, string.Empty, FormattingActions.ChangeSpacingAction, true));
            examples[7].Groups.Add(new RichEditExample("Reset Character Formatting", string.Empty, string.Empty, FormattingActions.ResetCharacterFormattingAction, true));
            examples[7].Groups.Add(new RichEditExample("Format Paragraph", string.Empty, string.Empty, FormattingActions.FormatParagraphAction, true));
            examples[7].Groups.Add(new RichEditExample("Reset Paragraph Formatting", string.Empty, string.Empty, FormattingActions.ResetParagraphFormattingAction, true));

            //Add nodes to the "Form Fields" group of examples.
            examples[8].Groups.Add(new RichEditExample("Insert a CheckBox", string.Empty, string.Empty, FormFieldsActions.InsertCheckBoxAction, false));

            //Add nodes to the "Headers and Footers" group of examples.
            examples[9].Groups.Add(new RichEditExample("Create a Header", string.Empty, string.Empty, HeadersAndFootersActions.CreateHeaderAction, false));
            examples[9].Groups.Add(new RichEditExample("Modify a Header", string.Empty, string.Empty, HeadersAndFootersActions.ModifyHeaderAction, false));

            //Add nodes to the "Import" group of examples.
            examples[10].Groups.Add(new RichEditExample("Import RTF Text", string.Empty, string.Empty, ImportActions.ImportRtfTextAction, false));
            examples[10].Groups.Add(new RichEditExample("Handle Before Import Event", string.Empty, string.Empty, ImportActions.BeforeImportAction, false));

            //Add nodes to the "Inline Pictures" group of examples.
            examples[11].Groups.Add(new RichEditExample("ImageFromFile", string.Empty, string.Empty, InlinePicturesActions.ImageFromFileAction, false));
            examples[11].Groups.Add(new RichEditExample("ImageCollection", string.Empty, string.Empty, InlinePicturesActions.ImageCollectionAction, false));
            examples[11].Groups.Add(new RichEditExample("SaveImageToFile", string.Empty, string.Empty, InlinePicturesActions.SaveImageToFileAction, false));

            //Add nodes to the "Lists" group of examples.
            examples[12].Groups.Add(new RichEditExample("CreateBulletedList", string.Empty, string.Empty, ListsActions.CreateBulletedListAction, false));
            examples[12].Groups.Add(new RichEditExample("CreateNumberedList", string.Empty, string.Empty, ListsActions.CreateNumberedListAction, false));
            examples[12].Groups.Add(new RichEditExample("CreateMultilevelList", string.Empty, string.Empty, ListsActions.CreateMultilevelListAction, false));

            //Add nodes to the "Notes" group of examples.
            examples[13].Groups.Add(new RichEditExample("InsertFootnotes", string.Empty, string.Empty, NotesActions.InsertFootnotesAction, false));
            examples[13].Groups.Add(new RichEditExample("InsertEndnotes", string.Empty, string.Empty, NotesActions.InsertEndnotesAction, false));
            examples[13].Groups.Add(new RichEditExample("EditFootnote", string.Empty, string.Empty, NotesActions.EditFootnoteAction, false));
            examples[13].Groups.Add(new RichEditExample("EditEndnote", string.Empty, string.Empty, NotesActions.EditEndnoteAction, false));
            examples[13].Groups.Add(new RichEditExample("EditSeparator", string.Empty, string.Empty, NotesActions.EditSeparatorAction, false));
            examples[13].Groups.Add(new RichEditExample("RemoveNotes", string.Empty, string.Empty, NotesActions.RemoveNotesAction, false));

            //Add nodes to the "Page Layout" group of examples.
            examples[14].Groups.Add(new RichEditExample("LineNumbering", string.Empty, string.Empty, PageLayoutActions.LineNumberingAction, false));
            examples[14].Groups.Add(new RichEditExample("CreateColumns", string.Empty, string.Empty, PageLayoutActions.CreateColumnsAction, false));
            examples[14].Groups.Add(new RichEditExample("PrintLayout", string.Empty, string.Empty, PageLayoutActions.PrintLayoutAction, false));
            examples[14].Groups.Add(new RichEditExample("TabStops", string.Empty, string.Empty, PageLayoutActions.TabStopsAction, false));

            //Add nodes to the "Protection" group of examples.
            examples[15].Groups.Add(new RichEditExample("ProtectDocument", string.Empty, string.Empty, ProtectionActions.ProtectDocumentAction, false));
            examples[15].Groups.Add(new RichEditExample("UnprotectDocument", string.Empty, string.Empty, ProtectionActions.UnprotectDocumentAction, false));
            examples[15].Groups.Add(new RichEditExample("CreateRangePermissions", string.Empty, string.Empty, ProtectionActions.CreateRangePermissionsAction, false));

            //Add nodes to the "Ranges" group of examples.
            examples[16].Groups.Add(new RichEditExample("SelectTextInRange", string.Empty, string.Empty, RangeActions.SelectTextInRangeAction, false));
            examples[16].Groups.Add(new RichEditExample("InsertTextInRange", string.Empty, string.Empty, RangeActions.InsertTextInRangeAction, false));
            examples[16].Groups.Add(new RichEditExample("AppendTextToRange", string.Empty, string.Empty, RangeActions.AppendTextToRangeAction, false));
            examples[16].Groups.Add(new RichEditExample("AppendToParagraph", string.Empty, string.Empty, RangeActions.AppendToParagraphAction, false));

            //Add nodes to the "Shapes" group of examples.
            examples[17].Groups.Add(new RichEditExample("AddFloatingPicture", string.Empty, string.Empty, ShapesActions.AddFloatingPictureAction, false));
            examples[17].Groups.Add(new RichEditExample("FloatingPictureOffset", string.Empty, string.Empty, ShapesActions.FloatingPictureOffsetAction, false));
            examples[17].Groups.Add(new RichEditExample("ChangeZorderAndWrapping", string.Empty, string.Empty, ShapesActions.ChangeZorderAndWrappingAction, false));
            examples[17].Groups.Add(new RichEditExample("AddTextBox", string.Empty, string.Empty, ShapesActions.AddTextBoxAction, false));
            examples[17].Groups.Add(new RichEditExample("InsertRichTextInTextBox", string.Empty, string.Empty, ShapesActions.InsertRichTextInTextBoxAction, false));
            examples[17].Groups.Add(new RichEditExample("RotateAndResize", string.Empty, string.Empty, ShapesActions.RotateAndResizeAction, false));
            examples[17].Groups.Add(new RichEditExample("SelectShape", string.Empty, string.Empty, ShapesActions.SelectShapeAction, false));

            //Add nodes to the "Styles" group of examples.
            examples[18].Groups.Add(new RichEditExample("CreateNewCharacterStyle", string.Empty, string.Empty, StylesAction.CreateNewCharacterStyleAction, false));
            examples[18].Groups.Add(new RichEditExample("CreateNewParagraphStyle", string.Empty, string.Empty, StylesAction.CreateNewParagraphStyleAction, false));
            examples[18].Groups.Add(new RichEditExample("CreateNewLinkedStyle", string.Empty, string.Empty, StylesAction.CreateNewLinkedStyleAction, false));

            //Add nodes to the "Tables" group of examples.
            examples[19].Groups.Add(new RichEditExample("CreateTable", string.Empty, string.Empty, TablesActions.CreateTableAction, false));
            examples[19].Groups.Add(new RichEditExample("CreateFixedTable", string.Empty, string.Empty, TablesActions.CreateFixedTableAction, false));
            examples[19].Groups.Add(new RichEditExample("ChangeTableColor", string.Empty, string.Empty, TablesActions.ChangeTableColorAction, false));
            examples[19].Groups.Add(new RichEditExample("CreateAndApplyTableStyle", string.Empty, string.Empty, TablesActions.CreateAndApplyTableStyleAction, false));
            examples[19].Groups.Add(new RichEditExample("UseConditionalStyle", string.Empty, string.Empty, TablesActions.UseConditionalStyleAction, false));
            examples[19].Groups.Add(new RichEditExample("ChangeColumnAppearance", string.Empty, string.Empty, TablesActions.ChangeColumnAppearanceAction, false));
            examples[19].Groups.Add(new RichEditExample("UseTableCellProcessor", string.Empty, string.Empty, TablesActions.UseTableCellProcessorAction, false));
            examples[19].Groups.Add(new RichEditExample("MergeCells", string.Empty, string.Empty, TablesActions.MergeCellsAction, false));
            examples[19].Groups.Add(new RichEditExample("SplitCells", string.Empty, string.Empty, TablesActions.SplitCellsAction, false));
            examples[19].Groups.Add(new RichEditExample("DeleteTableElements", string.Empty, string.Empty, TablesActions.DeleteTableElementsAction, false));
            examples[19].Groups.Add(new RichEditExample("WrapTextAroundTable", string.Empty, string.Empty, TablesActions.WrapTextAroundTableAction, false));

            //Add nodes to the "Watermarks" group of examples.
            examples[20].Groups.Add(new RichEditExample("CreateTextWatermark", string.Empty, string.Empty, WatermarkActions.CreateTextWatermarkAction, false));
            examples[20].Groups.Add(new RichEditExample("CreateImageWatermark", string.Empty, string.Empty, WatermarkActions.CreateImageWatermarkAction, false));



            return examples;
            #endregion
        }
        void DataBinding(GroupsOfRichEditExamples examples)
        {
            treeList1.DataSource = examples;
            treeList1.ExpandAll();
            treeList1.BestFitColumns();
        }

        ExampleLanguage CurrentExampleLanguage
        {
            get
            {
                if (xtraTabControl1.SelectedTabPage.Tag.ToString() == "CS") return ExampleLanguage.Csharp;
                else return ExampleLanguage.VB;
            }
            set
            {
                this.codeEditor.CurrentExampleLanguage = value;
                xtraTabControl1.SelectedTabPageIndex = (value == ExampleLanguage.Csharp) ? 0 : 1;
            }
        }

        void ShowExamplesInTreeList(TreeList treeList, List<CodeExampleGroup> examples)
        {
            #region InitializeTreeList
            treeList.OptionsPrint.UsePrintStyles = true;
            treeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.OnNewExampleSelected);
            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.ShowIndicator = false;
            #endregion
            TreeListColumn col1 = new TreeListColumn();
            col1.Caption = "Name";
            col1.VisibleIndex = 0;
            col1.OptionsColumn.AllowEdit = false;
            col1.OptionsColumn.AllowMove = false;
            col1.OptionsColumn.ReadOnly = true;
            treeList.Columns.AddRange(new TreeListColumn[] { col1 });
        }

        void ShowFirstExample()
        {
            treeList1.ExpandAll();
            if (treeList1.Nodes.Count > 0)
                treeList1.FocusedNode = treeList1.MoveFirst().FirstNode;
            RichEditExample example = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as RichEditExample;
            codeEditor.ShowExample(example);

        }

        void OnNewExampleSelected(object sender, FocusedNodeChangedEventArgs e)
        {
            RichEditExample codeExample = (sender as TreeList).GetDataRecordByNode(e.Node) as RichEditExample;

            if (codeExample == null)
                return;

            codeEditor.ShowExample(codeExample);
            codeExampleNameLbl.Text = CodeExampleDemoUtils.ConvertStringToMoreHumanReadableForm(codeExample.Name) + " example";
        }

        void DisableTabs(int examplesCSCount, int examplesVBCount)
        {
            if (examplesCSCount == 0)
                foreach (XtraTabPage t in xtraTabControl1.TabPages) if (t.Tag.ToString() == "CS") t.PageEnabled = false;
            if (examplesVBCount == 0)
                foreach (XtraTabPage t in xtraTabControl1.TabPages) if (t.Tag.ToString() == "VB") t.PageEnabled = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            wordProcessor.SaveDocument("Result.docx", DocumentFormat.OpenXml);
            Process.Start("Result.docx");

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RichEditExample example = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as RichEditExample;
            if (example == null)
                return;
            Action<RichEditDocumentServer> action = example.Action;
            action(wordProcessor);
            if (example.SaveResult) 
            {
                wordProcessor.SaveDocument("Result.docx", DocumentFormat.OpenXml);
                Process.Start("Result.docx");
            }
        }
    }   
}
