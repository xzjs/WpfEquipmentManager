using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfEquipmentManager
{
    /// <summary>
    /// RecordReportControl.xaml 的交互逻辑
    /// </summary>
    public partial class RecordReportControl : UserControl
    {
        public RecordReportControl()
        {
            InitializeComponent();
        }

        public void Load_Data(List<Record> _lr)
        {
            recordReportViewer.LocalReport.DataSources.Clear();
            List<RecordListItem> lrli = new List<RecordListItem>();
            foreach(var item in _lr)
            {
                RecordListItem rli = new RecordListItem
                {
                    Id = item.Id,
                    Start = item.Start.ToString(),
                    End = item.End == null ? "尚未归还" : item.End.ToString(),
                    Name = item.Name,
                    Phone = item.Phone.Replace("Phone.", ""),
                    Card = item.Card.Replace("NO.", ""),
                    EquipmentName = item.Equipment.Name,
                    Total = item.Total == 0 ? "尚未归还" : item.Total.ToString(),
                    LendNum = item.LendNum,
                    ReturnNum=item.ReturnNum
                };
                lrli.Add(rli);
            }
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lrli;
            recordReportViewer.LocalReport.ReportPath = Directory.GetCurrentDirectory() + "\\Report.rdlc";
            recordReportViewer.LocalReport.DataSources.Add(reportDataSource);

            recordReportViewer.RefreshReport();
        }

    }
}
