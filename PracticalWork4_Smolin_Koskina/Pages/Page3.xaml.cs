using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticalWork4_Smolin_Koskina
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        private double _x0 = -100;
        private double _xk = 100;
        private double _dxf = 0.2d;
        private double _dxv = 10;

        public Page3()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            ChartPayments.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Series1")
            {
                IsValueShownAsLabel = false
            };
            ChartPayments.Series.Add(currentSeries);

            ChartArea area = ChartPayments.ChartAreas.FirstOrDefault();

            area.AxisX.Interval = _dxv;
            area.AxisX.IntervalType = DateTimeIntervalType.Number;

            area.AxisX.Minimum = _x0;
            area.AxisX.Maximum = _xk;
            area.AxisX.MajorGrid.Interval = _dxv;
            area.AxisX.MajorTickMark.Interval = _dxv;

            area.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;

            area.AxisY.Interval = 2;
            area.AxisY.IntervalType = DateTimeIntervalType.Number;

            area.AxisY.Minimum = -2;
            area.AxisY.Maximum = 6;

            area.AxisY.MajorGrid.Interval = 2;
            area.AxisY.MajorTickMark.Interval = 2;

            area.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
        }

        private bool XBFieldsIsNotNullOrEmpty()
        {
            if (String.IsNullOrEmpty(XTextBox.Text))
            {
                Core.Warning("Не задано значение 'X'!");
                return false;
            }
            if (String.IsNullOrEmpty(BTextBox.Text))
            {
                Core.Warning("Не задано значение 'Y'!");
                return false;
            }

            return true;
        }
        private bool XBAnswerElementsIsNotNull()
        {
            if (XTextBox == null || BTextBox == null || AnswerTextBox == null)
            {
                Core.Error("Элементы не проинициализировались!");
                return false;
            }

            return true;
        }
        private bool TryConvertXBToDouble(string xString, string bString, out double x, out double b)
        {
            bool parsingResult = true;

            if (!(Double.TryParse(xString, out double resultX)))
            {
                Core.Warning("Не удалось преобразовать ввод 'X' в число!");
                parsingResult = false;
            }
            if (!(Double.TryParse(bString, out double resultY)))
            {
                Core.Warning("Не удалось преобразовать ввод 'Y' в число!");
                parsingResult = false;
            }
            x = resultX; b = resultY;
            return parsingResult;

        }
        private void ClearFields()
        {
            XTextBox.Clear();
            BTextBox.Clear();
            AnswerTextBox.Clear();
            ChartPayments.Series.FirstOrDefault().Points.Clear();
        }
        private string CalculateFunction()
        {
            string result;
            double x, b;
            if (TryConvertXBToDouble(XTextBox.Text, BTextBox.Text, out x, out b))
            {
                result = Core.Function3(x, b).ToString();
                UpdateChart(b);
            }
            else
            {
                Core.Warning("Операция возможна только с числовыми значениями!");
                result = "Ошибка!";
            }
            return result;
        }
        private void UpdateChart(double b)
        {
            Series currentSeries = ChartPayments.Series.FirstOrDefault();
            currentSeries.ChartType = SeriesChartType.Spline;
            currentSeries.Points.Clear();
            for (double i = _x0; i < _xk; i+=_dxf)
            {
                if (Math.Abs(i - b) < 0.01d)
                    currentSeries.Points.AddXY(b, 10000);
                else
                    currentSeries.Points.AddXY(i,Core.Function3(i, b));
            }
        }

        private void XTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void BTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void XTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);
        private void BTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (XBAnswerElementsIsNotNull())
            {
                ClearFields();
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (XBAnswerElementsIsNotNull() && XBFieldsIsNotNullOrEmpty())
            {
                AnswerTextBox.Text = CalculateFunction();
            }
        }
    }
}
