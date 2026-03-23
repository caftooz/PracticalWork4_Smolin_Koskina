using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;

namespace PracticalWork4_Smolin_Koskina
{
    /// <summary>
    /// Страница вычисления функции F3(x, b) с отображением графика.
    /// </summary>
    public partial class Page3 : Page
    {
        private readonly double _x0 = -100;
        private readonly double _xk = 100;
        private readonly double _dxf = 0.2d;
        private readonly double _dxv = 10;

        /// <summary>
        /// Инициализирует компоненты страницы Page3 и настраивает область графика.
        /// </summary>
        public Page3()
        {
            InitializeComponent();
            InitializeChart();
        }

        /// <summary>
        /// Настраивает параметры диаграммы: оси, интервалы, диапазоны значений.
        /// </summary>
        private void InitializeChart()
        {
            ChartPayments.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Series1") { IsValueShownAsLabel = false };
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

        /// <summary>
        /// Вычисляет функцию F3 по переданным строковым значениям переменных.
        /// При успешном вычислении обновляет график и записывает результат в <paramref name="result"/>.
        /// Возвращает <c>false</c> при ошибке парсинга.
        /// </summary>
        /// <param name="xText">Строковое представление переменной x.</param>
        /// <param name="bText">Строковое представление параметра b.</param>
        /// <param name="result">
        /// Выходной параметр: строка с результатом вычисления или сообщением об ошибке.
        /// </param>
        /// <returns><c>true</c>, если вычисление выполнено успешно; иначе <c>false</c>.</returns>
        public bool CalculateFunction(string xText, string bText, out string result)
        {
            if (!double.TryParse(xText, out double x))
            {
                result = "Ошибка!";
                return false;
            }
            if (!double.TryParse(bText, out double b))
            {
                result = "Ошибка!";
                return false;
            }

            result = Core.Function3(x, b).ToString();
            UpdateChart(b);
            return true;
        }

        /// <summary>
        /// Проверяет, что поля ввода X, B и поле результата не равны <c>null</c>.
        /// </summary>
        /// <returns><c>true</c>, если все элементы проинициализированы; иначе <c>false</c>.</returns>
        private bool XBAnswerElementsIsNotNull()
        {
            if (XTextBox == null || BTextBox == null || AnswerTextBox == null)
            {
                Core.Error("Элементы не проинициализировались!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверяет, что поля X и B не пусты.
        /// </summary>
        /// <returns><c>true</c>, если оба поля заполнены; иначе <c>false</c>.</returns>
        private bool XBFieldsIsNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(XTextBox.Text))
            {
                Core.Warning("Не задано значение 'X'!");
                return false;
            }
            if (string.IsNullOrEmpty(BTextBox.Text))
            {
                Core.Warning("Не задано значение 'B'!");
                return false;
            }
            return true;
        }

        /// <summary>Очищает поля ввода, поле результата и точки графика.</summary>
        private void ClearFields()
        {
            XTextBox.Clear();
            BTextBox.Clear();
            AnswerTextBox.Clear();
            ChartPayments.Series.FirstOrDefault()?.Points.Clear();
        }

        /// <summary>
        /// Обновляет серию данных графика для заданного значения параметра <paramref name="b"/>.
        /// В точке x ≈ b устанавливается условное значение 10000 (разрыв функции).
        /// </summary>
        /// <param name="b">Значение параметра b, задающего точку разрыва функции.</param>
        private void UpdateChart(double b)
        {
            Series currentSeries = ChartPayments.Series.FirstOrDefault();
            currentSeries.ChartType = SeriesChartType.Spline;
            currentSeries.Points.Clear();

            for (double i = _x0; i < _xk; i += _dxf)
            {
                if (Math.Abs(i - b) < 0.01d)
                    currentSeries.Points.AddXY(b, 10000);
                else
                    currentSeries.Points.AddXY(i, Core.Function3(i, b));
            }
        }

        private void XTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void BTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void XTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);
        private void BTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);

        /// <summary>Обработчик кнопки «Очистить»: сбрасывает все поля и график.</summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (XBAnswerElementsIsNotNull())
                ClearFields();
        }

        /// <summary>
        /// Обработчик кнопки «Рассчитать»: вызывает метод <see cref="CalculateFunction"/> и отображает результат.
        /// </summary>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (XBAnswerElementsIsNotNull() && XBFieldsIsNotNullOrEmpty())
            {
                if (!CalculateFunction(XTextBox.Text, BTextBox.Text, out string result))
                    Core.Warning("Операция возможна только с числовыми значениями!");
                AnswerTextBox.Text = result;
            }
        }
    }
}