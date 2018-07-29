﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsExamples
{
    public partial class MainForm : Form
    {
        BackgroundWorkerHelper bgwHelper;

        public MainForm()
        {
            InitializeComponent();
            bgwHelper = new BackgroundWorkerHelper(backgroundWorkFunc, onBackgroundProgressChangedFunc, onBackgroundWorkCompletedFunc);
        }

        private void backgroundWorkFunc()
        {
            for (var i = 0; i <= 100; i++)
            {
                bgwHelper.ReportProgress(i);
                Thread.Sleep(10);
            }
        }

        private void onBackgroundProgressChangedFunc(int percentage)
        {
            TestProgressBar.Value = percentage;

        }

        private void onBackgroundWorkCompletedFunc(bool result)
        {
            Console.WriteLine("BackgroundWork Completed");
        }

        private void BackgroundWorkerTestButton_Click(object sender, EventArgs e)
        {
            bgwHelper.StartWork();
        }


        public void OnChartDrawEnd()
        {
            ChartTestButton.Enabled = true;
        }

        private void ChartTestButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            
            ChartHelper.ClearSeries(TestChart);

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
            ChartHelper.AddSeries(TestChart, "Sin", data1, optimzeLevel);
            ChartHelper.AddSeries(TestChart, "Cos", data2, optimzeLevel);
            ChartHelper.AddSeries(TestChart, "Peak", data3, optimzeLevel);
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
