using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDFConverter.Models
{
    public class Converter
    {
        String pdfFilePath;
        String docxFilePath;

        public String PDFFilePath
        {
            get { return pdfFilePath; }
            set { pdfFilePath = value; }
        }
        public String DocxFilePath
        {
            get {return docxFilePath;}
            set {docxFilePath = value;}

        }

    }
}
