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
using UnitsMeasure;

namespace _1;

public partial class MainWindow : Window
{
    private MeasureMassDevice measureMassDevice;
    private EventHandler newMeasurementTaken;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void createInstance_Click(object sender, RoutedEventArgs e)
    {
        if (UnitsComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Metric")
        {
            measureMassDevice = new MeasureMassDevice(Units.Metric);
        }
        else
        {
            measureMassDevice = new MeasureMassDevice(Units.Imperial);
        }
    }


    private void StartCollecting_Click(object sender, RoutedEventArgs e)
    {
        if (measureMassDevice != null)
        {
            try
            {
                measureMassDevice.StartCollecting();
                newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
                measureMassDevice.NewMeasurementTaken += newMeasurementTaken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void device_NewMeasurementTaken(object sender, EventArgs e)
    {
        if (measureMassDevice != null)
        {
            rawDataListBox.Items.Clear();
            foreach (int i in measureMassDevice.GetRawData())
            {
                rawDataListBox.Items.Add(i);
                metricValueTextBlock.Text = $"Metric value: {measureMassDevice.MetricValue().ToString()}";
                imperialValueTextBlock.Text = $"Imperial value: {measureMassDevice.ImperialValue().ToString()}";
            }
        }
    }

    private void StopCollecting_Click(object sender, RoutedEventArgs e)
    {
        if (measureMassDevice != null)
        {
            measureMassDevice.StopCollecting();
            measureMassDevice.NewMeasurementTaken -= newMeasurementTaken;
        }
    }
}