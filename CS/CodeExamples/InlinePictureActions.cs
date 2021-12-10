﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit;

namespace RichEditDocumentServerAPIExample.CodeExamples
{
    class InlinePicturesActions
    {
        public static Action<RichEditDocumentServer> ImageFromFileAction = ImageFromFile;
        public static Action<RichEditDocumentServer> ImageCollectionAction = ImageCollection;
        public static Action<RichEditDocumentServer> SaveImageToFileAction = SaveImageToFile;

        static void ImageFromFile(RichEditDocumentServer wordProcessor)
        {
            #region #ImageFromFile
            Document document = wordProcessor.Document;
            DocumentPosition pos = document.Range.Start;
            document.Images.Insert(pos, DocumentImageSource.FromFile("Documents\\beverages.png"));
            #endregion #ImageFromFile
        }

        static void ImageCollection(RichEditDocumentServer wordProcessor)
        {
            #region #ImageCollection
            Document document = wordProcessor.Document;
            document.LoadDocument("Documents\\Grimm.docx", DocumentFormat.OpenXml);
            ReadOnlyDocumentImageCollection images = document.Images;
            // If the width of an image exceeds 50 millimeters, 
            // the image is scaled proportionally to half its size.
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i].Size.Width > DevExpress.Office.Utils.Units.MillimetersToDocumentsF(50))
                {
                    images[i].ScaleX /= 2;
                    images[i].ScaleY /= 2;
                }
            }
            #endregion #ImageCollection
        }

        static void SaveImageToFile(RichEditDocumentServer wordProcessor)
        {
            #region #SaveImageToFile
            Document document = wordProcessor.Document;
            document.LoadDocument("Documents\\MovieRentals.docx", DocumentFormat.OpenXml);
            DocumentRange myRange = document.CreateRange(0, 100);
            ReadOnlyDocumentImageCollection images = document.Images.Get(myRange);
            if (images.Count > 0)
            {
                DevExpress.Office.Utils.OfficeImage myImage = images[0].Image;
                System.Drawing.Image image = myImage.NativeImage;
                string imageName = String.Format("Image_at_pos_{0}.png", images[0].Range.Start.ToInt());
                image.Save(imageName);
                System.Diagnostics.Process.Start("explorer.exe", "/select," + imageName);
            }
            #endregion #SaveImageToFile
        }
    }
}

