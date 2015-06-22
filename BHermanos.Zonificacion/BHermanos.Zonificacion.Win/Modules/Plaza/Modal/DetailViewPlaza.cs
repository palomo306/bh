using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHermanos.Zonificacion.Win.Modules.Plaza.Modal
{
    public partial class DetailViewPlaza : Form
    {
         #region Propiedades
        public System.Data.DataTable Data { get; set; }
        public string Title
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        public string Plaza { get; set; }
        #endregion

        #region Constructor
        public DetailViewPlaza()
        {
            InitializeComponent();            
        }
        #endregion

        #region Métodos invocables
        public void LoadData()
        {
            dgvReportPlazas.DataSource = Data;
            dgvReportPlazas.Columns[0].Frozen = true;
            dgvReportPlazas.Columns[0].Width = 180;
            dgvReportPlazas.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;
            //Se alinean las cifras
            for (int i = 1; i < dgvReportPlazas.Columns.Count; i++)
            {
                dgvReportPlazas.Columns[i].Width = 75;
                dgvReportPlazas.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        }
        #endregion

        private void ExportToPdf(DataGridView Dv, string FilePath)
        {
            FontFactory.RegisterDirectories();
            iTextSharp.text.Font myfont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font myfontHead = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE);
            iTextSharp.text.Font myfontTitle = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 2f, 2f, 2f, 2f);

            pdfDoc.Open();
            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, new FileStream(FilePath, FileMode.Create));
            pdfDoc.Open();

            PdfPTable generalDat = new PdfPTable(4);
            Phrase pTitle = new Phrase(this.Title, myfontTitle);
            PdfPCell cellTitle = new PdfPCell(pTitle);
            cellTitle.Colspan = 4;
            cellTitle.Padding = 2;
            cellTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTitle.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            generalDat.AddCell(cellTitle);

            Phrase pBlank0 = new Phrase("    ", myfont);
            PdfPCell cellBlank0 = new PdfPCell(pBlank0);
            cellBlank0.Colspan = 4;
            cellBlank0.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellBlank0);            
            Phrase pEdoT = new Phrase("Plaza:", myfontHead);
            PdfPCell cellEdoT = new PdfPCell(pEdoT);
            cellEdoT.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
            cellEdoT.HorizontalAlignment = Element.ALIGN_LEFT;
            generalDat.AddCell(cellEdoT);
            Phrase pEdo = new Phrase(Plaza, myfont);
            PdfPCell cellEdo = new PdfPCell(pEdo);
            cellEdo.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            cellEdo.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellEdo);
            Phrase pMunT = new Phrase(" ", myfontHead);
            PdfPCell cellMunT = new PdfPCell(pMunT);
            cellMunT.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
            cellMunT.HorizontalAlignment = Element.ALIGN_LEFT;
            generalDat.AddCell(cellMunT);
            Phrase pMun = new Phrase(" ", myfont);
            PdfPCell cellMun = new PdfPCell(pMun);
            cellMun.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            cellMun.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellMun);
            Phrase pBlank = new Phrase("    ", myfont);
            PdfPCell cellBlank = new PdfPCell(pBlank);
            cellBlank.Colspan = 4;
            cellBlank.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellBlank);


            PdfPTable _mytable = new PdfPTable(Dv.ColumnCount);
            for (int j = 0; j < Dv.Columns.Count; ++j)
            {
                Phrase p = new Phrase(Dv.Columns[j].HeaderText, myfontHead);
                PdfPCell cell = new PdfPCell(p);
                cell.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _mytable.AddCell(cell);
            }
            //-------------------------
            for (int i = 0; i < Dv.Rows.Count - 1; ++i)
            {
                for (int j = 0; j < Dv.Columns.Count; ++j)
                {
                    Phrase p = new Phrase(Dv.Rows[i].Cells[j].Value.ToString(), myfont);
                    PdfPCell cell = new PdfPCell(p);
                    if (j != 0)
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    else
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    if (i % 2 == 1)
                        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    _mytable.AddCell(cell);
                }
            }
            //------------------------  
            pdfDoc.Add(generalDat);
            pdfDoc.Add(_mytable);
            pdfDoc.Close();
            System.Diagnostics.Process.Start(FilePath);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void ExportToExcel(DataGridView Dv, string FilePath)
        {
            int progress = 1;
            int total = Dv.ColumnCount * Dv.RowCount + Dv.ColumnCount;
            progressBar.Minimum = 0;
            progressBar.Maximum = total;
            progressBar.Step = 1;
            progressBar.Value = 0;            
            BackgroundWorker w = new BackgroundWorker();
            w.WorkerReportsProgress = true;
            w.DoWork += new DoWorkEventHandler((object senderDoWork, DoWorkEventArgs eDoWork) =>
            {                
                var backgroundWorker = (BackgroundWorker)senderDoWork;

                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int i = 0;
                int j = 0;

                Range range = xlWorkSheet.Cells[1, 1];
                range.Font.Size = 16;
                range.Font.Name = "Tahoma";
                range.Font.Bold = true;
                range.Value = this.Title;

                range = xlWorkSheet.Cells[3, 1];
                range.Font.Size = 12;
                range.Font.Name = "Tahoma";
                range.Font.Bold = true;
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                range.Value = "Plaza:";
                range = xlWorkSheet.Cells[3, 2];
                range.Font.Size = 12;
                range.Font.Name = "Tahoma";
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                range.Value = Plaza;
                range = xlWorkSheet.Cells[4, 1];
                range.Font.Size = 12;
                range.Font.Name = "Tahoma";
                range.Font.Bold = true;
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                range.Value = "";
                range = xlWorkSheet.Cells[4, 2];
                range.Font.Size = 12;
                range.Font.Name = "Tahoma";
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                range.Value = "";
                
                for (j = 0; j < Dv.ColumnCount; ++j)
                {
                    range = xlWorkSheet.Cells[i + 6, j + 1];
                    range.Value = Dv.Columns[j].Name;
                    range.Font.Size = 12;
                    range.Font.Name = "Tahoma";
                    range.Font.Bold = true;
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                    backgroundWorker.ReportProgress(progress);
                    progress++;
                }
                
                for (i = 0; i <= Dv.RowCount - 1; i++)
                {
                    for (j = 0; j <= Dv.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = Dv[j, i];
                        range = xlWorkSheet.Cells[i + 7, j + 1];
                        range.Font.Size = 12;
                        range.Font.Name = "Tahoma";
                        if (i % 2 == 1)
                            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                        range.Value = cell.Value;
                        range.EntireColumn.AutoFit();
                        backgroundWorker.ReportProgress(progress);
                        progress++;
                    }                    
                }
                xlWorkBook.SaveAs(FilePath);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                System.Diagnostics.Process.Start(FilePath);

            });
            w.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object senderRunWorkerCompleted, RunWorkerCompletedEventArgs eRunWorkerCompleted) =>
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 0;
                progressBar.Step = 1;
                progressBar.Value = 0;      
            });
            w.ProgressChanged += new ProgressChangedEventHandler((object senderProgressChangedEventHandler, ProgressChangedEventArgs eProgressChangedEventHandler) =>
            {
                progressBar.Value = eProgressChangedEventHandler.ProgressPercentage;
            });
            w.RunWorkerAsync();     

            
        }

        #region Export Reports
        private void btnPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Portable Document Format|*.pdf";
            saveDialog.DefaultExt = "pdf";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf(dgvReportPlazas, saveDialog.FileName);
            }
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Libro de Excel|*.xlsx";
            saveDialog.DefaultExt = "xlsx";          
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //progressBar.Width = statusStripBar.Width;
                ExportToExcel(dgvReportPlazas, saveDialog.FileName);
            }            
        }
        #endregion
    }
}
