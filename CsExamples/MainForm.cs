using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackgroundWorkerUtils;
using ChartUtils;
using FileUtils;

namespace CsExamples
{
    public partial class MainForm : Form
    {
        BackgroundWorkerUtils.BackgroundWorkerHelper bgwHelper;
        Dictionary<string, List<ChartData>> chartDataDic = new Dictionary<string, List<ChartData>>();

        public MainForm()
        {
            InitializeComponent();
            bgwHelper = new BackgroundWorkerUtils.BackgroundWorkerHelper(backgroundWorkFunc, onBackgroundProgressChangedFunc, onBackgroundWorkCompletedFunc);
            StatusLabel.Visible = false;
            StatusProgressBar.Visible = false;

            PrepareData();
        }

        private void PrepareData()
        {
            var data1 = new List<ChartData>();
            var data2 = new List<ChartData>();
            var data3 = new List<ChartData>();
            for (var i = 0; i < 1000000; i++)
            {
                data1.Add(new ChartData(i, (long)(Math.Sin((double)i * 0.01) * 100000.0)));
                data2.Add(new ChartData(i, (long)(Math.Cos((double)i * 0.01) * 100000.0)));

                if (i % 10 == 0)
                    data3.Add(new ChartData(i, i * 100));
                else
                    data3.Add(new ChartData(i, (long)(i * 0.1)));
            }

            chartDataDic.Add("Sin", data1);
            chartDataDic.Add("Cos", data2);
            chartDataDic.Add("Dis", data3);
        }

        private void backgroundWorkFunc()
        {
            for (var i = 0; i <= 100; i++)
            {
                Thread.Sleep(10);
                bgwHelper.ReportProgress(i);
            }
        }

        private void onBackgroundProgressChangedFunc(int percentage)
        {
            StatusProgressBar.Value = percentage;

        }

        private void onBackgroundWorkCompletedFunc(bool result)
        {
            //StatusLabel.Visible = false;
            //StatusProgressBar.Visible = false;
            Console.WriteLine("BackgroundWork Completed");
        }

        private void BackgroundWorkerTestButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Visible = true;
            StatusProgressBar.Visible = true;
            bgwHelper.StartWork();
        }


        public void OnChartDrawEnd()
        {
            ChartTestButton.Enabled = true;
        }

        private void ChartTestButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            var optimzeLevel = ChartOptimizeTrackBar.Maximum - ChartOptimizeTrackBar.Value;
            ChartUtils.ChartHelper.ClearAllSeries(TestChart);

            Parallel.ForEach(this.chartDataDic, keyValuePair =>
            {
                TestChart.BeginInvoke(new Action(() =>
                {
                    ChartUtils.ChartHelper.AddSeries(TestChart, keyValuePair.Key, keyValuePair.Value, optimzeLevel);
                }));   
            });

            /*
            var now = DateTime.Now;
            var data1 = new List<ChartData>();
            var data2 = new List<ChartData>();
            var data3 = new List<ChartData>();
            for (var i = 0; i < 1000; i++)
            {
                data1.Add(new ChartData(i, (long)(Math.Sin((double)i * 0.01) * 100000.0)));
                data2.Add(new ChartData(i, (long)(Math.Cos((double)i * 0.01) * 100000.0)));

                if (i % 10 == 0)
                    data3.Add(new ChartData(i, i*100));
                else 
                    data3.Add(new ChartData(i, (long)(i * 0.1)));
            }

            var optimzeLevel = ChartOptimizeTrackBar.Maximum - ChartOptimizeTrackBar.Value;
            ChartUtils.ChartHelper.AddSeries(TestChart, "Sin", data1, optimzeLevel);
            ChartUtils.ChartHelper.AddSeries(TestChart, "Cos", data2, optimzeLevel);
            ChartUtils.ChartHelper.AddSeries(TestChart, "Peak", data3, optimzeLevel);
            */
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

    public class ChartData : IChartSeriesPointCollection
    {
        private object xvalue;
        public object XValue
        {
            get { return xvalue; }
            set { xvalue = value; }
        }

        private long yvalue;
        public long YValue
        {
            get { return yvalue; }
            set { yvalue = value; }
        }

        public ChartData(object xv, long yv)
        {
            this.XValue = xv;
            this.YValue = yv;
        }
    }
}
