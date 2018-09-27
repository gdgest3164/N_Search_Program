using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace N_Search_Deesse
{
    public partial class File_Save : Form
    {
        public Action Worker { get; set; }
        
        public File_Save(Action worker)
        {
            InitializeComponent();

            if (worker == null)
                throw new ArgumentNullException();
            Worker = worker;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
            
        }

        /*static File_Save fs; static DialogResult fs_result = DialogResult.No;

        public static DialogResult Loading(string title, ListView emali_list)
        {
            fs = new File_Save();
            fs.label2.Text = title+"로 저장중...";
            
            fs.progressBar1.Style = ProgressBarStyle.Continuous;
            fs.progressBar1.Minimum = 0;
            fs.progressBar1.Maximum = emali_list.Items.Count;
            fs.progressBar1.Step = 1;
            fs.progressBar1.Value = 0;

            //엑셀 쓰기
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook wb = excelApp.Workbooks.Add(true);
            Excel._Worksheet workSheet = wb.Worksheets.get_Item(1) as Excel._Worksheet;

            workSheet.Cells[1, 1] = "이메일";
            workSheet.Cells[1, 1].ColumnWidth=30;
            workSheet.Cells[1, 2] = "추출일시";
            workSheet.Cells[1, 2].ColumnWidth = 20;
            workSheet.Cells[1, 3] = "키워드";
            workSheet.Cells[1, 3].ColumnWidth = 15;
            fs.ShowDialog();
            for (int et=0; et<emali_list.Items.Count; et++)
            {
                Application.DoEvents();
                fs.progressBar1.PerformStep();
                for (int et2 = 0; et2 < 3; et2++)
                {
                    Application.DoEvents();
                    workSheet.Cells[(et + 2), (et2+1)] = emali_list.Items[et].SubItems[et2].ToString().Replace("ListViewSubItem: {","").Replace("}","");
                }
            }
            ExcelDispose(excelApp, wb, workSheet, title);
            //fs_result = DialogResult.Yes; fs.Close();
            return fs_result;
        }

        public static void ExcelDispose(Excel.Application excelApp, Excel.Workbook wb, Excel._Worksheet workSheet, string title)
        {
            wb.SaveAs(@Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/"+DateTime.Now.ToString("MMddhhmmss")+" 이메일 추출 리스트.xls", Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            wb.Close(Type.Missing, Type.Missing, Type.Missing);
            
            excelApp.Quit();
            releaseObject(excelApp);
            releaseObject(workSheet);
            releaseObject(wb);
            
        }

        #region 메모리해제
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception e)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion*/

    }
}
