using System;
using System.Windows.Controls;
using System.Windows.Media;
using SciChart.Charting.Model.DataSeries;
using SciChart.Examples.ExternalDependencies.Common;
using SciChart.Examples.ExternalDependencies.Data;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals;
using System.Collections.Generic;

namespace WpfSciStockChart
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ucSciStockChart : UserControl
    {
        const int PRICE_CNT = 2;
        const int PNT_CNT = 4;
        const int IND_CNT = 4;

        List<OhlcDataSeries<DateTime, double>> priceSeries = new List<OhlcDataSeries<DateTime, double>>()
        {
            new OhlcDataSeries<DateTime, double>(),
            new OhlcDataSeries<DateTime, double>()
        };
        List<XyDataSeries<DateTime, double>> pntSeries = new List<XyDataSeries<DateTime, double>>()
        {
            new XyDataSeries<DateTime, double>() {
                SeriesName = "Buy"
            },
            new XyDataSeries<DateTime, double>() { SeriesName = "Sell" },
            new XyDataSeries<DateTime, double>() { SeriesName = "Open" },
            new XyDataSeries<DateTime, double>() { SeriesName = "Close" }
        };
        List<XyDataSeries<DateTime, double>> indSeries = new List<XyDataSeries<DateTime, double>>()
        {
            new XyDataSeries<DateTime, double>() { SeriesName = "Ind1" },
            new XyDataSeries<DateTime, double>() { SeriesName = "Ind2" },
            new XyDataSeries<DateTime, double>() { SeriesName = "Ind3" },
            new XyDataSeries<DateTime, double>() { SeriesName = "Ind4" }
        };

        bool bAutoScroll = true;
        int nBarCountToDisplay = 100;

        public ucSciStockChart()
        {
            InitializeLicense();
            InitializeComponent();
            initDatas();
        }

        private void InitializeLicense()
        {
            SciChartSurface.SetRuntimeLicenseKey("lwABAAEAAACpd/AT/cbVAQEAbQBDdXN0b21lcj1jcmF6eTtPcmRlcklkPUFCVDIwMDEwOS0zMjMwLTcyMjQ3O1N1YnNjcmlwdGlvblZhbGlkVG89MDQtTWF5LTIwMjU7UHJvZHVjdENvZGU9U0MtV1BGLVNESy1FTlRFUlBSSVNFW0XRyLvWwWtxw1e42joTUUOg9Ka9sbrMcE6xXxGlzoVkKN8n6b4mTgvW9b0yidQJ");
        }

        private void initDatas()
        {
            DataManager.Instance.GetPriceData(Instrument.Indu.Value, TimeFrame.Daily);

            PriceChart.BarTimeFrame = 60;// TimeSpan.FromHours(1).TotalSeconds;

            int id = 0;
            for (int i = 0; i < PRICE_CNT; i++) PriceChart.RenderableSeries[id++].DataSeries = priceSeries[i];
            for (int i = 0; i < PNT_CNT; i++) PriceChart.RenderableSeries[id++].DataSeries = pntSeries[i];
            for (int i = 0; i < IND_CNT; i++) PriceChart.RenderableSeries[id++].DataSeries = indSeries[i];
        }

        public DateTime getLastTime_price()
        {
            if (priceSeries[0].Count == 0)
                return default(DateTime);
            return priceSeries[0].XValues[priceSeries[0].Count - 1];
        }

        public DateTime getLastTime_indicator(int nID)
        {
            if (IND_CNT <= nID || indSeries[nID].Count == 0) return default(DateTime);
            return indSeries[nID].XValues[indSeries[nID].Count - 1];
        }

        public void update_indicator(int nID, bool bAppend, DateTime dt, double dIndVal)
        {
            if (nID >= IND_CNT) return;
            if (bAppend) indSeries[nID].Append(dt, dIndVal);
            else indSeries[nID].Update(dt, dIndVal);
            setBidLineY(dIndVal);
        }

        public void update_price(int nIndex, bool bAppend, DateTime dt, double open, double high, double low, double close , double ask = double.NaN)
        {
            if (nIndex >= PRICE_CNT) return;
            if (bAppend)
            {
                priceSeries[nIndex].Append(dt, open, high, low, close);
                if (nIndex == 0)
                {
                    try
                    {
                        for (int i = 0; i < pntSeries.Count; i++) pntSeries[i].Append(dt, double.NaN);
                        for (int i = 0; i < indSeries.Count; i++) indSeries[i].Append(dt, double.NaN);
                    }
                    catch { }
                }
                if (bAutoScroll)
                {
                    try
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            PriceChart.XAxis.VisibleRange.SetMinMax(Math.Max(0, priceSeries[nIndex].Count - nBarCountToDisplay), priceSeries[nIndex].Count);
                        });
                    }
                    catch (Exception ex)
                    {
                        //   MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                priceSeries[nIndex].Update(dt, open, high, low, close);
                if (nIndex == 0)
                {
                    /*
                    try
                    {
                        for (int i = 0; i < pntSeries.Count; i++) pntSeries[i].Update(dt, double.NaN);
                        for (int i = 0; i < indSeries.Count; i++) indSeries[i].Update(dt, double.NaN);
                    }
                    catch { }
                    */
                }
            }

            if(nIndex == 0) setBidLineY(close);
        }

        public void setAskLineY(double dVAl) { HLineAsk.Y1 = dVAl; }

        public void setBidLineY(double dVAl) { HLineBid.Y1 = dVAl; }


        public void update_ind(int nId, DateTime dt, double price)
        {
            if (nId >= PNT_CNT) return;
            int nID = findNearestIndex(ref dt);
            if (nID < 0)
                return;
            indSeries[nId].Update(dt, price);
        }

        public void update_pnt(int nId, DateTime dt, double price)
        {
            if (nId >= PNT_CNT) return;
            int nID = findNearestIndex(ref dt); //priceSeries.FindIndex(dtTime);
            if (nID < 0)
                return;
            pntSeries[nId].Update(dt, price);
        }

        public void setManipulation(bool bEnable)
        {
            PriceChart.IsManipulationEnabled = true;
        }

        public void setRubberBandZoom(bool bEnable)
        {
            PriceChart.IsRubberBandZoomEnabled = bEnable;
        }
        public void setPan(bool bEnable)
        {
            PriceChart.IsPanEnabled = bEnable;
        }
        public void setRollover(bool bEnable)
        {
            
            PriceChart.IsRolloverEnabled = bEnable;
        }

        public void setAutoScroll(bool bEnable, int nCount)
        {
            bAutoScroll = bEnable;
            nBarCountToDisplay = nCount;
            if (nBarCountToDisplay <= 0 || nBarCountToDisplay > 10000000) nBarCountToDisplay = 100;
        }

        public int findDateTime(DateTime dt)
        {
            return priceSeries[0].FindIndex(dt);
        }

        public void showByIndex(int index)
        {
            this.Dispatcher.Invoke(() =>
            {
                PriceChart.XAxis.VisibleRange.SetMinMax(Math.Max(0, index - 50), Math.Min(priceSeries[0].Count, index + 50));
            });
        }

        public void showInd(int nId, bool bVisible)
        {
            if (nId < IND_CNT) PriceChart.RenderableSeries[PRICE_CNT + PNT_CNT + nId].IsVisible = bVisible;
        }

        public void showpnt(int nId, bool bVisible)
        {
            if (nId < PNT_CNT) PriceChart.RenderableSeries[PRICE_CNT + nId].IsVisible = bVisible;
        }

        public void setPriceChartStyle(int indexStyle)
        {
            switch (indexStyle)
            {
                case 0:
                    PriceChart.RenderableSeries[0] = new SciChart.Charting.Visuals.RenderableSeries.FastCandlestickRenderableSeries();
                    PriceChart.RenderableSeries[4] = new SciChart.Charting.Visuals.RenderableSeries.FastCandlestickRenderableSeries()
                    {
                        StrokeDown = Color.FromRgb(255, 255, 0),
                        StrokeUp = Color.FromRgb(255, 0, 255),
                    };
                    break;
                case 1:
                    PriceChart.RenderableSeries[0] = new SciChart.Charting.Visuals.RenderableSeries.FastOhlcRenderableSeries();
                    PriceChart.RenderableSeries[4] = new SciChart.Charting.Visuals.RenderableSeries.FastOhlcRenderableSeries() 
                    {
                        StrokeDown = Color.FromRgb(255, 255, 0),
                        StrokeUp = Color.FromRgb(255, 0, 255),
                    };
                    break;
                case 2:
                    PriceChart.RenderableSeries[0] = new SciChart.Charting.Visuals.RenderableSeries.FastLineRenderableSeries();
                    PriceChart.RenderableSeries[0].Stroke = Color.FromRgb(0, 255, 0);

                    PriceChart.RenderableSeries[4] = new SciChart.Charting.Visuals.RenderableSeries.FastLineRenderableSeries();
                    PriceChart.RenderableSeries[4].Stroke = Color.FromRgb(255 , 0 ,0);

                    break;
                case 3:
                    PriceChart.RenderableSeries[0] = new SciChart.Charting.Visuals.RenderableSeries.FastMountainRenderableSeries();
                    PriceChart.RenderableSeries[4] = new SciChart.Charting.Visuals.RenderableSeries.FastMountainRenderableSeries();

                    PriceChart.RenderableSeries[0].Stroke = Color.FromRgb(0, 255, 0);
                    PriceChart.RenderableSeries[4].Stroke = Color.FromRgb(255, 0, 0);
                    break;
            }
            PriceChart.RenderableSeries[0].DataSeries = priceSeries[0];
            PriceChart.RenderableSeries[4].DataSeries = priceSeries[1];
        }

        public void ClearRate()
        {
            for (int i = 0; i < PRICE_CNT; i++) priceSeries[i].Clear();
            for (int i = 0; i < IND_CNT; i++) indSeries[i].Clear();
            for (int i = 0; i < PNT_CNT; i++) pntSeries[i].Clear();
        }

        public void ClearInd()
        {
            // for (int i = 0; i < IND_CNT; i++) indSeries[i].Clear();
        }

        public void ClearPnt()
        {
            // for (int i = 0; i < PNT_CNT; i++) pntSeries[i].Clear();
        }


        //BY KHS
        private int findNearestIndex(ref DateTime dtTime)
        {
            for (int i = 0; i < priceSeries[0].Count; i++)
            {
                if (priceSeries[0].XValues[i].AddSeconds(59) >= dtTime)
                {
                    dtTime = priceSeries[0].XValues[i];
                    return i;
                }
            }
            return -1;
        }

        public void AddAnnotation(string command, DateTime dtTime, double dPrice, string sText = "")
        {
            AnnotationBase annotation;
            if (sText != null && sText != "")
            {
                annotation = new TextAnnotation();

                annotation.X1 = dtTime;
                annotation.Y1 = dPrice;
                ((TextAnnotation)annotation).Text = sText;

                if (lastAnnotation == null || lastAnnotation.X1 != annotation.X1 || lastAnnotation.Text != sText)
                {
                    lastAnnotation = ((TextAnnotation)annotation);
                    PriceChart.Annotations.Add(annotation);
                }
            }
        }

        TextAnnotation lastAnnotation = null;
    }
}
