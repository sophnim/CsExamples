using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsExamples
{
    public interface IChartSeriesPointCollection
    {
        object Xvalue { get; set; }
        long Yvalue { get; set; }
    }

    public class ChartHelper
    {
        public static void ClearSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            while (chart.Series.Count > 0)
            {
                chart.Series.RemoveAt(0);
            }
        }

        public static void AddSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart, string seriesName, IEnumerable<IChartSeriesPointCollection> dataCollection, int optimizeLevel)
        {
            int optimizeThresold = (int)(getDeltaMedian(dataCollection) * ((double)optimizeLevel / 3.0));
            Console.WriteLine("optimizeThresold {0}", optimizeThresold);

            var optimizedDataList = optimizeData(dataCollection, optimizeThresold);
            Console.WriteLine("OptimizedDataList Count: {0}", optimizedDataList.Count);

            chart.Series.Add(seriesName);
            chart.Series[seriesName].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Auto;
            chart.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart.Series[seriesName].Points.DataBind(optimizedDataList, "Xvalue", "Yvalue", null);
        }

        private static double getDeltaMedian(IEnumerable<IChartSeriesPointCollection> dataCollection)
        {
            int[] deltas = new int[dataCollection.Count() - 1];
            IChartSeriesPointCollection prevPoint = null;

            int index = 0;
            foreach (var point in dataCollection)
            {
                if (null != prevPoint)
                {
                    deltas[index++] = (int)Math.Abs(point.Yvalue - prevPoint.Yvalue);
                }
                prevPoint = point;
            }

            int numberCount = deltas.Count();
            int halfIndex = deltas.Count() / 2;
            var sortedNumbers = deltas.OrderBy(n => n);
            double median;
            if ((numberCount % 2) == 0)
            {
                median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1)) / 2;
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }

            return median + 1;
        }

        private static List<IChartSeriesPointCollection> optimizeData(IEnumerable<IChartSeriesPointCollection> dataCollection, int optimizeThresold)
        {
            if (optimizeThresold <= 0)
            {
                return dataCollection.ToList<IChartSeriesPointCollection>();
            }
 
            var seriesPointList = new List<IChartSeriesPointCollection>();

            IChartSeriesPointCollection prevPoint = null, pivotPoint = null, addPoint = null;
            long pivotDelta = 0, prevDelta = 0, prevPrevDelta;
            
            foreach (var point in dataCollection)
            {
                var xv = Convert.ToInt32(point.Xvalue);
                prevPrevDelta = prevDelta;
                if (null != prevPoint)
                {
                    if (null == pivotPoint)
                    {
                        pivotPoint = point;
                    }
                    else
                    {
                        prevDelta = prevPoint.Yvalue - point.Yvalue;
                        pivotDelta = (pivotPoint.Yvalue - point.Yvalue);

                        //Console.WriteLine("prevDelta {0} pivotDelta {1} optTh {2}", prevDelta, pivotDelta, optimizeThresold);

                        if ((pivotDelta == 0) || (prevDelta * prevPrevDelta > 0 && Math.Abs(pivotDelta) < optimizeThresold))
                        {
                            prevPoint = point;
                            continue;
                        }

                        pivotPoint = point;
                        if (addPoint != prevPoint)
                        {
                            seriesPointList.Add(prevPoint);
                        }
                    }

                    seriesPointList.Add(point);
                    addPoint = point;
                }

                prevPoint = point;
            }

            var lastPoint = dataCollection.ElementAt(dataCollection.Count() - 1);
            if (addPoint != lastPoint)
            {
                seriesPointList.Add(lastPoint);
            }
            
            return seriesPointList;
        }
    }
}
