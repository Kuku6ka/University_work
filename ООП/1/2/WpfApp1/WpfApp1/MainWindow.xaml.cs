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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        foreach (UIElement el in Main_grid.Children)
        {
            if (el is Button)
            {
                ((Button)el).Click += Button_Click;
            }   
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        decimal num, guess, eps = 1;

        try
        {
            Iterat_output.Text = "";
            Result.Text = "";
            
            int i = 0;
            decimal prev_guess;
            num = Convert.ToDecimal(TextBox_for_Input_Num.Text);
            guess = Convert.ToDecimal(TextBox_for_Guess.Text);
            double MathSqrt = Math.Sqrt((double)num);
            Result.Foreground = new SolidColorBrush(Colors.Black);
            Result.Text += "MathSqrt: \t";
            Result.Text += Convert.ToString(MathSqrt);
            while (eps > ((decimal)1.7E-12))
            {
                prev_guess = guess;
                guess = ((num / guess) + guess) / 2;
                eps = prev_guess - guess;
                if (eps < 0)
                {
                    eps *= -1;
                }
                i++;
                Iterat_output.Text += Convert.ToString(i);
                Iterat_output.Text += "\t";
                Iterat_output.Text += Convert.ToString(eps);
                Iterat_output.Text += "\n";
            }

            Result.Text += "\n";
            Result.Text += "Newton: \t";
            Result.Text += Convert.ToString(guess);
        }
        catch
        {
            Result.Foreground = new SolidColorBrush(Colors.Red);
            Result.Text = "Произошла непредвиденная ошибка";
        }
    }
}