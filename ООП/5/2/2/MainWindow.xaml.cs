using System.Text;
using System.Windows;
using MeasureLengthDeviceNamespace;

using units;
using MeasureLengthDeviceNamespace;

namespace _2
{
    public partial class MainWindow : Window
    {
        private IMeasuringDevice device;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            
            if (UnitsComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Metric")
            {
                device = new MeasureMassDevice(Units.Metric);
                Console.WriteLine("Metric device");
            }
            else
            {
                device = new MeasureMassDevice(Units.Imperial);
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
}
