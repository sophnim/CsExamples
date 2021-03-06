﻿namespace CsExamples
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TestChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartTestButton = new System.Windows.Forms.Button();
            this.BackgroundWorkerTestButton = new System.Windows.Forms.Button();
            this.ChartOptimizeTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.TestChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartOptimizeTrackBar)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestChart
            // 
            chartArea1.Name = "ChartArea1";
            this.TestChart.ChartAreas.Add(chartArea1);
            this.TestChart.Location = new System.Drawing.Point(12, 12);
            this.TestChart.Name = "TestChart";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.TestChart.Series.Add(series1);
            this.TestChart.Size = new System.Drawing.Size(776, 300);
            this.TestChart.TabIndex = 0;
            this.TestChart.Text = "chart1";
            // 
            // ChartTestButton
            // 
            this.ChartTestButton.Location = new System.Drawing.Point(712, 319);
            this.ChartTestButton.Name = "ChartTestButton";
            this.ChartTestButton.Size = new System.Drawing.Size(75, 23);
            this.ChartTestButton.TabIndex = 1;
            this.ChartTestButton.Text = "Chart Test";
            this.ChartTestButton.UseVisualStyleBackColor = true;
            this.ChartTestButton.Click += new System.EventHandler(this.ChartTestButton_Click);
            // 
            // BackgroundWorkerTestButton
            // 
            this.BackgroundWorkerTestButton.Location = new System.Drawing.Point(712, 370);
            this.BackgroundWorkerTestButton.Name = "BackgroundWorkerTestButton";
            this.BackgroundWorkerTestButton.Size = new System.Drawing.Size(75, 23);
            this.BackgroundWorkerTestButton.TabIndex = 2;
            this.BackgroundWorkerTestButton.Text = "Bgw Test";
            this.BackgroundWorkerTestButton.UseVisualStyleBackColor = true;
            this.BackgroundWorkerTestButton.Click += new System.EventHandler(this.BackgroundWorkerTestButton_Click);
            // 
            // ChartOptimizeTrackBar
            // 
            this.ChartOptimizeTrackBar.Location = new System.Drawing.Point(602, 318);
            this.ChartOptimizeTrackBar.Maximum = 40;
            this.ChartOptimizeTrackBar.Name = "ChartOptimizeTrackBar";
            this.ChartOptimizeTrackBar.Size = new System.Drawing.Size(104, 45);
            this.ChartOptimizeTrackBar.TabIndex = 4;
            this.ChartOptimizeTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(526, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Chart Detail";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.StatusProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(109, 17);
            this.StatusLabel.Text = "BackgroundWorker";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChartOptimizeTrackBar);
            this.Controls.Add(this.BackgroundWorkerTestButton);
            this.Controls.Add(this.ChartTestButton);
            this.Controls.Add(this.TestChart);
            this.Name = "MainForm";
            this.Text = "CSharp Examples";
            ((System.ComponentModel.ISupportInitialize)(this.TestChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartOptimizeTrackBar)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart TestChart;
        private System.Windows.Forms.Button ChartTestButton;
        private System.Windows.Forms.Button BackgroundWorkerTestButton;
        private System.Windows.Forms.TrackBar ChartOptimizeTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}

