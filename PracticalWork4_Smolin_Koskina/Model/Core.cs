using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticalWork4_Smolin_Koskina
{
    public static class Core
    {
        public static double Function1(double x, double y, double z)
        {
            var dividend = Math.Pow((y + Math.Pow((x - 1), 1d / 3d)), 1d / 4d);
            var divisor = Math.Abs(x - y) * (Math.Pow(Math.Sin(z), 2d) + Math.Tan(z));
            return (dividend / divisor);
        }
        public static double Function2(double x, double y, Func<double,double> f)
        {
            double result;
            if (x - y == 0)
            {
                result = Math.Pow(f(x), 2) + Math.Pow(y, 2) + Math.Sin(y);
            } 
            else if (x - y > 0)
            {
                result = Math.Pow((f(x) - y), 2) + Math.Cos(y);
            }
            else
            {
                result = Math.Pow((y - f(x)), 2) + Math.Tan(y);
            }

            return result;
        }

        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Warning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void CheckIsNumeric(TextBox sender, TextCompositionEventArgs e)
        {
            double result;
            bool dot = sender.Text.IndexOf(",") < 0 && e.Text.Equals(",") && sender.Text.Length > 0;
            if (!(Double.TryParse(e.Text, out result) || dot))
            {
                e.Handled = true;
            }
        }
        public static void CheckIsSpace(KeyEventArgs e) { if (e.Key == Key.Space) e.Handled = true; }
    }
}
