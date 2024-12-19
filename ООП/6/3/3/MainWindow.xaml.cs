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

public partial class MainWindow : Window
{
    private const int Ball_Count = 6;
    private Ellipse[] balls;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Initializing_Balls()
    {
        balls = new Ellipse[Ball_Count];
        for (int i = 0; i < Ball_Count; i++)
        {
            balls[i] = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Crimson,
                Stroke = Brushes.Black
            };
            Canvas.SetLeft(balls[i], 0);
            Canvas.SetTop(balls[i], i * 40);
            canvas.Children.Add(balls[i]);
        }
    }

    private void Start_Move()
    {
        
    }

    private void Stop_Move()
    {
        
    }

    private void Move_Ball_Monitor(int index)
    {
        bool is_stop = false;
        while (is_stop == false)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                double left = Canvas.GetLeft(balls[index]);

                if (left == 0)
                {
                    Canvas.SetLeft(balls[index], left + 1);
                }
                if (left >= 700 )
                {
                    Canvas.SetLeft(balls[index], left - 1);
                }
            });
        }
    }

    private void Button_Create_OnClick(object sender, RoutedEventArgs e)
    {
        Initializing_Balls();
    }
}