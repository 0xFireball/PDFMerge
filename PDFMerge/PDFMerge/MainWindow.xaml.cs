using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using PDFMerge.Shared;

namespace PDFMerge
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Private Fields

        private string file1;
        private string file2;
        private string outputFile;

        #endregion Private Fields

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void btnFile1_Click(object sender, RoutedEventArgs e)
        {
            file1 = PdfMergeWpfUtil.BrowseForOpenFile("PDF Documents (*.pdf)|*.pdf|All Files (*.*)|*.*", "Select File 1");
            tFile1.Text = Path.GetFileName(file1);
        }

        private void btnFile2_Click(object sender, RoutedEventArgs e)
        {
            file2 = PdfMergeWpfUtil.BrowseForOpenFile("PDF Documents (*.pdf)|*.pdf|All Files (*.*)|*.*", "Select File 2");
            tFile2.Text = Path.GetFileName(file2);
        }

        private async void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            //Generate the merged document
            if (file1 == null || file2 == null)
            {
                await this.ShowMessageAsync("Error", "Please select all input files to continue.");
            }
            else
            {
                try
                {
                    outputFile = PdfMergeWpfUtil.BrowseForSaveFile("PDF Documents (*.pdf)|*.pdf|All Files (*.*)|*.*", "Save Output File");
                    PDFMerger.MergePdfFiles(file1, file2, outputFile);
                    var result = await this.ShowMessageAsync("Success!", "Your PDF files have been merged! Do you want to open the merged PDF?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "Yes", NegativeButtonText = "No" });
                    if (result == MessageDialogResult.Affirmative)
                    {
                        Process.Start(outputFile);
                    }
                }
                catch (Exception ex)
                {
                    await this.ShowMessageAsync("Error", $"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        #endregion Private Methods

        private void BtnZetaPhase_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://zetaphase.io");
        }
    }
}