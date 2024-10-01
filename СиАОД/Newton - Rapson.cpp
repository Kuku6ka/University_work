#include <iostream>
#include <cmath>
#include <vector>

//Система уравнений
auto f(const std::vector<double>& x){
    std::vector<double> result;
    result.push_back(cos((x[0] + 0.5)) + x[1] - 0.8);
    result.push_back(sin(x[1]) + 2 * x[0] - 1.6);
    return result;
}

//Производная от данной математической функции
auto jacobi(const std::vector<double>& x, const unsigned long n){
    std::vector<std::vector<double>> result (n, std::vector<double> (n));
    result[0][0] = (- sin((2*x[0] + 1)));
    result[0][1] = 1;
    result[1][0] = 2;
    result[1][1] = (cos(x[1]));
    return result;
}

// Метод Ньютона - Рафсона
auto metod_Neuton_Rapson(const std::vector<double>& xx, double eps, unsigned long n){
    std::vector<double> x = xx;
    std::vector<double> delta_x(n);
    do {
        std::vector<double> fx = f(xx);
        std::vector<std::vector<double>> J = jacobi(xx, n);

        //Вычисляем приращения x с помощью обратной матрицы Якоби
        std::vector<std::vector<double>> J_obr (n, std::vector<double> (n));
        double opr = J[0][0] * J [1][1] - J[0][1] * J[1][0]; //определитель обратной матрицы Якоби
        J_obr[0][0] = J[1][1] / opr;
        J_obr[0][1] = -J[0][1] / opr;
        J_obr[1][0] = -J[1][0] / opr;
        J_obr[1][1] = J[0][0] / opr;

        delta_x[0] = J_obr[0][0] * fx[0] + J_obr[0][1] * fx[1];
        delta_x[1] = J_obr[1][0] * fx[0] + J_obr[1][1] * fx[1];

        //обновляем значения x
        x[0] -= delta_x[0];
        x[1] -= delta_x[1];
    } while (sqrt(delta_x[0] * delta_x[0] + delta_x[1] * delta_x[1]) < eps);
    return x;
}


int main(){
    unsigned long n {2}; // Заданная размерность
    double eps (1e-4); // Точность
    std::vector<double> x(n, 0.0); // Начальное приближение
    std::vector<double> result = metod_Neuton_Rapson(x, eps, n);
    std::cout << "решением уравнения являются:\t" << result[0] << "\t" << result[1];
}
