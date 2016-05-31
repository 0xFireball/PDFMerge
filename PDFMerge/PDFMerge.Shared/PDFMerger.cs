using System;
using System.Collections.Generic;
using System.Text;

namespace PDFMerge.Shared
{
    class PDFMerger
    {
        public static void MergePdfFiles(string file1, string file2, string outputFile)
        {
            //Now merge and save the PDF
            var outputDocument = new PdfDocument();
            //Merge inputs
            string[] mergeFiles = { file1, file2 };
            foreach (var file in mergeFiles)
            {
                // Open the document to import pages from it.
                var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                var count = inputDocument.PageCount;
                for (var idx = 0; idx < count; idx++)
                {
                    //Extract page
                    var page = inputDocument.Pages[idx];
                    // Append page to output
                    outputDocument.AddPage(page);
                }
            }
            outputDocument.Save(outputFile);
        }
    }
}
