using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        foreach (UIElement el in Main_Grid.Children)
        {
            if (el is Button && el == Button_num)
            {
                ((Button)el).Click += Button_num_Click;
            }

            if (el is Button && el == Button_rim)
            {
                ((Button)el).Click += Button_rim_Click;
            }
        }
    }

    private static string Conv(int n, int radix)
    {
        string digits = "0123456789ABCDEF";
        string result = "";

        while (n > 0)
        {
            int remainder = n % radix;
            char digit = digits[remainder];
            result = digit + result;
            n = n / radix;
        }

        return result;
    }

    /// <summary>
    /// Обработчик нажатия кнопки для перевода системы счисления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_num_Click(object sender, RoutedEventArgs e)
    {
        int targetNum, numBaseInInt;
        string numBase, result;

        if (int.TryParse(NumTextBox.Text, out targetNum))
        {
            targetNum = Convert.ToInt32(NumTextBox.Text);
            numBase = ComboBox.Text;
            numBaseInInt = Convert.ToInt32(numBase);
            result = Conv(targetNum, numBaseInInt);
            TextBlock_result.Text = result;
        }
        else
        {
            TextBlock_result.Text = "Введёное число имеет неверный формат.\nПожалйста введите целое число";
        }
    }

    private void Button_rim_Click(object sender, RoutedEventArgs e)
    {
        string num = RIM_BOX.Text;
        string combo = rim_combo.Text;
        if (combo == "to_rim")
        {
            int num_int;
            if (int.TryParse(num, out num_int))
            {
                num_int = Convert.ToInt32(num_int);
                string result = num_int.ToRoman();
                result_rim.Text = result;
            }
            else
            {
                result_rim.Text = "Введённое число имеет неверный формат или не является числом";
            }
        }

        if (combo == "to_arabic")
        {
            int result = num.ToArabic();
            result_rim.Text = Convert.ToString(result);
        }
    }
}

static class rim
{
    public static int ToArabic(this string romanNumber)
    {
        (int number, string symbol)[] table = new (int num, string symbol)[13]
        {
            (1, "I"), (4, "IV"), (5, "V"), (9, "IV"),
            (10, "X"), (40, "XL"), (50, "L"), (90, "XC"),
            (100, "C"), (400, "CD"), (500, "D"), (900, "CM"),
            (1000, "M")
        };
        int i = 12;
        int p = 1;
        int res = 0;
        while (p <= romanNumber.Length)
        {
            while (romanNumber.Substring(p - 1, table[i].symbol.Length) != table[i].symbol)
            {
                i--;
                if (i == 0) break;
            }

            res += table[i].number;
            p += table[i].symbol.Length;
        }

        return res;
    }

    public static string ToRoman(this int number)
    {
        (int number, string symbol)[] table = new (int num, string symbol)[13]
        {
            (1, "I"), (4, "IV"), (5, "V"), (9, "IV"),
            (10, "X"), (40, "XL"), (50, "L"), (90, "XC"),
            (100, "C"), (400, "CD"), (500, "D"), (900, "CM"),
            (1000, "M")
        };
        string result = "";
        int N = number;
        int i = 12;
        while (N > 0)
        {
            while (table[i].number > N)
            {
                i--;
            }

            result += table[i].symbol;
            N -= table[i].number;
        }

        return result;
    }
}