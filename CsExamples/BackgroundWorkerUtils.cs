using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BackgroundWorkerUtils
{
    public class BackgroundWorkerHelper
    {
        BackgroundWorker worker;
        Action workFunc;
        Action<int> onProgressChangedFunc;
        Action<bool> onCompletedFunc;

        public BackgroundWorkerHelper(Action workFunc, Action<int> onProgressChangedFunc, Action<bool> onCompletedFunc)
        {
            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new DoWorkEventHandler(DoWork);
            this.worker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompleted);

            this.workFunc = workFunc;
            this.onProgressChangedFunc = onProgressChangedFunc;
            this.onCompletedFunc = onCompletedFunc;
        }

        public bool StartWork()
        {
            if (worker.IsBusy)
            {
                return false;
            }

            worker.RunWorkerAsync();
            return true;
        }

        public void ReportProgress(int percent)
        {
            this.worker.ReportProgress(percent);
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            this.workFunc();
        }

        void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.onProgressChangedFunc(e.ProgressPercentage);
        }

        void OnCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            onCompletedFunc(e.Error == null);
        }
    }
}
