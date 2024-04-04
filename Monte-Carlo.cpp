#include <iostream>
#include <random>
#include <cmath>
#include <algorithm>

double Error(int k, int n, double a, double b, double f(double)){
    double sqS = 0; double nsqS = 0;
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_real_distribution<double> dist1(a, b);

    for(int j {0}; j < k; j++) {
        double sum1{0};
        for (int i = 0; i < n; i++) {
            double x = dist1(gen);
            sum1 += f(x);
        }
        double integral = ((b - a) * sum1 / n);
        sqS += integral * integral;
        nsqS += integral;
    }

    double error = sqrt(fabs(sqS/k - (nsqS/k * nsqS/k)));
    return error;
}


std::pair<double, double> monteCarloIntegration(double f(double), double a, double b, int n, int k) {
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_real_distribution<double> dist(a, b);

    double sum{0};
    for (int i = 0; i < n; i++) {
        double x = dist(gen);
        sum += f(x);
    }
    double integral = ((b - a) * sum / n);
    double error_end = Error(k, n, a, b, f);

    std::pair<double, double> result {integral, error_end};
    return result;
}



int main() {
    int k {3};
    // Функция, для которой вычисляется интеграл
    auto f = [](double x) { return 1 / (1 - 0.49 * sin(x) * sin(x)); };
    // Пределы интегрирования
    std::cout << "Введите пределы интегрирования:";
    double a; std::cin >> a;
    double b; std::cin >> b;
    // Количество образцов
    std::cout << "Введите количество образцов:";
    int n; std::cin >> n;
    std::pair<double, double> pr = monteCarloIntegration(f, a, b, n, k);
    std::cout << "Приближенное значение интеграла: " << pr.first << "\n" << "Погрешность вычисления: +-" << pr.second;
}
