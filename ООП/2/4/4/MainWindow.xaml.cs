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

namespace _4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Create_Matrix_Click(object sender, RoutedEventArgs e)
    {
        Grid_matrix_1.Children.Clear();
        Grid_matrix_2.Children.Clear();
        Grid_matrix_1.RowDefinitions.Clear();
        Grid_matrix_2.RowDefinitions.Clear();
        Grid_matrix_1.ColumnDefinitions.Clear();
        Grid_matrix_2.ColumnDefinitions.Clear();

        if (int.TryParse(TextBox_row_1.Text, out int row_1) && int.TryParse(TextBox_col_1.Text, out int col_1) &&
            int.TryParse(TextBox_col_2.Text, out int col_2))
        {
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
                        Random generator = new Random();
                        string value = Convert.ToString(generator.NextDouble() * (1000.00 + 1000.00) - 1000.00);
                        TextBox textBox = new TextBox
                        {
                            FontSize = 12,
                            Width = 50,
                            Height = 30,
                            Text = value,
                            IsReadOnly = true,
                            Margin = new Thickness(5),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        Grid.SetRow(textBox, i);
                        Grid.SetColumn(textBox, j);
                        
                        Grid_matrix_1.Children.Add(textBox);
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
                        Random generator = new Random();
                        string value = Convert.ToString(generator.NextDouble() * (1000.00 + 100.00) - 1000.00);
                        TextBox textBox = new TextBox
                        {
                            FontSize = 10,
                            Width = 50,
                            Height = 30,
                            Text = value,
                            IsReadOnly = true,
                            Margin = new Thickness(5),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        Grid.SetRow(textBox, i);
                        Grid.SetColumn(textBox, j); 
                        
                        Grid_matrix_2.Children.Add(textBox);
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
                TextBox textBox = (TextBox)Grid_matrix_1.Children[i * cols_1 + j];
                if (!double.TryParse(textBox.Text, out matrix1[i, j]))
                {
                    MessageBox.Show($"Некорректное значение в первой матрице на позиции ({i + 1}, {j + 1}).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        
        for (int i = 0; i < rows_2; i++)
        {
            for (int j = 0; j < cols_2; j++)
            {
                TextBox textBox = (TextBox)Grid_matrix_2.Children[i * cols_2 + j];
                if (!double.TryParse(textBox.Text, out matrix2[i, j]))
                {
                    MessageBox.Show($"Некорректное значение во второй матрице на позиции ({i + 1}, {j + 1}).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                TextBox textBox = new TextBox
                {
                    FontSize = 10,
                    Text = resultMatrix[i, j].ToString("F2"),
                    Width = 50,
                    Height = 30,
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    IsReadOnly = true
                };

                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, j);
                Grid_matrix_result.Children.Add(textBox);
            }
        }
    }
}