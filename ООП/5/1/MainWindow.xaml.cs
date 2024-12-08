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

namespace _1;

using MeasureLengthDeviceNamespace;
using units;
using DeviceControl;

public partial class MainWindow : Window
{
    private IMeasuringDevice device;
    private Units unit = Units.Imperial;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void createInstance_Click(object sender, RoutedEventArgs e)
    {
        if (unitsComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Metric")
        {
            device = new MeasureLengthDevice(Units.Metric);
            Console.WriteLine("Metric device");
        }
        else
        {
            device = new MeasureLengthDevice(Units.Imperial);
            Console.WriteLine("Imperial device");
        }
        Console.WriteLine("Device created");
    }

    private void Start_Collecting_Click(object sender, RoutedEventArgs e)
    {
        if (device != null)
        {
            device.StartCollecting();
            Console.WriteLine("Start Collecting");
        }
    }

    private void GetRawData_Click(object sender, RoutedEventArgs e)
    {
        if (device != null)
        {
            rawDataListBox.Items.Clear();
            foreach (int i in device.GetRawData())
            {
                rawDataListBox.Items.Add(i);
            }
        }
    }

    private void GetMetric_Click(object sender, RoutedEventArgs e)
    {
        if (device != null)
        {
            metricValueTextBlock.Text = $"MetricValue: {device.MetricValue()}";
        }
    }

    private void GetImperial_Click(object sender, RoutedEventArgs e)
    {
        if (device != null)
        {
            imperialValueTextBlock.Text = $"ImperialValue: {device.ImperialValue()}";
        }
    }

    private void Stop_Collecting_Click(object sender, RoutedEventArgs e)
    {
        if (device != null)
        {
            device.StopCollecting();
            Console.WriteLine($"Device stop collecting");
        }
    }
}
