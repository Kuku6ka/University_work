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

namespace _2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        foreach (UIElement el in StackPanel_button.Children)
        {
            if (el is Button && el == Button_create)
            {
                ((Button)el).Click += Button_create_Click;
            }

            if (el is Button && el == Button_result)
            {
                ((Button)el).Click += Button_Calculate_Click;
            }
        }
    }

    private void Button_create_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Grid_matrix_1.Children.Clear();
            Grid_matrix_2.Children.Clear();
            Grid_matrix_1.RowDefinitions.Clear();
            Grid_matrix_2.RowDefinitions.Clear();
            Grid_matrix_1.ColumnDefinitions.Clear();
            Grid_matrix_2.ColumnDefinitions.Clear();

            double min = 1;
            double max = 100;

            if (int.TryParse(TextBox_row_1.Text, out int row_1) && int.TryParse(TextBox_col_1.Text, out int col_1) &&
                int.TryParse(TextBox_col_2.Text, out int col_2) && int.TryParse(TextBox_row_2.Text, out int row_2))
            {
                // Исключения
                {
                    if (col_1 != row_2)
                    {
                        throw new ArgumentException(
                            "Количество колонок первой матрицы должно быть равно количеству строк второй матрицы");
                    }
                }

                // Код генерации матриц
                Random generator_1 = new Random();
                {
                    for (int i = 0; i < row_1; i++)
                    {
                        Grid_matrix_1.RowDefinitions.Add(new RowDefinition());
                    }

                    for (int i = 0; i < col_1; i++)
                    {
                        Grid_matrix_1.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    for (int i = 0; i < row_1; i++)
                    {
                        for (int j = 0; j < col_1; j++)
                        {
                            string value = Convert.ToString(min + generator_1.NextDouble() * (max - min));

                            if (Convert.ToDouble(value) <= 0)
                            {
                                throw new ArgumentException("Неверные данные в сегменте");
                            }
                            
                            TextBlock textBlock = new TextBlock
                            {
                                FontSize = 14,
                                Width = 70,
                                Height = 30,
                                Text = value,
                                Margin = new Thickness(5),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            };
                            Grid.SetRow(textBlock, i);
                            Grid.SetColumn(textBlock, j);

                            Grid_matrix_1.Children.Add(textBlock);
                        }
                    }
                }

                {
                    for (int i = 0; i < col_1; i++)
                    {
                        Grid_matrix_2.RowDefinitions.Add(new RowDefinition());
                    }

                    for (int i = 0; i < col_2; i++)
                    {
                        Grid_matrix_2.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    for (int i = 0; i < col_1; i++)
                    {
                        for (int j = 0; j < col_2; j++)
                        {
                            string value = Convert.ToString(min + generator_1.NextDouble() * (max - min));
                            
                            if (Convert.ToDouble(value) <= 0)                             
                            {                                                             
                                throw new ArgumentException("Неверные данные в сегменте");
                            }                                                             
                            
                            TextBlock textBlock = new TextBlock
                            {
                                FontSize = 14,
                                Width = 70,
                                Height = 30,
                                Text = value,
                                Margin = new Thickness(5),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            };
                            Grid.SetRow(textBlock, i);
                            Grid.SetColumn(textBlock, j);

                            Grid_matrix_2.Children.Add(textBlock);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Введены неправильные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
    }

    private void Button_Calculate_Click(object sender, RoutedEventArgs e)
    {
        int rows_1 = Grid_matrix_1.RowDefinitions.Count;
        int cols_1 = Grid_matrix_1.ColumnDefinitions.Count;
        int rows_2 = Grid_matrix_2.RowDefinitions.Count;
        int cols_2 = Grid_matrix_2.ColumnDefinitions.Count;

        double[,] matrix1 = new double[rows_1, cols_1];
        double[,] matrix2 = new double[rows_2, cols_2];
        double[,] resultMatrix = new double[rows_1, cols_2];


        for (int i = 0; i < rows_1; i++)
        {
            for (int j = 0; j < cols_1; j++)
            {
                TextBlock textBlock = (TextBlock)Grid_matrix_1.Children[i * cols_1 + j];
                if (!double.TryParse(textBlock.Text, out matrix1[i, j]))
                {
                    MessageBox.Show($"Некорректное значение в первой матрице на позиции ({i + 1}, {j + 1}).",
                        "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        for (int i = 0; i < rows_2; i++)
        {
            for (int j = 0; j < cols_2; j++)
            {
                TextBlock textBlock = (TextBlock)Grid_matrix_2.Children[i * cols_2 + j];
                if (!double.TryParse(textBlock.Text, out matrix2[i, j]))
                {
                    MessageBox.Show($"Некорректное значение во второй матрице на позиции ({i + 1}, {j + 1}).",
                        "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }


        for (int i = 0; i < rows_1; i++)
        {
            for (int j = 0; j < cols_2; j++)
            {
                resultMatrix[i, j] = 0;
                for (int k = 0; k < cols_1; k++)
                {
                    resultMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }
        }


        Grid_matrix_result.Children.Clear();
        Grid_matrix_result.ColumnDefinitions.Clear();
        Grid_matrix_result.RowDefinitions.Clear();

        int rows = resultMatrix.GetLength(0);
        int columns = resultMatrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            Grid_matrix_result.RowDefinitions.Add(new RowDefinition());
        }

        for (int j = 0; j < columns; j++)
        {
            Grid_matrix_result.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                TextBlock textBlock = new TextBlock
                {
                    FontSize = 14,
                    Text = resultMatrix[i, j].ToString("F2"),
                    Width = 70,
                    Height = 30,
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                Grid.SetRow(textBlock, i);
                Grid.SetColumn(textBlock, j);
                Grid_matrix_result.Children.Add(textBlock);
            }
        }
    }
}