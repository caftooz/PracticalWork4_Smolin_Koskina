using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticalWork4_Smolin_Koskina
{
    /// <summary>
    /// Страница вычисления функции F2(x, y, f(x)).
    /// </summary>
    public partial class Page2 : Page
    {
        /// <summary>Выбранная пользователем функция f(x).</summary>
        private Func<double, double> _f;

        /// <summary>
        /// Инициализирует компоненты страницы Page2.
        /// </summary>
        public Page2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вычисляет функцию F2 по переданным строковым значениям и функции <paramref name="f"/>.
        /// Возвращает <c>true</c> при успешном вычислении и записывает результат в <paramref name="result"/>;
        /// возвращает <c>false</c> при ошибке парсинга.
        /// </summary>
        /// <param name="xText">Строковое представление переменной x.</param>
        /// <param name="yText">Строковое представление переменной y.</param>
        /// <param name="f">Функция f(x), применяемая при вычислении.</param>
        /// <param name="result">
        /// Выходной параметр: строка с результатом вычисления или сообщением об ошибке.
        /// </param>
        /// <returns><c>true</c>, если вычисление выполнено успешно; иначе <c>false</c>.</returns>
        public bool CalculateFunction(string xText, string yText, Func<double, double> f, out string result)
        {
            if (!double.TryParse(xText, out double x))
            {
                result = "Ошибка!";
                return false;
            }
            if (!double.TryParse(yText, out double y))
            {
                result = "Ошибка!";
                return false;
            }

            result = Core.Function2(x, y, f).ToString();
            return true;
        }

        /// <summary>
        /// Проверяет, что все элементы управления (поля ввода, переключатели, поле результата) не равны <c>null</c>.
        /// </summary>
        /// <returns><c>true</c>, если все элементы проинициализированы; иначе <c>false</c>.</returns>
        private bool XYFAnswerElementsIsNotNull()
        {
            if (XTextBox == null || YTextBox == null || shXRadioButton == null ||
                x2RadioButton == null || xeRadioButton == null || AnswerTextBox == null)
            {
                Core.Error("Элементы не проинициализировались!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверяет, что поля X, Y заполнены и выбрана функция f(x).
        /// </summary>
        /// <returns><c>true</c>, если все данные введены; иначе <c>false</c>.</returns>
        private bool XYFFieldsIsNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(XTextBox.Text))
            {
                Core.Warning("Не задано значение 'X'!");
                return false;
            }
            if (string.IsNullOrEmpty(YTextBox.Text))
            {
                Core.Warning("Не задано значение 'Y'!");
                return false;
            }
            if (shXRadioButton.IsChecked == false && x2RadioButton.IsChecked == false && xeRadioButton.IsChecked == false)
            {
                Core.Warning("Не задана функция для 'X'!");
                return false;
            }
            return true;
        }

        /// <summary>Очищает все поля ввода, переключатели и поле результата.</summary>
        private void ClearFields()
        {
            XTextBox.Clear();
            YTextBox.Clear();
            AnswerTextBox.Clear();
            shXRadioButton.IsChecked = false;
            x2RadioButton.IsChecked = false;
            xeRadioButton.IsChecked = false;
        }

        private void XTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void YTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void XTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);
        private void YTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);

        /// <summary>Обработчик кнопки «Очистить»: сбрасывает все поля.</summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (XYFAnswerElementsIsNotNull())
                ClearFields();
        }

        /// <summary>
        /// Обработчик кнопки «Рассчитать»: вызывает метод <see cref="CalculateFunction"/> и отображает результат.
        /// </summary>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (XYFAnswerElementsIsNotNull() && XYFFieldsIsNotNullOrEmpty())
            {
                if (!CalculateFunction(XTextBox.Text, YTextBox.Text, _f, out string result))
                    Core.Warning("Операция возможна только с числовыми значениями!");
                AnswerTextBox.Text = result;
            }
        }

        /// <summary>Устанавливает f(x) = sh(x) при выборе соответствующего переключателя.</summary>
        private void shXRadioButton_Checked(object sender, RoutedEventArgs e) { _f = x => Math.Sinh(x); }

        /// <summary>Устанавливает f(x) = x² при выборе соответствующего переключателя.</summary>
        private void x2RadioButton_Checked(object sender, RoutedEventArgs e) { _f = x => Math.Pow(x, 2); }

        /// <summary>Устанавливает f(x) = x^e при выборе соответствующего переключателя.</summary>
        private void xeRadioButton_Checked(object sender, RoutedEventArgs e) { _f = x => Math.Pow(x, Math.E); }
    }
}