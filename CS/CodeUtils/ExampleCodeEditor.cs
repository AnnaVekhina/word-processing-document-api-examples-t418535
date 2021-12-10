using DevExpress.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Internal;
using System;

namespace RichEditDocumentServerAPIExample.CodeUtils
{
    public class ExampleCodeEditor
    {
        readonly IRichEditControl codeEditorCs;
        readonly IRichEditControl codeEditorVb;

        ExampleLanguage current;

        bool richEditTextChanged = false;
        DateTime lastExampleCodeModifiedTime = DateTime.Now;

        public ExampleCodeEditor(IRichEditControl codeEditorCs, IRichEditControl codeEditorVb/*, IRichEditControl codeEditorCsClass, IRichEditControl codeEditorVbClass*/)
        {
            this.codeEditorCs = codeEditorCs;
            this.codeEditorVb = codeEditorVb;

            this.codeEditorCs.InnerControl.InitializeDocument += new System.EventHandler(this.InitializeSyntaxHighlightForCs);
            this.codeEditorVb.InnerControl.InitializeDocument += new System.EventHandler(this.InitializeSyntaxHighlightForVb);
        }

        void InitializeSyntaxHighlightForCs(object sender, EventArgs e)
        {
            SyntaxHightlightInitializeHelper syntaxHightlightInitializator = new SyntaxHightlightInitializeHelper();
            syntaxHightlightInitializator.Initialize(codeEditorCs, CodeExampleDemoUtils.GetCodeExampleFileExtension(ExampleLanguage.Csharp));

            DisableRichEditFeatures(codeEditorCs);
        }


        void InitializeSyntaxHighlightForVb(object sender, EventArgs e)
        {
            SyntaxHightlightInitializeHelper syntaxHightlightInitializator = new SyntaxHightlightInitializeHelper();
            syntaxHightlightInitializator.Initialize(codeEditorVb, CodeExampleDemoUtils.GetCodeExampleFileExtension(ExampleLanguage.VB));

            DisableRichEditFeatures(codeEditorVb);
        }

        public InnerRichEditControl CurrentCodeEditor
        {
            get
            {
                if (CurrentExampleLanguage == ExampleLanguage.Csharp)
                    return codeEditorCs.InnerControl;
                else
                    return codeEditorVb.InnerControl;
            }
        }

        public ExampleLanguage CurrentExampleLanguage
        {
            get { return current; }
            set { current = value; }
        }      

        public void ShowExample(RichEditExample codeExample)
        {
            InnerRichEditControl richEditControlCs = codeEditorCs.InnerControl;
            InnerRichEditControl richEditControlVb = codeEditorVb.InnerControl;

            if (codeExample != null)
            {
                richEditControlCs.Text = codeExample.CodeCS;
                SpecifyMargins(richEditControlCs);
                richEditControlVb.Text = codeExample.CodeVB;
                SpecifyMargins(richEditControlCs);
            }
        }
        void SpecifyMargins(InnerRichEditControl richEditControl) 
        {
            richEditControl.Document.Sections[0].Margins.Left = 0.5f;
            richEditControl.Document.Sections[0].Margins.Right = 0.5f;
            richEditControl.Document.Sections[0].Margins.Top = 0.5f;
        }
        void DisableRichEditFeatures(IRichEditControl codeEditor)
        {
            RichEditControlOptionsBase options = codeEditor.InnerDocumentServer.Options;
            options.DocumentCapabilities.Hyperlinks = DocumentCapability.Disabled;
            options.DocumentCapabilities.Numbering.Bulleted = DocumentCapability.Disabled;
            options.DocumentCapabilities.Numbering.Simple = DocumentCapability.Disabled;
            options.DocumentCapabilities.Numbering.MultiLevel = DocumentCapability.Disabled;

            options.DocumentCapabilities.Tables = DocumentCapability.Disabled;
            options.DocumentCapabilities.Bookmarks = DocumentCapability.Disabled;

            options.DocumentCapabilities.CharacterStyle = DocumentCapability.Disabled;
            options.DocumentCapabilities.ParagraphStyle = DocumentCapability.Disabled;
        }
    }

}
