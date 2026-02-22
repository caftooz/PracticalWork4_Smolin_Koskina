using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticalWork4_Smolin_Koskina
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private bool XYZTextsIsNotNullOrEmpty()
        {
            if (String.IsNullOrEmpty(XTextBox.Text))
            {
                Core.Warning("Не задано значение 'X'!");
                return false;
            }
            if (String.IsNullOrEmpty(YTextBox.Text))
            {
                Core.Warning("Не задано значение 'Y'!");
                return false;
            }
            if (String.IsNullOrEmpty(ZTextBox.Text))
            {
                Core.Warning("Не задано значение 'Z'!");
                return false;
            }

            return true;
        }
        private bool XYZAnswerTextBoxesIsNotNull()
        {
            if (XTextBox == null || YTextBox == null || ZTextBox == null || AnswerTextBox == null)
            {
                Core.Error("Элементы не проинициализировались!");
                return false;
            }

            return true;
        }
        private string CalculateFunction()
        {
            string result;
            double x, y, z;
            if (TryConvertXYZToDouble(XTextBox.Text, YTextBox.Text, ZTextBox.Text, out x, out y, out z))
            {
                result = Core.Function1(x, y, z).ToString();
            }
            else
            {
                Core.Warning("Операция возможна только с числовыми значениями!");
                result = "Ошибка!";
            }
            return result;
        }
        private bool TryConvertXYZToDouble(
            string xString, string yString, string zString,
            out double x, out double y, out double z)
        {
            bool parsingResult = true;

            if (!(Double.TryParse(xString, out double resultX)))
            {
                Core.Warning("Не удалось преобразовать ввод 'X' в число!");
                parsingResult = false;
            }
            if (!(Double.TryParse(yString, out double resultY)))
            {
                Core.Warning("Не удалось преобразовать ввод 'Y' в число!");
                parsingResult = false;
            }
            if (!(Double.TryParse(zString, out double resultZ)))
            {
                Core.Warning("Не удалось преобразовать ввод 'Z' в число!");
                parsingResult = false;
            }

            x = resultX; y = resultY; z = resultZ;
            return parsingResult;

        }
        private void ClearTexts() { XTextBox.Clear(); YTextBox.Clear(); ZTextBox.Clear(); AnswerTextBox.Clear(); }

        private void CheckIsNumeric(TextBox sender, TextCompositionEventArgs e)
        {
            double result;
            bool dot = sender.Text.IndexOf(",") < 0 && e.Text.Equals(",") && sender.Text.Length > 0;
            if (!(Double.TryParse(e.Text, out result) || dot))
            {
                e.Handled = true;
            }
        }
        private void CheckIsSpace(KeyEventArgs e) { if (e.Key == Key.Space) e.Handled = true; }

        private void XTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckIsNumeric(sender as TextBox, e);
        private void YTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckIsNumeric(sender as TextBox, e);
        private void ZTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckIsNumeric(sender as TextBox, e); 
        private void XTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => CheckIsSpace(e);
        private void YTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => CheckIsSpace(e);
        private void ZTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => CheckIsSpace(e);

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (XYZAnswerTextBoxesIsNotNull())
            {
                ClearTexts();
            }
        }
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (XYZAnswerTextBoxesIsNotNull() && XYZTextsIsNotNullOrEmpty())
            {
                AnswerTextBox.Text = CalculateFunction();
            }
        }
    }
}
