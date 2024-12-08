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

namespace _3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        materialListBox.ItemsSource = Enum.GetValues(typeof(Material));
        crossSectionListBox.ItemsSource = Enum.GetValues(typeof(CrossSection));
        testResultListBox.ItemsSource = Enum.GetValues(typeof(TestResult));

        foreach (UIElement el in MainGrid.Children)
        {
            if (el is Button && el == Button_result)
            {
                ((Button)el).Click += OnClick;
            }
        }
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SelectionChanged();
    }

    public void SelectionChanged()
{
    Material selectedMaterial = (Material)materialListBox.SelectedItem;
    CrossSection selectedCrossSection = (CrossSection)crossSectionListBox.SelectedItem;
    TestResult selectedTestResult = (TestResult)testResultListBox.SelectedItem;
    
    StringBuilder selectionStringBuilder = new StringBuilder();
    
    switch (selectedMaterial)
    {
        case Material.StainlessSteel:
            selectionStringBuilder.Append("Material: Stainless Steel, ");
            break;
        case Material.Aluminium:
            selectionStringBuilder.Append("Material: Aluminium, ");
            break;
        case Material.ReinforcedConcrete:
            selectionStringBuilder.Append("Material: Reinforced Concrete, ");
            break;
        case Material.Composite:
            selectionStringBuilder.Append("Material: Composite, ");
            break;
        case Material.Titanium:
            selectionStringBuilder.Append("Material: Titanium, ");
            break;
    }

    switch (selectedCrossSection)
    {
        case CrossSection.IBeam:
            selectionStringBuilder.Append("Crosssection: I-Beam, ");
            break;
        case CrossSection.Box:
            selectionStringBuilder.Append("Crosssection: Box, ");
            break;
        case CrossSection.ZShaped:
            selectionStringBuilder.Append("Crosssection: Z-Shaped, ");
            break;
        case CrossSection.CShaped:
            selectionStringBuilder.Append("Crosssection: C-Shaped, ");
            break;
    }

    switch (selectedTestResult)
    {
        case TestResult.Pass:
            selectionStringBuilder.Append("Result: Pass.");
            break;
        case TestResult.Fail:
            selectionStringBuilder.Append("Result: Fail.");
            break;
    }

    ResulTextBlock.Text = selectionStringBuilder.ToString();
}
}