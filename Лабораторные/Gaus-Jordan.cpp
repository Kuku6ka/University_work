//Библиотеки
#include <iostream>
#include <vector>

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

//Метод Гауса*********************************************************************
void Gaus(std::vector<std::vector<double>>& matrix, std::vector<double>& b){
    //Нахождение максимального элемента
    for (int i = 0; i < b.size(); ++i) {
        int max_row = i;
        for (int k = i + 1; k < b.size(); ++k) {
            if (std::abs(matrix[k][i]) > std::abs(matrix[max_row][i]))
                max_row = k;
        }
        //Выбор максимального элемента(ведущего элемента)
        if (max_row != i) {
            std::swap(matrix[i], matrix[max_row]);
            std::swap(b[i], b[max_row]);
        }

        double elem = matrix[i][i];

        //Преобразование системы с удалением неизвестного
        for (int k = 0; k < b.size(); ++k) {
            if (k == i) continue;

            double factor = matrix[k][i] / elem;
            for (int j = i; j < b.size(); ++j) {
                matrix[k][j] -= factor * matrix[i][j];
            }
            b[k] -= factor * b[i];
        }
    }

    // Обратный ход
    for (int i = 0; i < b.size(); ++i) {
        double factor = matrix[i][i];
        for (int j = 0; j < b.size(); ++j) {
            matrix[i][j] /= factor;
        }
        b[i] /= factor;
    }
}
//********************************************************************************

//Вывод решения на экран
void output_result(std::vector<double>& b) {
    std::cout << "Решение СЛАУ:" << std::endl;
    for (int i = 0; i < b.size(); ++i) {
        std::cout << "x" << i+1 << " = " << b[i] << std::endl;
    }
}


int main() {
    int rows, cols;
    std::cout << "Введите размер матрицы: ";
    std::cin >> rows >> cols;
    std::vector<std::vector<double>> matrix;
    std::cout << "Введите элементы матрицы: " << std::endl;
    input_matrix(matrix, rows, cols);
    std::cout << "Введите b:";
    std::vector<double> b;
    input_b(b, rows);
    Gaus(matrix, b);
    output_result(b);
} 
