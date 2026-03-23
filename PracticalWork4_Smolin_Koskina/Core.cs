using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticalWork4_Smolin_Koskina
{
    /// <summary>
    /// Статический класс, содержащий математические функции и вспомогательные методы UI.
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Вычисляет значение функции F1(x, y, z) = (y + (x-1)^(1/3))^(1/4) / (|x-y| * (sin²(z) + tg(z))).
        /// </summary>
        /// <param name="x">Значение переменной x.</param>
        /// <param name="y">Значение переменной y.</param>
        /// <param name="z">Значение переменной z.</param>
        /// <returns>Результат вычисления функции.</returns>
        public static double Function1(double x, double y, double z)
        {
            var dividend = Math.Pow((y + Math.Pow((x - 1), 1d / 3d)), 1d / 4d);
            var divisor = Math.Abs(x - y) * (Math.Pow(Math.Sin(z), 2d) + Math.Tan(z));
            return dividend / divisor;
        }

        /// <summary>
        /// Вычисляет значение функции F2(x, y, f) по одному из трёх условий:
        /// если x == y: f(x)² + y² + sin(y);
        /// если x > y: (f(x) - y)² + cos(y);
        /// если x &lt; y: (y - f(x))² + tg(y).
        /// </summary>
        /// <param name="x">Значение переменной x.</param>
        /// <param name="y">Значение переменной y.</param>
        /// <param name="f">Функция f(x), применяемая к переменной x.</param>
        /// <returns>Результат вычисления функции.</returns>
        public static double Function2(double x, double y, Func<double, double> f)
        {
            if (x - y == 0)
                return Math.Pow(f(x), 2) + Math.Pow(y, 2) + Math.Sin(y);
            else if (x - y > 0)
                return Math.Pow((f(x) - y), 2) + Math.Cos(y);
            else
                return Math.Pow((y - f(x)), 2) + Math.Tan(y);
        }

        /// <summary>
        /// Вычисляет значение функции F3(x, b) = |x-b|^(1/2) / |b³-x³|^(3/2) + ln|x-b|.
        /// </summary>
        /// <param name="x">Значение переменной x.</param>
        /// <param name="b">Значение параметра b.</param>
        /// <returns>Результат вычисления функции.</returns>
        public static double Function3(double x, double b)
        {
            var dividend = Math.Pow(Math.Abs(x - b), 1d / 2d);
            var divisor = Math.Pow(Math.Abs(Math.Pow(b, 3) - Math.Pow(x, 3)), 3d / 2d);
            return dividend / divisor + Math.Log(Math.Abs(x - b));
        }

        /// <summary>
        /// Отображает диалоговое окно с сообщением об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Отображает диалоговое окно с предупреждением.
        /// </summary>
        /// <param name="message">Текст предупреждения.</param>
        public static void Warning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Проверяет, является ли вводимый символ допустимым для числового поля ввода.
        /// Разрешает цифры, запятую (один раз) и минус только в начале.
        /// </summary>
        /// <param name="sender">Элемент TextBox, инициировавший событие.</param>
        /// <param name="e">Аргументы события ввода текста.</param>
        public static void CheckIsNumeric(TextBox sender, TextCompositionEventArgs e)
        {
            bool minus = sender.Text.Length == 0 && e.Text.Equals("-");
            bool dot = sender.Text.IndexOf(",") < 0 && e.Text.Equals(",") && sender.Text.Length > 0;
            bool minusNotAtStart = sender.Text.Length > 0 && e.Text.Equals("-");

            if (!(Double.TryParse(e.Text, out _) || dot || minus) || minusNotAtStart)
                e.Handled = true;
        }

        /// <summary>
        /// Блокирует ввод пробела в текстовых полях.
        /// </summary>
        /// <param name="e">Аргументы события нажатия клавиши.</param>
        public static void CheckIsSpace(KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
    }
}