#include <iostream>
#include <vector>
#include <cmath>

//Ввод матрицы
void input_matrix(std::vector<std::vector<double>>& matrix, int rows, int cols) {
    for (int i = 0; i < rows; i++) {
        std::vector<double> row;
        for (int j = 0; j < cols; j++) {
            int element;
            std::cin >> element;
            row.push_back(element);
        }
        matrix.push_back(row);
    }
}

//Ввод свободных членов
void input_b(std::vector<double> & b, int rows){
    for (; rows > 0; rows--){
        int element;
        std::cin >> element;
        b.push_back(element);
    }
}

// Метод Зейделя для решения СЛАУ
std::vector<double> zeidelMethod(const std::vector<std::vector<double>>& A, const std::vector<double>& b, double eps, int rows, int max_iterations) {

    // Инициализация приближённого решения
    std::vector<double> x(rows, 0.0);

    int iteration = 0;
    while (iteration < max_iterations) {
        double max_error = 0;
        for (int i = 0; i < rows; ++i) {
            double new_x_i = b[i];
            for (int j = 0; j < rows; ++j) {
                if (i != j) {
                    new_x_i -= A[i][j] * x[j];
                }
            }
            new_x_i /= A[i][i];

            // Вычисление погрешности
            double error = fabs(new_x_i - x[i]);
            if (error > max_error) {
                max_error = error;
            }
            // Обновление значения x_i
            x[i] = new_x_i;
        }
        // Проверка погрешности
        if (max_error < eps) {
            break;
        }

        iteration++;
    }
    return x;
}


int main(){
    int rows, cols;
    double eps {0.0001};
    int iteration {3};
    std::vector<std::vector<double>> matrix;
    std::vector<double> b;
    std::cout << "Введите размер матрицы(строки/колонки):";
    std::cin >> rows >> cols;
    std::cout << "Введите матрицу:" << "\n";
    input_matrix(matrix, rows, cols);
    std::cout << "Введите матрицу b:" << "\n";
    input_b(b, rows);
    std::vector<double> x = zeidelMethod(matrix, b, eps, rows, iteration);
    for(double elem : x){
        std::cout << elem << "\t";
    }
}
