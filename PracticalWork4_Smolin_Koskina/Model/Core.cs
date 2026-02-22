using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Warning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
