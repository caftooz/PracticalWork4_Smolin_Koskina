using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticalWork4_Smolin_Koskina
{
    /// <summary>
    /// Страница вычисления функции F1(x, y, z).
    /// </summary>
    public partial class Page1 : Page
    {
        /// <summary>
        /// Инициализирует компоненты страницы Page1.
        /// </summary>
        public Page1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вычисляет функцию F1 по переданным строковым значениям переменных.
        /// Возвращает <c>true</c> при успешном вычислении и записывает результат в <paramref name="result"/>;
        /// возвращает <c>false</c> при ошибке парсинга или некорректных данных.
        /// </summary>
        /// <param name="xText">Строковое представление переменной x.</param>
        /// <param name="yText">Строковое представление переменной y.</param>
        /// <param name="zText">Строковое представление переменной z.</param>
        /// <param name="result">
        /// Выходной параметр: строка с результатом вычисления или сообщением об ошибке.
        /// </param>
        /// <returns><c>true</c>, если вычисление выполнено успешно; иначе <c>false</c>.</returns>
        public bool CalculateFunction(string xText, string yText, string zText, out string result)
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
            if (!double.TryParse(zText, out double z))
            {
                result = "Ошибка!";
                return false;
            }

            result = Core.Function1(x, y, z).ToString();
            return true;
        }

        /// <summary>
        /// Проверяет, что все поля ввода (X, Y, Z) и поле результата не равны <c>null</c>.
        /// </summary>
        /// <returns><c>true</c>, если все элементы проинициализированы; иначе <c>false</c>.</returns>
        private bool XYZAnswerTextBoxesIsNotNull()
        {
            if (XTextBox == null || YTextBox == null || ZTextBox == null || AnswerTextBox == null)
            {
                Core.Error("Элементы не проинициализировались!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверяет, что поля ввода X, Y, Z не пусты и не состоят из пробелов.
        /// </summary>
        /// <returns><c>true</c>, если все поля заполнены; иначе <c>false</c>.</returns>
        private bool XYZTextsIsNotNullOrEmpty()
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
            if (string.IsNullOrEmpty(ZTextBox.Text))
            {
                Core.Warning("Не задано значение 'Z'!");
                return false;
            }
            return true;
        }

        /// <summary>Очищает все поля ввода и поле результата.</summary>
        private void ClearTexts()
        {
            XTextBox.Clear();
            YTextBox.Clear();
            ZTextBox.Clear();
            AnswerTextBox.Clear();
        }

        private void XTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void YTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void ZTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void XTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);
        private void YTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);
        private void ZTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);

        /// <summary>Обработчик кнопки «Очистить»: сбрасывает все поля.</summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (XYZAnswerTextBoxesIsNotNull())
                ClearTexts();
        }

        /// <summary>
        /// Обработчик кнопки «Рассчитать»: вызывает метод <see cref="CalculateFunction"/> и отображает результат.
        /// </summary>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (XYZAnswerTextBoxesIsNotNull() && XYZTextsIsNotNullOrEmpty())
            {
                if (!CalculateFunction(XTextBox.Text, YTextBox.Text, ZTextBox.Text, out string result))
                    Core.Warning("Операция возможна только с числовыми значениями!");
                AnswerTextBox.Text = result;
            }
        }
    }
}