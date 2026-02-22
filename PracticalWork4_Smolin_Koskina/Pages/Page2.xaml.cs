using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private Func<double, double> _f;

        private bool XYFFieldsIsNotNullOrEmpty()
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
            if(shXRadioButton.IsChecked == false && x2RadioButton.IsChecked == false && xeRadioButton.IsChecked == false)
            {
                Core.Warning("Не задана функция для 'X'!");
                return false;
            }

            return true;
        }
        private bool XYFAnswerElementsIsNotNull()
        {
            if (XTextBox == null || YTextBox == null || shXRadioButton == null || x2RadioButton == null || xeRadioButton == null || AnswerTextBox == null)
            {
                Core.Error("Элементы не проинициализировались!");
                return false;
            }

            return true;
        }
        private bool TryConvertXYToDouble(string xString, string yString, out double x, out double y)
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
            x = resultX; y = resultY;
            return parsingResult;

        }
        private void ClearFields()
        {
            XTextBox.Clear();
            YTextBox.Clear();
            AnswerTextBox.Clear();
            shXRadioButton.IsChecked = false;
            x2RadioButton.IsChecked = false;
            xeRadioButton.IsChecked = false;
        }
        private string CalculateFunction()
        {
            string result;
            double x, y;
            if (TryConvertXYToDouble(XTextBox.Text, YTextBox.Text, out x, out y))
            {
                result = Core.Function2(x, y, _f).ToString();
            }
            else
            {
                Core.Warning("Операция возможна только с числовыми значениями!");
                result = "Ошибка!";
            }
            return result;
        }

        private void XTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void YTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Core.CheckIsNumeric(sender as TextBox, e);
        private void XTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);
        private void YTextBox_PreviewKeyDown(object sender, KeyEventArgs e) => Core.CheckIsSpace(e);

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (XYFAnswerElementsIsNotNull())
            {
                ClearFields();
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (XYFAnswerElementsIsNotNull() && XYFFieldsIsNotNullOrEmpty())
            {
                AnswerTextBox.Text = CalculateFunction();
            }
        }

        private void shXRadioButton_Checked(object sender, RoutedEventArgs e) { _f = (x) => Math.Sinh(x); }
        private void x2RadioButton_Checked(object sender, RoutedEventArgs e) { _f = (x) => Math.Pow(x, 2); }
        private void xeRadioButton_Checked(object sender, RoutedEventArgs e) { _f = (x) => Math.Pow(x, Math.E); }
    }
}
