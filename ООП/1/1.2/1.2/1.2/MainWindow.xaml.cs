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

namespace _1._2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        foreach (UIElement el in Main_Grid.Children)
        {
            if (el is Button)
            {
                ((Button)el).Click += Button_Click;
            }

        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string input = TextBox_input.Text;
        bool num = double.TryParse(input.Replace(".", ","), out double double_num);
        int.TryParse(input, out int int_num);
        if (num && int_num != double_num)
        {
            TextBlock_error.Text = "";
            TextBlock_pass.Text = "Число является вещественным";
        }
        else
        {
            TextBlock_error.Text = "Число не является вещественным";
            TextBlock_pass.Text = "";
        }
    }
}