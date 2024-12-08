using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
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

        TestGCD.RunTests();
        
        foreach (UIElement el in MainGrid.Children)
        {
            if (el is Button && el == Button_GCD_2)
            {
                ((Button)el).Click += Button_GCD_2_Click;
            }

            if (el is Button && el == Button_GCD_3)
            {
                ((Button)el).Click += Button_GCD_3_Click;
            }

            if (el is Button && el == Button_GCD_4)
            {
                ((Button)el).Click += Button_GCD_4_Click;
            }

            if (el is Button && el == Button_GCD_5)
            {
                ((Button)el).Click += Button_GCD_5_Click;    
            }

            if (el is Button && el == Button_GCD_space)
            {
                ((Button)el).Click += Button_GCD_space_Click;
            }

            if (el is Button && el == Button_GCD_Stein)
            {
                ((Button)el).Click += Button_GCD_Stein_Click;
            }
        }
        
    }

    private void Button_GCD_2_Click(object sender, RoutedEventArgs e)
    {
        string input_value_1 = TextBox_1.Text;
        string input_value_2 = TextBox_2.Text;
        
        bool success_1 = int.TryParse(input_value_1, out int int_value1);
        bool success_2 = int.TryParse(input_value_2, out int int_value2);

        if (success_1 && success_2)
        {
            TextBlock_Error.Text = "";
            int result = FindGCD.GCD(int_value1, int_value2);
            TextBlock_result.Text = result.ToString();
        }
        else
        {
            TextBlock_Error.Text = "Неправильно введены данные!";
        }
    }

    private void Button_GCD_3_Click(object sender, RoutedEventArgs e)
    {
        string input_value_1 = TextBox_1.Text;
        string input_value_2 = TextBox_2.Text;
        string input_value_3 = TextBox_3.Text;
        
        bool success_1 = int.TryParse(input_value_1, out int int_value1);
        bool success_2 = int.TryParse(input_value_2, out int int_value2);
        bool success_3 = int.TryParse(input_value_3, out int int_value3);
        
        if (success_1 && success_2 && success_3)
        {
            TextBlock_Error.Text = "";
            int result = FindGCD.GCD(int_value1, int_value2, int_value3);
            TextBlock_result.Text = result.ToString();
        }
        else
        {
            TextBlock_Error.Text = "Неправильно введены данные!";
        } 
    }
    
    private void Button_GCD_4_Click(object sender, RoutedEventArgs e)
    {
        string input_value_1 = TextBox_1.Text;
        string input_value_2 = TextBox_2.Text;
        string input_value_3 = TextBox_3.Text;
        string input_value_4 = TextBox_4.Text;
        
        bool success_1 = int.TryParse(input_value_1, out int int_value1);
        bool success_2 = int.TryParse(input_value_2, out int int_value2);
        bool success_3 = int.TryParse(input_value_3, out int int_value3);
        bool success_4 = int.TryParse(input_value_4, out int int_value4);
        
        if (success_1 && success_2 && success_3 && success_4)
        {
            TextBlock_Error.Text = "";
            int result = FindGCD.GCD(int_value1, int_value2, int_value3, int_value4);
            TextBlock_result.Text = result.ToString();
        }
        else
        {
            TextBlock_Error.Text = "Неправильно введены данные!";
        } 
    } 
    
    private void Button_GCD_5_Click(object sender, RoutedEventArgs e)
    {
        string input_value_1 = TextBox_1.Text;
        string input_value_2 = TextBox_2.Text;
        string input_value_3 = TextBox_3.Text;
        string input_value_4 = TextBox_4.Text;
        string input_value_5 = TextBox_5.Text;
        
        bool success_1 = int.TryParse(input_value_1, out int int_value1);
        bool success_2 = int.TryParse(input_value_2, out int int_value2);
        bool success_3 = int.TryParse(input_value_3, out int int_value3);
        bool success_4 = int.TryParse(input_value_4, out int int_value4);
        bool success_5 = int.TryParse(input_value_5, out int int_value5);
        
        if (success_1 && success_2 && success_3 && success_4 && success_5)
        {
            TextBlock_Error.Text = "";
            int result = FindGCD.GCD(int_value1, int_value2, int_value3, int_value4, int_value5);
            TextBlock_result.Text = result.ToString();
        }
        else
        {
            TextBlock_Error.Text = "Неправильно введены данные!";
        } 
    }

    private void Button_GCD_space_Click(object sender, RoutedEventArgs e)
    {
        string input = TextBox_space.Text;
        if (input == "")
        {
            TextBlock_Error.Text = "Неправильно введены данные!";
            return;
        }
        
        char d = ' ';
        
        int result = FindGCD.GCD(input, d);
        
        TextBlock_result.Text = result.ToString();
    }

    private void Button_GCD_Stein_Click(object sender, RoutedEventArgs e)
    {
        string input_1 = TextBox_1.Text;
        string input_2 = TextBox_2.Text;
        
        bool success_1 = int.TryParse(input_1, out int int_value1);
        bool success_2 = int.TryParse(input_2, out int int_value2);
        
        if (success_1 && success_2)
        {
            Stopwatch timer_GCD = new Stopwatch();
            timer_GCD.Start();

            int result_1 = FindGCD.GCD(int_value1, int_value2);
            
            timer_GCD.Stop();
            TimeSpan ts_GCD = timer_GCD.Elapsed;
            
            Stopwatch timer_Stein = new Stopwatch();
            timer_Stein.Start();
            
            int result_2 = FindGCDStein.GCDStein(int_value1, int_value2);
            
            timer_Stein.Stop();
            TimeSpan ts_Stein = timer_Stein.Elapsed;

            TextBlock_Time.Text = "Метод Штейна:\n";
            TextBlock_Time.Text += result_2.ToString();
            TextBlock_Time.Text += "\nВремя выполнения:\n";
            TextBlock_Time.Text += ts_Stein.ToString();
            
            TextBlock_Time.Text += "\nМетод Евклида:\n";
            TextBlock_Time.Text += result_1.ToString();
            TextBlock_Time.Text += "\nВремя выполнения:\n";
            TextBlock_Time.Text += ts_GCD.ToString();
        }
        else
        {
            TextBlock_Error.Text = "Неправильно введены данные";
        }
    }
}