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
using StressTest;

namespace _4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private TestCaseResult[] results;
    
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void RunTests_Click(object sender, RoutedEventArgs e)
    {
        reasonsList.Items.Clear();
        
        results = new TestCaseResult[10];
        
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = TestManager.GenerateResult();
        }
        
        int passCount = 0;
        int failCount = 0;
        
        foreach (var result in results)
        {
            if (result.Result == TestResult.Pass)
            {
                passCount++;
            }
            else
            {
                failCount++;
                reasonsList.Items.Add(result.ReasonForFailure); 
            }
        }
        
        passCountLabel.Content = $"{passCount}";
        failCountLabel.Content = $"{failCount}";
    }
}

