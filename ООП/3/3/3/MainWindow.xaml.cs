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

namespace _3;

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
            if (el is Button && el == Button_1)
            {
                ((Button)el).Click += OnClick_Button;
            }
        }
    }
    private void OnClick_Button(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(TextBox_1.Text.Trim(), out int int_1) && int.TryParse(TextBox_2.Text.Trim(), out int int_2))
        {
            try
            {
                int result = checked(int_1 * int_2);
                TextBlock_1.Text = result.ToString();
            }
            catch (OverflowException ex)
            {
                TextBlock_1.Text = ex.Message;
            }
        }
    }
}