using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartUtils
{
    public interface IChartSeriesPointCollection
    {
        object XValue { get; set; }
        long YValue { get; set; }
    }

    public class ChartHelper
    {
        public static void ClearAllSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            while (chart.Series.Count > 0)
            {
                chart.Series.RemoveAt(0);
            }
        }

        public static void AddSeries(
            System.Windows.Forms.DataVisualization.Charting.Chart chart, 
            string seriesName, 
            IEnumerable<IChartSeriesPointCollection> dataCollection, 
            int optimizeLevel = 0, 
            System.Windows.Forms.DataVisualization.Charting.ChartValueType chartXValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Auto,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
            )
        {
            const int optimizeMinSize = 10;
            IEnumerable<IChartSeriesPointCollection> optimizedDataList; 
            if (dataCollection.Count() > optimizeMinSize)
            {
                int optimizeThresold = (int)(getDeltaMedian(dataCollection) * ((double)optimizeLevel));
                Console.WriteLine("optimizeThresold {0}", optimizeThresold);

                optimizedDataList = optimizeSeriesData(dataCollection, optimizeThresold);
                Console.WriteLine("OptimizedDataList Count: {0}", optimizedDataList.Count());
            }
            else
            {
                optimizedDataList = dataCollection;
            }

            chart.Series.Add(seriesName);
            chart.Series[seriesName].XValueType = chartXValueType;
            chart.Series[seriesName].ChartType = seriesChartType;
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
                    deltas[index++] = (int)Math.Abs(point.YValue - prevPoint.YValue);
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

        private static IEnumerable<IChartSeriesPointCollection> optimizeSeriesData(IEnumerable<IChartSeriesPointCollection> dataCollection, int optimizeThresold)
        {
            if (optimizeThresold <= 0)
            {
                return dataCollection;
            }
 
            var seriesPointList = new List<IChartSeriesPointCollection>();

            IChartSeriesPointCollection prevPoint = null, pivotPoint = null, lastAddPoint = null;
            long pivotDelta = 0, prevDelta = 0, prevPrevDelta;
            
            foreach (var point in dataCollection)
            {
                prevPrevDelta = prevDelta;
                if (null != prevPoint)
                {
                    if (null == pivotPoint)
                    {
                        pivotPoint = point;
                    }
                    else
                    {
                        prevDelta = prevPoint.YValue - point.YValue;
                        pivotDelta = (pivotPoint.YValue - point.YValue);

                        if ((pivotDelta == 0) || (prevDelta * prevPrevDelta > 0 && Math.Abs(pivotDelta) < optimizeThresold))
                        {
                            prevPoint = point;
                            continue;
                        }

                        pivotPoint = point;
                        if (lastAddPoint != prevPoint)
                        {
                            seriesPointList.Add(prevPoint);
                        }
                    }

                    seriesPointList.Add(point);
                    lastAddPoint = point;
                }
                else
                {
                    // 첫번째 포인트는 무조건 추가한다 
                    seriesPointList.Add(point);
                    lastAddPoint = point;
                }

                prevPoint = point;
            }

            var lastPoint = dataCollection.ElementAt(dataCollection.Count() - 1);
            if (lastAddPoint != lastPoint)
            {
                // 마지막 포인트도 무조건 추가
                seriesPointList.Add(lastPoint);
            }
            
            return seriesPointList;
        }
    }
}
