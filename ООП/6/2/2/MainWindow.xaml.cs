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
using MeasuringDevice;
using UnitsEnumeration;
using static MeasuringDevice.IEventEnabledMeasuringDevice;

namespace _2;

    public partial class MainWindow : Window
    {
        private MeasureMassDevice measureMassDevice;
        private Units unit = Units.Metric;
        private EventHandler newMeasurementTaken;
        private HeartBeatEventHandler HeartBeat;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            if (UnitsComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Metric")
            {
                measureMassDevice = new MeasureMassDevice(Units.Metric, 30);
            }
            else
            {
                measureMassDevice = new MeasureMassDevice(Units.Imperial, 30);
            }
        }


        private void StartCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (measureMassDevice != null)
            {
                try
                {
                    newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
                    measureMassDevice.NewMeasurementTaken += newMeasurementTaken;
;
                    HeartBeat = new IEventEnabledMeasuringDevice.HeartBeatEventHandler(device_HearBeat);
                    measureMassDevice.HeartBeat += HeartBeat;
                    measureMassDevice.StartCollecting();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void device_HearBeat(object sender, EventArgs e)
        {
            imperialValueTextBlock.Text = "Heart Beat TimeStamp: " + ((HeartBeatEventArgs)e).TimeStamp.ToString();
        }

        private void device_NewMeasurementTaken(object sender, EventArgs e)
        {
            if (measureMassDevice != null)
            {
                rawDataListBox.Items.Clear();
                foreach (int i in measureMassDevice.GetRawData())
                {
                    rawDataListBox.Items.Add(i);
                    metricValueTextBlock.Text = $"Metric value: {measureMassDevice.MetricValue().ToString()} \nImperial value: {measureMassDevice.ImperialValue().ToString()}";
                }
            }
        }
        
        private void StopCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (measureMassDevice != null)
            {
                measureMassDevice.StopCollecting();
                measureMassDevice.NewMeasurementTaken -= newMeasurementTaken;
                measureMassDevice.HeartBeat -= HeartBeat;
            }
        }
    }
    